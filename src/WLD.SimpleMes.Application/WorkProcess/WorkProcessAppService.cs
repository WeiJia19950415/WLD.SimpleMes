using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using JHT.CommonUtity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WLD.SimpleMes.AttachFile;
using WLD.SimpleMes.Authorization;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.BOM.Dto;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.JHTOrganzation;
using WLD.SimpleMes.K3DBInfo;
using WLD.SimpleMes.LineSideWarehouse;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Users.Dto;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkProcessSet;
using WLD.SimpleMes.WorkStation;

namespace WLD.SimpleMes.WorkProcess
{
    public class WorkProcessAppService : AsyncCrudAppService<WorkProcessInfo, WorkProcessInfoDto, long, DTO.CommonPageRequestDto, WorkProcessInfoDto, WorkProcessInfoDto>,
        IWorkProcessAppService
    {
        private readonly IRepository<WorkProcessStationRelation, long> _workProcessStationRepository;
        private readonly IRepository<View_OrderMaterialProduceStatuses, long> _viewOrderMaterialProduceStatusesRepository;
        private readonly WorkOrderBomManager _workOrderBomManager;
        private readonly WorkOrderManager _workOrderManager;
        private readonly IRepository<User, long> _userRep;
        private readonly IRepository<WorkStationInfo, long> _workStationRep;
        private readonly IRepository<MaterialBatchNumber, long> _materialBatchRep;
        private readonly IRepository<MaterialInfo, long> _materialRep;
        private readonly WorkProcessInfoManager _workProcessInfoManager;
        private readonly ProcessSetManager _workProcessSetManager;
        private readonly WorkStationManager _workStationManager;
        private readonly MaterialBatchNumberManager _materialBatchNumberManager;
        private readonly AppOrganizationUnitManager _organzationManager;
        private readonly FormTemplateInfoManager _formTemplateInfoManager;
        private readonly IRepository<ProblemRecord, long> _problemRecordRep;
        private readonly IMaterialBatchNumberCache _materialBatchNumberCache;
        private readonly IK3ErpRepostiory _k3ErpRepostiory;
        private readonly IWorkProcessSetCache _workProcessSetCache;
        private readonly FileSaveOptions _fileSaveOptions;
        private readonly IRepository<ProblemDealRecord, long> _problemDealRecordRep;
        private readonly IRepository<ERPInStockInfo, long> _erpInStockRepository;
        private readonly IRepository<LineSideMaterialInfoBomItem, long> _lineSideMaterialInfoBomItemRep;
        private readonly IRepository<LineSideMaterialInfo, long> _lineSideMaterialInfoRep;
        private readonly IConfigurationAppService _configurationAppService;
        private readonly IRepository<WorkProcessMaterialRecord, long> _processMaterialRecordRep;
        private readonly IRepository<View_DDImportantInfos, long> _viewDDImportantRep;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly UserManager _userManager;
        private readonly MaterialManager _materialManager;
        private readonly IMaterialBatchNumberAppService _materialBatchNumberAppService;
        private readonly IRepository<MaterialDiscardRecord, long> _materialDiscardRecordRep;
        private readonly IRepository<ERPInStockInfoOperateRecord, long> _erpInstockInfoRecord;

        public WorkProcessAppService(
            IRepository<WorkStationInfo, long> workStationRep,
            IRepository<WorkProcessInfo, long> repository,
            IRepository<MaterialBatchNumber, long> materialBacthRep,
            IRepository<LineSideMaterialInfoBomItem, long> lineSideMaterialInfoBomItemRep,
            WorkProcessInfoManager workProcessInfoManager,
            ProcessSetManager workProcessSetManager,
            WorkStationManager workStationManager,
            WorkOrderBomManager workOrderBomManager,
            MaterialBatchNumberManager materialBatchNumberManager,
            FormTemplateInfoManager formTemplateInfoManager,
            IRepository<ERPInStockInfo, long> erpInStockRepository,
             IRepository<MaterialInfo, long> materialRep,
              IRepository<User, long> userRep,
              IRepository<ProblemRecord, long> problemRecordRep,
             WorkOrderManager workOrderManager,
            IOptionsMonitor<FileSaveOptions> fileSaveOptionsMonitor,
             IWorkProcessSetCache workProcessSetCache,
             IRepository<ProblemDealRecord, long> problemDealRecord,
             IRepository<WorkProcessMaterialRecord, long> processMaterialRecordRep,
        IMaterialBatchNumberCache materialBatchNumberCache,
        IConfigurationAppService configurationAppService,
        //IRepository<CutMaterialConfig, long> cutMaterialConfigRep,
        IRepository<MaterialDiscardRecord, long> materialDiscardRecordRep,
        IRepository<LineSideMaterialInfo, long> lineSideMaterialInfoRep,
        IWorkOrderAppService workOrderAppService,
        UserManager userManager,
        IK3ErpRepostiory k3ErpRepostiory,
        IRepository<View_DDImportantInfos, long> viewDDImportantRep,
        IRepository<View_OrderMaterialProduceStatuses, long> viewOrderMaterialProduceStatusesRepository,
        MaterialManager materialManager,
        AppOrganizationUnitManager organzationManager,
        IMaterialBatchNumberAppService materialBatchNumberAppService,
        IRepository<ERPInStockInfoOperateRecord, long> erpInstockInfoRecord,
            IRepository<WorkProcessStationRelation, long> workProcessStationRepository) : base(repository)
        {
            _workProcessStationRepository = workProcessStationRepository;
            _workStationRep = workStationRep;
            _workProcessInfoManager = workProcessInfoManager;
            _workProcessSetManager = workProcessSetManager;
            _materialBatchRep = materialBacthRep;
            _materialRep = materialRep;
            _workStationManager = workStationManager;
            _workOrderBomManager = workOrderBomManager;
            _materialBatchNumberCache = materialBatchNumberCache;
            _materialBatchNumberManager = materialBatchNumberManager;
            _erpInStockRepository = erpInStockRepository;
            _workOrderManager = workOrderManager;
            _workProcessSetCache = workProcessSetCache;
            _formTemplateInfoManager = formTemplateInfoManager;
            _fileSaveOptions = fileSaveOptionsMonitor.CurrentValue;
            _userRep = userRep;
            _problemRecordRep = problemRecordRep;
            _problemDealRecordRep = problemDealRecord;
            _k3ErpRepostiory = k3ErpRepostiory;
            _configurationAppService = configurationAppService;
            _lineSideMaterialInfoBomItemRep = lineSideMaterialInfoBomItemRep;
            _workOrderAppService = workOrderAppService;
            _lineSideMaterialInfoRep = lineSideMaterialInfoRep;
            _processMaterialRecordRep = processMaterialRecordRep;
            _userManager = userManager;
            _materialManager = materialManager;
            _viewOrderMaterialProduceStatusesRepository = viewOrderMaterialProduceStatusesRepository;
            _viewDDImportantRep = viewDDImportantRep;
            _materialBatchNumberAppService = materialBatchNumberAppService;
            _materialDiscardRecordRep = materialDiscardRecordRep;
            _organzationManager = organzationManager;
            _erpInstockInfoRecord = erpInstockInfoRecord;
        }

        public PageData<WorkProcessInfoDto> SearchWorkProcessInfo(JHTPageAjaxResquest<WorkProcessConditionDto> pageAjaxResquest)
        {
            PageData<WorkProcessInfoDto> result = new PageData<WorkProcessInfoDto>();
            var condition = pageAjaxResquest.Condition;
            var query = this.Repository
                .GetAllIncluding(p => p.WorkProcessStationRelations)
                .AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(condition.KeyWord), p => p.ProcessNumber.Contains(condition.KeyWord) || p.ProcessName.Contains(condition.KeyWord))
                .WhereIf(condition.WorkProcessPowerType.HasValue, p => p.WorkProcessPowerType == condition.WorkProcessPowerType)
                .WhereIf(condition.WorkProcessType.HasValue, p => p.WorkProcessType == condition.WorkProcessType);
            result.Total = query.Count();
            var data = query.OrderBy(p => p.ProcessNumber).Skip(pageAjaxResquest.SkipCount).Take(pageAjaxResquest.PageSize).ToList();
            result.List = ObjectMapper.Map<List<WorkProcessInfoDto>>(data);

            var distinctStationIds = new List<long>();
            data.Select(p => p.WorkProcessStationRelations).ToList().ForEach(p =>
            {
                foreach (var item in p)
                {
                    if (!distinctStationIds.Contains(item.BelongWorkStationId))
                    {
                        distinctStationIds.Add(item.BelongWorkStationId);
                    }
                }
            });

            var loadWorkStation = _workStationRep.GetAll().AsNoTracking().Where(p => distinctStationIds.Contains(p.Id));
            foreach (var item in result.List)
            {
                var relationWorkInfo = data.FirstOrDefault(p => p.Id == item.Id).WorkProcessStationRelations;
                foreach (var relation in relationWorkInfo)
                {
                    List<long> stationIds = new List<long>();
                    var stationInfo = loadWorkStation.FirstOrDefault(p => p.Id == relation.BelongWorkStationId);
                    if (stationInfo != null)
                    {
                        stationIds.Add(stationInfo.BelongWorkShopId.GetValueOrDefault());
                        stationIds.Add(stationInfo.BelongProductLineId.GetValueOrDefault());
                        stationIds.Add(relation.BelongWorkStationId);
                    }

                    item.BelongWorkStationNames.Add(stationInfo.WorkStationNumber);
                    item.BelongWorkStationsIds.Add(stationIds);
                }
            }

            return result;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public override async Task<WorkProcessInfoDto> CreateAsync(WorkProcessInfoDto input)
        {
            input.TenantId = AbpSession.TenantId;
            if (_workProcessInfoManager.CheckUniqueWorkProcessNumber(input.ProcessNumber) == false)
            {
                throw new UserFriendlyException("该工序编号已被使用");
            }

            var workProcess = ObjectMapper.Map<WorkProcessInfo>(input);
            List<WorkProcessStationRelation> relations = new List<WorkProcessStationRelation>();
            foreach (var item in input.BelongWorkStationsIds)
            {
                relations.Add(new WorkProcessStationRelation()
                {
                    BelongWorkStationId = item[2],
                    CreatTime = DateTime.Now,
                });
            }

            return ObjectMapper.Map<WorkProcessInfoDto>(await _workProcessInfoManager.AddWorkProcessInfoAsync(workProcess, relations));
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public override async Task<WorkProcessInfoDto> UpdateAsync(WorkProcessInfoDto input)
        {
            if (_workProcessInfoManager.CheckUniqueWorkProcessNumber(input.ProcessNumber, input.Id) == false)
            {
                throw new UserFriendlyException("该工序编号已被使用");
            }

            if (_workProcessSetManager.IsProcessIsUsed(input.Id))
            {
                throw new UserFriendlyException("该工序已被使用,禁止更新");
            }

            var workProcess = ObjectMapper.Map<WorkProcessInfo>(input);
            List<WorkProcessStationRelation> relations = new List<WorkProcessStationRelation>();
            foreach (var item in input.BelongWorkStationsIds)
            {
                relations.Add(new WorkProcessStationRelation()
                {
                    BelongWorkStationId = item[2],
                    CreatTime = DateTime.Now,
                    BelongWorkProcessId = input.Id,
                });
            }

            await _workProcessInfoManager.UpdateWorkProcessAsync(workProcess, relations);
            return input;
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public async Task ToggleEnableWorkProcessAsync(EntityDto<long> dto)
        {
            if (_workProcessSetManager.IsProcessIsUsed(dto.Id))
            {
                throw new UserFriendlyException("该工序已被使用,禁止更新");
            }

            await _workProcessInfoManager.ToggleEnableWorkProcessAsync(dto.Id);
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public async Task<JHTAjaxResponse> SetWorkProcessMaterialConfigAsync(WorkProcessMaterialConfigDto configDto)
        {
            await _workProcessInfoManager.SetConfigMaterialAsync(configDto.WorkProcessId, configDto.MaterialIds);
            return new JHTAjaxResponse() { Msg = "配置成功" };
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public async Task<List<MaterialInfoDto>> LoadConfigdMaterialInfosAsync(EntityDto<long> id)
        {
            var workProcess = await Repository.FirstOrDefaultAsync(x => x.Id == id.Id);
            var configIds = workProcess.GetConfigMaterials();
            if (configIds == null)
            {
                return new List<MaterialInfoDto>();
            }
            return ObjectMapper.Map<List<MaterialInfoDto>>(_materialRep.GetAll().Where(p => configIds.Contains(p.Id)).ToList());
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public List<WorkProcessFormInfoRelationDto> GetWorkProcessFormRelation(long id)
        {
            return ObjectMapper.Map<List<WorkProcessFormInfoRelationDto>>(_workProcessInfoManager.LoadWorkProcessRelationForms(id));
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public void SetWorkProcessFormRelation(WorkProcessFormRelationConfigDto relationConfigDto)
        {
            List<WorkProcessFormInfoRelation> relations = new List<WorkProcessFormInfoRelation>();
            foreach (var item in relationConfigDto.FormTemplateIds)
            {
                if (_workProcessInfoManager.IsUsedFormTemplateId(relationConfigDto.Id, item, relationConfigDto.FormUseType) == false)
                {
                    relations.Add(new WorkProcessFormInfoRelation()
                    {
                        BelongFormInfoId = item,
                        BelongWorkProcessId = relationConfigDto.Id,
                        FormUseType = relationConfigDto.FormUseType,
                        IsEnabled = false
                    });
                }
            }

            if (relations.Count > 0)
            {
                _workProcessInfoManager.SetWorkProcessFormTemlate(relations);
            }
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public async Task ToggleWorkProcessFormEnabledAsync(EntityDto<long> realitonId)
        {
            await _workProcessInfoManager.ToggleWorkProcessFormEnabledAsync(realitonId.Id);
        }

        [AbpAuthorize(PermissionNames.Page_ProcessManage)]
        public async Task SetWorkProcessFormUseTypeAsync(WorkProcessFormInfoRelationDto relation)
        {
            await _workProcessInfoManager.SetWorkProcessFormUseTypeAsync(relation.Id, relation.FormUseType);
        }

        /// <summary>
        /// 检查物料能否在该工序中使用
        /// </summary>
        /// <param name="productMaterialBatchNumber"></param>
        /// <param name="inputMaterialBatchNumber"></param>
        /// <param name="currentWorkProcessId"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckInputMaterialBatchNumberAsync(string productMaterialBatchNumber, string inputMaterialBatchNumber, long currentWorkProcessId)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            ajaxResponse.Code = 500;
            // 1.查找对应的工单BOM ，找到工单BOM中该工序的投入数量
            var prodcutInfo = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(productMaterialBatchNumber);
            var inputMaterialInfo = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(inputMaterialBatchNumber);
            _materialBatchNumberManager.CheckBatchNumberIsDiscard(inputMaterialBatchNumber);

            if (inputMaterialInfo == null)
            {
                var erpBatchNumber = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == inputMaterialBatchNumber);
                if (erpBatchNumber != null && erpBatchNumber.MaterialStatu == MaterialStatuEnum.可用)// 允许直接从ERP批次号中获取数据
                {
                    var materialInfo = _materialRep.FirstOrDefault(p => p.MaterialNumber == erpBatchNumber.MaterialNumber);
                    inputMaterialInfo = new MaterialBatchNumberDto()
                    {
                        BatchNumber = erpBatchNumber.BatchNo,
                        FromErpBatchNumber = erpBatchNumber.BatchNo,
                        MaterialName = erpBatchNumber.MaterialName,
                        MaterialNumber = erpBatchNumber.MaterialNumber,
                        MaterialId = materialInfo.Id,
                        WrapUniteName = materialInfo.UnitName,
                    };
                }
                else if (erpBatchNumber != null && erpBatchNumber.MaterialStatu != MaterialStatuEnum.可用)
                {
                    ajaxResponse.Msg = $"物料：{erpBatchNumber.MaterialNumber},批次号：{erpBatchNumber.BatchNo}，状态为【{erpBatchNumber.MaterialStatu}】，请联系质量部门进行处理";
                    return ajaxResponse;
                }
                else
                {
                    ajaxResponse.Msg = "该物料不存在，请重新扫码";
                    return ajaxResponse;
                }
            }
            // 判断是否为在制品，如果为在制品，则需要找到相关的组成原材料
            if (inputMaterialInfo.IsLineMaterialInfo)
            {
                var inputmaterilaList = _processMaterialRecordRep.GetAll().Where(p => p.ProductBatchNumber == inputMaterialBatchNumber).ToList();
                foreach (var item in inputmaterilaList)
                {
                    if (!string.IsNullOrEmpty(item.BatchNo))
                    {    // 检查在制品相关的物料组成的物料信息是否未可用状态
                        var erpBatchNumber = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == inputMaterialBatchNumber);
                        if (erpBatchNumber != null && erpBatchNumber.MaterialStatu != MaterialStatuEnum.可用)
                        {
                            ajaxResponse.Msg = $"物料：{erpBatchNumber.MaterialNumber},批次号：{erpBatchNumber.BatchNo}，状态为【{erpBatchNumber.MaterialStatu}】，请联系质量部门进行处理";
                            return ajaxResponse;
                        }
                    }

                    // 工单物料信息检查
                    if (!_workOrderBomManager.CheckWorkBomMaterail(item.InputMaterilId, currentWorkProcessId, prodcutInfo.FromOrderNumber))
                    {
                        ajaxResponse.Msg = $"在制品中组成的物料：{item.InputMaterialName}不用于当前工序，请联系班组长！";
                        return ajaxResponse;
                    }
                }
            }
            else
            {
                // 工单物料信息检查
                if (!_workOrderBomManager.CheckWorkBomMaterail(inputMaterialInfo.MaterialId, currentWorkProcessId, prodcutInfo.FromOrderNumber))
                {
                    ajaxResponse.Msg = $"物料：{inputMaterialInfo.MaterialName}不能用于当前工序，请联系班组长";
                    return ajaxResponse;
                }
            }

            // 2.查找物料投入记录表，检查该物料批次号是否被使用,使用的数量逻辑需要进行控制
            var canReusedBatchNumber = bool.Parse(SettingManager.GetSettingValue(AppSettingNames.CanReusedBatchNumber));
            if (!canReusedBatchNumber && _workProcessInfoManager.CanMaterialBatchNumberBeUse(inputMaterialBatchNumber, out string message) == false)
            {
                // 判断批次物料是否已超量使用
                ajaxResponse.Msg = message;
                return ajaxResponse;
            }

            // 3.返回物料批次信息
            ajaxResponse.Data = inputMaterialInfo;
            ajaxResponse.Code = 200;
            return ajaxResponse;
        }

        /// <summary>
        /// 生成前置物料准备的小批次号
        /// 1、检查该物料能否用于该工序
        /// 2、检查该工序是否未为前置工序
        /// 3、调用批次号生成规则
        /// 4、记录操作记录、物料投入记录
        /// </summary>
        /// <param name="buildSubMaterialBatchNumberDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildPrepareWorkProcessBatchNumberAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            var workProcessInfo = await this.Repository.FirstOrDefaultAsync(p => p.Id == buildSubMaterialBatchNumberDto.CurrentWorkProcessId);
            var workOrderInfo = _workOrderAppService.GetWorkOrderInfoByOrderNumber(buildSubMaterialBatchNumberDto.WorkOrderNumber);
            if (workProcessInfo.WorkProcessType != WorkProcessTypeEnum.前置物料准备工序)
            {
                ajaxResponse.Msg = "非前置物料准备工序，请勿操作";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            foreach (var item in buildSubMaterialBatchNumberDto.InputMatreilInfos)
            {
                var erpInstokcInfo = _erpInStockRepository.FirstOrDefault(p => p.MaterialNumber == item.MaterialNumber && p.BatchNo == item.FromErpBatchNumber);
                if (erpInstokcInfo == null)
                {
                    ajaxResponse.Msg = $"批次号：{item.FromErpBatchNumber},与物料:{item.MaterialNumber}对应物料编号不一致";// 核对物料信息
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }
            }

            #region 检查前置物料种类是否完备
            if (buildSubMaterialBatchNumberDto.OnlineMaterialInfoId > 0)
            {
                var ajaxRslt = await CheckLineMaterialInfoBOMAsync(buildSubMaterialBatchNumberDto);
                if (ajaxRslt.Code != 200)
                {
                    return ajaxRslt;
                }
            }

            #endregion

            if (buildSubMaterialBatchNumberDto.IsRepairedInput)
            {
                bool anyRepairedProduct = false;
                // 检查对应的生产任务单是否关闭，且该生产任务单状态下有返修电堆
                if (workOrderInfo.WorkOrderStatu == WorkOrderStatuEnum.已关闭)
                {
                    anyRepairedProduct = _viewOrderMaterialProduceStatusesRepository.GetAll()
                        .Any(p => p.WorkOrderNumber == workOrderInfo.OrderNumber &&
                        p.ProduceStatus != ProduceStatusEnum.已完成
                        && p.ProduceStatus != ProduceStatusEnum.报废);
                }
                // 未关闭生产任务单查看是否有返修入库电堆
                if (workOrderInfo.WorkOrderStatu == WorkOrderStatuEnum.生产中)
                {
                    var repairedProdcutSn = _viewOrderMaterialProduceStatusesRepository
                        .GetAll()
                        .Where(p => p.WorkOrderNumber == workOrderInfo.OrderNumber && p.HaveRepaired &&
                        p.ProduceStatus != ProduceStatusEnum.已完成 && p.ProduceStatus != ProduceStatusEnum.报废).Select(p => p.MaterialBatchNumber).ToList();

                    anyRepairedProduct = _viewDDImportantRep.GetAll().Any(p => repairedProdcutSn.Contains(p.BelongMaterialBatchNumber) && p.IsInStock == 1);

#if DEBUG
                    anyRepairedProduct = true;
#endif
                }
                // 是否售后返修
                if (anyRepairedProduct == false)
                {
                    ajaxResponse.Msg = "该生产任务单已关闭，无法进行裁切领料";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }
            }

            var factoryCode = SettingManager.GetSettingValue(AppSettingNames.FactoryCode);
            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == buildSubMaterialBatchNumberDto.CurrentWorkStationId);
            var shiftCode = ShitCodeEnum.D;
            var shiftInfo = _configurationAppService.GetCurrentShiftInfo();
            if (shiftInfo != null && !Enum.TryParse(shiftInfo.ShiftCode, out shiftCode))
            {
                shiftCode = ShitCodeEnum.D;
            }
            int flowNumber = 0;
            var mainMaterialInfo = buildSubMaterialBatchNumberDto.InputMatreilInfos.FirstOrDefault();
            var materialInfo = await _materialRep.FirstOrDefaultAsync(p => p.MaterialNumber == mainMaterialInfo.MaterialNumber);


            string batchNumber = "";
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                if (buildSubMaterialBatchNumberDto.OnlineMaterialInfoId > 0)
                {
                    batchNumber = _materialBatchNumberManager
                        .GenerateMaterialBatchNumber(buildSubMaterialBatchNumberDto.OnlineMaterialInfoId.GetValueOrDefault(), factoryCode, workStaion.BelongProductLine, out flowNumber, shiftInfo: shiftInfo, isLineSideMaterial: true);
                }
                else
                {
                    batchNumber = _materialBatchNumberManager.GenerateMaterialBatchNumber(materialInfo.Id, factoryCode, workStaion.BelongProductLine, out flowNumber, shiftInfo: shiftInfo, workProcessTypeEnum: workProcessInfo.WorkProcessType);
                }
            }

            MaterialBatchNumber result = new MaterialBatchNumber()
            {
                BatchNumber = batchNumber,
                CreatorUserId = AbpSession.UserId.GetValueOrDefault(),
                FromOrderNumber = String.IsNullOrEmpty(buildSubMaterialBatchNumberDto.WorkOrderNumber) ? "" : buildSubMaterialBatchNumberDto.WorkOrderNumber,
                CreateWorkStationId = buildSubMaterialBatchNumberDto.CurrentWorkStationId,
                CreateProductLineId = workStaion.BelongProductLineId,
                Creator = String.Join(',', buildSubMaterialBatchNumberDto.Creator.Select(p => p.Name)),
                CreatorIds = String.Join(',', buildSubMaterialBatchNumberDto.Creator.Select(p => p.Id)),
                CreateWorkStationName = workStaion.WorkStationName,
                FlowNumber = flowNumber
            };

            var user = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());
            if (buildSubMaterialBatchNumberDto.Creator == null || buildSubMaterialBatchNumberDto.Creator.Count() == 0)
            {
                buildSubMaterialBatchNumberDto.Creator = new List<UserDto>() { new UserDto() { Id = user.Id, Name = user.Name } };
            }

            if (buildSubMaterialBatchNumberDto.OnlineMaterialInfoId > 0)
            {
                // 创建批次号
                var onlineMateralInfo = _lineSideMaterialInfoRep.FirstOrDefault(p => p.Id == buildSubMaterialBatchNumberDto.OnlineMaterialInfoId);
                result.MaterialId = materialInfo.Id;
                result.FromErpBatchNumber = "";
                result.MaterialName = onlineMateralInfo.MaterialName;
                result.MaterialNumber = onlineMateralInfo.MaterialNumber;
                result.MatrialCount = buildSubMaterialBatchNumberDto.MatrialCount;
                result.WrapUniteName = onlineMateralInfo.UnitName;
                result.BOMMaterialCount = buildSubMaterialBatchNumberDto.MatrialCount;
                result.BOMMaterialUnitName = onlineMateralInfo.UnitName;
            }
            else
            {
                // 创建批次号 裁切单一物料生成批次号
                var itemCutMaterialConfig = _materialManager.LoadCutMaterialConfig(workOrderInfo.MaterialInfoId, mainMaterialInfo.MaterialNumber);
                if (itemCutMaterialConfig != null)
                {
                    result.BOMMaterialCount = itemCutMaterialConfig.ConversionRatio * mainMaterialInfo.MatrialCount.GetValueOrDefault();
                    result.BOMMaterialUnitName = itemCutMaterialConfig.ConfigMaterialUnitName;
                }
                else
                {
                    result.BOMMaterialCount = mainMaterialInfo.MatrialCount.GetValueOrDefault();
                    result.BOMMaterialUnitName = materialInfo.UnitName;
                }

                result.BatchNumber = batchNumber;
                result.MaterialId = materialInfo.Id;
                result.FromErpBatchNumber = mainMaterialInfo.FromErpBatchNumber;
                result.MaterialName = materialInfo.MaterialName;
                result.MaterialNumber = materialInfo.MaterialNumber;

                result.MatrialCount = mainMaterialInfo.MatrialCount.GetValueOrDefault();
                result.WrapUniteName = itemCutMaterialConfig == null ? materialInfo.UnitName : itemCutMaterialConfig.CutUnitName;
                result.CreatorUserId = AbpSession.UserId.GetValueOrDefault();
            }

            result = _materialBatchNumberManager.InsertMaterialBatchNumber(result);

            // 结束生产操作记录
            _workProcessInfoManager.EndWorkProcessOperatorRecord(new WorkProcessOperatorRecord()
            {
                Id = buildSubMaterialBatchNumberDto.OperateRecordId.GetValueOrDefault(),
                CurrentOperatroAccountId = AbpSession.UserId.GetValueOrDefault(),
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                WorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                WorkProcessNumber = workProcessInfo.ProcessNumber,
                WorkProcessName = workProcessInfo.ProcessName,
                WorkStationId = workStaion.Id,
                IsNormalFinish = true,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                WorkProcessOperateType = WorkProcessOperateTypeEnum.开始生产,
                BatchNumber = string.IsNullOrEmpty(mainMaterialInfo.BatchNumber) ? mainMaterialInfo.FromErpBatchNumber : mainMaterialInfo.BatchNumber,
            }, batchNumber);

            #region 添加工序物料操作记录
            string message = "操作成功";
            message = await BuildPreparMaterialWorkProcessRecord(buildSubMaterialBatchNumberDto, buildSubMaterialBatchNumberDto.IsRepairedInput, workOrderInfo.MaterialInfoId, batchNumber, workProcessInfo, workStaion);
            #endregion

            // 如果有更新在制品，则替换操作记录信息
            ajaxResponse.Data = ObjectMapper.Map<MaterialBatchNumberDto>(result);
            ajaxResponse.Msg = message;
            if (ajaxResponse.Code == 200 && !string.IsNullOrEmpty(message) && message != "操作成功")
            {
                ajaxResponse.Code = 201;// 需要前端确认信息
            }
            return ajaxResponse;
        }


        private async Task<string> BuildPreparMaterialWorkProcessRecord(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto, bool IsRepairedInput, long materialInfoId, string productBatchNumber, WorkProcessInfo workProcessInfo, WorkStationInfo workStaion)
        {
            string tipMessage = "";
            List<WorkProcessMaterialRecord> addRecord = new List<WorkProcessMaterialRecord>();
            var RecordHistory = new List<WorkProcessMaterialRecordHistory>();
            foreach (var item in buildSubMaterialBatchNumberDto.InputMatreilInfos)
            {
                await _workOrderAppService.IsCutMaterialEnough(buildSubMaterialBatchNumberDto.WorkOrderNumber, workProcessInfo.Id, item.MaterialNumber, !IsRepairedInput);

                var itemaErpInstockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == item.BatchNumber || p.BatchNo == item.FromErpBatchNumber);
                var itemMaterialInfo = _materialRep.FirstOrDefault(p => p.MaterialNumber == item.MaterialNumber);
                var itemCutMaterialConfig = _materialManager.LoadCutMaterialConfig(materialInfoId, item.MaterialNumber);
                //_cutMaterialConfigAppService.LoadCutMaterialConfig(new CutMaterialConfigDto() { UsedProductId = materialInfoId, ConfigMaterialNumber = item.MaterialNumber });
                item.BOMMaterialCount = item.MatrialCount;
                item.BOMUnitName = itemMaterialInfo.UnitName;
                if (itemCutMaterialConfig != null)
                {
                    item.BOMMaterialCount = itemCutMaterialConfig.ConversionRatio * item.MatrialCount;
                    item.BOMUnitName = itemCutMaterialConfig.ConfigMaterialUnitName;
                }
                else
                {
                    item.WrapUniteName = itemMaterialInfo.UnitName;
                }

                var isOrigianlMaterial = _materialBatchNumberCache.GetByMaterialBatchNumber(item.BatchNumber) == null;
                if (isOrigianlMaterial)// 针对原始物料判断ERP物料是否用尽
                {
                    bool isUsedOut = _materialBatchNumberAppService.CheckERPBacthNumberMaterialIsUsedOut(item.FromErpBatchNumber, item.BOMMaterialCount.GetValueOrDefault(), true);
                    if (isUsedOut)
                    {
                        tipMessage += $"批次物料：{item.FromErpBatchNumber},已超用，请注意跟换批次号";
                    }
                }


                DateTime nowTime = DateTime.Now;
                addRecord.Add(new WorkProcessMaterialRecord()
                {
                    CreateTime = nowTime,
                    ProductBatchNumber = productBatchNumber,
                    IsRepairedInput = IsRepairedInput,
                    OrderNumber = buildSubMaterialBatchNumberDto.WorkOrderNumber,
                    InputMaterialBatchNumber = string.IsNullOrEmpty(item.BatchNumber) ? item.FromErpBatchNumber : item.BatchNumber,
                    ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                    InputMaterilId = itemMaterialInfo.Id,
                    WorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                    WorkProcessName = workProcessInfo.ProcessName,
                    WorkStationId = workStaion.Id,
                    WorkStationName = workStaion.WorkStationName,
                    InputMaterialNumber = itemMaterialInfo.MaterialNumber,
                    InputMaterialName = itemMaterialInfo.MaterialName,
                    Supplier = itemaErpInstockInfo.Supplier,
                    BatchNo = itemaErpInstockInfo.BatchNo,
                    WarehousingTime = itemaErpInstockInfo.WarehousingTime,
                    InputUnitName = itemCutMaterialConfig != null ? itemCutMaterialConfig.CutUnitName : itemMaterialInfo.UnitName,
                    InputMaterialCount = item.MatrialCount,
                    BOMMaterialCount = item.BOMMaterialCount,
                    BOMUnitName = itemMaterialInfo.UnitName,
                    WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                });


                RecordHistory.Add(new WorkProcessMaterialRecordHistory()
                {
                    CreateTime = nowTime,
                    OrderNumber = buildSubMaterialBatchNumberDto.WorkOrderNumber,
                    InputMaterialBatchNumber = string.IsNullOrEmpty(item.BatchNumber) ? item.FromErpBatchNumber : item.BatchNumber,
                    ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                    InputMaterilId = itemMaterialInfo.Id,
                    WorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                    WorkProcessName = workProcessInfo.ProcessName,
                    WorkStationId = workStaion.Id,
                    WorkStationName = workStaion.WorkStationName,
                    InputMaterialNumber = itemMaterialInfo.MaterialNumber,
                    InputMaterialName = itemMaterialInfo.MaterialName,
                    Supplier = itemaErpInstockInfo.Supplier,
                    BatchNo = itemaErpInstockInfo.BatchNo,
                    WarehousingTime = itemaErpInstockInfo.WarehousingTime,
                    ChangeReason = "新增",
                    InputUnitName = itemCutMaterialConfig != null ? itemCutMaterialConfig.CutUnitName : itemMaterialInfo.UnitName,
                    InputMaterialCount = item.MatrialCount,
                    BOMMaterialCount = item.BOMMaterialCount,
                    BOMUnitName = itemMaterialInfo.UnitName,
                    ProductBatchNumber = productBatchNumber,
                    WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                });
            }
            _workProcessInfoManager.BatchAddMaterilRecord(addRecord);
            _workProcessInfoManager.BatchAddMaterilRecordHistory(RecordHistory);
            return tipMessage;
        }

        /// <summary>
        /// 检查在制品是否够用
        /// </summary>
        /// <param name="buildSubMaterialBatchNumberDto"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> CheckLineMaterialInfoBOMAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto)
        {
            JHTAjaxResponse<MaterialBatchNumberDto> ajaxResponse = new JHTAjaxResponse<MaterialBatchNumberDto>();
            var bomInfo = _lineSideMaterialInfoBomItemRep.GetAll()
                                .Where(p => p.LineSideMaterialInfoId == buildSubMaterialBatchNumberDto.OnlineMaterialInfoId.GetValueOrDefault()).ToList();

            var workOrderInfo = _workOrderAppService.GetWorkOrderInfoByOrderNumber(buildSubMaterialBatchNumberDto.WorkOrderNumber);
            // 不能加工不允许的BOM物料
            foreach (var item in buildSubMaterialBatchNumberDto.InputMatreilInfos)
            {
                var cutCongfig = _materialManager.LoadCutMaterialConfig(workOrderInfo.MaterialInfoId, item.MaterialNumber);
                if (bomInfo.Any(p => item.MaterialNumber.StartsWith(p.FormMaterialCategoryNumber)) == false)
                {
                    ajaxResponse.Msg = $"该物料{item.MaterialName}不能用于该在制品的加工";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

                var checkResult = await _workOrderAppService.CheckIsBomMaterialAsync(buildSubMaterialBatchNumberDto.WorkOrderNumber, item.MaterialNumber);
                if (checkResult.Code != 200)
                {
                    ajaxResponse.Msg = $"该物料{item.MaterialName}不能用于该工单中";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

                // 判断批次物料是否已超量使用  不阻止使用
                var canReusedBatchNumber = bool.Parse(SettingManager.GetSettingValue(AppSettingNames.CanReusedBatchNumber));
                if (cutCongfig != null)
                {
                    item.BOMMaterialCount = item.MatrialCount * cutCongfig.ConversionRatio;
                    item.BOMUnitName = cutCongfig.ConfigMaterialUnitName;
                }
                else
                {
                    item.BOMMaterialCount = item.MatrialCount;
                    item.BOMUnitName = item.WrapUniteName;
                }

                if (!canReusedBatchNumber && _workProcessInfoManager.CanMaterialBatchNumberBeUse(item.BatchNumber, out string message, item.BOMMaterialCount.GetValueOrDefault()) == false)
                {
                    ajaxResponse.Code = 500;
                    ajaxResponse.Msg = message;
                    return ajaxResponse;
                }

                // 检查工单物料是否足够  不阻止使用
                var result = await _workOrderAppService.IsCutMaterialEnough(buildSubMaterialBatchNumberDto.WorkOrderNumber, buildSubMaterialBatchNumberDto.CurrentWorkProcessId, item.MaterialNumber, false);
                if (result)
                {
                    ajaxResponse.Msg = $"加工物料{item.MaterialName}已满足工单所需，请更换生产任务单号！";
                }
            }

            // 物料数量是否足够
            if (ajaxResponse.Code == 200)
            {
                foreach (var item in bomInfo)
                {
                    if (buildSubMaterialBatchNumberDto.InputMatreilInfos.Any(p => p.MaterialNumber.StartsWith(item.FormMaterialCategoryNumber)) == false)
                    {
                        ajaxResponse.Msg = $"该在制品还需要添加物料{item.FormMaterialCategoryName}";
                        ajaxResponse.Code = 500;
                        return ajaxResponse;
                    }

                    var useCount = buildSubMaterialBatchNumberDto.InputMatreilInfos.Where(p => p.MaterialNumber.StartsWith(item.FormMaterialCategoryNumber)).Sum(p => p.MatrialCount);
                    var actualNeedCount = buildSubMaterialBatchNumberDto.MatrialCount * item.FormMaterialAmount;
                    if (useCount < actualNeedCount)
                    {
                        ajaxResponse.Msg = $"该在制品还需要添加物料:{item.FormMaterialCategoryName}，数量{actualNeedCount - useCount}";
                        ajaxResponse.Code = 500;
                        return ajaxResponse;
                    }
                }
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 添加人员生产记录
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse> StartProduce(InputOperatorRecordInfo entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workProcessInfo = await this.Repository.FirstOrDefaultAsync(p => p.Id == entityDto.WorkProcessId);
            var innertBatchNumber = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(entityDto.OperatroMaterilBatchNumber);
            MaterialInfo material = null;
            if (innertBatchNumber == null)
            {
                var erpInstockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == entityDto.OperatroMaterilBatchNumber);
                if (erpInstockInfo != null)
                {
                    material = await _materialRep.FirstOrDefaultAsync(p => p.MaterialNumber == erpInstockInfo.MaterialNumber);
                }
            }
            else
            {
                material = await _materialRep.FirstOrDefaultAsync(p => p.Id == innertBatchNumber.MaterialId);
            }

            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == entityDto.WorkStationId);
            WorkOrderInfo workOrderInfo = null;
            if (innertBatchNumber != null)
            {
                var workOrderInfoNumber = string.IsNullOrEmpty(entityDto.WorkOrderNumber) ? innertBatchNumber.FromOrderNumber : entityDto.WorkOrderNumber;
                workOrderInfo = _workOrderManager.GetWorkOrderByOrderNumber(workOrderInfoNumber);
            }


            var proStatu = _workOrderManager.GetMaterialProduceStatu(entityDto.OperatroMaterilBatchNumber);
            // 更新产品生产状态
            if (innertBatchNumber != null && innertBatchNumber.FromOrderNumber == entityDto.WorkOrderNumber)
            {
                if (innertBatchNumber.CreateProductLineId == null || innertBatchNumber.CreateProductLineId == 0)
                {
                    throw new UserFriendlyException("产品序列号未指定生产产线");
                }

                if (workOrderInfo.WorkOrderStatu != WorkOrderStatuEnum.已暂停 && workOrderInfo.WorkOrderStatu != WorkOrderStatuEnum.已关闭)
                {
                    workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.生产中);
                }

                var workProcessSet = await _workProcessSetCache.GetAsync(workOrderInfo.WorkProcessSetId.GetValueOrDefault());
                var leftCount = workProcessSet.ComputeLeftProcessCount(entityDto.WorkProcessId);// 计算剩余的工序数量

                if (proStatu == null)
                {
                    proStatu = new OrderMaterialProduceStatu()
                    {
                        CurrentWorkProcessId = entityDto.WorkProcessId,
                        CurrentWorkStationId = entityDto.WorkStationId,
                        CurrentProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                        NormalWorkProcessId = entityDto.WorkProcessId,
                        MaterialBatchNumber = entityDto.OperatroMaterilBatchNumber,
                        MaterialInfoId = innertBatchNumber.MaterialId,
                        WorkOrderNumber = innertBatchNumber.FromOrderNumber,
                        LeftWorkProcessCount = leftCount,
                        ProduceStatus = ProduceStatusEnum.生产中,
                        IsCurrentWorkProcessDone = false,
                        StartTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now,
                    };
                }
                else
                {
                    proStatu.LeftWorkProcessCount = leftCount;
                    if (proStatu.NormalWorkProcessId > 0)
                    {
                        var normalLeftCount = workProcessSet.ComputeLeftProcessCount(proStatu.NormalWorkProcessId);
                        proStatu.NormalWorkProcessId = normalLeftCount <= leftCount ? proStatu.NormalWorkProcessId : entityDto.WorkProcessId;
                        proStatu.ProduceStatus = normalLeftCount < leftCount ? ProduceStatusEnum.返修中 : ProduceStatusEnum.生产中;
                    }
                    else
                    {
                        proStatu.NormalWorkProcessId = entityDto.WorkProcessId;
                        proStatu.ProduceStatus = ProduceStatusEnum.生产中;
                    }

                    proStatu.CurrentWorkStationId = entityDto.WorkStationId;
                    proStatu.CurrentProductLineId = workStaion.BelongProductLineId.GetValueOrDefault();
                    proStatu.CurrentWorkProcessId = entityDto.WorkProcessId;
                    proStatu.IsCurrentWorkProcessDone = false;
                }


                _workOrderManager.SetMaterilStatu(proStatu);
                UnitOfWorkManager.Current.SaveChanges();// 立即保存数据确保投产量一致
                workOrderInfo.ProdcuingCount = _workOrderManager.GetMaterialProductingProduces(innertBatchNumber.FromOrderNumber);
            }

            // 添加工序操作记录
            var orgId = _userManager.GetOrganizationUnits(new User() { Id = AbpSession.UserId.GetValueOrDefault() });
            ajaxResponse.Data = _workProcessInfoManager.StartWorkProcessOperatorRecord(new WorkProcessOperatorRecord()
            {
                CurrentOperatroAccountId = AbpSession.UserId.GetValueOrDefault(),
                DepartmentId = orgId.FirstOrDefault().Id,
                OpertaorId = entityDto.Users != null && entityDto.Users.Count > 0 ? string.Join(",", entityDto.Users.Select(p => p.Id.ToString())) : "",
                OperatroName = entityDto.Users != null && entityDto.Users.Count > 0 ? string.Join(",", entityDto.Users.Select(p => p.Name.ToString())) : "",
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                WorkProcessId = entityDto.WorkProcessId,
                WorkProcessNumber = workProcessInfo.ProcessNumber,
                WorkProcessName = workProcessInfo.ProcessName,
                OrderNumber = innertBatchNumber == null ? entityDto.WorkOrderNumber : innertBatchNumber.FromOrderNumber,
                WorkStationId = workStaion.Id,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                WorkProcessOperateType = entityDto.OperatroMaterilBatchType,
                BatchNumber = entityDto.OperatroMaterilBatchNumber,
                WorkProcessSetId = workOrderInfo != null ? workOrderInfo.WorkProcessSetId : null,
                StartTime = DateTime.Now,
                IsRepaired = proStatu != null && proStatu.NormalWorkProcessId != entityDto.WorkProcessId,// 这里的值做反了，需要进行校正
                IsLastFqcRepaired = proStatu != null ? proStatu.IsLastFqcRepaired : false,
            });

            return ajaxResponse;
        }

        /// <summary>
        /// 投放物料数据
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse> InputMaterialAndOperatorAsync(InputOperatorRecordInfo inputInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse() { Code = 500 };
            var proStatu = _workOrderManager.GetMaterialProduceStatu(inputInfo.OperatroMaterilBatchNumber);
            if (proStatu != null && (proStatu.ProduceStatus == ProduceStatusEnum.异常 || proStatu.ProduceStatus == ProduceStatusEnum.异常处置))
            {
                ajaxResponse.Msg = $"该产品处于异常中，请联系班组长进行处理！";
                return ajaxResponse;
            }

            foreach (var item in inputInfo.Users)  // 检查用户是否可用
            {
                if (!_workStationManager.IsMangerWorkStation(item.Id, inputInfo.WorkStationId))
                {
                    ajaxResponse.Msg = $"用户【{item.UserName}】不能操作该工序，请移除！";
                    return ajaxResponse;
                }
            }

            // 是否允许工序修改物料数据
            var canModifyMaterailInfo = Boolean.Parse(SettingManager.GetSettingValue(AppSettingNames.CanStandWorkProcessModifyMaterialInfo));
            List<WorkProcessMaterialRecord> inputRecords = null;
            inputRecords = _workProcessInfoManager.LoadWorkProcessMaterilRecord(inputInfo.WorkProcessId, inputInfo.OperatroMaterilBatchNumber);
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            if ((canModifyMaterailInfo || inputRecords == null) && await CheckInputMaterial(inputInfo, ajaxResponse, productBatchNumber) == false)
            {
                return ajaxResponse;
            }

            var startRecordResult = await StartProduce(inputInfo);
            if (startRecordResult.Code != 200)
            {
                return startRecordResult;
            }

            var workStaion = _workStationRep.FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var listInputMateril = new List<WorkProcessMaterialRecord>();



            // 更新当前操作人员
            var processRecord = _workProcessInfoManager.LoadWorkProcessRecord(inputInfo.WorkProcessId, inputInfo.OperatroMaterilBatchNumber, inputInfo.OperatroMaterilBatchType);
            processRecord.OperatroName = String.Join(",", inputInfo.Users.Select(p => p.Name).ToArray());
            processRecord.OpertaorId = String.Join(",", inputInfo.Users.Select(p => p.Id).ToArray());

            if (canModifyMaterailInfo || inputRecords == null || inputRecords.Count() == 0)// 正常投料仅允许初次时添加
            {
                ajaxResponse = await UpdateWorkProcessMaterialInfoAsync(inputInfo);
            }
            else
            {
                ajaxResponse.Code = 200;
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 检查投入物料信息是否满足BOM需求
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <param name="ajaxResponse"></param>
        /// <param name="productBatchNumber"></param>
        /// <param name="updateInputInfos">调整后的数据信息</param>
        /// <returns></returns>
        private async Task<bool> CheckInputMaterial(InputOperatorRecordInfo inputInfo, JHTAjaxResponse ajaxResponse, MaterialBatchNumberDto productBatchNumber, List<MaterialBatchNumberDto> updateInputInfos = null)
        {
            // 检查该工序的物料种类及数量是否正确
            var processShouldInputMaterial = _workOrderBomManager.GetWorkOrderBomItems(inputInfo.WorkProcessId, productBatchNumber.FromOrderNumber);

            // 在制品替换成原料
            var lineSideMaterialInfos = inputInfo.InputMaterialInfos.Where(p => p.IsLineMaterialInfo).ToList();
            List<MaterialBatchNumberDto> origialMaterial = TranseLineMaterialToOrigialMaterial(lineSideMaterialInfos);
            inputInfo.InputMaterialInfos.AddRange(origialMaterial);

            // BOM数量检查需要进行换算
            foreach (var subitem in processShouldInputMaterial)
            {
                var cutMaterialConfig = _materialManager.LoadCutMaterialConfig(productBatchNumber.MaterialId, subitem.InputMaterial.MaterialNumber);
                var rellayInputMaterial = inputInfo.InputMaterialInfos.Where(p => p.MaterialNumber == subitem.InputMaterial.MaterialNumber).ToList();
                if (rellayInputMaterial.Count == 0 && subitem.InputMaterialCount > 0)
                {
                    ajaxResponse.Msg = $"该工序还需投入物料【{subitem.InputMaterial.MaterialNumber}】,请添加！";
                    return false;
                }

                var ignoreUnitName = SettingManager.GetSettingValue(AppSettingNames.IgnoreBomUniteName).Split(',');
                var reallyBomCount = rellayInputMaterial.Sum(p => p.MatrialCount);

                // 判断如果BOM是面积类的单位，就不校验相关数值。
                if (ignoreUnitName.Contains(subitem.InputMaterial.UnitName) && subitem.InputMaterialCount > 0)
                {
                    if (cutMaterialConfig != null)
                    {
                        reallyBomCount = reallyBomCount * cutMaterialConfig.ConversionRatio;
                    }

                    if (reallyBomCount > subitem.InputMaterialCount)// 校验是否多投
                    {
                        ajaxResponse.Msg = $"物料【{subitem.InputMaterial.MaterialNumber}】投入总量大于【{subitem.InputMaterialCount} {subitem.InputMaterial.UnitName}】，请注意物料数量！";
                        return false;
                    }
                }

                if (reallyBomCount < subitem.InputMaterialCount && ignoreUnitName.Contains(subitem.InputMaterial.UnitName) == false)// 校验是否少投
                {
                    ajaxResponse.Msg = $"物料【{subitem.InputMaterial.MaterialNumber}】投入总量少于【{subitem.InputMaterialCount} {subitem.InputMaterial.UnitName}】,请继续添加！";
                    return false;
                }


            }


            var checkBatchNumberCount = updateInputInfos != null ? updateInputInfos : inputInfo.InputMaterialInfos;
            foreach (var item in checkBatchNumberCount)
            {
                var batchNumberInfo = _materialBatchNumberCache.GetByMaterialBatchNumber(item.BatchNumber);
                if (batchNumberInfo == null)
                {
                    var erpBatchNumber = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == item.BatchNumber);
                    if (erpBatchNumber == null)
                    {
                        ajaxResponse.Msg = $"物料【{item.MaterialNumber}】批次号：{item.BatchNumber}，未找到请核对批次相关信息！";
                        return false;
                    }
                }

                // todo 屏蔽物料使用,不检查库存物料
                if (!processShouldInputMaterial.Any(p => p.InputMaterialId == item.MaterialId) && item.IsLineMaterialInfo == false)
                {
                    ajaxResponse.Msg = $"物料【{item.MaterialNumber}】不能被用于该工序，请移除！";
                    return false;
                }

                var canReusedBatchNumber = bool.Parse(SettingManager.GetSettingValue(AppSettingNames.CanReusedBatchNumber));// 物料批次号能否被使用
                // 需要生产的每批次数量与标准的批次量一致的情况下启用
                if (!canReusedBatchNumber && _workProcessInfoManager.CanMaterialBatchNumberBeUse(item.BatchNumber, out string message, item.BOMMaterialCount.GetValueOrDefault(), inputInfo.WorkProcessId) == false)
                {
                    ajaxResponse.Msg = message;
                    return false;
                }

                // 检查工单物料是否超用
                await _workOrderAppService.IsCutMaterialEnough(productBatchNumber.FromOrderNumber, inputInfo.WorkProcessId, item.MaterialNumber, inputInfo.IsRepiredInput ? false : true);
            }

            foreach (var item in origialMaterial)
            {
                inputInfo.InputMaterialInfos.Remove(item);
            }

            return true;
        }

        /// <summary>
        /// 按在制品BOM信息将物料还原成原材料
        /// </summary>
        /// <param name="lineSideMaterialInfos"></param>
        /// <returns></returns>
        private List<MaterialBatchNumberDto> TranseLineMaterialToOrigialMaterial(List<MaterialBatchNumberDto> lineSideMaterialInfos)
        {
            List<MaterialBatchNumberDto> origialMaterial = new List<MaterialBatchNumberDto>();
            foreach (var item in lineSideMaterialInfos)
            {
                var orginalInfo = _processMaterialRecordRep.GetAll().Where(p => p.ProductBatchNumber == item.BatchNumber);
                var bomItems = _lineSideMaterialInfoBomItemRep.GetAllIncluding(p => p.LineSideMaterialInfo).Where(p => p.LineSideMaterialInfo.MaterialNumber == item.MaterialNumber).ToList();
                foreach (var orgItem in orginalInfo)
                {
                    // 解构成一个在制品可使用多少数据
                    var lineBom = bomItems.FirstOrDefault(p => orgItem.InputMaterialNumber.StartsWith(p.FormMaterialCategoryNumber));
                    var computeInputCount = lineBom.FormMaterialAmount * item.MatrialCount;
                    var materilCount = computeInputCount > orgItem.InputMaterialCount ? orgItem.InputMaterialCount : computeInputCount;
                    origialMaterial.Add(new MaterialBatchNumberDto()
                    {
                        MaterialId = orgItem.InputMaterilId,
                        MaterialNumber = orgItem.InputMaterialNumber,
                        BatchNumber = orgItem.BatchNo,
                        MatrialCount = materilCount
                    });
                }
            }

            return origialMaterial;
        }

        /// <summary>
        /// 加载当前工序的操作信息
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse<WorkProcessOperatorRecordDto> LoadCurrentWorkProcessOperatorInfo(InputOperatorRecordInfo inputInfo)
        {
            JHTAjaxResponse<WorkProcessOperatorRecordDto> ajaxResponse = new JHTAjaxResponse<WorkProcessOperatorRecordDto>();
            ajaxResponse.Data = ObjectMapper.Map<WorkProcessOperatorRecordDto>(_workProcessInfoManager.LoadWorkProcessRecord(inputInfo.WorkProcessId, inputInfo.OperatroMaterilBatchNumber, inputInfo.OperatroMaterilBatchType));
            if (ajaxResponse.Data != null)
            {
                var materilInfo = _workProcessInfoManager.LoadWorkProcessMaterilRecord(inputInfo.WorkProcessId, inputInfo.OperatroMaterilBatchNumber);
                ajaxResponse.Data.InputMaterilaInfo = ObjectMapper.Map<List<MaterialBatchNumberDto>>(materilInfo);
                var shouldInputMaterial = _workOrderBomManager.GetWorkOrderBomItems(inputInfo.WorkProcessId, inputInfo.WorkOrderNumber);
                ajaxResponse.Data.ShouldInputMaterial = ObjectMapper.Map<List<BomItemDto>>(shouldInputMaterial);
                ajaxResponse.Data.CanModifyMaterial = materilInfo.Count == 0 || Boolean.Parse(SettingManager.GetSettingValue(AppSettingNames.CanStandWorkProcessModifyMaterialInfo));
            }
            return ajaxResponse;
        }

        /// <summary>
        /// 保存表单草稿
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse<EntityDto<long>> SaveFormDraft(CompleteWorkProcessRecordDto inputInfo, bool isDraft = true)
        {
            JHTAjaxResponse<EntityDto<long>> ajaxResponse = new JHTAjaxResponse<EntityDto<long>>();
            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            if (inputInfo.FormTemlpateId > 0)
            {
                var operatro = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());
                FormInfoRecord formInfoRecord = BuildFormInfoRecord(inputInfo, workStaion, workProcess, productBatchNumber, operatro);
                formInfoRecord.IsDraft = isDraft;
                var formInfoRecordSave = _formTemplateInfoManager.AddFromInfoRecord(formInfoRecord);
                ajaxResponse.Data = new EntityDto<long>() { Id = formInfoRecordSave.Id };
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 完成工序
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public JHTAjaxResponse NormalCompleteCurrentWorkProcess(CompleteWorkProcessRecordDto inputInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var proStatu = _workOrderManager.GetMaterialProduceStatu(inputInfo.OperatroMaterilBatchNumber);
            if (proStatu != null && proStatu.ProduceStatus == ProduceStatusEnum.异常 || proStatu.ProduceStatus == ProduceStatusEnum.异常处置)
            {
                ajaxResponse.Msg = $"该产品处于异常中，请联系班助长进行处理！";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            if (inputInfo.IsNormalFinish)
            {
                proStatu.IsCurrentWorkProcessDone = true;
                // 0 判断当前产品是否完成最后一步工序
                var workWorder = _workOrderManager.GetWorkOrderByOrderNumber(productBatchNumber.FromOrderNumber);
                var processInfo = _workProcessSetCache.Get(workWorder.WorkProcessSetId.GetValueOrDefault());
                var leftWorkProcessCount = processInfo.ComputeLeftProcessCount(inputInfo.WorkProcessId);
                if (leftWorkProcessCount == 0)
                {
                    proStatu.ProduceStatus = ProduceStatusEnum.已完成;
                    proStatu.EndTime = DateTime.Now;
                    proStatu.TestCounts = proStatu.TestCounts + 1;
                    proStatu.PassCounts = proStatu.PassCounts + 1;
                    workWorder.ProdcuingCount = _workOrderManager.GetMaterialProductingProduces(productBatchNumber.FromOrderNumber);
                    workWorder.FinishedCount = _workOrderManager.GetMaterialFinishedProduces(productBatchNumber.FromOrderNumber) + 1;
                }
                // 1、关闭当前工序操作
                WorkProcessOperatorRecord operatorRecord = WorkProcessOperatorRecord.BuildEndWorkProcessRecord(AbpSession.UserId.GetValueOrDefault(), inputInfo.IsNormalFinish, inputInfo.OperatroMaterilBatchNumber, workStaion, workProcess);
                _workProcessInfoManager.EndWorkProcessOperatorRecord(operatorRecord);
            }

            // 2、记录填报数据
            if (inputInfo.FormTemlpateId > 0)
            {
                var operatro = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());
                FormInfoRecord formInfoRecord = BuildFormInfoRecord(inputInfo, workStaion, workProcess, productBatchNumber, operatro);

                formInfoRecord.Id = inputInfo.FormRecordInfoId;
                formInfoRecord.IsDraft = false;
                _formTemplateInfoManager.AddFromInfoRecord(formInfoRecord);

            }

            return ajaxResponse;
        }

        private FormInfoRecord BuildFormInfoRecord(
            CompleteWorkProcessRecordDto inputInfo,
            WorkStationInfo workStation,
            WorkProcessInfo workProcess,
            MaterialBatchNumberDto productBatchNumber,
            User operatro)
        {
            return new FormInfoRecord()
            {
                Id = inputInfo.FormRecordInfoId,
                BelongFormId = inputInfo.FormTemlpateId,
                BelongMaterialBatchNumber = inputInfo.OperatroMaterilBatchNumber,
                BelongOrderNumber = productBatchNumber.FromOrderNumber,
                BelongWorkProcessId = workProcess.Id,
                BelongWorkProcessNumber = workProcess.ProcessNumber,
                WorkProcessName = workProcess.ProcessName,
                FormRecordData = inputInfo.FormRecordInfo,
                MatreialName = productBatchNumber.MaterialName,
                BelongProductLineId = workStation.BelongProductLineId.GetValueOrDefault(),
                BelongProductLineName = workStation.BelongProductLineId > 0 ? workStation.BelongProductLine.ProductLineName : "",
                MaterialId = productBatchNumber.MaterialId,
                MaterialNumber = productBatchNumber.MaterialNumber,
                OperatorTime = DateTime.Now,
                FormUseType = FormUseTypeEnum.标准工序填报,
                Operator = operatro.Name,
                OperatorUserId = AbpSession.UserId.GetValueOrDefault(),
            };
        }


        /// <summary>
        /// 保存异常图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<string> SaveExceptionImgs(IFormFile file)
        {
            return await this.SaveImgs("Exceptions", file);
        }

        /// <summary>
        /// 保存表单图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<string> SaveDymaicFormImgs(IFormFile file)
        {
            return await this.SaveImgs("DymaicForms", file);
        }


        private async Task<string> SaveImgs(string pathCategory, IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileRenameName = Guid.NewGuid().ToString(); //Path.GetRandomFileName();

            var path = string.Format("/{0}/{1}/{2}/{3}", pathCategory, AbpSession.TenantId.GetValueOrDefault(), DateTime.Now.ToString(_fileSaveOptions.SaveStragety), fileRenameName + fileExtension);
            List<string> errors = new List<string>();
            var stemaContent = await FileHelpers.ProcessFormFile(file, errors, _fileSaveOptions.AllowedExtensions, _fileSaveOptions.AllowedFileSzie);
            if (errors.Count > 0)
            {
                throw new UserFriendlyException(string.Join(",", errors));
            }
            else
            {
                var savaPath = _fileSaveOptions.DeafaultSavePath + path;
                var dic = Path.GetDirectoryName(savaPath);
                if (!Directory.Exists(dic))
                {
                    Directory.CreateDirectory(dic);
                }

                using var stream = File.Create(savaPath);
                await stream.WriteAsync(stemaContent);
            }

            var imagepath = string.Format("{0}{1}", _fileSaveOptions.DeafaultSaveDomain, path);
            return imagepath;
        }

        /// <summary>
        /// 上报普通的问题记录信息
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <param name="problemDealRecord"></param>
        /// <returns></returns> 
        [AbpAuthorize(PermissionNames.Page_QualityManager_QC)]
        public JHTAjaxResponse ReportCommonProblem(ProblemRecordDto inputInfo, ProblemDealRecordDto problemDealRecord)
        {
            var user = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();

            MaterialBatchNumber productBatchNumber = _materialBatchRep.FirstOrDefault(p => p.BatchNumber == inputInfo.BatchMaterilaNumber);
            var inStockInfo = _erpInStockRepository.FirstOrDefault(p => p.MaterialNumber == inputInfo.MaterialNumber && p.BatchNo == inputInfo.BatchMaterilaNumber);

            if (!string.IsNullOrEmpty(inputInfo.BatchMaterilaNumber))
            {
                // 已经是封存或全部报废的物料无需进行处理
                if ((productBatchNumber != null && (productBatchNumber.MaterialStatu == MaterialStatuEnum.封存 || productBatchNumber.MaterialStatu == MaterialStatuEnum.全部报废))
                    || (inStockInfo != null && (inStockInfo.MaterialStatu == MaterialStatuEnum.封存 || inStockInfo.MaterialStatu == MaterialStatuEnum.全部报废)))
                {
                    var currentStatu = productBatchNumber != null ? productBatchNumber.MaterialStatu : inStockInfo.MaterialStatu;
                    ajaxResponse.Msg = $"该批次物料当前状态为{currentStatu},无需再进行操作处理！";
                    return ajaxResponse;
                }
            }

            if ((productBatchNumber == null || productBatchNumber.IsLineMaterialInfo == false) && !string.IsNullOrEmpty(inputInfo.MaterialNumber))
            {
                // 检查物料与工单是否匹配
                var pickMaterialInfoCount = _k3ErpRepostiory.GetWorkOrderPickingMaterilInfo(inputInfo.WorkOrderNumber, inputInfo.MaterialNumber);
                if (pickMaterialInfoCount == null || pickMaterialInfoCount.PickingCount < inputInfo.ProblemCount)
                {
                    ajaxResponse.Msg = "未在该工单领取对应物料，不能在该工单中进行报废处理";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }
            }

            if (productBatchNumber != null && productBatchNumber.IsLineMaterialInfo && productBatchNumber.FromOrderNumber != inputInfo.WorkOrderNumber)
            {
                // 在制品加工物料数据
                ajaxResponse.Msg = $"该批次物料未在工单{inputInfo.WorkOrderNumber}中进行加工";
                return ajaxResponse;
            }

            // 普通问题，不走分析审批路径
            inputInfo.IsClosed = true; inputInfo.IsEffect = true;
            inputInfo.AuditorId = user.Id; inputInfo.AuditorName = user.Name;
            inputInfo.AuditTime = DateTime.Now; inputInfo.ResponsibleWorkProcessId = inputInfo.BelongWorkProcessId;
            inputInfo.ResponsibleDepartmentId = inputInfo.ResponsibleDepartmentId;

            if (inputInfo.ResponsibleDepartmentId > 0)
            {
                var org = _organzationManager.FindById(inputInfo.ResponsibleDepartmentId.GetValueOrDefault());
                inputInfo.ResponsibleDepartmentName = org != null ? org.DisplayName : inputInfo.ResponsibleDepartmentName;
            }

            ajaxResponse = this.ReportProblem(inputInfo, false);
            if (ajaxResponse.Code == 200)
            {
                problemDealRecord.DealTime = DateTime.Now;
                problemDealRecord.OperatorDescreption = problemDealRecord.ProblemDealType.ToString();
                problemDealRecord.OperatorId = user.Id;
                problemDealRecord.OperatorName = user.Name;
                problemDealRecord.ProblemRecordId = inputInfo.Id; // 根据处理结果，进行物料报废判断，判断对应批次号物料是否超量报废
                this.SaveProblemDealRecord(problemDealRecord);
                if (problemDealRecord.ProblemDealType == ProblemDealTypeEnum.全部报废 || problemDealRecord.ProblemDealType == ProblemDealTypeEnum.部分报废 || problemDealRecord.ProblemDealType == ProblemDealTypeEnum.封存)
                {
                    var discardRecord = new MaterialDiscardRecord()
                    {
                        BatchNumber = inputInfo.BatchMaterilaNumber,
                        WorkOrderNumber = inputInfo.WorkOrderNumber,
                        DeiscardReasonDescreption = "",
                        DiccardCount = inputInfo.ProblemCount.GetValueOrDefault(),

                        UnitName = inputInfo.UnitName,
                        WrapUnitName = inputInfo.WrapUnitName,
                        DiccardWarpCount = inputInfo.ProblemWarpCount.GetValueOrDefault(),
                        DiscardType = inputInfo.DiscardType.GetValueOrDefault(),
                        MaterialName = inputInfo.MaterialName,
                        MaterialNumber = inputInfo.MaterialNumber,
                        RecordDate = DateTime.Now,
                        ProblemDefineId = inputInfo.BelongProblemDefineId,
                        ProblemRecordId = inputInfo.Id,
                        RecordUserId = AbpSession.UserId.GetValueOrDefault(),
                        RecordUserName = user.Name,
                        Supplier = inputInfo.Supplier,
                        ErpBatchNumber = productBatchNumber != null ? productBatchNumber.FromErpBatchNumber : "",
                        ProblemDefineNumber = inputInfo.QualityProblemDefineNumber
                    };

                    // 如果为全部报废，封存的原材料批次号，则设置相关原材料的状态,插入相关操作记录
                    if (productBatchNumber != null && problemDealRecord.ProblemDealType != ProblemDealTypeEnum.部分报废)//
                    {
                        productBatchNumber.MaterialStatu = problemDealRecord.ProblemDealType == ProblemDealTypeEnum.封存 ? MaterialStatuEnum.封存 : MaterialStatuEnum.全部报废;
                        if (problemDealRecord.ProblemDealType == ProblemDealTypeEnum.全部报废)
                        {
                            discardRecord.DiccardCount = productBatchNumber.MatrialCount == 0 ? productBatchNumber.MatrialCount : productBatchNumber.BOMMaterialCount;
                            discardRecord.UnitName = productBatchNumber.MatrialCount == 0 ? productBatchNumber.WrapUniteName : productBatchNumber.BOMMaterialUnitName;
                            discardRecord.DiccardWarpCount = productBatchNumber.MatrialCount;
                            discardRecord.WrapUnitName = productBatchNumber.WrapUniteName;
                        }
                    }

                    if (inStockInfo != null && problemDealRecord.ProblemDealType != ProblemDealTypeEnum.部分报废)
                    {
                        inStockInfo.MaterialStatu = problemDealRecord.ProblemDealType == ProblemDealTypeEnum.封存 ? MaterialStatuEnum.封存 : MaterialStatuEnum.全部报废;
                        if (inStockInfo != null && problemDealRecord.ProblemDealType == ProblemDealTypeEnum.全部报废)
                        {
                            discardRecord.DiccardCount = inStockInfo.ReceiptQuantity;
                            discardRecord.UnitName = inStockInfo.UnitName;
                        }
                    }

                    if (problemDealRecord.ProblemDealType != ProblemDealTypeEnum.封存)
                    {
                        // 添加物料报废记录
                        _materialDiscardRecordRep.Insert(discardRecord);
                    }

                    // 操作记录
                    _erpInstockInfoRecord.Insert(new ERPInStockInfoOperateRecord()
                    {
                        BatchNo = inputInfo.BatchMaterilaNumber,
                        MaterialNumber = inputInfo.MaterialNumber,
                        MaterialName = inputInfo.MaterialName,
                        OperatorId = AbpSession.UserId.GetValueOrDefault(),
                        OperateTime = DateTime.Now,
                        OperateDesp = problemDealRecord.ProblemDealTypeEnumString,
                        Operator = user.Name
                    });
                }
            }


            return ajaxResponse;

        }

        /// <summary>
        /// 上报异常
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse ReportProblem(ProblemRecordDto inputInfo, bool needChangeProudctStatu = true)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            if (!string.IsNullOrEmpty(inputInfo.WorkOrderNumber))
            {
                var workOrderNumber = _workOrderAppService.GetWorkOrderInfoByOrderNumber(inputInfo.WorkOrderNumber);
                if (workOrderNumber == null)
                {
                    ajaxResponse.Msg = "该工单不存在，请检查";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }
            }

            var user = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            inputInfo.Createor = user.Name;
            var workStaion = _workStationRep.FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.BelongWorkProcessId);
            var productBatchNumber = _materialBatchRep.FirstOrDefault(p => p.BatchNumber == inputInfo.BatchMaterilaNumber);

            var orderMaterilStatu = _viewOrderMaterialProduceStatusesRepository.FirstOrDefault(p => p.MaterialBatchNumber == inputInfo.BatchMaterilaNumber);
            inputInfo.OnWorkProcessNumber = workProcess == null ? "" : workProcess.ProcessNumber;
            inputInfo.WorkProcessName = workProcess == null ? "" : workProcess.ProcessName;
            inputInfo.BelongWorkStaionId = workStaion == null ? null : workStaion.Id;
            inputInfo.BelongProductLineId = workStaion == null ? null : workStaion.BelongProductLineId.GetValueOrDefault();


            if (productBatchNumber != null && string.IsNullOrEmpty(inputInfo.MaterialNumber))
            {
                inputInfo.MaterialName = productBatchNumber.MaterialName;
                inputInfo.MaterialNumber = productBatchNumber.MaterialNumber;
                inputInfo.UnitName = !string.IsNullOrEmpty(productBatchNumber.BOMMaterialUnitName) ? productBatchNumber.BOMMaterialUnitName : productBatchNumber.WrapUniteName;
            }

            var problem = ObjectMapper.Map<ProblemRecord>(inputInfo);
            problem.SetImgs(inputInfo.RelationImgs);
            if (orderMaterilStatu != null && needChangeProudctStatu)
            {
                // 产品异常上报处理
                var opertatorRecord = WorkProcessOperatorRecord.BuildEndWorkProcessRecord(AbpSession.UserId.GetValueOrDefault(), false, inputInfo.BatchMaterilaNumber, workStaion, workProcess);
                _workProcessInfoManager.EndWorkProcessOperatorRecord(opertatorRecord);
                _workOrderManager.SetMaterilStatu(new OrderMaterialProduceStatu()
                {
                    CurrentWorkProcessId = inputInfo.BelongWorkProcessId.GetValueOrDefault(),
                    MaterialBatchNumber = inputInfo.BatchMaterilaNumber,
                    MaterialInfoId = productBatchNumber.MaterialId,
                    WorkOrderNumber = productBatchNumber.FromOrderNumber,
                    ProduceStatus = ProduceStatusEnum.异常,
                    StartTime = DateTime.Now,
                });

                problem.MaterialName = orderMaterilStatu.MaterialName;
                problem.MaterialNumber = orderMaterilStatu.MaterialNumber;

            }

            inputInfo.Id = _workProcessInfoManager.SaveProblemRecord(problem);

            return ajaxResponse;
        }

        /// <summary>
        /// 获取已完工工序
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse<List<WorkProcessInfoDto>> LoadFinishWorkPorcess(InputOperatorRecordInfo inputInfo)
        {
            JHTAjaxResponse<List<WorkProcessInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessInfoDto>>();
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            var workOrderInfo = _workOrderManager.GetWorkOrderByOrderNumber(productBatchNumber.FromOrderNumber);
            var workProcessSet = _workProcessSetCache.Get(workOrderInfo.WorkProcessSetId.GetValueOrDefault());

            var finishWorkProcessIds = workProcessSet.GetFinishedWorkProcess(inputInfo.WorkProcessId);// 计算剩余的工序数量
            if (finishWorkProcessIds.Count == 0)
            {
                var orderStatus = _workOrderManager.GetMaterialProduceStatu(inputInfo.OperatroMaterilBatchNumber);
                finishWorkProcessIds = workProcessSet.GetFinishedWorkProcess(orderStatus.CurrentWorkProcessId);
            }

            ajaxResponse.Data = ObjectMapper.Map<List<WorkProcessInfoDto>>(Repository.GetAll().Where(p => finishWorkProcessIds.Contains(p.Id)).ToList());
            return ajaxResponse;
        }

        /// <summary>
        /// 开始异常处置
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse> StartExceptionDealAsync(InputOperatorRecordInfo entityDto)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var innertBatchNumber = await _materialBatchNumberCache.GetByMaterialBatchNumberAsync(entityDto.OperatroMaterilBatchNumber);
            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == entityDto.WorkStationId);
            var workProcessInfo = await this.Repository.FirstOrDefaultAsync(p => p.Id == entityDto.WorkProcessId);
            var operatorUser = this._userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());
            var operaterDep = this._userManager.GetOrganizationUnits(operatorUser);
            _workProcessInfoManager.StartWorkProcessOperatorRecord(new WorkProcessOperatorRecord()
            {
                CurrentOperatroAccountId = AbpSession.UserId.GetValueOrDefault(),
                OpertaorId = AbpSession.UserId.ToString(),
                OperatroName = operatorUser.Name,
                DepartmentId = operaterDep.FirstOrDefault().Id,
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                WorkProcessId = entityDto.WorkProcessId,
                WorkProcessNumber = workProcessInfo.ProcessNumber,
                WorkProcessName = workProcessInfo.ProcessName,
                OrderNumber = innertBatchNumber == null ? "" : innertBatchNumber.FromOrderNumber,
                WorkStationId = workStaion.Id,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                WorkProcessOperateType = entityDto.OperatroMaterilBatchType,
                BatchNumber = entityDto.OperatroMaterilBatchNumber,
                StartTime = DateTime.Now
            });

