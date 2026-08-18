using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Auditing;
using Abp.Authorization;
using JHT.Abp.CommonModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.DynamicForms.DTO;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Models;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkOrder.DTO;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkProcessSetBom.Dto;
using WLD.SimpleMes.WorkStation.Dto;

namespace WLD.SimpleMes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AbpAuthorize]
    public class WorkOrderController : SimpleMesControllerBase
    {
        private readonly IWorkOrderAppService _workOrderAppService;
        public WorkOrderController(IWorkOrderAppService workOrderAppService)
        {
            _workOrderAppService = workOrderAppService;
        }

        /// <summary>
        /// 获取生产订单信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkOrderInfoDto>>> SearchWorOrderInfoAsync([FromBody] JHTPageAjaxResquest<ConditonWorkOrderInfoDto> where)
        {
            JHTPageAjaxRespone<PageData<WorkOrderInfoDto>> ajaxRespone = new JHTPageAjaxRespone<PageData<WorkOrderInfoDto>>();
            var result = await _workOrderAppService.GetAllAsync(new DTO.CommonPageRequestDto()
            {
                SkipCount = where.SkipCount,
                MaxResultCount = where.PageSize,
                QueryConditionObj = where.Condition
            });

            ajaxRespone.Data = new PageData<WorkOrderInfoDto>()
            {
                List = result.Items.ToList(),
                Total = result.TotalCount
            };

            return ajaxRespone;
        }

        /// <summary>
        /// 获取订单是否超量的信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JHTPageAjaxRespone<PageData<WorkOrderInfoDto>>> SearchOverUsedWorOrderInfoAsync([FromBody] JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            JHTPageAjaxRespone<PageData<WorkOrderInfoDto>> ajaxRespone = new JHTPageAjaxRespone<PageData<WorkOrderInfoDto>>();
            var result = await _workOrderAppService.SearchOverUsedWorOrderInfoAsync(new DTO.CommonPageRequestDto()
            {
                SkipCount = where.SkipCount,
                MaxResultCount = where.PageSize,
                QueryConditionObj = where.Condition
            });

            ajaxRespone.Data = new PageData<WorkOrderInfoDto>()
            {
                List = result.Items.ToList(),
                Total = result.TotalCount
            };

            return ajaxRespone;
        }

        /// <summary>
        /// 生产订单创建
        /// </summary>
        /// <param name="workOrderInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderInfoManage)]
        public async Task<JHTAjaxResponse> CreateWorkOrderInfoAsync([FromBody] CreateUpdateWorkOrderInfoDto workOrderInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Msg = "订单创建成功";
            ajaxResponse.Data = await _workOrderAppService.CreateAsync(workOrderInfo);
            return ajaxResponse;
        }

        /// <summary>
        /// 生产订单更新
        /// </summary>
        /// <param name="workOrderInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderInfoManage)]
        public async Task<JHTAjaxResponse> UpdateWorkOrderInfoAsync([FromBody] CreateUpdateWorkOrderInfoDto workOrderInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Msg = "订单更新成功";
            ajaxResponse.Data = await _workOrderAppService.UpdateAsync(workOrderInfo);
            return ajaxResponse;
        }

        /// <summary>
        /// 下发生产订单
        /// </summary>
        /// <param name="issuedWorkOrderInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Issues)]
        public async Task<JHTAjaxResponse> IssuedWorkOrderInfoAsync([FromBody] IssuedWorkOrderInfoDto issuedWorkOrderInfo)
        {
            return await _workOrderAppService.IssuedWorkOrderAsync(issuedWorkOrderInfo);
        }

        /// <summary>
        /// 撤销下发订单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Revert)]
        public JHTAjaxResponse RevertIssedWorkOrder([FromBody] EntityDto<long> entityDto)
        {
            return _workOrderAppService.RevertIssuedWorkOrder(entityDto);
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Pause)]
        public JHTAjaxResponse PauseWorkOrder([FromBody] EntityDto<long> entityDto)
        {
            return _workOrderAppService.PauseWorkOrder(entityDto);
        }

        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Recover)]
        public JHTAjaxResponse RecoverWorkOrder([FromBody] EntityDto<long> entityDto)
        {
            return _workOrderAppService.RecoverWorkOrder(entityDto);
        }

        /// <summary>
        /// 取消生产订单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Cancel)]
        public JHTAjaxResponse CancelOrder([FromBody] EntityDto<long> entityDto)
        {
            return _workOrderAppService.CancelOrder(entityDto);
        }

        /// <summary>
        /// 关闭生产订单
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_Close)]
        public JHTAjaxResponse CloseOrder([FromBody] EntityDto<long> entityDto)
        {
            return _workOrderAppService.CloseOrder(entityDto);
        }

        /// <summary>
        /// 获取当前工单状态
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<FinishedProductStatusDto>> LoadFinishedProductStatusDtos([FromBody] EntityDto<long> entityDto)
        {
            return new JHTAjaxResponse<List<FinishedProductStatusDto>>()
            {
                Data = _workOrderAppService.LoadFinishedProductStatusDtos(entityDto)
            };
        }

        /// <summary>
        /// 生产生产订单编号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<string> GenerateWorkOrderNumber()
        {
            return _workOrderAppService.GenerateWorkOrderNumber();
        }

        /// <summary>
        /// 创建订单物料批次号
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage_SNManage)]
        public JHTAjaxResponse<MaterialBatchNumberDto> CreateWorkOrderBatchNumber(CreateWorkOrderBatchNumberDto entityDto)
        {
            return _workOrderAppService.CreateWorkOrderBatchNumber(entityDto);
        }

        /// <summary>
        /// 加载当前工单的物料批次号
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<MaterialBatchNumberDto>> LoadWorkOrderBatchNumbers(EntityDto<long> entityDto)
        {
            JHTAjaxResponse<List<MaterialBatchNumberDto>> ajaxResponse = new JHTAjaxResponse<List<MaterialBatchNumberDto>>();
            ajaxResponse.Data = _workOrderAppService.LoadWorkOrderBatchNumbers(entityDto);
            return ajaxResponse;
        }

        /// <summary>
        /// 创建工单BOM -- 详情复制工艺BOM
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<bool>> CreateWorkOrderBOM([FromBody] WorkOrderBomDto workOrderBomDto)
        {
            JHTAjaxResponse<bool> ajaxResponse = new JHTAjaxResponse<bool>();
            if (workOrderBomDto.OrderBomItemDtos.Count > 0)
            {
                _workOrderAppService.UpdateWorkOrderBOMItems(workOrderBomDto);
            }
            else
            {
                await _workOrderAppService.CreateWorkOrderBOMAsync(workOrderBomDto);
            }

            ajaxResponse.Data = true;
            return ajaxResponse;
        }


        /// <summary>
        /// 获取工单BOM信息
        /// </summary>
        /// <param name="workOrderBomDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse<WorkOrderBomDto>> GetWorkOrderBomInfoAsync([FromBody] EntityDto<long> workOrderId)
        {
            JHTAjaxResponse<WorkOrderBomDto> workOrderBOM = new JHTAjaxResponse<WorkOrderBomDto>();
            workOrderBOM.Data = await _workOrderAppService.GetWorkOrderBomInfoAsync(workOrderId);
            return workOrderBOM;
        }

        /// <summary>
        /// 重置工单BOM
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpMvcAuthorize(PermissionNames.Page_WorkOrderManage)]
        public async Task<JHTAjaxResponse<bool>> ResetWorkOrderBOM([FromBody] EntityDto<long> entityDto)
        {
            JHTAjaxResponse<bool> ajaxResponse = new JHTAjaxResponse<bool>();
            ajaxResponse.Data = true;
            await _workOrderAppService.ResetWorkOrderBOM(entityDto.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 重置工单BOM
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<WorkProcessSetBomDto> GetSetBOMByWorkOrdBOM([FromBody] EntityDto<long> entityDto)
        {
            JHTAjaxResponse<WorkProcessSetBomDto> ajaxResponse = new JHTAjaxResponse<WorkProcessSetBomDto>();
            ajaxResponse.Data = _workOrderAppService.GetSetBOMByWorkOrdBOM(entityDto.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 获取订单各产品的进度信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<OrderMaterialProduceStatuDto>> LoadWorkOrderProductPercentInfo([FromBody] EntityDto<long> entityDto)
        {
            JHTAjaxResponse<List<OrderMaterialProduceStatuDto>> ajaxResponse = new JHTAjaxResponse<List<OrderMaterialProduceStatuDto>>();
            ajaxResponse.Data = _workOrderAppService.LoadWorkOrderProductPercentInfo(entityDto);
            return ajaxResponse;
        }

        /// <summary>
        /// 加载工单操作记录信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessOperatorRecordDto>> LoadWorkOrderOperatorRecordInfo([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<List<WorkProcessOperatorRecordDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessOperatorRecordDto>>();
            ajaxResponse.Data = _workOrderAppService.LoadWorkOrderOperatorRecordInfo(entityDto);
            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessMaterialHistoryGroupModel>> LoadWorkOrderMaterialRecord([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<List<WorkProcessMaterialHistoryGroupModel>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessMaterialHistoryGroupModel>>();
            ajaxResponse.Data = new List<WorkProcessMaterialHistoryGroupModel>();
            foreach (var item in _workOrderAppService.LoadWorkOrderMaterialRecordHistory(entityDto).GroupBy(p => new { OperDate = p.CreateTime.ToString("yyyy-MM-dd HH:mm"), p.WorkStationName }))
            {
                ajaxResponse.Data.Add(new WorkProcessMaterialHistoryGroupModel()
                {
                    Key = item.Key.OperDate,
                    WorkStationName = item.Key.WorkStationName,
                    Value = item.ToList()
                });
            }

            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<List<FormInfoRecordDto>> LoadWorkOrderFillReportList([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<List<FormInfoRecordDto>> ajaxResponse = new JHTAjaxResponse<List<FormInfoRecordDto>>();
            ajaxResponse.Data = _workOrderAppService.LoadWorkOrderFillReportList(entityDto);
            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<List<WorkProcessMaterialRecordDto>> LoadWorkOrderFinalyUsedMaterial([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<List<WorkProcessMaterialRecordDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessMaterialRecordDto>>();
            ajaxResponse.Data = _workOrderAppService.LoadWorkOrderFinalyUsedMaterial(entityDto);
            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<ProductLineDto> LoadSelectSnProdcutInfo([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<ProductLineDto> ajaxResponse = new JHTAjaxResponse<ProductLineDto>();
            ajaxResponse.Data = _workOrderAppService.LoadSelectSnProdcutInfo(entityDto);
            return ajaxResponse;
        }

        [HttpPost]
        public JHTAjaxResponse<List<WorkOrderInfoDto>> GetProductLineWorkOrderInfo([FromBody] EntityDto<long> productLineId)
        {
            JHTAjaxResponse<List<WorkOrderInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkOrderInfoDto>>();

            ajaxResponse.Data = _workOrderAppService.GetProductLineWorkOrderInfoByProductLineId(productLineId.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 根据工单号获取工单信息
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<WorkOrderInfoDto> GetWorkOrderInfoByNumber([FromBody] EntityDto<string> entityDto)
        {
            JHTAjaxResponse<WorkOrderInfoDto> ajaxResponse = new JHTAjaxResponse<WorkOrderInfoDto>();
            ajaxResponse.Data = _workOrderAppService.GetWorkOrderInfoByOrderNumber(entityDto.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 获取已关闭的工单信息
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<List<WorkOrderInfoDto>> GetCloseWorkOrderInfo([FromBody] EntityDto<string> orderNumber)
        {
            JHTAjaxResponse<List<WorkOrderInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkOrderInfoDto>>();

            ajaxResponse.Data = _workOrderAppService.GetCloseWorkOrderInfoByOrderNumber(orderNumber.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 是否有返修的产品
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<bool> HaveRepairedProduct([FromBody] EntityDto<string> orderNumber)
        {
            JHTAjaxResponse<bool> ajaxResponse = new JHTAjaxResponse<bool>();
            ajaxResponse.Data = _workOrderAppService.DoWorkOrderHaveRepairdProduct(orderNumber.Id);
            return ajaxResponse;
        }

        /// <summary>
        /// 获取已经工单物料已领用的数量
        /// </summary>
        /// <param name="workOrderPickingMaterilInfoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JHTAjaxResponse<WorkOrderPickingMaterilInfoDto> GetWorkOrderPickingMaterilInfo([FromBody] WorkOrderPickingMaterilInfoDto workOrderPickingMaterilInfoDto)
        {
            return _workOrderAppService.GetWorkOrderPickingMaterilInfo(workOrderPickingMaterilInfoDto);
        }


        /// <summary>
        /// 设置工单产品客制化信息
        /// </summary>
        /// <param name="customerProductInfoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JHTAjaxResponse> SetWorkOrderCustomerInfoAsync([FromBody] CustomerProductInfoDto customerProductInfoDto)
        {
            return await _workOrderAppService.SetWorkOrderCustomerProductInfo(customerProductInfoDto);
        }
    }
}
