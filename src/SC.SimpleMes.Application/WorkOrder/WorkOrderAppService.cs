using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using Abp.Json;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.BOM;
using SC.SimpleMes.BOM.Dto;
using SC.SimpleMes.Configuration;
using SC.SimpleMes.Configuration.Dto;
using SC.SimpleMes.DTO;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.DynamicForms.DTO;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Material;
using SC.SimpleMes.Material.Dto;
using SC.SimpleMes.Report.Dto;
using SC.SimpleMes.WorkOrder.DomainEvent;
using SC.SimpleMes.WorkOrder.DTO;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkProcess.Dto;
using SC.SimpleMes.WorkProcessSet;
using SC.SimpleMes.WorkProcessSetBom.Dto;
using SC.SimpleMes.WorkStation;
using SC.SimpleMes.WorkStation.Dto;

namespace SC.SimpleMes.WorkOrder
{
    public class WorkOrderAppService : AsyncCrudAppService<WorkOrderInfo, WorkOrderInfoDto, long, CommonPageRequestDto, CreateUpdateWorkOrderInfoDto, CreateUpdateWorkOrderInfoDto>,
        IWorkOrderAppService
    {
        private readonly WorkOrderManager _workOrderManager;
        private readonly IRepository<ProductLine, long> _productLineRepository;
        private readonly IRepository<MaterialBatchNumber, long> _batchNumberRepository;
        private readonly IRepository<View_OverUseWorkOrderInfo, long> _overUserWorkOrderInfoRep;
        private readonly IRepository<View_DDImportantInfos, long> _viewDDImportantRep;
        private readonly BomUnitManager _bomUnitManager;
        private readonly IRepository<OrderMaterialProduceStatu, long> _orderMaterialProduceStatuRep;
        private readonly MaterialBatchNumberManager _materialBatchNumberManager;
        private readonly IWorkProcessSetCache _workProcessSetCache;
        private readonly IRepository<WorkProcess.WorkProcessSet, long> _workProcessSet;
        private readonly IRepository<WorkProcessSetProductRelation, long> _productProcessSetRelationRep;
        private readonly IRepository<WorkProcessInfo, long> _processRep;
        private readonly IRepository<WorkProcessMaterialRecordHistory, long> _processMaterialRecordHistoryRep;
        private readonly IRepository<WorkProcessMaterialRecord, long> _processMaterialRecordRep;
        private readonly IRepository<WorkProcessOperatorRecord, long> _workProcessOperatorRecordRep;
        private readonly IRepository<FormInfoRecord, long> _formInfoRecordRep;
        private readonly IRepository<FormTemplateInfo, long> _formTemplateInfoRep;
        private readonly IRepository<WorkOrderBom, long> _workOrderBOMRep;
        private readonly IConfigurationAppService _configurationAppService;
        private readonly IRepository<BOM.WorkProcessSetBom, long> _workProcessSetBomRep;
        private readonly IRepository<User, long> _userRep;
        private readonly IK3ErpRepostiory _k3ErpRepostiory;
        private readonly IEventBus _eventBus;

        public WorkOrderAppService(IRepository<WorkOrderInfo, long> repository,
            IRepository<ProductLine, long> productLineRepository,
            IRepository<MaterialBatchNumber, long> batchNumberRepository,
             IRepository<OrderMaterialProduceStatu, long> orderMaterialProduceStatuRep,
             IWorkProcessSetCache workProcessSetCache,
             BomUnitManager bomUnitManager,
             IRepository<WorkProcessInfo, long> processRep,
        MaterialBatchNumberManager materialBatchNumberManager,
        IRepository<WorkProcessSetProductRelation, long> productProcessSetRelationRep,
            WorkOrderManager workOrderManager,
            IRepository<WorkProcess.WorkProcessSet, long> workProcessSet,
            IRepository<FormInfoRecord, long> formInfoRecordRep,
             IRepository<FormTemplateInfo, long> formTemplateInfoRep,
             IRepository<WorkProcessMaterialRecord, long> processMaterialRecordRep,
        IRepository<WorkProcessMaterialRecordHistory, long> materialRecordHistStory,
        IRepository<WorkOrderBom, long> workOrderBOMRep,
        IConfigurationAppService configurationAppService,
        IRepository<User, long> userRep,
        IRepository<View_DDImportantInfos, long> viewDDImportantRep,
         IEventBus eventBus,
        IRepository<BOM.WorkProcessSetBom, long> workProcessSetBomRep,
        IRepository<View_OverUseWorkOrderInfo, long> overUserWorkOrderInfoRep,
        IK3ErpRepostiory k3ErpRepostiory,
            IRepository<WorkProcessOperatorRecord, long> workProcessOperatorRecord) : base(repository)
        {
            _workProcessSetBomRep = workProcessSetBomRep;
            _workOrderManager = workOrderManager;
            _productLineRepository = productLineRepository;
            _orderMaterialProduceStatuRep = orderMaterialProduceStatuRep;
            _batchNumberRepository = batchNumberRepository;
            _materialBatchNumberManager = materialBatchNumberManager;
            _workProcessSetCache = workProcessSetCache;
            _bomUnitManager = bomUnitManager;
            _productProcessSetRelationRep = productProcessSetRelationRep;
            _processRep = processRep;
            _workProcessSet = workProcessSet;
            _workProcessOperatorRecordRep = workProcessOperatorRecord;
            _processMaterialRecordHistoryRep = materialRecordHistStory;
            _formInfoRecordRep = formInfoRecordRep;
            _formTemplateInfoRep = formTemplateInfoRep;
            _processMaterialRecordRep = processMaterialRecordRep;
            _workOrderBOMRep = workOrderBOMRep;
            _configurationAppService = configurationAppService;
            _userRep = userRep;
            _eventBus = eventBus;
            _overUserWorkOrderInfoRep = overUserWorkOrderInfoRep;
            _viewDDImportantRep = viewDDImportantRep;
            _k3ErpRepostiory = k3ErpRepostiory;
        }


        [AbpAuthorize(PermissionNames.Page_WorkOrderManage, PermissionNames.BaseInfo_Edit)]
        public override Task<WorkOrderInfoDto> CreateAsync(CreateUpdateWorkOrderInfoDto input)
        {
            if (input.PlanStartTime > input.PlanEndTime)
            {
                throw new UserFriendlyException("计划开始时间不应该小于计划结束时间");
            }

            input.TenantId = AbpSession.TenantId;
            return base.CreateAsync(input);

        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage)]
        public override async Task<WorkOrderInfoDto> UpdateAsync(CreateUpdateWorkOrderInfoDto input)
        {
            if (input.PlanStartTime > input.PlanEndTime)
            {
                throw new UserFriendlyException("计划开始时间不应该小于计划结束时间");
            }

            var dataOrder = this.Repository.FirstOrDefault(p => p.Id == input.Id);

            if (dataOrder.WorkOrderStatu != WorkOrderStatuEnum.未开始)
            {
                throw new UserFriendlyException("订单已经变动，请勿修改");
            }
            dataOrder.FromOrderNumber = input.FromOrderNumber;
            dataOrder.PlanEndTime = input.PlanEndTime;
            dataOrder.PlanStartTime = input.PlanStartTime;
            dataOrder.DeletionTime = input.DeliveryTime;

            await UnitOfWorkManager.Current.SaveChangesAsync();
            return ObjectMapper.Map<WorkOrderInfoDto>(dataOrder);
        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage_Cancel)]
        public JHTAjaxResponse CancelOrder(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workOrderInfo = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            if (workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.已取消))
            {
                ajaxResponse.Msg = "订单已取消";
            }
            else
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = $"订单取消失败，订单状态为：{workOrderInfo.WorkOrderStatu}";
            }

            return ajaxResponse;
        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage)]
        public JHTAjaxResponse CloseOrder(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workOrderInfo = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            // 检查对应的产品是否设定等级标识，提醒设置产品设置等级
            if (workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.已关闭))
            {
                ajaxResponse.Msg = "订单已关闭";
            }
            else
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = $"订单关闭失败，订单状态为：{workOrderInfo.WorkOrderStatu}";
            }

            return ajaxResponse;
        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage_SNManage)]
        public JHTAjaxResponse<string> GenerateWorkOrderNumber()
        {
            return new JHTAjaxResponse<string>()
            {
                Data = _workOrderManager.GeneratWorkOrderNumber()
            };

        }


        protected override IQueryable<WorkOrderInfo> CreateFilteredQuery(CommonPageRequestDto input)
        {
            var queryCondition = input.QueryConditionObj as ConditonWorkOrderInfoDto;
            queryCondition.AnalyseTime();
            var query = this.Repository.GetAllIncluding(p => p.MaterialInfo, p => p.ProduceWorkShop, prop => prop.ProduceLine)
                .WhereIf(!string.IsNullOrEmpty(queryCondition.KeyWord), p => p.OrderNumber.Contains(queryCondition.KeyWord) || p.FromOrderNumber.Contains(queryCondition.KeyWord) || p.ProjectNumber.Contains(queryCondition.KeyWord) || p.ProjectName.Contains(queryCondition.KeyWord))
                .WhereIf(queryCondition.PlanStartTimeRange != null && queryCondition.PlanStartTimeRange.Length == 2, p => p.PlanStartTime >= queryCondition.PlanStartTimeStart && p.PlanStartTime <= queryCondition.PlanStartTimeEnd)
                .WhereIf(queryCondition.PlanEndTimeRange != null && queryCondition.PlanEndTimeRange.Length == 2, p => p.PlanStartTime >= queryCondition.PlanEndTimeStart && p.PlanStartTime <= queryCondition.PlanEndTimeEnd)
                .WhereIf(queryCondition.DeliveryTimeRange != null && queryCondition.DeliveryTimeRange.Length == 2, p => p.DeliveryTime >= queryCondition.DeliveryTimeStart && p.PlanStartTime <= queryCondition.DeliveryTimeEnd)
                .WhereIf(queryCondition.WorkOrderStatus != null && queryCondition.WorkOrderStatus.Length > 0, p => queryCondition.WorkOrderStatus.Contains(p.WorkOrderStatu))
                .WhereIf(queryCondition.MaterialInfoId > 0, p => p.MaterialInfoId == queryCondition.MaterialInfoId)
                ;

            return query;
        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage)]
        public JHTAjaxResponse RevertIssuedWorkOrder(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workOrderInfo = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            if (workOrderInfo.WorkOrderBomId != null)
            {
                _bomUnitManager.DelWorkOrderBomItem(workOrderInfo.WorkOrderBomId.GetValueOrDefault());
                _workOrderBOMRep.Delete(new WorkOrderBom() { Id = workOrderInfo.WorkOrderBomId.GetValueOrDefault() });
            }

            if (!workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.未开始))
            {
                ajaxResponse.Msg = $"订单撤回失败，订单状态为：{workOrderInfo.WorkOrderStatu}";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            ajaxResponse.Msg = "撤回成功";
            return ajaxResponse;
        }

        [AbpAuthorize(PermissionNames.Page_WorkOrderManage_Issues)]
        public async Task<JHTAjaxResponse> IssuedWorkOrderAsync(IssuedWorkOrderInfoDto workOrderInfoDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workOrderInfo = this.Repository.FirstOrDefault(p => p.Id == workOrderInfoDto.Id);
            var productLine = _productLineRepository.FirstOrDefault(p => p.Id == workOrderInfoDto.ProduceLineId);
            if (productLine == null)
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = $"订单下发失败，产线不存在";
                return ajaxResponse;
            }

            if (productLine.BelongWorkShopId != workOrderInfoDto.ProduceWorkShopId)
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = $"订单下发失败，产线所属车间与实际车间不一致";
                return ajaxResponse;
            }
            if (workOrderInfo.WorkOrderBomId == null)
            {

                // 1、查看对应产品是否有BOM
                if (workOrderInfo.BOMId == null)
                {
                    var bomInfo = _bomUnitManager.GetCurrentBomByMaterialId(workOrderInfo.MaterialInfoId);
                    if (bomInfo.Count != 1)
                    {
                        ajaxResponse.Code = 500;
                        ajaxResponse.Msg = $"订单下发失败，该产品有多个BOM，请联系管理员配置工艺BOM";
                        return ajaxResponse;
                    }
                    workOrderInfo.BOMId = bomInfo.FirstOrDefault().Id;
                }

                // 2、根据物料编码查询当前产品生效的工艺信息
                var workProcessSetInfo = _bomUnitManager.GetMaterialCurrentWorkProcessSetByMmaterialId(workOrderInfo.MaterialInfoId);
                if (workProcessSetInfo == null)
                {
                    ajaxResponse.Code = 500;
                    ajaxResponse.Msg = $"订单下发失败，未设置该产品的工艺信息！";
                    return ajaxResponse;
                }
                workOrderInfo.WorkProcessSetId = workProcessSetInfo.Id;

                // 3、查找该产品的工艺BOM信息
                var setBomInfo = _bomUnitManager.GetCurrentWorkProcessSetBomInfoBy(workOrderInfo.BOMId, workProcessSetInfo.Id);
                if (setBomInfo == null)
                {
                    ajaxResponse.Code = 500;
                    ajaxResponse.Msg = $"订单下发失败，未设置该产品的工艺BOM！";
                    return ajaxResponse;
                }

                // 创建工单BOM
                await this.CreateWorkOrderBOMAsync(new WorkOrderBomDto()
                {
                    MaterialId = workOrderInfo.MaterialInfoId,
                    WorkOrderId = workOrderInfo.Id,
                    WorkOrderNumber = workOrderInfo.OrderNumber,
                    WorkProcessSetBomId = setBomInfo.Id,
                });

            }


            if (_workOrderManager.IssuedWorkOrder(workOrderInfo, workOrderInfoDto.ProduceWorkShopId, workOrderInfoDto.ProduceLineId))
            {
                ajaxResponse.Msg = "订单已下发";
                return ajaxResponse;
            }
            else
            {
                ajaxResponse.Code = 500;
                ajaxResponse.Msg = $"订单下发失败，订单状态为：{workOrderInfo.WorkOrderStatu}";
                return ajaxResponse;
            }
        }

        public List<MaterialBatchNumberDto> LoadWorkOrderBatchNumbers(EntityDto<long> entityDto)
        {
            var workOrder = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            var dataListInfos = _batchNumberRepository
                                .GetAll()
                                .AsNoTracking()
                                .Where(p => p.FromOrderNumber == workOrder.OrderNumber && p.MaterialId == workOrder.MaterialInfoId).OrderByDescending(p => p.Id).ToList();


            var materialDto = ObjectMapper.Map<List<MaterialBatchNumberDto>>(dataListInfos);

            if (workOrder.CustomerProductInfo != null)
            {
                materialDto.ForEach(p =>
                {
                    // 2024-09-05  客制化名称显示
                    p.MaterialName = workOrder.CustomerProductInfo.MaterialName;
                });
            }

            return materialDto;
        }

        public List<FinishedProductStatusDto> LoadFinishedProductStatusDtos(EntityDto<long> entityDto)
        {
            List<FinishedProductStatusDto> finishedProductStatusDtos = new List<FinishedProductStatusDto>();
            var workOrder = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            var workProcessSetDetails = _workProcessSet.Get(workOrder.WorkProcessSetId.Value).GetWorkProcessSetDetails();
            var dataListInfos = _batchNumberRepository
                    .GetAll()
                    .AsNoTracking()
                    .Where(p => p.FromOrderNumber == workOrder.OrderNumber && p.MaterialId == workOrder.MaterialInfoId).ToList();
            foreach (var item in dataListInfos)
            {
                var addData = new FinishedProductStatusDto()
                {
                    BatchNumber = item.BatchNumber,
                    MaterialName = item.MaterialName,
                    MaterialNumber = item.MaterialNumber,
                    WorkOrderNumber = workOrder.OrderNumber,
                    Id = workOrder.Id
                };
                addData = finishedProductStatusDto(addData, workProcessSetDetails);
                addData.CompleteNumber = addData.ProduceSteps.Where(p => p.PlanStartTimeEnd != null).Count();
                finishedProductStatusDtos.Add(addData);
            }

            return finishedProductStatusDtos;
        }

        private FinishedProductStatusDto finishedProductStatusDto(FinishedProductStatusDto data, List<WorkProcessSetDetail> workProcessSetDetails)
        {
            var process = _processRep.GetAll().ToList();
            List<ProduceStep> produceSteps = new List<ProduceStep>();
            //已执行的工序
            var list = _workProcessOperatorRecordRep.GetAll().Where(p => p.OrderNumber == data.WorkOrderNumber
            && p.BatchNumber == data.BatchNumber).OrderBy(p => p.StartTime).ToList();
            var listids = list.Select(p => p.WorkProcessId).ToList();
            //还未执行过的工序
            var noworkProcessSetDetails = workProcessSetDetails.Where(p => !listids.Contains(p.BelongWorkProcessSetId)).ToList();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ProduceStep produceStep = new ProduceStep()
                    {
                        PlanStartTimeStart = list[i].StartTime,
                        PlanStartTimeEnd = list[i].EndTime,
                        ProcessInfoState = list[i].EndTime == null ? "生产中" : "已完成",
                        WorkProcessInfoId = list[i].WorkProcessId,
                        WorkProcessInfoName = process.Where(p => p.Id == list[i].WorkProcessId).FirstOrDefault().ProcessName
                    };
                    if (list[i].WorkProcessOperateType == WorkProcessOperateTypeEnum.异常反馈)
                    {
                        data.ProduceStatus = "异常";
                        produceStep.ProcessInfoState = "异常";
                    }
                    if (list[i].WorkProcessOperateType == WorkProcessOperateTypeEnum.异常处置)
                    {
                        data.ProduceStatus = "异常处置";
                        produceStep.ProcessInfoState = "异常处置";
                    }
                    produceSteps.Add(produceStep);
                }
            }
            else
            {
                data.ProduceStatus = "未开始";
                produceSteps.Add(new ProduceStep()
                {
                    PlanStartTimeEnd = null,
                    PlanStartTimeStart = null,
                    ProcessInfoState = "未开始",
                    WorkProcessInfoId = noworkProcessSetDetails.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).FirstOrDefault().BelongWorkProcessInfoId,
                    WorkProcessInfoName = noworkProcessSetDetails.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).FirstOrDefault().WorkProcessName
                });

            }
            foreach (var item in noworkProcessSetDetails)
            {
                string lastNodeId;
                if (workProcessSetDetails.Where(p => p.BelongWorkProcessInfoId == produceSteps[produceSteps.Count - 1].WorkProcessInfoId).FirstOrDefault() != null)
                {
                    var detail = workProcessSetDetails.Where(p => p.BelongWorkProcessInfoId == produceSteps[produceSteps.Count - 1].WorkProcessInfoId).FirstOrDefault();
                    lastNodeId = detail.NodeId;
                    if (produceSteps[produceSteps.Count - 1].ProcessInfoState == "异常处置")
                    {
                        break;
                    }
                }
                else
                {
                    var detail = workProcessSetDetails.Where(p => p.BelongWorkProcessInfoId == produceSteps[produceSteps.Count - 2].WorkProcessInfoId).FirstOrDefault();
                    lastNodeId = detail.NodeId;
                    if (produceSteps[produceSteps.Count - 1].ProcessInfoState == "异常处置")
                    {
                        break;
                    }
                }
                if (workProcessSetDetails.Where(p => p.ParentNodeId.Contains(lastNodeId)).Count() > 0)
                {
                    data.ProduceStatus = "生产中";
                    var stayAdd = workProcessSetDetails.Where(p => p.ParentNodeId.Contains(lastNodeId)).FirstOrDefault();
                    produceSteps.Add(new ProduceStep()
                    {
                        PlanStartTimeEnd = null,
                        PlanStartTimeStart = null,
                        ProcessInfoState = "未开始",
                        WorkProcessInfoId = noworkProcessSetDetails.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).FirstOrDefault().BelongWorkProcessInfoId,
                        WorkProcessInfoName = noworkProcessSetDetails.Where(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).FirstOrDefault().WorkProcessName
                    });
                }
                else
                {
                    data.ProduceStatus = "已完成";
                }
            }
            data.ProduceSteps = produceSteps;
            return data;
        }



        public JHTAjaxResponse<MaterialBatchNumberDto> CreateWorkOrderBatchNumber(CreateWorkOrderBatchNumberDto entityDto)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            var workOrder = this.Repository.GetAllIncluding(p => p.ProduceLine, prop => prop.MaterialInfo).FirstOrDefault(p => p.Id == entityDto.Id);

            var batchNumberCount = _batchNumberRepository.GetAll().Where(p => p.FromOrderNumber == workOrder.OrderNumber && p.MaterialId == workOrder.MaterialInfoId).Count();

            if (batchNumberCount == workOrder.ProduceCount)
            {
                ajaxResponse.Msg = "该订单物料已满足需求";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            var factoryCode = SettingManager.GetSettingValue(AppSettingNames.FactoryCode);

            var shiftCode = ShitCodeEnum.D;
            //if (!Enum.TryParse(entityDto.ShiftCode, out shiftCode))
            //{
            //    shiftCode = ShitCodeEnum.D;
            //}

            // 自动计算班次
            var shiftInfo = _configurationAppService.GetCurrentShiftInfo();
            

            var produtcLine = workOrder.ProduceLine;
            if (entityDto.ProductLineId != null)
            {
                produtcLine = this._productLineRepository.FirstOrDefault(p => p.Id == entityDto.ProductLineId);
            }

            if (!string.IsNullOrEmpty(entityDto.BatchNumber))
            {
                if (!PermissionChecker.IsGranted(PermissionNames.Page_SNBatcNumberManage_ManuallyInsert))
                {
                    ajaxResponse.Msg = "您没有权限插入编号信息";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

                if (_batchNumberRepository.GetAll().Any(p => p.BatchNumber == entityDto.BatchNumber))
                {
                    ajaxResponse.Msg = "该编号已经被占用，请勿重复插入";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

            }


            int flowNumber = 0;
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                if (string.IsNullOrEmpty(entityDto.BatchNumber))
                {
                    entityDto.BatchNumber = _materialBatchNumberManager.GenerateMaterialBatchNumber(workOrder.MaterialInfoId, factoryCode, produtcLine, out flowNumber, shiftInfo: shiftInfo);
                }
                else
                {
                    _materialBatchNumberManager.IniteMaterialBatchNumberManager(workOrder.MaterialInfoId, factoryCode, produtcLine);
                    flowNumber = _materialBatchNumberManager.GetMaterialBatchNumberFlower(entityDto.BatchNumber);
                }
            }

            var user = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            var batchNumber = new MaterialBatchNumber()
            {
                BatchNumber = entityDto.BatchNumber,
                FlowNumber = flowNumber,
                CreationTime = DateTime.Now,
                CreatorUserId = AbpSession.UserId,
                FromOrderNumber = workOrder.OrderNumber,
                MaterialName = workOrder.MaterialInfo.MaterialName,
                WrapUniteName = workOrder.MaterialInfo.UnitName,
                BOMMaterialUnitName = workOrder.MaterialInfo.UnitName,
                IsSerialsNumber = true,
                LastPrintTime = null,
                MaterialId = workOrder.MaterialInfoId,
                CreateProductLineId = produtcLine.Id,
                CreateWorkStationId = entityDto.CreateWorkStationId,
                CreateWorkStationName = entityDto.CreateWorkStationName,
                Creator = user.Name,
                CreatorIds = AbpSession.UserId.ToString(),
                MatrialCount = entityDto.MaterialCount,
                MaterialNumber = workOrder.MaterialInfo.MaterialNumber
            };
            batchNumber.Id = _batchNumberRepository.InsertAndGetId(batchNumber);
            ajaxResponse.Data = ObjectMapper.Map<MaterialBatchNumberDto>(batchNumber);

            return ajaxResponse;
        }

        public WorkOrderInfoDto GetWorkOrderInfoByOrderNumber(string orderNumer)
        {
            return ObjectMapper.Map<WorkOrderInfoDto>(this.Repository.GetAll().FirstOrDefault(p => p.OrderNumber == orderNumer));
        }

        public ProduceStatusEnum? GetProduceStatus(string materialBatchNumber)
        {
            var materilStatu = _orderMaterialProduceStatuRep.FirstOrDefault(p => p.MaterialBatchNumber == materialBatchNumber);
            return materilStatu == null ? null : materilStatu.ProduceStatus;
        }

        public bool CheckMaterialBatchNumberWorkProcess(string materialBatchNumber, long currentWorkProcessId, long workProcessSetId, ref string message)
        {
            var productStatus = _orderMaterialProduceStatuRep.FirstOrDefault(p => p.MaterialBatchNumber == materialBatchNumber);

            var currentOperateWorkProcessInfo = _processRep.FirstOrDefault(p => p.Id == currentWorkProcessId);
            // 生产状态为异常
            if (productStatus != null && (productStatus.ProduceStatus == ProduceStatusEnum.异常 || productStatus.ProduceStatus == ProduceStatusEnum.异常处置))
            {
                if (!PermissionChecker.IsGranted(PermissionNames.Page_QualityManager_ProblemDeal))
                {
                    message = $"该产品状态为{productStatus.ProduceStatus},您没有权限处理该产品，请联系质检人员";
                    return false;
                }

                if (currentOperateWorkProcessInfo.WorkProcessType != WorkProcessTypeEnum.返修工序)
                {
                    message = $"该产品已报生产状态异常,请至维修工序处理";
                    return false;
                }
            }

            if (productStatus != null && productStatus.ProduceStatus == ProduceStatusEnum.已完成)
            {

                if (currentOperateWorkProcessInfo.WorkProcessType == WorkProcessTypeEnum.FQC && PermissionChecker.IsGranted(PermissionNames.Page_QualityManager_UpdateFormInfo))
                {
                    return true;
                }
                else
                {
                    message = $"该产品状态为{productStatus.ProduceStatus},请勿操作";
                    return false;
                }
            }

            var workProcessSetInfo = _workProcessSetCache.Get(workProcessSetId);
            var operaterWorkProcess = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.BelongWorkProcessInfoId == currentWorkProcessId);

            // 当前工序为空，判断是否为IPQC工序 或返修工序
            if (operaterWorkProcess == null)
            {
                var processInfo = _processRep.FirstOrDefault(p => p.Id == currentWorkProcessId);
                if (processInfo != null && processInfo.WorkProcessType == WorkProcessTypeEnum.IPQC)
                {
                    return true;
                }

                if (productStatus != null && processInfo != null && processInfo.WorkProcessType == WorkProcessTypeEnum.返修工序 && (productStatus.ProduceStatus == ProduceStatusEnum.异常 || productStatus.ProduceStatus == ProduceStatusEnum.异常处置))
                {
                    return true;
                }

                if (processInfo != null && processInfo.WorkProcessType == WorkProcessTypeEnum.返修工序 && (productStatus.ProduceStatus != ProduceStatusEnum.异常 || productStatus.ProduceStatus != ProduceStatusEnum.异常处置))
                {
                    message = $"该产品状态为{productStatus.ProduceStatus},请调整至对应工位进行操作";
                    return false;
                }

                message = $"该产品无需在该工位进行操作";
                return false;
            }

            // 物料还未开始加工
            if (productStatus == null && (operaterWorkProcess.ParentNodeId == null || operaterWorkProcess.ParentNodeId.Count == 0))
            {
                return true;
            }

            if (productStatus == null && operaterWorkProcess.ParentNodeId != null && operaterWorkProcess.ParentNodeId.Count > 0)
            {
                message = $"请从该产品的第一道工序【{workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).WorkProcessName}】开始加工！";
                return false;
            }

            // 确保工序顺序操作
            if (productStatus != null && productStatus.CurrentWorkProcessId != currentWorkProcessId)
            {
                // 当前工序还未完成
                var currentWorkPorcess = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.BelongWorkProcessInfoId == productStatus.CurrentWorkProcessId);
                if (productStatus.IsCurrentWorkProcessDone == false)
                {
                    message = $"该产品工序【{currentWorkPorcess.WorkProcessName}】还未完成，请勿操作！";
                    return false;
                }

                // 如果是返修中则不用检查工序顺序,但不能越过正常的工序
                if (productStatus.ProduceStatus == ProduceStatusEnum.返修中 && workProcessSetInfo.ComputeLeftProcessCount(currentWorkProcessId) >= workProcessSetInfo.ComputeLeftProcessCount(productStatus.NormalWorkProcessId))
                {
                    return true;
                }


                // 判断工序当前位置  当操作工序在当前工序之前时
                if (workProcessSetInfo.ComputeLeftProcessCount(currentWorkProcessId) > workProcessSetInfo.ComputeLeftProcessCount(productStatus.CurrentWorkProcessId))
                {
                    message = $"该工序已完成,请勿重复操作！";
                    return false;
                }

                var paerntWorkProcess = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => operaterWorkProcess.ParentNodeId.Contains(p.NodeId));
                WorkProcessInfo parentWorkProcesInfo = null;
                do
                {
                    if (paerntWorkProcess == null && currentWorkPorcess.ParentNodeId != null && currentWorkPorcess.ParentNodeId.Count > 0)
                    {
                        message = $"该工序已完成,请勿重复操作！";
                        return false;
                    }

                    parentWorkProcesInfo = _processRep.FirstOrDefault(p => p.Id == paerntWorkProcess.BelongWorkProcessInfoId);
                    if (paerntWorkProcess.BelongWorkProcessInfoId == productStatus.CurrentWorkProcessId) // 上一工序是否相同
                    {
                        return true;
                    }

                    if (parentWorkProcesInfo.Id != productStatus.CurrentWorkProcessId && !parentWorkProcesInfo.CanJump)
                    {
                        message = $"该产品上一工序【{paerntWorkProcess.WorkProcessName}】还未完成，请勿操作！";
                        return false;
                    }

                    if (parentWorkProcesInfo.Id != productStatus.CurrentWorkProcessId && parentWorkProcesInfo.CanJump)
                    {
                        paerntWorkProcess = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => paerntWorkProcess.ParentNodeId.Contains(p.NodeId));
                    }
                }
                while (paerntWorkProcess != null && parentWorkProcesInfo.CanJump);
            }

            if (productStatus != null && productStatus.CurrentWorkProcessId == currentWorkProcessId)
            {
                if (productStatus.IsCurrentWorkProcessDone)
                {
                    message = $"该产品的本道工序已完成,请勿重复操作！";
                    return false;
                }

                return true;
            }

            return false;
        }

        public async Task CreateWorkOrderBOMAsync(WorkOrderBomDto workOrderBomDto)
        {
            var workOrder = await this.Repository.GetAsync(workOrderBomDto.WorkOrderId);
            WorkOrderBom add = new WorkOrderBom()
            {
                MaterialId = workOrder.MaterialInfoId,
                WorkOrderId = workOrder.Id,
                WorkOrderInfo = workOrder,
                WorkOrderNumber = workOrder.OrderNumber,
                WorkProcessSetBomId = workOrderBomDto.WorkProcessSetBomId
            };

            var WorkOrderBOMId = await _bomUnitManager.CreateWorkOrderBOM(add);
            workOrder.WorkOrderBomId = WorkOrderBOMId;
            var relation = _productProcessSetRelationRep.GetAll().FirstOrDefault(p => p.MaterialInfoId == workOrder.MaterialInfoId && p.IsCurrent == true);
            workOrder.WorkProcessSetId = relation.BelongWorkProcessSetId;
            await this.Repository.UpdateAsync(workOrder);


        }

        public async Task ResetWorkOrderBOM(long WorkOrderId)
        {
            var workOrder = await this.Repository.GetAsync(WorkOrderId);
            await _bomUnitManager.ResetWorkOrderBOM(WorkOrderId);
            workOrder.WorkOrderBomId = null;
            workOrder.WorkProcessSetId = null;
            await this.Repository.UpdateAsync(workOrder);
        }

        public WorkProcessSetBomDto GetSetBOMByWorkOrdBOM(long WorkOrderId)
        {
            return ObjectMapper.Map<WorkProcessSetBomDto>(_bomUnitManager.GetSetBOMByWorkOrdBOM(WorkOrderId));
        }

        public List<OrderMaterialProduceStatuDto> LoadWorkOrderProductPercentInfo(EntityDto<long> entityDto)
        {
            var workOrder = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            var materilaStatuInfos = _orderMaterialProduceStatuRep.GetAll().Where(p => p.WorkOrderNumber == workOrder.OrderNumber);
            var workProcessSet = _workProcessSetCache.Get(workOrder.WorkProcessSetId.GetValueOrDefault());
            var overyDays = int.Parse(SettingManager.GetSettingValue(AppSettingNames.OverDayConfing));

            List<OrderMaterialProduceStatuDto> produceStatuDtos = ObjectMapper.Map<List<OrderMaterialProduceStatuDto>>(materilaStatuInfos);
            produceStatuDtos.ForEach(p =>
            {
                p.FinishPercentage = 100 - (int)Math.Ceiling(p.LeftWorkProcessCount * 100.0 / workProcessSet.WorkProcessSetDetails.Count);
                p.IsOverDay = (DateTime.Now - p.LastUpdateTime.GetValueOrDefault()).TotalDays > overyDays;
            });

            return produceStatuDtos;
        }

        [AbpAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse_ProduceRecord, PermissionNames.Data_SinlgeOperatorRecord, RequireAllPermissions = false)]
        public List<WorkProcessOperatorRecordDto> LoadWorkOrderOperatorRecordInfo(EntityDto<string> entityDto)
        {
            return ObjectMapper.Map<List<WorkProcessOperatorRecordDto>>(_workProcessOperatorRecordRep.GetAll().Where(p => p.BatchNumber == entityDto.Id).OrderByDescending(p => p.Id));
        }

        [AbpAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse_ProduceRecord, PermissionNames.Data_SinlgeOperatorRecord, RequireAllPermissions = false)]
        public List<WorkProcessMaterialRecordDto> LoadWorkOrderMaterialRecordHistory(EntityDto<string> entityDto)
        {
            var resultList = ObjectMapper.Map<List<WorkProcessMaterialRecordDto>>(_processMaterialRecordHistoryRep.GetAll().Where(p => p.ProductBatchNumber == entityDto.Id).OrderByDescending(p => p.Id));
            if (!IsGranted(PermissionNames.Page_MaterialInfoSupplier))
            {
                resultList.ForEach(p =>
                {
                    p.InputMaterialName = string.Empty;
                    p.Supplier = string.Empty;
                    p.BatchNo = string.Empty;
                });
            }

            return resultList;
        }

        [AbpAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse_ProduceRecord, PermissionNames.Data_SinlgeOperatorRecord, RequireAllPermissions = false)]
        public List<FormInfoRecordDto> LoadWorkOrderFillReportList(EntityDto<string> entityDto)
        {
            var allFormInfoRecord = _formInfoRecordRep.GetAll().Where(p => p.BelongMaterialBatchNumber == entityDto.Id).OrderByDescending(p => p.Id);
            var result = ObjectMapper.Map<List<FormInfoRecordDto>>(allFormInfoRecord);
            var templateIds = result.Select(p => p.BelongFormId).Distinct();
            var templateInfos = _formTemplateInfoRep.GetAll().Where(p => templateIds.Contains(p.Id));
            foreach (var item in result)
            {
                item.FormTemplateInfo = ObjectMapper.Map<FormTemplateInfoDto>(templateInfos.FirstOrDefault(p => p.Id == item.BelongFormId));
            }

            return result;
        }

        [AbpAuthorize(PermissionNames.Report_OrderMaterialProduceStatuse_ProduceRecord, PermissionNames.Data_SinlgeOperatorRecord, RequireAllPermissions = false)]
        public List<WorkProcessMaterialRecordDto> LoadWorkOrderFinalyUsedMaterial(EntityDto<string> entityDto)
        {
            var resultList = ObjectMapper.Map<List<WorkProcessMaterialRecordDto>>(_processMaterialRecordRep.GetAll().Where(p => p.ProductBatchNumber == entityDto.Id).OrderByDescending(p => p.Id));
            if (!IsGranted(PermissionNames.Page_MaterialInfoSupplier))
            {
                resultList.ForEach(p =>
                {
                    p.InputMaterialName = string.Empty;
                    p.Supplier = string.Empty;
                    p.BatchNo = string.Empty;
                });
            }
            else
            {
                foreach (var item in resultList)
                {
                    if (item.IsLineSideMaterial)
                    {
                        item.SubMaterialRecord = ObjectMapper.Map<List<WorkProcessMaterialRecordDto>>(_processMaterialRecordRep.GetAll().Where(p => p.ProductBatchNumber == item.InputMaterialBatchNumber).OrderByDescending(p => p.Id));
                    }
                }
            }

            return resultList;
        }

        public ProductLineDto LoadSelectSnProdcutInfo(EntityDto<string> snBatchNumber)
        {
            // 如果产线没有信息的话
            var bacthNumber = _materialBatchNumberManager.GetMaterialBatchNumberInfo(snBatchNumber.Id);
            if (bacthNumber == null)
            {
                throw new UserFriendlyException("未找到该序列号信息");
            }

            var workOrderInfo = this.Repository.GetAll().FirstOrDefault(p => p.OrderNumber == bacthNumber.FromOrderNumber);
            return ObjectMapper.Map<ProductLineDto>(_productLineRepository.GetAllIncluding(p => p.BelongWorkShop).FirstOrDefault(p => p.Id == bacthNumber.CreateProductLineId || p.Id == workOrderInfo.ProduceLineId));
        }

        /// <summary>
        /// 根据产线ID获取生产中的订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WorkOrderInfoDto> GetProductLineWorkOrderInfoByProductLineId(long? id)
        {
            return ObjectMapper.Map<List<WorkOrderInfoDto>>(this.Repository.GetAllIncluding(p => p.ProduceLine, d => d.MaterialInfo)
                .WhereIf(id > 0, p => p.ProduceLineId == id)
                .Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已下发 || p.WorkOrderStatu == WorkOrderStatuEnum.生产中).ToList());
        }

        public async Task<JHTAjaxResponse> CheckIsBomMaterialAsync(string workOrderNumber, string materialNumber)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Code = 200;
            var workOrderInfo = this.Repository.GetAllIncluding(p => p.WorkOrderBom).FirstOrDefault(p => p.OrderNumber == workOrderNumber);
            if (this._bomUnitManager.IsMaterialInWorkOrderBom(workOrderInfo.WorkOrderBomId, materialNumber) == false)
            {
                ajaxResponse.Msg = "该物料不能用于当前工单中";
                ajaxResponse.Code = 500;
            }

            return ajaxResponse;
        }

        public JHTAjaxResponse PauseWorkOrder(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Code = 200;
            var workOrder = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            if (!workOrder.SetWorkOrderStatu(WorkOrderStatuEnum.已暂停))
            {
                ajaxResponse.Msg = "当前工单状态不允许暂停";
            }

            return ajaxResponse;
        }

        public JHTAjaxResponse<WorkOrderPickingMaterilInfoDto> GetWorkOrderPickingMaterilInfo(WorkOrderPickingMaterilInfoDto workOrderPickingMaterilInfoDto)
        {
            var dataResult = _k3ErpRepostiory.GetWorkOrderPickingMaterilInfo(workOrderPickingMaterilInfoDto.WorkOrderNumber, workOrderPickingMaterilInfoDto.MaterialNumber);
            return new JHTAjaxResponse<WorkOrderPickingMaterilInfoDto>()
            {
                Data = this.ObjectMapper.Map<WorkOrderPickingMaterilInfoDto>(dataResult)
            };
        }

        public JHTAjaxResponse RecoverWorkOrder(EntityDto<long> entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            ajaxResponse.Code = 200;
            var workOrder = this.Repository.FirstOrDefault(p => p.Id == entityDto.Id);
            if (!workOrder.SetWorkOrderStatu(WorkOrderStatuEnum.生产中))
            {
                ajaxResponse.Msg = "当前工单状态不允许恢复";
            }

            return ajaxResponse;
        }

        public async Task<WorkOrderBomDto> GetWorkOrderBomInfoAsync(EntityDto<long> workOrderId)
        {
            WorkOrderBomDto reuslt = new WorkOrderBomDto();
            var workOrderInfo = await Repository.FirstOrDefaultAsync(p => p.Id == workOrderId.Id);
            reuslt = ObjectMapper.Map<WorkOrderBomDto>(_workOrderBOMRep.FirstOrDefault(p => p.Id == workOrderInfo.WorkOrderBomId));
            if (workOrderInfo.WorkOrderBomId > 0)
            {
                reuslt.OrderBomItemDtos = ObjectMapper.Map<List<WorkOrderBomItemDto>>(_bomUnitManager.GetWorkOrderBomItems(workOrderInfo.WorkOrderBomId.GetValueOrDefault()));
            }

            return reuslt;
        }

        /// <summary>
        /// 更新工单BOM项信息
        /// </summary>
        /// <param name="workOrderBomDto"></param>
        public void UpdateWorkOrderBOMItems(WorkOrderBomDto workOrderBomDto)
        {
            var workOrderBomInfo = _workOrderBOMRep.FirstOrDefault(p => p.Id == workOrderBomDto.Id);
            var workOrderInfo = Repository.FirstOrDefault(p => p.Id == workOrderBomDto.WorkOrderId);
            if (workOrderBomInfo == null)
            {
                workOrderBomInfo = new WorkOrderBom()
                {
                    MaterialId = workOrderInfo.MaterialInfoId,
                    WorkOrderId = workOrderBomDto.WorkOrderId,
                    WorkOrderNumber = workOrderInfo.OrderNumber,
                    WorkProcessSetBomId = workOrderBomDto.WorkProcessSetBomId,
                };

                workOrderBomDto.Id = _workOrderBOMRep.InsertAndGetId(workOrderBomInfo);
                workOrderInfo.WorkOrderBomId = workOrderBomDto.Id;
                UnitOfWorkManager.Current.SaveChanges();
            }

            // 更新工艺BOMID
            var setBom = _workProcessSetBomRep.FirstOrDefault(p => p.Id == workOrderBomDto.WorkProcessSetBomId);
            workOrderInfo.WorkProcessSetId = setBom.BelongWorkProcessSetId;

            workOrderBomInfo.WorkProcessSetBomId = workOrderBomDto.WorkProcessSetBomId;
            _bomUnitManager.DelWorkOrderBomItem(workOrderBomDto.Id);

            List<WorkOrderBomItem> bomItems = new List<WorkOrderBomItem>();
            foreach (var item in workOrderBomDto.OrderBomItemDtos)
            {
                bomItems.Add(new WorkOrderBomItem()
                {
                    BelongWorkOrderBomId = workOrderBomDto.Id,
                    BelongWorkProcessId = item.BelongWorkProcessId,
                    CreationTime = DateTime.Now,
                    InputMaterialId = item.InputMaterialId,
                    InputMaterialCount = item.InputMaterialCount,
                    CreatorUserId = AbpSession.UserId,
                });
            }

            _bomUnitManager.AddWorkOrderBomItems(bomItems);// 添加BomItemInfo
            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<bool> IsCutMaterialEnough(string workOrderNumber, long currentWorkProcessId, string materialNumber, bool needRecord = false)
        {
            var isEnough = false;
            List<WorkOrderMaterilCostItem> result = new List<WorkOrderMaterilCostItem>();
            // 加载工单BOM信息
            var perpareWorkProcess = _processRep.GetAll().Where(p => p.WorkProcessType == WorkProcessTypeEnum.前置物料准备工序).Select(p => p.Id).ToList();
            if (perpareWorkProcess.Any(p => p == currentWorkProcessId) == false)
            {
                // 非前置工序无序检查
                return true;
            }
            var workOrderInfo = this.Repository.FirstOrDefault(p => p.OrderNumber == workOrderNumber);
            // 非在制品加工工序
            if (workOrderInfo != null)
            {
                var bomData = await _bomUnitManager.GetBomItemInfosByBomIdAsync(workOrderInfo.BOMId.GetValueOrDefault());

                // 除在制品投入总物料的数量
                var haveUsedMaterialStatic = _processMaterialRecordRep.GetAll()
                     .Where(p => p.OrderNumber == workOrderNumber &&
                     p.InputMaterialNumber == materialNumber &&
                     p.ProductBatchNumber.Contains("WIP") == false && p.WorkProcessId == currentWorkProcessId &&
                     p.IsRepairedInput == false
                     )
                     .GroupBy(p => new { p.InputMaterialNumber })
                     .Select(d => new WorkProcessMaterialRecordDto()
                     {
                         InputMaterialNumber = d.Key.InputMaterialNumber,
                         BOMMaterialCount = d.Sum(p => p.BOMMaterialCount)
                     }).ToList();

                // 在制品物料投入的数量
                var wipHaveUsedMaterialStatic = _processMaterialRecordRep.GetAll()
                     .Where(p => p.OrderNumber == workOrderNumber && p.InputMaterialNumber == materialNumber
                     && p.ProductBatchNumber.Contains("WIP")
                     && p.WorkProcessId == currentWorkProcessId
                     && p.IsRepairedInput == false
                     )
                     .GroupBy(p => new { p.InputMaterialNumber })
                     .Select(d => new WorkProcessMaterialRecordDto()
                     {
                         InputMaterialNumber = d.Key.InputMaterialNumber,
                         BOMMaterialCount = d.Sum(p => p.BOMMaterialCount)
                     }).ToList();

                if ((haveUsedMaterialStatic.Count > 0 || wipHaveUsedMaterialStatic.Count > 0) && bomData.Any(p => p.FormMaterialNumber == materialNumber))
                {
                    var neededCount = bomData.FirstOrDefault(p => p.FormMaterialNumber == materialNumber).FormCount * workOrderInfo.ProduceCount;
                    if (haveUsedMaterialStatic.Count > 0)
                    {
                        isEnough = (haveUsedMaterialStatic.Count > 0 && neededCount <= haveUsedMaterialStatic.FirstOrDefault().BOMMaterialCount);
                    }

                    if (wipHaveUsedMaterialStatic.Count > 0)
                    {
                        isEnough = (wipHaveUsedMaterialStatic.Count > 0 && neededCount <= wipHaveUsedMaterialStatic.FirstOrDefault().BOMMaterialCount);
                    }

                    if (isEnough && needRecord)// 未关闭的工单触发超用统计
                    {
                        _eventBus.Trigger(new WorkOrderMaterialOverUseEventData() { WorkOrderNumber = workOrderNumber });
                    }
                }
            }

            return isEnough;
        }

        public async Task<PagedResultDto<WorkOrderInfoDto>> SearchOverUsedWorOrderInfoAsync(CommonPageRequestDto input)
        {
            var queryCondition = input.QueryConditionObj as ReportQueryConditonDto;
            queryCondition.ParseTime();
            var query = this._overUserWorkOrderInfoRep.GetAll()
                .WhereIf(!string.IsNullOrEmpty(queryCondition.KeyWord), p => p.OrderNumber.Contains(queryCondition.KeyWord) || p.FromOrderNumber.Contains(queryCondition.KeyWord) || p.ProjectNumber.Contains(queryCondition.KeyWord) || p.ProjectName.Contains(queryCondition.KeyWord))
                .WhereIf(queryCondition.StartDate != null, p => p.FirstWarningTime >= queryCondition.StartDate)
                .WhereIf(queryCondition.EndDate != null, p => p.FirstWarningTime <= queryCondition.StartDate)
                .WhereIf(queryCondition.IsOverUsed == true, p => p.FirstWarningTime != null)
                .WhereIf(queryCondition.ProductLineId > 0, p => p.ProduceLineId == queryCondition.ProductLineId)
                .WhereIf(queryCondition.MaterialId > 0, p => p.MaterialInfoId == queryCondition.MaterialId)
                ;
            return new PagedResultDto<WorkOrderInfoDto>()
            {
                Items = ObjectMapper.Map<List<WorkOrderInfoDto>>(await query.OrderByDescending(p => p.FirstWarningTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync()),
                TotalCount = await query.CountAsync(),

            };
        }

        public List<WorkOrderInfoDto> GetCloseWorkOrderInfoByOrderNumber(string orderNumber)
        {
            var workOrderInfo = ObjectMapper.Map<List<WorkOrderInfoDto>>(this.Repository.GetAllIncluding(p => p.MaterialInfo)
                .Where(p => p.OrderNumber == orderNumber && p.WorkOrderStatu == WorkOrderStatuEnum.已关闭).ToList());

            if (workOrderInfo == null || workOrderInfo.Count == 0)
            {
                throw new UserFriendlyException("该工单不存在");
            }

            var anyRepairedMaterialInfo = _orderMaterialProduceStatuRep.GetAll()
                .Any(p => p.WorkOrderNumber == orderNumber && p.ProduceStatus != ProduceStatusEnum.已完成 && p.ProduceStatus != ProduceStatusEnum.报废);
            if (anyRepairedMaterialInfo == false)
            {
                throw new UserFriendlyException("该工单没有存在返修的产品");
            }

            return workOrderInfo;
        }

        public bool DoWorkOrderHaveRepairdProduct(string orderNumber)
        {
            var repairedProdcutSn = _orderMaterialProduceStatuRep
                        .GetAll()
                        .Where(p => p.WorkOrderNumber == orderNumber && p.HaveRepaired &&
                        p.ProduceStatus != ProduceStatusEnum.已完成 && p.ProduceStatus != ProduceStatusEnum.报废).Select(p => p.MaterialBatchNumber).ToList();

            return _viewDDImportantRep.GetAll().Any(p => repairedProdcutSn.Contains(p.BelongMaterialBatchNumber) && p.IsInStock == 1);
        }

        /// <summary>
        /// 设置客制化信息
        /// </summary>
        /// <param name="customerProductInfo"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse> SetWorkOrderCustomerProductInfo(CustomerProductInfoDto customerProductInfo)
        {
            var workOrderInfo = await this.Repository.FirstOrDefaultAsync(p => p.OrderNumber == customerProductInfo.WorkOrderNumber);
            workOrderInfo.SetCustomerProductInfo(ObjectMapper.Map<CustomerProductInfo>(customerProductInfo));
            await this.UnitOfWorkManager.Current.SaveChangesAsync();
            return new JHTAjaxResponse()
            {
                Msg = "设置成功"
            };
        }
    }
}