            return ajaxResponse;
        }


        /// <summary>
        /// 更新判断信息
        /// </summary>
        /// <param name="problemRecord"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Page_QualityManager_ProblemJudge)]
        public JHTAjaxResponse UpdateProblemJudgeInfo(ProblemRecordDto problemRecord)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            // 更新问题定义
            var problemInfo = _problemRecordRep.FirstOrDefault(p => p.Id == problemRecord.Id);
            if (problemInfo == null || problemInfo.IsClosed)
            {
                ajaxResponse.Msg = "问题不存在或问题已关闭";
                return ajaxResponse;
            }

            // 调整问题信息
            problemInfo.QualityProblemDefineNumber = problemRecord.QualityProblemDefineNumber;
            problemInfo.ResponsibleDepartmentId = problemRecord.ResponsibleDepartmentId;
            if (problemRecord.ResponsibleDepartmentId > 0)
            {
                var org = _organzationManager.FindById(problemRecord.ResponsibleDepartmentId.GetValueOrDefault());
                problemRecord.ResponsibleDepartmentName = org != null ? org.DisplayName : problemRecord.ResponsibleDepartmentName;
            }

            problemInfo.BelongProblemDefineId = problemRecord.BelongProblemDefineId;
            problemInfo.DetailDescretion = problemRecord.DetailDescretion;
            problemInfo.ResponsibleWorkProcessId = problemRecord.ResponsibleWorkProcessId;
            var operatorInfo = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            problemInfo.AuditorId = operatorInfo.Id;
            problemInfo.AuditorName = operatorInfo.Name;
            problemInfo.AuditTime = DateTime.Now;
            problemInfo.ReasonAnlysis = problemRecord.ReasonAnlysis;

            this.UnitOfWorkManager.Current.SaveChanges();
            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }


        [AbpAuthorize(PermissionNames.Page_QualityManager_ProblemDeal)]
        public JHTAjaxResponse SaveProblemDealRecord(ProblemDealRecordDto problemDealRecord)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            //
            var dataProblemDealRecord = _problemDealRecordRep.FirstOrDefault(p => p.ProblemRecordId == problemDealRecord.ProblemRecordId);
            // 更新问题定义
            var problemInfo = _problemRecordRep.FirstOrDefault(p => p.Id == problemDealRecord.ProblemRecordId);
            if (problemInfo == null || dataProblemDealRecord != null)
            {
                ajaxResponse.Msg = "问题不存在或问题已关闭";
                return ajaxResponse;
            }

            if (problemDealRecord.ProblemDealType == 0)
            {
                problemInfo.IsEffect = false;
            }
            else
            {
                problemInfo.IsEffect = true;
            }

            var operatorInfo = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            // 增加异常处理情况记录
            if (problemDealRecord.Id == 0 && dataProblemDealRecord == null)
            {
                _problemDealRecordRep.Insert(new ProblemDealRecord()
                {
                    DealTime = DateTime.Now,
                    OperatorDescreption = problemDealRecord.OperatorDescreption,
                    OperatorId = AbpSession.UserId.GetValueOrDefault(),
                    ProblemDealType = problemDealRecord.ProblemDealType,
                    OperatorName = operatorInfo.Name,
                    ProblemRecordId = problemDealRecord.ProblemRecordId,
                });
            }

            // 更新已有的异常情况处理
            if (problemDealRecord.Id > 0 && problemInfo.IsClosed == false)
            {
                var dealRecordInfo = _problemDealRecordRep.FirstOrDefault(p => p.Id == problemDealRecord.Id);
                if (dealRecordInfo != null)
                {
                    dealRecordInfo.DealTime = DateTime.Now;
                    dealRecordInfo.OperatorDescreption = problemDealRecord.OperatorDescreption;
                    dealRecordInfo.OperatorId = AbpSession.UserId.GetValueOrDefault();
                    dealRecordInfo.OperatorName = operatorInfo.Name;
                    dealRecordInfo.ProblemDealType = problemDealRecord.ProblemDealType;
                    dealRecordInfo.ProblemRecordId = problemDealRecord.ProblemRecordId;
                }
            }

            ajaxResponse.Msg = "保存成功";
            return ajaxResponse;
        }


        public JHTAjaxResponse SaveProblemDealRecord(ProblemDealRecordDto problemDealRecord, ProblemRecordDto problemRecord)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            // 更新问题定义
            var problemInfo = _problemRecordRep.FirstOrDefault(p => p.Id == problemRecord.Id);
            if (problemInfo == null || problemInfo.IsClosed)
            {
                ajaxResponse.Msg = "问题不存在或问题已关闭";
                return ajaxResponse;
            }

            // 调整问题信息
            problemInfo.QualityProblemDefineNumber = problemRecord.QualityProblemDefineNumber;
            problemInfo.BelongProblemDefineId = problemRecord.BelongProblemDefineId;
            problemInfo.DetailDescretion = problemRecord.DetailDescretion;
            problemInfo.ResponsibleWorkProcessId = problemRecord.ResponsibleWorkProcessId;
            problemInfo.SetImgs(problemRecord.RelationImgs);

            if (problemDealRecord.ProblemDealType == 0)
            {
                problemInfo.IsEffect = false;
            }
            else
            {
                problemInfo.IsEffect = true;
            }

            var operatorInfo = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            if (problemDealRecord.Id == 0 && problemInfo.IsClosed == false)
            {
                // 增加异常处理情况记录
                _problemDealRecordRep.Insert(new ProblemDealRecord()
                {
                    DealTime = DateTime.Now,
                    OperatorDescreption = problemDealRecord.OperatorDescreption,
                    OperatorId = AbpSession.UserId.GetValueOrDefault(),
                    ProblemDealType = problemDealRecord.ProblemDealType,
                    OperatorName = operatorInfo.Name,
                    ProblemRecordId = problemRecord.Id,
                });
            }

            // 更新已有的异常情况处理
            if (problemDealRecord.Id > 0 && problemInfo.IsClosed == false)
            {
                var dealRecordInfo = _problemDealRecordRep.FirstOrDefault(p => p.Id == problemDealRecord.Id);
                if (dealRecordInfo != null)
                {
                    dealRecordInfo.DealTime = DateTime.Now;
                    dealRecordInfo.OperatorDescreption = problemDealRecord.OperatorDescreption;
                    dealRecordInfo.OperatorId = AbpSession.UserId.GetValueOrDefault();
                    dealRecordInfo.OperatorName = operatorInfo.Name;
                    dealRecordInfo.ProblemRecordId = problemDealRecord.ProblemRecordId;
                }
            }

            ajaxResponse.Msg = "操作成功";
            return ajaxResponse;
        }

        /// <summary>
        /// 完成产品异常处理
        /// </summary>
        /// <param name="problemDealRecord"></param>
        /// <param name="inputOperatorRecordInfo"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Page_QualityManager_ProblemDeal)]
        public JHTAjaxResponse FinishExceptionDeal(ProblemDealRecordDto problemDealRecord, InputOperatorRecordInfo inputOperatorRecordInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();

            var problemInfo = _problemRecordRep.GetAll().Where(p => p.BatchMaterilaNumber == inputOperatorRecordInfo.OperatroMaterilBatchNumber && p.IsClosed == false).ToList();
            var problemRecordId = problemInfo.Select(p => p.Id).ToList();
            foreach (var item in problemInfo)
            {
                item.IsClosed = true;
                item.CloseTime = DateTime.Now;
                item.IsEffect = true;
            }

            var operatorInfo = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId);
            // 关闭异常处理操作
            _workProcessInfoManager.EndWorkProcessOperatorRecord(new WorkProcessOperatorRecord()
            {
                BatchNumber = inputOperatorRecordInfo.OperatroMaterilBatchNumber,
                CurrentOperatroAccountId = AbpSession.UserId.GetValueOrDefault(),
                OperatorDescreption = $"处理方式:{problemDealRecord.ProblemDealType}",
                OrderNumber = problemInfo[0].WorkOrderNumber,
                WorkProcessId = inputOperatorRecordInfo.WorkProcessId,
                WorkProcessOperateType = WorkProcessOperateTypeEnum.异常处置,
                WorkStationId = inputOperatorRecordInfo.WorkStationId,
                IsNormalFinish = true,
            });

            var problemDelRecordData = _problemDealRecordRep.GetAll().Where(p => problemRecordId.Contains(p.ProblemRecordId));
            foreach (var item in problemDelRecordData)
            {
                item.ProblemDealType = problemDealRecord.ProblemDealType;
            }

            var batchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(problemInfo[0].BatchMaterilaNumber);
            // 将订单状态更新为正常
            var orderMaterilStatu = new OrderMaterialProduceStatu()
            {
                CurrentWorkProcessId = problemInfo[0].BelongWorkProcessId,
                MaterialBatchNumber = problemInfo[0].BatchMaterilaNumber,
                MaterialInfoId = batchNumber.MaterialId,
                WorkOrderNumber = batchNumber.FromOrderNumber,
                ProduceStatus = ProduceStatusEnum.生产中,
                StartTime = DateTime.Now,
            };

            var lastRecord = _workProcessInfoManager.LoadWorkProcessRecord(problemInfo[0].BelongWorkProcessId, problemInfo[0].BatchMaterilaNumber, WorkProcessOperateTypeEnum.开始生产);
            if (problemDealRecord.ProblemDealType == ProblemDealTypeEnum.让步接收 || problemDealRecord.ProblemDealType == ProblemDealTypeEnum.正常接收 || problemDealRecord.ProblemDealType == ProblemDealTypeEnum.复测)
            {
                orderMaterilStatu.IsCurrentWorkProcessDone = false;
                _workOrderManager.SetMaterilStatu(orderMaterilStatu);
                lastRecord.EndTime = null;
                CurrentUnitOfWork.SaveChanges();
            }

            if (problemDealRecord.ProblemDealType == ProblemDealTypeEnum.返修)
            {
                orderMaterilStatu.HaveRepaired = true;
                orderMaterilStatu.ProduceStatus = ProduceStatusEnum.返修中;
                orderMaterilStatu.NormalWorkProcessId = problemInfo[0].BelongWorkProcessId;// 正常工序位置
                orderMaterilStatu.IsCurrentWorkProcessDone = false;
                var workOrderInfo = _workOrderManager.GetWorkOrderByOrderNumber(batchNumber.FromOrderNumber);
                var workProcessSet = _workProcessSetCache.Get(workOrderInfo.WorkProcessSetId.GetValueOrDefault());

                // 在最后后的测试工位
                if (orderMaterilStatu.LeftWorkProcessCount == 0)
                {
                    orderMaterilStatu.TestCounts = orderMaterilStatu.TestCounts + 1;
                    orderMaterilStatu.FailCounts = orderMaterilStatu.FailCounts + 1;
                    orderMaterilStatu.IsLastFqcRepaired = true;
                }

                orderMaterilStatu.LeftWorkProcessCount = workProcessSet.ComputeLeftProcessCount(problemDealRecord.StartWorkProcessId.GetValueOrDefault());

                orderMaterilStatu.CurrentWorkProcessId = problemDealRecord.StartWorkProcessId.GetValueOrDefault();
                _workOrderManager.SetMaterilStatu(orderMaterilStatu);
            }

            return ajaxResponse;
        }

        public List<WorkProcessInfoDto> LoadProductSortedWorkProcess(string productMaterialBatchNumber)
        {
            var batchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(productMaterialBatchNumber);
            var orderInfo = _workOrderManager.GetWorkOrderByOrderNumber(batchNumber.FromOrderNumber);
            var processSet = _workProcessSetCache.Get(orderInfo.WorkProcessSetId.GetValueOrDefault());
            var workProcessIds = processSet.WorkProcessSetDetails.Select(p => p.BelongWorkProcessInfoId);

            return ObjectMapper.Map<List<WorkProcessInfoDto>>(Repository.GetAll().Where(p => workProcessIds.Contains(p.Id)));
        }

        /// <summary>
        /// 完成IPQC工序填报
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse CompleteIPQCWorkProcess(CompleteWorkProcessRecordDto inputInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var proStatu = _workOrderManager.GetMaterialProduceStatu(inputInfo.OperatroMaterilBatchNumber);
            if (proStatu != null && proStatu.ProduceStatus == ProduceStatusEnum.异常 || proStatu.ProduceStatus == ProduceStatusEnum.异常处置)
            {
                ajaxResponse.Msg = $"该产品处于异常中，请联系班助长进行处理！";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            var workStaion = _workStationRep.GetAllIncluding(p => p.BelongProductLine).FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);

            var operatro = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());

            // 2、记录填报数据
            if (inputInfo.FormTemlpateId > 0)
            {
                _formTemplateInfoManager.AddFromInfoRecord(new FormInfoRecord()
                {
                    BelongFormId = inputInfo.FormTemlpateId,
                    BelongMaterialBatchNumber = inputInfo.OperatroMaterilBatchNumber,
                    BelongOrderNumber = productBatchNumber.FromOrderNumber,
                    BelongWorkProcessId = workProcess.Id,
                    BelongProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                    BelongProductLineName = workStaion.BelongProductLine?.ProductLineName,
                    WorkProcessName = workProcess.ProcessName,
                    BelongWorkProcessNumber = workProcess.ProcessNumber,
                    FormRecordData = inputInfo.FormRecordInfo,
                    MaterialId = productBatchNumber.MaterialId,
                    MatreialName = productBatchNumber.MaterialName,
                    MaterialNumber = productBatchNumber.MaterialNumber,
                    OperatorTime = DateTime.Now,
                    FormUseType = FormUseTypeEnum.巡检填报,
                    Operator = operatro.Name,
                    OperatorUserId = AbpSession.UserId.GetValueOrDefault(),
                });
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 获取当前产品已完成的工序
        /// </summary>
        /// <param name="inputOperatorRecordInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse<WorkProcessInfoDto> LoadProductCurrentWorkProcess(InputOperatorRecordInfo inputOperatorRecordInfo)
        {
            JHTAjaxResponse<WorkProcessInfoDto> ajaxResponse = new JHTAjaxResponse<WorkProcessInfoDto>();
            var matrialStatu = _workOrderManager.GetMaterialProduceStatu(inputOperatorRecordInfo.OperatroMaterilBatchNumber);
            if (matrialStatu != null && matrialStatu.CurrentWorkProcessId > 0)
            {
                ajaxResponse.Data = ObjectMapper.Map<WorkProcessInfoDto>(Repository.FirstOrDefault(p => p.Id == matrialStatu.CurrentWorkProcessId));
            }

            if (matrialStatu == null || matrialStatu.CurrentWorkProcessId == 0)
            {
                ajaxResponse.Msg = "该产品还未开始生产";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            if (matrialStatu.ProduceStatus == ProduceStatusEnum.已完成)
            {
                ajaxResponse.Msg = "该产品已完成生产";
                ajaxResponse.Code = 500;
                return ajaxResponse;
            }

            return ajaxResponse;
        }

        /// <summary>
        /// 更新工序物料信息
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public async Task<JHTAjaxResponse> UpdateWorkProcessMaterialInfoAsync(InputOperatorRecordInfo inputInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse() { Code = 500 };
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            var listInputMateril = new List<WorkProcessMaterialRecord>();
            var listChangeRecord = new List<WorkProcessMaterialRecordHistory>();
            var inputRecords = _workProcessInfoManager.LoadWorkProcessMaterilRecord(inputInfo.WorkProcessId, inputInfo.OperatroMaterilBatchNumber);
            var workStaion = _workStationRep.FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var workOrderInfo = _workOrderManager.GetWorkOrderByOrderNumber(inputInfo.WorkOrderNumber);
            var nowTime = DateTime.Now;
            var isInputRepaired = _k3ErpRepostiory.GetSNInStockInfo(productBatchNumber.BatchNumber) == null ? false : true;
            var ChangedMaterialList = new List<MaterialBatchNumberDto>();

            var inputRecrodMaterialNumber = inputRecords.Select(p => p.InputMaterialNumber).ToList();
            // 进行物料换算处理，更新对应电堆信息
            foreach (var item in inputInfo.InputMaterialInfos)
            {
                var cutMaterialConfig = _materialManager.LoadCutMaterialConfig(productBatchNumber.MaterialId, item.MaterialNumber);
                item.BOMMaterialCount = item.MatrialCount;
                item.BOMUnitName = item.WrapUniteName;
                var inputMaterialBatchInfo = _materialBatchNumberCache.GetByMaterialBatchNumber(item.BatchNumber);

                _materialBatchNumberManager.CheckBatchNumberIsDiscard(item.BatchNumber);// 新增或修改物料判断物料状态

                // 如果为原材料则取入库信息，若入库信息不存则取序列号信息，如果序列号信息不存在则报错。
                ERPInStockInfo eRPInStockInfo = new ERPInStockInfo();
                if (inputMaterialBatchInfo != null && inputMaterialBatchInfo.IsLineMaterialInfo)
                {
                    eRPInStockInfo.Supplier = "伟力得";
                    eRPInStockInfo.WarehousingTime = inputMaterialBatchInfo.CreationTime.GetValueOrDefault();
                    eRPInStockInfo.WarehousingNumber = "";
                }

                if (item.IsLineMaterialInfo)
                {
                    item.CreationTime = inputMaterialBatchInfo.CreationTime;
                }

                if (inputMaterialBatchInfo != null && inputMaterialBatchInfo.IsLineMaterialInfo == false)
                {
                    item.CreationTime = inputMaterialBatchInfo.CreationTime;
                    eRPInStockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == inputMaterialBatchInfo.FromErpBatchNumber);
                    if (eRPInStockInfo == null)
                    {
                        // todo 从ERP数据库中获取
                        var snInstockInfo = _k3ErpRepostiory.GetSNInStockInfo(inputMaterialBatchInfo.BatchNumber);
                        if (snInstockInfo != null)
                        {
                            eRPInStockInfo = new ERPInStockInfo
                            {
                                BatchNo = inputMaterialBatchInfo.FromErpBatchNumber,
                                WarehousingNumber = snInstockInfo.InStockBillNo,
                                Supplier = snInstockInfo.Supplier,
                                WarehousingTime = snInstockInfo.WarehousingTime,
                            };
                        }
                    }
                }

                if (inputMaterialBatchInfo == null)
                {
                    eRPInStockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == item.BatchNumber);
                }

                if (eRPInStockInfo == null)
                {
                    ajaxResponse.Msg = $"物料：{item.MaterialName},批次号/序列号：{item.BatchNumber} 未能找到相关入库信息，";
                    ajaxResponse.Code = 500;
                    return ajaxResponse;
                }

                if (cutMaterialConfig != null)
                {
                    item.BOMMaterialCount = cutMaterialConfig.ConversionRatio * item.MatrialCount;
                    item.BOMUnitName = cutMaterialConfig.ConfigMaterialUnitName;
                }
                else
                {
                    item.BOMMaterialCount = item.MatrialCount;
                    item.BOMUnitName = item.WrapUniteName;
                }

                if (!inputRecords.Any(p => p.InputMaterialBatchNumber == item.BatchNumber))
                {
                    ChangedMaterialList.Add(item.Clone());
                    WorkProcessMaterialRecordHistory recordHistory = BuildRecordHistory(inputInfo, productBatchNumber, workStaion, workProcess, nowTime, item, eRPInStockInfo);
                    recordHistory.ChangeReason = "新增该物料";
                    listChangeRecord.Add(recordHistory);
                }
                else
                {
                    WorkProcessMaterialRecordHistory recordHistory = BuildRecordHistory(inputInfo, productBatchNumber, workStaion, workProcess, nowTime, item, eRPInStockInfo);
                    var oldData = inputRecords.Find(p => p.InputMaterialBatchNumber.Equals(item.BatchNumber));
                    if (cutMaterialConfig != null)
                    {
                        oldData.InputMaterialCount = item.BOMMaterialCount;
                        oldData.InputUnitName = item.BOMUnitName;
                    }

                    if (oldData.InputMaterialCount != item.BOMMaterialCount)
                    {
                        if (item.BOMMaterialCount - oldData.InputMaterialCount > 0)
                        {
                            var modifyMaterial = item.Clone();
                            modifyMaterial.BOMMaterialCount = (item.BOMMaterialCount - oldData.InputMaterialCount).GetValueOrDefault();
                            ChangedMaterialList.Add(modifyMaterial);
                        }

                        recordHistory.ChangeReason = $"物料数量从{oldData.InputMaterialCount}{oldData.InputUnitName},变为{item.MatrialCount}{item.WrapUniteName}";
                        listChangeRecord.Add(recordHistory);
                    }
                }


                WorkProcessMaterialRecord materialRecord = BuildMaterialRecord(inputInfo, isInputRepaired, productBatchNumber, workStaion, workProcess, nowTime, item, eRPInStockInfo);
                listInputMateril.Add(materialRecord);
            }

            if (await CheckInputMaterial(inputInfo, ajaxResponse, productBatchNumber, ChangedMaterialList) == false)
            {
                return ajaxResponse;
            }

            foreach (var item in inputRecords)
            {
                ERPInStockInfo eRPInStockInfo = new ERPInStockInfo();
                if (!inputInfo.InputMaterialInfos.Any(p => p.BatchNumber == item.InputMaterialBatchNumber))
                {
                    var inputMaterialBatchInfo = _materialBatchNumberCache.GetByMaterialBatchNumber(item.InputMaterialBatchNumber);
                    var eRPInStockNumber = inputMaterialBatchInfo != null ? inputMaterialBatchInfo.FromErpBatchNumber : item.InputMaterialBatchNumber;
                    eRPInStockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == eRPInStockNumber);
                    if (eRPInStockInfo == null)
                    {
                        eRPInStockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == item.InputMaterialBatchNumber);
                        var snInstockInfo = _k3ErpRepostiory.GetSNInStockInfo(inputMaterialBatchInfo.BatchNumber);
                        if (snInstockInfo != null)
                        {
                            eRPInStockInfo = new ERPInStockInfo
                            {
                                BatchNo = inputMaterialBatchInfo.FromErpBatchNumber,
                                WarehousingNumber = snInstockInfo.InStockBillNo,
                                Supplier = snInstockInfo.Supplier,
                                WarehousingTime = snInstockInfo.WarehousingTime,
                            };
                        }
                    }

                    if (inputMaterialBatchInfo == null)
                    {
                        eRPInStockInfo = _erpInStockRepository.FirstOrDefault(p => p.BatchNo == item.InputMaterialBatchNumber);
                    }

                    WorkProcessMaterialRecordHistory recordHistory = BuildRecordHistory(inputInfo, productBatchNumber, workStaion, workProcess, nowTime, new MaterialBatchNumberDto()
                    {
                        MatrialCount = item.InputMaterialCount.GetValueOrDefault(),
                        MaterialName = item.InputMaterialName,
                        MaterialId = item.InputMaterilId,
                        MaterialNumber = item.InputMaterialNumber,
                        WrapUniteName = item.InputUnitName,
                        BatchNumber = item.InputMaterialBatchNumber,
                        CreationTime = item.CreateTime
                    }, eRPInStockInfo);
                    recordHistory.ChangeReason = "该物料已被移除";
                    listChangeRecord.Add(recordHistory);
                }
            }

            _workProcessInfoManager.BatchAddMaterilRecord(listInputMateril);
            _workProcessInfoManager.BatchAddMaterilRecordHistory(listChangeRecord);
            var userInfo = await _userManager.FindByIdAsync(AbpSession.UserId.GetValueOrDefault());

            // 增加物料报废信息
            if (inputInfo.MaterialDiscardRecords != null)
            {
                foreach (var item in inputInfo.MaterialDiscardRecords)
                {
                    // todo  更新供应商信息
                    item.RecordDate = DateTime.Now;
                    item.RecordUserId = AbpSession.UserId.GetValueOrDefault();
                    item.RecordUserName = userInfo.Name;
                    var sourcItem = listInputMateril.FirstOrDefault(p => p.InputMaterialBatchNumber == item.BatchNumber);
                    if (sourcItem != null)
                    {
                        item.Supplier = sourcItem.Supplier;
                    }
                    else
                    {
                        var sourcItem2 = listChangeRecord.FirstOrDefault(p => p.InputMaterialBatchNumber == item.BatchNumber);
                        item.Supplier = sourcItem2.Supplier;
                    }
                }
                // 需要检查相关的报废物料数据超量报废的问题？
                // 批量增加物料报废记录信息
                _workProcessInfoManager.BatchAddMaterilDiscardRecords(ObjectMapper.Map<List<MaterialDiscardRecord>>(inputInfo.MaterialDiscardRecords));
            }

            ajaxResponse.Code = 200;
            ajaxResponse.Msg = "物料替换成功";
            return ajaxResponse;
        }

        private static WorkProcessMaterialRecord BuildMaterialRecord(
            InputOperatorRecordInfo inputInfo,
            bool IsRepairedInput,
            MaterialBatchNumberDto productBatchNumber,
            WorkStationInfo workStaion,
            WorkProcessInfo workProcess,
            DateTime nowTime,
            MaterialBatchNumberDto item,
            ERPInStockInfo stockInfo)
        {
            if (stockInfo == null)
            {
                stockInfo = new ERPInStockInfo()
                {
                    BatchNo = item.BatchNumber,
                    WarehousingTime = DateTime.Now,
                    Supplier = "伟力得",
                };
            }

            return new WorkProcessMaterialRecord()
            {
                CreateTime = nowTime,
                InputMaterialBatchNumber = item.BatchNumber,
                IsRepairedInput = IsRepairedInput,
                InputMaterialCount = item.MatrialCount,
                InputMaterialName = item.MaterialName,
                InputUnitName = item.WrapUniteName,
                InputMaterialNumber = item.MaterialNumber,
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                InputMaterilId = item.MaterialId,
                WorkProcessId = inputInfo.WorkProcessId,
                WorkProcessName = workProcess.ProcessName,
                WorkStationId = workStaion.Id,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                OrderNumber = productBatchNumber.FromOrderNumber,
                ProductBatchNumber = inputInfo.OperatroMaterilBatchNumber,
                BOMMaterialCount = item.BOMMaterialCount,
                BOMUnitName = item.BOMUnitName,
                Supplier = item.IsLineMaterialInfo ? "伟力得" : stockInfo.Supplier,
                BatchNo = item.IsLineMaterialInfo ? item.BatchNumber : stockInfo.BatchNo,
                WarehousingTime = item.IsLineMaterialInfo ? item.CreationTime.GetValueOrDefault() : stockInfo.WarehousingTime
            };
        }

        private static WorkProcessMaterialRecordHistory BuildRecordHistory(
            InputOperatorRecordInfo inputInfo,
            MaterialBatchNumberDto productBatchNumber,
            WorkStationInfo workStaion,
            WorkProcessInfo workProcess,
            DateTime nowTime,
            MaterialBatchNumberDto item,
            ERPInStockInfo stockInfo
            )
        {
            if (stockInfo == null)
            {
                stockInfo = new ERPInStockInfo()
                {
                    BatchNo = item.BatchNumber,
                    WarehousingTime = DateTime.Now,
                    Supplier = "伟力得",
                };
            }

            return new WorkProcessMaterialRecordHistory()
            {
                CreateTime = nowTime,
                InputMaterialBatchNumber = item.BatchNumber,
                InputMaterialCount = item.MatrialCount,
                InputMaterialName = item.MaterialName,
                InputUnitName = item.WrapUniteName,
                InputMaterialNumber = item.MaterialNumber,
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                InputMaterilId = item.MaterialId,
                WorkProcessId = inputInfo.WorkProcessId,
                WorkProcessName = workProcess.ProcessName,
                WorkStationId = workStaion.Id,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                OrderNumber = productBatchNumber.FromOrderNumber,
                ProductBatchNumber = inputInfo.OperatroMaterilBatchNumber,
                BOMUnitName = item.BOMUnitName,
                BOMMaterialCount = item.BOMMaterialCount,
                Supplier = item.IsLineMaterialInfo ? "伟力得" : stockInfo.Supplier,
                BatchNo = item.IsLineMaterialInfo ? item.BatchNumber : stockInfo.BatchNo,
                WarehousingTime = item.IsLineMaterialInfo ? item.CreationTime.GetValueOrDefault() : stockInfo.WarehousingTime

            };
        }

        /// <summary>
        /// 更新物料信息
        /// </summary>
        /// <param name="inputInfo"></param>
        /// <returns></returns>
        public JHTAjaxResponse UpdateWorkProcessFillInfo(CompleteWorkProcessRecordDto inputInfo)
        {
            JHTAjaxResponse ajaxResponse = new JHTAjaxResponse();
            var workStaion = _workStationRep.FirstOrDefault(p => p.Id == inputInfo.WorkStationId);
            var workProcess = Repository.FirstOrDefault(p => p.Id == inputInfo.WorkProcessId);
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputInfo.OperatroMaterilBatchNumber);
            // 1、记录填报数据
            if (inputInfo.FormTemlpateId > 0)
            {
                var operatro = _userRep.FirstOrDefault(p => p.Id == AbpSession.UserId.GetValueOrDefault());
                _formTemplateInfoManager.AddFromInfoRecord(new FormInfoRecord()
                {
                    BelongFormId = inputInfo.FormTemlpateId,
                    BelongMaterialBatchNumber = inputInfo.OperatroMaterilBatchNumber,
                    BelongOrderNumber = productBatchNumber.FromOrderNumber,
                    WorkProcessName = workProcess.ProcessName,
                    BelongWorkProcessId = workProcess.Id,
                    BelongWorkProcessNumber = workProcess.ProcessNumber,
                    FormRecordData = inputInfo.FormRecordInfo,
                    MaterialId = productBatchNumber.MaterialId,
                    MaterialNumber = productBatchNumber.MaterialNumber,
                    OperatorTime = DateTime.Now,
                    FormUseType = FormUseTypeEnum.标准工序填报,
                    Operator = operatro.UserName,
                    OperatorUserId = AbpSession.UserId.GetValueOrDefault(),
                });
            }

            return ajaxResponse;
        }

        public JHTAjaxResponse<List<WorkProcessInfoDto>> LoadStartWorkProcess(InputOperatorRecordInfo inputOperatorRecordInfo)
        {
            JHTAjaxResponse<List<WorkProcessInfoDto>> ajaxResponse = new JHTAjaxResponse<List<WorkProcessInfoDto>>();
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(inputOperatorRecordInfo.OperatroMaterilBatchNumber);
            var orderInfo = _workOrderManager.GetWorkOrderByOrderNumber(productBatchNumber.FromOrderNumber);
            var workProcessSetInfo = _workProcessSetCache.Get(orderInfo.WorkProcessSetId.GetValueOrDefault());
            var firstWorkProcessId = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).BelongWorkProcessInfoId;
            var workProcessInfo = this.Repository.FirstOrDefault(p => p.Id == firstWorkProcessId);
            ajaxResponse.Data = new List<WorkProcessInfoDto>()
            {
                ObjectMapper.Map<WorkProcessInfoDto>(workProcessInfo)
            };
            return ajaxResponse;
        }

        public WorkProcessInfoDto GetProductCurrentWorkProcessInfo(string productMaterialBatchNumber)
        {
            var productInfo = _materialBatchNumberCache.GetByMaterialBatchNumber(productMaterialBatchNumber);
            var matrialStatu = _workOrderManager.GetMaterialProduceStatu(productMaterialBatchNumber);

            long currentWorkProcessInfoId = 0;
            var orderInfo = _workOrderManager.GetWorkOrderByOrderNumber(productInfo.FromOrderNumber);
            var workProcessSetInfo = _workProcessSetCache.Get(orderInfo.WorkProcessSetId.GetValueOrDefault());
            if (matrialStatu == null)
            {
                currentWorkProcessInfoId = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.ParentNodeId == null || p.ParentNodeId.Count == 0).BelongWorkProcessInfoId;
            }

            if (matrialStatu != null && matrialStatu.ProduceStatus == ProduceStatusEnum.生产中 && matrialStatu.LeftWorkProcessCount >= 1)
            {
                if (matrialStatu.IsCurrentWorkProcessDone)
                {
                    var nextNodedId = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.BelongWorkProcessInfoId == matrialStatu.CurrentWorkProcessId).NodeId;
                    currentWorkProcessInfoId = workProcessSetInfo.WorkProcessSetDetails.FirstOrDefault(p => p.ParentNodeId.Contains(nextNodedId)).BelongWorkProcessInfoId;
                }
                else
                {
                    currentWorkProcessInfoId = matrialStatu.CurrentWorkProcessId;
                }
            }

            WorkProcessInfoDto workProcessInfoDto = null;
            if (currentWorkProcessInfoId > 0)
            {
                workProcessInfoDto = ObjectMapper.Map<WorkProcessInfoDto>(Repository.FirstOrDefault(p => p.Id == currentWorkProcessInfoId));
                workProcessInfoDto.CurrentWorkStationId = _workProcessStationRepository.GetAllIncluding(p => p.BelongWorkStation)
                    .Where(p => p.BelongWorkStation.BelongProductLineId == orderInfo.ProduceLineId && p.BelongWorkProcessId == currentWorkProcessInfoId)
                    .FirstOrDefault().BelongWorkStationId;
            }

            return workProcessInfoDto;
        }

        public async Task<JHTAjaxResponse<MaterialBatchNumberDto>> BuildWorkOrderBatchNumberAsync(BuildSubMaterialBatchNumberDto buildSubMaterialBatchNumberDto)
        {
            var workProcessInfo = await this.Repository.FirstOrDefaultAsync(p => p.Id == buildSubMaterialBatchNumberDto.CurrentWorkProcessId);
            var workStaion = await this._workStationRep.FirstOrDefaultAsync(p => p.Id == buildSubMaterialBatchNumberDto.CurrentWorkStationId);
            var workOrderInfo = _workOrderManager.GetWorkOrderByOrderNumber(buildSubMaterialBatchNumberDto.WorkOrderNumber);
            var ajaxResponse = _workOrderAppService.CreateWorkOrderBatchNumber(new WorkOrder.DTO.CreateWorkOrderBatchNumberDto()
            {
                ProductLineId = workStaion.BelongProductLineId,
                Id = workOrderInfo.Id,
                CreateWorkStationName = workStaion.WorkStationName,
                CreateWorkStationId = workStaion.Id,
                MaterialCount = (long)buildSubMaterialBatchNumberDto.MatrialCount
            });
            var workProcessSet = await _workProcessSetCache.GetAsync(workOrderInfo.WorkProcessSetId.GetValueOrDefault());
            var leftCount = workProcessSet.ComputeLeftProcessCount(buildSubMaterialBatchNumberDto.CurrentWorkProcessId);// 计算剩余的工序数量
            //新增记录
            var proStatu = new OrderMaterialProduceStatu()
            {
                CurrentWorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                CurrentWorkStationId = buildSubMaterialBatchNumberDto.CurrentWorkStationId,
                CurrentProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                NormalWorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                MaterialBatchNumber = ajaxResponse.Data.BatchNumber,
                MaterialInfoId = ajaxResponse.Data.MaterialId,
                WorkOrderNumber = ajaxResponse.Data.FromOrderNumber,
                LeftWorkProcessCount = leftCount,
                ProduceStatus = ProduceStatusEnum.生产中,
                IsCurrentWorkProcessDone = false,
                StartTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                CurrentMatrialCount = buildSubMaterialBatchNumberDto.MatrialCount,
            };
            //完成最后一步
            var productBatchNumber = _materialBatchNumberCache.GetByMaterialBatchNumber(ajaxResponse.Data.BatchNumber);
            proStatu.IsCurrentWorkProcessDone = true;
            // 0 判断当前产品是否完成最后一步工序
            if (leftCount == 0)
            {
                proStatu.ProduceStatus = ProduceStatusEnum.已完成;
                proStatu.EndTime = DateTime.Now;
                proStatu.TestCounts = proStatu.TestCounts + 1;
                proStatu.PassCounts = proStatu.PassCounts + 1;
                workOrderInfo.ProdcuingCount = _workOrderManager.GetMaterialProductingProducesWithCurrentMatrialCount(productBatchNumber.FromOrderNumber) + buildSubMaterialBatchNumberDto.MatrialCount;
                workOrderInfo.FinishedCount = _workOrderManager.GetMaterialFinishedProducesWithCurrentMatrialCount(productBatchNumber.FromOrderNumber) + buildSubMaterialBatchNumberDto.MatrialCount;
            }
            _workOrderManager.SetMaterilStatu(proStatu);
            UnitOfWorkManager.Current.SaveChanges();// 立即保存数据确保投产量一致
            // 完成操作
            _workProcessInfoManager.EndWorkProcessOperatorRecord(new WorkProcessOperatorRecord()
            {
                Id = buildSubMaterialBatchNumberDto.OperateRecordId.GetValueOrDefault(),
                CurrentOperatroAccountId = AbpSession.UserId.GetValueOrDefault(),
                ProductLineId = workStaion.BelongProductLineId.GetValueOrDefault(),
                WorkProcessId = buildSubMaterialBatchNumberDto.CurrentWorkProcessId,
                WorkProcessNumber = workProcessInfo.ProcessNumber,
                WorkProcessName = workProcessInfo.ProcessName,
                WorkStationId = workStaion.Id,
                IsNormalFinish = true,
                WorkStationName = workStaion.WorkStationName,
                WrokShopId = workStaion.BelongWorkShopId.GetValueOrDefault(),
                WorkProcessOperateType = WorkProcessOperateTypeEnum.开始生产,
            }, ajaxResponse.Data.BatchNumber);

            // 判断工单完成工作量

            var finishedCount = _materialBatchRep.GetAll().Where(p => p.FromOrderNumber == workOrderInfo.OrderNumber && p.MaterialNumber == workOrderInfo.MaterialInfo.MaterialNumber).Sum(p => p.MatrialCount);
            workOrderInfo.FinishedCount = finishedCount;
            workOrderInfo.ProdcuingCount = finishedCount;
            workOrderInfo.SetWorkOrderStatu(WorkOrderStatuEnum.生产中);// 设置工单状态

            return ajaxResponse;
        }
    }
}
