using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.BatchNoByInStockInfo.Dto;
using SC.SimpleMes.DTO;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Material.DomainEvent;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Report;
using SC.SimpleMes.WorkOrder;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SC.SimpleMes.Material
{
    public class MaterialBatchNumberAppService : AsyncCrudAppService<MaterialBatchNumber, MaterialBatchNumberDto, long, CommonPageRequestDto, MaterialBatchNumberDto, MaterialBatchNumberDto>,
        IMaterialBatchNumberAppService
    {
        private readonly IRepository<WorkProcessMaterialRecord, long> _materialRecordRep;
        private readonly IRepository<WorkProcessMaterialRecordHistory, long> _materialRecordHistoryRep;
        private readonly IMaterialBatchNumberCache _materialBatchNumberCache;
        private readonly IRepository<BatchNumberPrintRecord, long> _printRecordRep;
        private readonly IRepository<User, long> _userRep;
        private readonly IRepository<OrderMaterialProduceStatu, long> _orderProduStatuRep;
        private readonly IMaterialCache _materialCache;
        private readonly IRepository<WorkProcessInfo, long> _workProcessInfoRep;
        private readonly IRepository<MaterialInfo, long> _materialInfoRep;
        private readonly IRepository<WorkOrderInfo, long> _workOrderRep;
        private readonly IRepository<View_BatchMaterialUsedReport, string> _batchNumberUserReportRep;
        private readonly IRepository<View_MaterialBatchNumbers, long> _viewMaterialBatchNumber;
        private readonly IWorkProcessMaterialRecordDapperRep _workProcessMaterialRecordDapperRep;
        private readonly IEventBus _eventBus;
        private readonly WorkProcessInfoManager _workProcessInfoManager;
        private readonly IK3ErpRepostiory _k3ErpRepostiory;

        private readonly IRepository<ERPInStockInfo, long> _erpInstockrRepository;
        public MaterialBatchNumberAppService(IRepository<MaterialBatchNumber, long> repository,
            IMaterialBatchNumberCache materialBatchNumberCache,
            IRepository<ERPInStockInfo, long> erpInstockrRepository,
            IRepository<BatchNumberPrintRecord, long> printRecordRep,
            IRepository<WorkProcessMaterialRecordHistory, long> materialRecordHistoryRep,
            IRepository<OrderMaterialProduceStatu, long> orderProduStatuRep,
            IRepository<MaterialInfo, long> materialInfoRep,
             IRepository<WorkProcessInfo, long> workProcessInfoRep,
            IMaterialCache materialCache,
            IRepository<User, long> userRep,
            IRepository<WorkOrderInfo, long> workOrderRep,
            IEventBus eventBus,
            IK3ErpRepostiory k3ErpRepostiory,
            IWorkProcessMaterialRecordDapperRep workProcessMaterialRecordDapperRep,
            IRepository<View_BatchMaterialUsedReport, string> batchNumberUserReportRep,
            IRepository<View_MaterialBatchNumbers, long> viewMaterialBatchNumber,
            WorkProcessInfoManager workProcessInfoManager,
            IRepository<WorkProcessMaterialRecord, long> materialRecordRep) : base(repository)
        {
            _materialRecordRep = materialRecordRep;
            _materialBatchNumberCache = materialBatchNumberCache;
            _materialCache = materialCache;
            _printRecordRep = printRecordRep;
            _userRep = userRep;
            _erpInstockrRepository = erpInstockrRepository;
            _orderProduStatuRep = orderProduStatuRep;
            _materialInfoRep = materialInfoRep;
            _workProcessInfoRep = workProcessInfoRep;
            _batchNumberUserReportRep = batchNumberUserReportRep;
            _viewMaterialBatchNumber = viewMaterialBatchNumber;
            _materialRecordHistoryRep = materialRecordHistoryRep;
            _workProcessMaterialRecordDapperRep = workProcessMaterialRecordDapperRep;
            _eventBus = eventBus;
            _k3ErpRepostiory = k3ErpRepostiory;
            _workProcessInfoManager = workProcessInfoManager;
        }

        protected override IQueryable<MaterialBatchNumber> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var query = base.CreateFilteredQuery(input);
            var conditionInfo = input.QueryConditionObj as MaterialBatchNumberConditionDto;
            conditionInfo.ParseTime();

            query = query
                 .WhereIf(!string.IsNullOrEmpty(conditionInfo.KeyWord), p => p.BatchNumber.Contains(conditionInfo.KeyWord) || p.FromOrderNumber.Contains(conditionInfo.KeyWord) || p.FromErpBatchNumber.Contains(conditionInfo.KeyWord))
                 .WhereIf(conditionInfo.BelongMaterialId.HasValue, p => p.MaterialId == conditionInfo.BelongMaterialId)
                 .WhereIf(conditionInfo.CreateStaionId.HasValue && conditionInfo.ShowAll == false, p => p.CreateWorkStationId == conditionInfo.CreateStaionId)
                 .WhereIf(conditionInfo.StartDate != null, p => p.CreationTime > conditionInfo.StartDate)
                 .WhereIf(conditionInfo.EndDate != null, p => p.CreationTime < conditionInfo.EndDate)
                 .WhereIf(conditionInfo.OnlyShowProduct == true, p => p.MaterialNumber.StartsWith("D02.") || p.MaterialNumber.StartsWith("D01."))
                 ;

            if (conditionInfo.CreationTime.HasValue)
            {
                var startTime = conditionInfo.CreationTime.Value.Date;
                var endTime = conditionInfo.CreationTime.Value.Date.AddDays(1).Date;
                query = query.Where(p => p.CreationTime >= startTime && p.CreationTime <= endTime);
            }

            return query;
        }



        [AbpAuthorize(PermissionNames.Page_SNBatcNumberInfoDel)]
        public override Task DeleteAsync(EntityDto<long> input)
        {
            var batchNumber = this.Repository.FirstOrDefault(p => p.Id == input.Id);

            if (_materialRecordRep.GetAll().Any(p => p.InputMaterialBatchNumber == batchNumber.BatchNumber))
            {
                throw new UserFriendlyException("该物料批次号已经被使用，请勿删除");
            }

            _workProcessMaterialRecordDapperRep.BatchDelMaterialRecord(batchNumber.BatchNumber);

            _eventBus.Trigger<MaterialBatchNumberDelEventData>(new MaterialBatchNumberDelEventData()
            {
                MaterialBatchNumberDeleted = batchNumber
            });
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                return base.DeleteAsync(input);
            }
        }

        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> GetByProductMaterialBatchNumberAsync(string materialBatchNumber)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> result = new JHTAjaxResponse<MaterialBatchNumberDto>();
            var batchNumberDto = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(materialBatchNumber);
            var materialInfo = _materialCache.GetByMaterialNumber(batchNumberDto.MaterialNumber);
            var productBatchNumberRecord = _materialRecordRep.GetAll().Where(p => p.ProductBatchNumber == materialBatchNumber);
            StringBuilder materilStatuTipInfo = new StringBuilder("产品中如下物料的状态存在问题，");
            bool needTipInfo = false;

            foreach (var item in productBatchNumberRecord)
            {
                View_MaterialBatchNumbers batchNumber = null;
                batchNumber = await _viewMaterialBatchNumber.FirstOrDefaultAsync(p => p.BatchNumber == item.InputMaterialBatchNumber && p.MaterialNumber == item.InputMaterialNumber);
                if (batchNumber != null && batchNumber.IsLineMaterialInfo)// 在制品查询在制品信息
                {
                    if (batchNumber.MaterialStatu != MaterialStatuEnum.可用)
                    {
                        materilStatuTipInfo.AppendLine($"物料：{batchNumber.MaterialNumber},批次号：{batchNumber.BatchNumber}，状态为【{batchNumber.MaterialStatu}】{Environment.NewLine}");
                        needTipInfo = true;
                    }

                    if (!string.IsNullOrEmpty(batchNumber.FromErpBatchNumber))
                    {
                        var sourcMaterialInfo = _erpInstockrRepository.FirstOrDefault(p => p.BatchNo == item.BatchNo);
                        if (sourcMaterialInfo != null && sourcMaterialInfo.MaterialStatu != MaterialStatuEnum.可用)
                        {
                            materilStatuTipInfo.AppendLine($"物料：{sourcMaterialInfo.MaterialNumber},源批次号：{sourcMaterialInfo.BatchNo}，状态为【{sourcMaterialInfo.MaterialStatu}】{Environment.NewLine}");
                            needTipInfo = true;
                        }
                    }

                    var lineSideMaterialInfo = _materialRecordRep.GetAll().Where(p => p.ProductBatchNumber == batchNumber.BatchNumber);
                    foreach (var item2 in lineSideMaterialInfo)// 递归一层
                    {
                        var subbatchNumber = await _viewMaterialBatchNumber.FirstOrDefaultAsync(p => p.BatchNumber == item2.InputMaterialBatchNumber && p.MaterialNumber == item2.InputMaterialNumber);
                        if (subbatchNumber != null && subbatchNumber.MaterialStatu != MaterialStatuEnum.可用)
                        {
                            materilStatuTipInfo.AppendLine($"物料：{subbatchNumber.MaterialNumber},批次号：{subbatchNumber.BatchNumber}，状态为【{subbatchNumber.MaterialStatu}】{Environment.NewLine}");
                            needTipInfo = true;
                        }

                        if (!string.IsNullOrEmpty(item2.BatchNo))
                        {
                            var sourcMaterialInfo = _erpInstockrRepository.FirstOrDefault(p => p.BatchNo == item2.BatchNo);
                            if (sourcMaterialInfo != null && sourcMaterialInfo.MaterialStatu != MaterialStatuEnum.可用)
                            {
                                materilStatuTipInfo.AppendLine($"物料：{sourcMaterialInfo.MaterialNumber},源批次号：{sourcMaterialInfo.BatchNo}，状态为【{sourcMaterialInfo.MaterialStatu}】{Environment.NewLine}");
                                needTipInfo = true;
                            }
                        }
                    }
                }
                else
                {
                    // 如果是直接原材料
                    var sourcMaterialInfo = _erpInstockrRepository.FirstOrDefault(p => p.BatchNo == item.BatchNo);
                    if (sourcMaterialInfo != null && sourcMaterialInfo.MaterialStatu != MaterialStatuEnum.可用)
                    {
                        needTipInfo = true;
                        materilStatuTipInfo.AppendLine($"物料：{sourcMaterialInfo.MaterialNumber},批次号：{sourcMaterialInfo.BatchNo}，状态为【{sourcMaterialInfo.MaterialStatu}】{Environment.NewLine}");
                    }
                }
            }

            materilStatuTipInfo.AppendLine(",请联系质量部门进行处理！");
            if (needTipInfo)
            {
                result.Msg = materilStatuTipInfo.ToString();
            }

            if (materialInfo.MaterialType == MaterialTypeEnum.成品 || materialInfo.MaterialType == MaterialTypeEnum.半成品)
            {
                result.Data = batchNumberDto;
                return result;
            }

            throw new UserFriendlyException($"该物料类别为{materialInfo.MaterialType},非成品或半成品类型");

        }

        /// <summary>
        /// 检查该物料能否应用于该工序
        /// </summary>
        /// <param name="currentWorkProcessId"></param>
        /// <param name="materilNumber"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse<MaterialInfoDto>> CheckMaterialCanUseInWorkProcessAsync(long currentWorkProcessId, string materilNumber)
        {
            JHTAjaxResponse<MaterialInfoDto> ajaxResponse = new JHTAjaxResponse<MaterialInfoDto>();
            // 前置物料准备工序，能否被使用
            var workProcessInfo = await this._workProcessInfoRep.FirstOrDefaultAsync(p => p.Id == currentWorkProcessId);
            if (workProcessInfo.WorkProcessType != WorkProcessTypeEnum.前置物料准备工序)
            {
                ajaxResponse.Msg = "非前置物料准备工序，请勿操作";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }
            var materialInfo = await _materialCache.GetByMaterialNumberAsync(materilNumber);
            //if (!workProcessInfo.GetConfigMaterials().Contains(materialInfo.Id))
            //{
            //    ajaxResponse.Msg = "该工序不能加工该物料，请联系班组长！";
            //    ajaxResponse.Code = 500;
            //    return ajaxResponse;
            //}

            ajaxResponse.Data = materialInfo;
            return ajaxResponse;
        }

        public async Task<JHTAjaxResponse<ERPInStockInfoDto>> LoadErpBatchNumberAsync(string erpInstockBatchNumber)
        {
            JHTAjaxResponse<ERPInStockInfoDto> ajaxResponse = new JHTAjaxResponse<ERPInStockInfoDto>();
            var instockInfo = await _erpInstockrRepository.FirstOrDefaultAsync(p => p.BatchNo == erpInstockBatchNumber);

            // 处理入库批次原材料封存和全部报废的提示
            if (instockInfo != null && (instockInfo.MaterialStatu == MaterialStatuEnum.封存 || MaterialStatuEnum.全部报废 == instockInfo.MaterialStatu))
            {
                string tipInfo = instockInfo.MaterialStatu == MaterialStatuEnum.封存 ? "请与质量部门联系确认" : "不允许继续使用";
                return new JHTAjaxResponse<ERPInStockInfoDto>()
                {
                    Code = 500,
                    Msg = $"物料：{instockInfo.MaterialNumber},批次号：{instockInfo.BatchNo}，" +
                        $"状态为【{instockInfo.MaterialStatu}】，" +
                        $"{tipInfo}!"
                };
            }

            var erpInstockInfo = ObjectMapper.Map<ERPInStockInfoDto>(instockInfo);
            if (instockInfo == null)
            {
                var snInfo = _k3ErpRepostiory.GetSNInStockInfo(erpInstockBatchNumber);
                if (snInfo != null)
                {
                    erpInstockInfo = new ERPInStockInfoDto()
                    {
                        BatchNo = snInfo.SNumber,
                        WarehousingTime = snInfo.WarehousingTime,
                        MaterialName = snInfo.MaterialName,
                        MaterialNumber = snInfo.MaterialNumber,
                        UnitName = snInfo.UnitName,
                        UseUnitName = snInfo.UseUnitName,
                    };
                }
            }
            ajaxResponse.Data = erpInstockInfo;
            return ajaxResponse;
        }

        public async Task<JHTAjaxResponse> AddPrintBatchNumberRecordAsync(PrintBatchNoDto printBatch)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var batchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(printBatch.BatchNumber);
            var userInfo = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());

            var batchNumberInfo = this.Repository.FirstOrDefault(p => p.BatchNumber == batchNumber.BatchNumber && p.MaterialNumber == batchNumber.MaterialNumber);
            batchNumberInfo.LastPrintTime = DateTime.Now;
            batchNumberInfo.PrintTimes = _printRecordRep.GetAll().Count(p => p.BatchNumber == printBatch.BatchNumber && p.MaterialNumber == batchNumber.MaterialNumber) + 1;
            // 添加打印记录
            await _printRecordRep.InsertAsync(new BatchNumberPrintRecord()
            {
                BatchNumber = printBatch.BatchNumber,
                MaterialName = batchNumber.MaterialName,
                MaterialNumber = batchNumber.MaterialNumber,
                PrintMachine = "",
                PrintCounts = printBatch.PrintCounts,
                PrintTime = DateTime.Now,
                OperatorId = AbpSession.UserId.GetValueOrDefault(),
                OperatorName = userInfo.UserName
            });

            // 查找产品状态，更新产品打印信息
            return ajaxResponse;
        }


        public bool CheckERPBacthNumberMaterialIsUsedOut(string erpInstockBatchNumber, decimal bomBatchNumberCount = 0, bool needSaveTip = true)
        {
            var isUsedOut = true;
            var erpInstockInfo = _erpInstockrRepository.FirstOrDefault(p => p.BatchNo == erpInstockBatchNumber);

            var usedReport = _batchNumberUserReportRep.GetAll().FirstOrDefault(p => p.MaterialNumber == erpInstockInfo.MaterialNumber && p.BatchNo == erpInstockInfo.BatchNo);
            var nowUseCount = usedReport.DDUsedCount > usedReport.PrepaireMaterialCount ? usedReport.DDUsedCount : usedReport.PrepaireMaterialCount;
            nowUseCount = nowUseCount + bomBatchNumberCount;
            isUsedOut = nowUseCount > usedReport.ReceiptQuantity;

            if (needSaveTip && isUsedOut)
            {
                erpInstockInfo.IsUsedOut = isUsedOut;
                _eventBus.Trigger(new ERPBatchNumberOverUsedEventData()
                {
                    ActualUseAmount = nowUseCount,
                    BatchNumber = erpInstockBatchNumber,
                    FirstNoticeUserId = AbpSession.UserId.GetValueOrDefault(),
                });
            }


            return isUsedOut;
        }

        /// <summary>
        /// 加载加工批次号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageData<MaterialBatchNumberDto>> LoadCreatedBatchNumberAsync(JHTPageAjaxResquest<MaterialBatchNumberConditionDto> input)
        {
            var conditionInfo = input.Condition as MaterialBatchNumberConditionDto;
            conditionInfo.ParseTime();

            var query = _viewMaterialBatchNumber.GetAll()
                  .WhereIf(!string.IsNullOrEmpty(conditionInfo.KeyWord), p => p.BatchNumber.Contains(conditionInfo.KeyWord) || p.FromOrderNumber.Contains(conditionInfo.KeyWord) || p.FromErpBatchNumber.Contains(conditionInfo.KeyWord))
                  .WhereIf(conditionInfo.BelongMaterialId.HasValue, p => p.MaterialId == conditionInfo.BelongMaterialId)
                  .WhereIf(conditionInfo.CreateStaionId.HasValue && conditionInfo.ShowAll == false, p => p.CreateWorkStationId == conditionInfo.CreateStaionId)
                  .WhereIf(conditionInfo.StartDate != null, p => p.CreationTime > conditionInfo.StartDate)
                  .WhereIf(conditionInfo.EndDate != null, p => p.CreationTime < conditionInfo.EndDate)
                  .WhereIf(conditionInfo.UsedState != null, p => p.IsUsed == conditionInfo.UsedState)
                  .WhereIf(conditionInfo.OnlyShowProduct == true, p => p.MaterialNumber.StartsWith("D02.") || p.MaterialNumber.StartsWith("D01."))
                  .WhereIf(conditionInfo.ProductLineId != null, p => p.CreateProductLineId == conditionInfo.ProductLineId)
                  ;

            if (conditionInfo.CreationTime.HasValue)
            {
                var startTime = conditionInfo.CreationTime.Value.Date;
                var endTime = conditionInfo.CreationTime.Value.Date.AddDays(1).Date;
                query = query.Where(p => p.CreationTime >= startTime && p.CreationTime <= endTime);
            }

            PageData<MaterialBatchNumberDto> pageData = new PageData<MaterialBatchNumberDto>
            {
                List = ObjectMapper.Map<List<MaterialBatchNumberDto>>(query.OrderByDescending(p => p.Id).Skip(input.SkipCount).Take(input.PageSize).ToList()),
                Total = await query.CountAsync()
            };

            return pageData;
        }

        public async Task<List<MaterialBatchNumberExportDto>> LoadCreatedBatchNumberExportDtoAsync(JHTPageAjaxResquest<MaterialBatchNumberConditionDto> input)
        {
            var conditionInfo = input.Condition as MaterialBatchNumberConditionDto;
            conditionInfo.ParseTime();

            var query = _viewMaterialBatchNumber.GetAll()
                  .WhereIf(!string.IsNullOrEmpty(conditionInfo.KeyWord), p => p.BatchNumber.Contains(conditionInfo.KeyWord) || p.FromOrderNumber.Contains(conditionInfo.KeyWord) || p.FromErpBatchNumber.Contains(conditionInfo.KeyWord))
                  .WhereIf(conditionInfo.BelongMaterialId.HasValue, p => p.MaterialId == conditionInfo.BelongMaterialId)
                  .WhereIf(conditionInfo.CreateStaionId.HasValue && conditionInfo.ShowAll == false, p => p.CreateWorkStationId == conditionInfo.CreateStaionId)
                  .WhereIf(conditionInfo.StartDate != null, p => p.CreationTime > conditionInfo.StartDate)
                  .WhereIf(conditionInfo.EndDate != null, p => p.CreationTime < conditionInfo.EndDate)
                  .WhereIf(conditionInfo.UsedState != null, p => p.IsUsed == conditionInfo.UsedState)
                  .WhereIf(conditionInfo.OnlyShowProduct == true, p => p.MaterialNumber.StartsWith("D02.") || p.MaterialNumber.StartsWith("D01."))
                  .WhereIf(conditionInfo.ProductLineId != null, p => p.CreateProductLineId == conditionInfo.ProductLineId)
                  ;

            if (conditionInfo.CreationTime.HasValue)
            {
                var startTime = conditionInfo.CreationTime.Value.Date;
                var endTime = conditionInfo.CreationTime.Value.Date.AddDays(1).Date;
                query = query.Where(p => p.CreationTime >= startTime && p.CreationTime <= endTime);
            }

            return ObjectMapper.Map<List<MaterialBatchNumberExportDto>>(await query.OrderByDescending(p => p.Id).ToListAsync());
        }

        public bool CanMaterialBatchNumberBeUse(string batchNumber, out string message)
        {
            return _workProcessInfoManager.CanMaterialBatchNumberBeUse(batchNumber, out message);
        }

        public bool IsProductHaveInstocked(string batchNumber)
        {
            return _k3ErpRepostiory.GetSNInStockInfo(batchNumber) != null;
        }
    }
}
