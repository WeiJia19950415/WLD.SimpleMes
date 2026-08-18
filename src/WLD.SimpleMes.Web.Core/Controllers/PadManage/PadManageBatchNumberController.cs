using Abp.Application.Services.Dto;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BatchNoByInStockInfo.Dto;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Models;
using WLD.SimpleMes.Models.PadManageRequest;
using WLD.SimpleMes.OnLineStock;
using WLD.SimpleMes.Report;
using WLD.SimpleMes.Startup;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [AbpAuthorize]
    [ApiController]
    public class PadManageBatchNumberController : SimpleMesControllerBase
    {
        private readonly IMaterialBatchNumberAppService _materialBatchNumberAppService;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IWorkStationAppService _workStationAppService;
        private readonly IWorkProcessAppService _workProcessAppService;
        private readonly ILineSideMaterialInfoAppService _lineSideMaterialInfoAppService;
        private readonly WorkOrderManager _workOrderManager;
        private readonly MaterialBatchNumberCache _materialBatchNumberCache;
        private readonly IReportAppService _reportAppService;

        public PadManageBatchNumberController(IMaterialBatchNumberAppService materialBatchNumberAppService,
            IWorkStationAppService workStationAppService,
            IWorkProcessAppService workProcessAppService,
            ICutMaterialConfigAppService cutMaterialConfigAppService,
            MaterialBatchNumberCache materialBatchNumberCache,
            ILineSideMaterialInfoAppService lineSideMaterialInfoAppService,
            WorkOrderManager workOrderManager,
            ReportAppService reportAppService,
            IWorkOrderAppService workOrderAppService)
        {
            _materialBatchNumberAppService = materialBatchNumberAppService;
            _workOrderAppService = workOrderAppService;
            _workStationAppService = workStationAppService;
            _workProcessAppService = workProcessAppService;
            _workOrderManager = workOrderManager;
            _lineSideMaterialInfoAppService = lineSideMaterialInfoAppService;
            _materialBatchNumberCache = materialBatchNumberCache;
            _reportAppService = reportAppService;
        }


        /// <summary>
        /// 加载 成品/半成品 批次号信息
        /// 用于开始生产时的扫码
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> LoadProductBatchNumberAsync([FromBody] PadManageRequestModel entityDto)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            var batchNumberRessult = await _materialBatchNumberAppService.GetByProductMaterialBatchNumberAsync(entityDto.ProductMaterialBatchNumber);
            if (batchNumberRessult.Code != 200)
            {
                return batchNumberRessult;
            }

            ajaxResponse.Msg = batchNumberRessult.Msg;
            var batchNumber = batchNumberRessult.Data;
            var workOrderInfo = _workOrderAppService.GetWorkOrderInfoByOrderNumber(batchNumber.FromOrderNumber);

            if (!string.IsNullOrEmpty(workOrderInfo.Remark))
            {
                ajaxResponse.Msg = $"该工单有如下备注信息：{workOrderInfo.Remark},请注意！";
            }

            if (workOrderInfo == null)
            {
                ajaxResponse.Msg = "生产订单号不存在，请重新输入！";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            // 已经在产线的不进行排除，未开始的不允许投入
            if (!(workOrderInfo.WorkOrderStatu == WorkOrderStatuEnum.已下发 || workOrderInfo.WorkOrderStatu == WorkOrderStatuEnum.生产中))
            {
                var proStatus = _workOrderManager.GetMaterialProduceStatu(entityDto.ProductMaterialBatchNumber);
                if (proStatus == null || proStatus.ProduceStatus == ProduceStatusEnum.未开始)
                {
                    ajaxResponse.Msg = $"订单状态为{workOrderInfo.WorkOrderStatu},不可进行操作，请联系车间管理员！";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }
            }

            batchNumber.IsRepired = _materialBatchNumberAppService.IsProductHaveInstocked(batchNumber.BatchNumber);
            if (batchNumber.IsRepired)
            {
                ajaxResponse.Msg = $"该产品为售后返修,请确认已完成返修领料！";
            }

            WorkProcessInfoDto workProcessInfo = null;
            if (entityDto.CurrentWorkProcessId == 0)
            {
                if (workProcessInfo == null)
                {
                    ajaxResponse.Msg = "请选择生产工位和工序！";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

                entityDto.CurrentWorkProcessId = workProcessInfo.Id;
                entityDto.CurrentWorkStaionId = workProcessInfo.CurrentWorkStationId;
            }

            // 检查当前是否正在被操作，工位是否为同一个
            var recrodInfo = _workProcessAppService.LoadCurrentWorkProcessOperatorInfo(new InputOperatorRecordInfo()
            {
                WorkProcessId = entityDto.CurrentWorkProcessId,
                WorkOrderNumber = batchNumber.FromOrderNumber,
                OperatroMaterilBatchNumber = entityDto.ProductMaterialBatchNumber,
                OperatroMaterilBatchType = WorkProcessOperateTypeEnum.开始生产
            });
            if (recrodInfo.Data != null && recrodInfo.Data.IsNormalFinish == false && recrodInfo.Data.WorkStationId != entityDto.CurrentWorkStaionId)
            {
                var olworkStaionInfo = await _workStationAppService.GetAsync(new EntityDto<long>() { Id = recrodInfo.Data.WorkStationId });
                ajaxResponse.Msg = $"请在工位：{olworkStaionInfo.ProductLineName + "—" + olworkStaionInfo.WorkStationName}操作！";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            // 检查非当前产线不能生产,当为维修工序，巡检工序时 不检查工序产线
            var workStaionInfo = await _workStationAppService.GetAsync(new EntityDto<long>() { Id = entityDto.CurrentWorkStaionId });
            var canMixedProductLine = bool.Parse(SettingManager.GetSettingValue(AppSettingNames.CanMixedProductLine));
            if (canMixedProductLine == false && workStaionInfo.IsShared == false && workStaionInfo.BelongProductLineId != workOrderInfo.ProduceLineId && workStaionInfo.IsShared == false)
            {
                ajaxResponse.Msg = $"该工单不能在该产线进行生产，请联系车间管理员！";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }


            // 检查是否按工艺顺序进行
            string message = "";
            if (!_workOrderAppService.CheckMaterialBatchNumberWorkProcess(entityDto.ProductMaterialBatchNumber, entityDto.CurrentWorkProcessId, workOrderInfo.WorkProcessSetId.GetValueOrDefault(), ref message))
            {
                ajaxResponse.Msg = message;
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            ajaxResponse.Data = batchNumber;
            ajaxResponse.Data.ProduceStatus = _workOrderAppService.GetProduceStatus(entityDto.ProductMaterialBatchNumber);
            if (workProcessInfo != null)
            {
                ajaxResponse.Data.CurrentWorkProcess = workProcessInfo;
                ajaxResponse.Data.CurrentWorkStationId = workProcessInfo.CurrentWorkStationId;
            }
            return ajaxResponse;
        }



        /// <summary>
        /// 加载erp的批次号信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [TypeFilter(typeof(PadManageActionFilter))]
        public async Task<JHTAjaxResponse<ERPInStockInfoDto>> LoadErpBatchNumberAsync([FromBody] LoadErpInstocsRequestModel entityDto)
        {
            var erpBatchNumberResult = await _materialBatchNumberAppService.LoadErpBatchNumberAsync(entityDto.ErpInstockBatchNumber);

            if (erpBatchNumberResult.Data == null && erpBatchNumberResult.Code != 200)
            {
                return erpBatchNumberResult;
            }

            var inputMaterialInfo = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(entityDto.ErpInstockBatchNumber);

            if (entityDto.NeedCheck == false)// 不需要进行检查时
            {
                if (inputMaterialInfo != null)
                {
                    erpBatchNumberResult = await _materialBatchNumberAppService.LoadErpBatchNumberAsync(inputMaterialInfo.FromErpBatchNumber);
                }

                if (inputMaterialInfo != null && erpBatchNumberResult.Data == null)
                {
                    erpBatchNumberResult.Data = new ERPInStockInfoDto()
                    {
                        MaterialName = inputMaterialInfo.MaterialName,
                        MaterialNumber = inputMaterialInfo.MaterialNumber,
                        UnitName = string.IsNullOrEmpty(inputMaterialInfo.InputUnitName) ? inputMaterialInfo.WrapUniteName : string.IsNullOrEmpty(inputMaterialInfo.WrapUniteName) ? inputMaterialInfo.BOMUnitName : "",
                        Supplier = inputMaterialInfo.IsLineMaterialInfo ? "自制" : ""
                    };
                }

                return erpBatchNumberResult;
            }



            if (erpBatchNumberResult.Data == null)
            {
                // 加工批次物料使用判断 
                string meessage = "";
                if (_materialBatchNumberAppService.CanMaterialBatchNumberBeUse(entityDto.ErpInstockBatchNumber, out meessage) == false)
                {
                    erpBatchNumberResult.Code = 500;
                    erpBatchNumberResult.Msg = meessage;
                    return erpBatchNumberResult;
                }

                if (inputMaterialInfo != null)
                {
                    erpBatchNumberResult = await _materialBatchNumberAppService.LoadErpBatchNumberAsync(inputMaterialInfo.FromErpBatchNumber);
                    entityDto.ErpInstockBatchNumber = inputMaterialInfo.FromErpBatchNumber;
                }

                if (erpBatchNumberResult.Data == null)
                {
                    erpBatchNumberResult.Code = 500;
                    erpBatchNumberResult.Msg = "未找到该批次号信息，请确认后再输入！";
                    return erpBatchNumberResult;
                }
            }

            var checkresult = await _materialBatchNumberAppService.CheckMaterialCanUseInWorkProcessAsync(entityDto.CurrentWorkProcessId, erpBatchNumberResult.Data.MaterialNumber);
            if (checkresult.Code != 200)
            {
                erpBatchNumberResult.Code = 500;
                erpBatchNumberResult.Msg = checkresult.Msg;
                return erpBatchNumberResult;
            }

            erpBatchNumberResult.Data.UseUnitName = checkresult.Data.UnitName;
            if (!string.IsNullOrEmpty(entityDto.WorkOrderNumber))
            {
                // 如果是需要判断对应BOM中的物料编号与当前加工物料编码是否一致
                var workProcessSetResponse = await _workOrderAppService.CheckIsBomMaterialAsync(entityDto.WorkOrderNumber, erpBatchNumberResult.Data.MaterialNumber);
                if (workProcessSetResponse.Code != 200)
                {
                    erpBatchNumberResult.Code = 500;
                    erpBatchNumberResult.Msg = workProcessSetResponse.Msg;
                    return erpBatchNumberResult;
                }
            }

            if (entityDto.OnlineMaterialInfoId > 0)
            {
                var onlineBom = _lineSideMaterialInfoAppService.GetLineSideMaterialInfoBomItemDtosByMaterilId(new EntityDto<long>() { Id = entityDto.OnlineMaterialInfoId.GetValueOrDefault() });
                if (onlineBom != null && !onlineBom.Any(p => erpBatchNumberResult.Data.MaterialNumber.StartsWith(p.FormMaterialCategoryNumber)))
                {
                    erpBatchNumberResult.Code = 500;
                    erpBatchNumberResult.Msg = "该物料不能用于该在制品加工";
                    return erpBatchNumberResult;
                }
            }

            // 后续从工单领料单中查询领料单中的数据信息，判断是否已经领料。

            // 检查该批次的物料是否已经用尽
            bool isUsedOut = _materialBatchNumberAppService.CheckERPBacthNumberMaterialIsUsedOut(entityDto.ErpInstockBatchNumber, 0, false);
            if (isUsedOut && inputMaterialInfo == null)// 只查询入库批次号时判断物料是否用尽
            {
                erpBatchNumberResult.Code = 201;
                erpBatchNumberResult.Msg = "该批次物料已经被用尽，请尽快更换物料批次号！";
            }

            // 检查该类型的物料的生产任务订单可以进行切换
            bool isCutMaterialEnough = await _workOrderAppService.IsCutMaterialEnough(entityDto.WorkOrderNumber, entityDto.CurrentWorkProcessId, erpBatchNumberResult.Data.MaterialNumber);

            if (isCutMaterialEnough)
            {
                erpBatchNumberResult.Code = 201;
                erpBatchNumberResult.Msg = $"该加工物料{erpBatchNumberResult.Data.MaterialName}已满足工单所需，请更换生产任务单号！";
            }

            if (erpBatchNumberResult.Code != 500)
            {
                var orderCostInfo = await _reportAppService.LoadKeyMaterilCostByWorkOrderNumberAsync(entityDto.WorkOrderNumber);
                if (orderCostInfo != null)
                {
                    var workProcessInfo = await _workProcessAppService.GetAsync(new EntityDto<long>(entityDto.CurrentWorkProcessId));
                    var materialCostInfo = orderCostInfo.FirstOrDefault(p => p.FormMaterialNumber == erpBatchNumberResult.Data.MaterialNumber && p.WorkProcessName == workProcessInfo.ProcessName);
                    if (materialCostInfo != null)
                    {
                        erpBatchNumberResult.Data.MaterialWorkOrderCostInfo = $"物料名称：{materialCostInfo.FormMaterialName}，" +
                            $"工单标准用量：{materialCostInfo.WorkOrderCount.ToString("0.000")}{materialCostInfo.BOMUnitName}，" +
                            $"已投入量(仅供参考)：{materialCostInfo.BOMMaterialCount.ToString("0.000")}{materialCostInfo.BOMUnitName}，" +
                            $"加工数量(仅供参考)：{materialCostInfo.MatrialCount.ToString("0.000")}{materialCostInfo.WrapUniteName}"
                            ;
                    }
                    else
                    {
                        erpBatchNumberResult.Data.MaterialWorkOrderCostInfo = string.Empty;
                    }

                }
            }

            return erpBatchNumberResult;
        }

        /// <summary>
        /// PAD端加载批次号信息
        /// </summary>
        /// <param name="pageAjaxResquest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>> LoadBatchNumber([FromBody] JHTPageAjaxResquest<MaterialBatchNumberConditionDto> pageAjaxResquest)
        {
            JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>> pageAjaxRespone = new JHTPageAjaxRespone<PageData<MaterialBatchNumberDto>>();
            var result = await _materialBatchNumberAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                MaxResultCount = pageAjaxResquest.PageSize,
                SkipCount = pageAjaxResquest.SkipCount,
                QueryConditionObj = pageAjaxResquest.Condition
            });

            pageAjaxRespone.Data = new PageData<MaterialBatchNumberDto>()
            {
                List = result.Items.ToList(),
                Total = result.TotalCount,
            };

            return pageAjaxRespone;
        }

        /// <summary>
        /// 添加打印记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> AddPrintBatchNumberRecordAsync([FromBody] PrintBatchNoDto printBatch)
        {
            return await this._materialBatchNumberAppService.AddPrintBatchNumberRecordAsync(printBatch);
        }
    }
}
