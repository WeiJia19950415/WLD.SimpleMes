using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;
using JHT.Abp.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.BOM.Dto;
using WLD.SimpleMes.Common;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.DynamicForms.DTO;
using WLD.SimpleMes.K3DBInfo;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Material.Dto;
using WLD.SimpleMes.Material.SerialNumberGenerator;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.WorkOrder.DTO;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkProcess.Dto;
using WLD.SimpleMes.WorkProcessSet;
using WLD.SimpleMes.WorkStation;
using WLD.SimpleMes.WorkStation.Dto;
using static WLD.SimpleMes.DynamicForms.DDImportantInfos;

namespace WLD.SimpleMes.Report
{
    /// <summary>
    /// 报表应用
    /// </summary>
    public class ReportAppService : SimpleMesAppServiceBase, IReportAppService
    {
        private readonly IReportRepository _reportRepository;

        private readonly IRepository<WorkProcessCapacityDailyReportRecord, long> _stationCapacityDailyReportRecordRep;

        private readonly IWorkProcessSetCache _workProcessSetCache;

        private readonly IRepository<WorkProcessSetProductRelation, long> _workProcessSetProductRelation;

        private readonly IRepository<ProductLineCapacityDailyReportRecord, long> _productLineDialyReportRep;

        private readonly IRepository<WorkProcessProblemDailyReportRecord, long> _problemDailyReportRep;

        private readonly IRepository<WorkProcessOnePassRateReport, long> _workProcessOnePassRateReportRep;

        private readonly IRepository<PrepaireWorkProcessDayReport, long> _prepaireWorkProcessDayReportRep;

        private readonly IRepository<View_DDImportantInfos, long> _viewddImportantInfosRep;

        private readonly IRepository<DDImportantInfos, long> _ddImportantInfosRep;

        private readonly IRepository<WorkProcessOperatorRecord, long> _workProcessOperatorLogRep;

        private readonly IRepository<View_ProblemRecord, long> _viewProblemRecordRep;

        private readonly IRepository<View_OrderMaterialProduceStatuses, long> _ordreMaterialStatuRep;

        private readonly IRepository<WorkProcessInfo, long> _workProcessRep;

        private readonly IRepository<WorkOrderInfo, long> _workOrderInfoRep;

        private readonly IRepository<ProductLine, long> _prodcutLineRep;

        private readonly IRepository<WorkProcessMaterialRecord, long> _recordMaterialRep;

        private readonly IRepository<View_BatchMaterialUsedReport, string> _batchMaterialUsedRep;

        private readonly IRepository<MaterialBatchNumber, long> _materialBatchNumberRep;

        private readonly IRepository<View_ProductConstructMaterialInfo, long> _productConstructMaterialInfoRep;

        private readonly IRepository<View_PrepareUserWorkStatic, long> _prePareUserWorkStaticRep;

        private readonly IRepository<View_MaterialDiscardRecord, long> _viewMaterialDiscardRep;

        private readonly IRepository<View_DDTestDayKPI, long> _DDTestDayKPIRep;
        private readonly BomUnitManager _bomUnitManager;

        private readonly MaterialCategoryManager _materialCategoryManager;

        private readonly IK3ErpRepostiory _erpRepostiory;

        private readonly IReportDapperRepository _reportDapperRepository;

        private readonly IRepository<ERPInStockInfoOperateRecord, long> _instockOperateRecord;
        public ReportAppService(IReportRepository reportRepository,
            IWorkProcessSetCache workProcessSetCache,
            IRepository<WorkProcessSetProductRelation, long> workProcessSetProductRelation,
            IRepository<ProductLineCapacityDailyReportRecord, long> productLineDialyReportRep,
            IRepository<WorkProcessProblemDailyReportRecord, long> problemDailyReportRep,
            IRepository<View_DDImportantInfos, long> viewddImportantInfosRep,
            IRepository<WorkProcessOnePassRateReport, long> workProcessOnePassRateReportRep,
            IRepository<WorkProcessCapacityDailyReportRecord, long> stationCapacityDailyReportRecordRep,
            IRepository<PrepaireWorkProcessDayReport, long> prepaireWorkProcessDayReportRep,
            IRepository<View_OrderMaterialProduceStatuses, long> ordreMaterialStatuRep,
            IRepository<View_ProblemRecord, long> viewProblemRecordRep,
            IRepository<WorkProcessOperatorRecord, long> workProcessOperatorLogRep,
            IRepository<WorkOrderInfo, long> workOrderInfoRep,
            IRepository<WorkProcessInfo, long> workProcessRep,
            IRepository<View_BatchMaterialUsedReport, string> batchMaterialUsedRep,
            IRepository<View_ProductConstructMaterialInfo, long> productConstructMaterialInfoRep,
            IK3ErpRepostiory erpRepostiory,
            IRepository<ProductLine, long> prodcutLineRep,
            IRepository<DDImportantInfos, long> ddImportantInfosRep,
            BomUnitManager bomUnitManager,
            MaterialCategoryManager materialCategoryManager,
            IRepository<MaterialBatchNumber, long> materialBatchNumberRep,
        IRepository<WorkProcessMaterialRecord, long> recordMaterialRep,
        IRepository<View_DDTestDayKPI, long> DDTestDayKPIRep,
            IRepository<View_PrepareUserWorkStatic, long> prePareUserWorkStaticRep,
            IRepository<View_MaterialDiscardRecord, long> viewMaterialDiscardRep,
            IRepository<ERPInStockInfoOperateRecord, long> instockOperateRecord,
            IReportDapperRepository reportDapperRepository
            )
        {
            this._reportRepository = reportRepository;
            this._stationCapacityDailyReportRecordRep = stationCapacityDailyReportRecordRep;
            this._workProcessSetCache = workProcessSetCache;
            this._workProcessSetProductRelation = workProcessSetProductRelation;
            this._productLineDialyReportRep = productLineDialyReportRep;
            this._problemDailyReportRep = problemDailyReportRep;
            this._workProcessOnePassRateReportRep = workProcessOnePassRateReportRep;
            _viewddImportantInfosRep = viewddImportantInfosRep;
            _ddImportantInfosRep = ddImportantInfosRep;
            _prepaireWorkProcessDayReportRep = prepaireWorkProcessDayReportRep;
            _ordreMaterialStatuRep = ordreMaterialStatuRep;
            _viewProblemRecordRep = viewProblemRecordRep;
            _workProcessOperatorLogRep = workProcessOperatorLogRep;
            _workOrderInfoRep = workOrderInfoRep;
            _workProcessRep = workProcessRep;
            _recordMaterialRep = recordMaterialRep;
            _erpRepostiory = erpRepostiory;
            _batchMaterialUsedRep = batchMaterialUsedRep;
            _bomUnitManager = bomUnitManager;
            _materialCategoryManager = materialCategoryManager;
            _materialBatchNumberRep = materialBatchNumberRep;
            _productConstructMaterialInfoRep = productConstructMaterialInfoRep;
            _prePareUserWorkStaticRep = prePareUserWorkStaticRep;
            _DDTestDayKPIRep = DDTestDayKPIRep;
            _prodcutLineRep = prodcutLineRep;
            _reportDapperRepository = reportDapperRepository;
            _viewMaterialDiscardRep = viewMaterialDiscardRep;
            _instockOperateRecord = instockOperateRecord;
        }

        /// <summary>
        /// 生成产线报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>

        [UnitOfWork]
        public virtual async Task BuildProductLineCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001")
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
            {
                var materialIds = await _reportRepository.GetTodayProductLineMaterialAsync(staticDate, materialNumber);
                var inUsedWorkProcess = this._workProcessSetProductRelation.GetAll().Where(p => materialIds.Contains(p.MaterialInfoId) && p.IsCurrent).ToList();
                foreach (var item in materialIds)
                {
                    var processSetId = inUsedWorkProcess.FirstOrDefault(p => p.MaterialInfoId == item).BelongWorkProcessSetId;
                    var workProcessSet = _workProcessSetCache.Get(processSetId);
                    if (workProcessSet != null)
                    {
                        await _reportRepository.BuildProductLineCapacityDailyReportAsync(staticDate, item, workProcessSet.GetFirstWorkProcessId().FirstOrDefault().BelongWorkProcessInfoId, workProcessSet.GetLastWorkProcessId().FirstOrDefault().BelongWorkProcessInfoId);
                    }
                }
            }
        }

        [UnitOfWork]
        public virtual async Task BuildWorkProcessCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001")
        {
            await _reportRepository.BuildWorkProcessCapacityDailyReportAsync(staticDate, materialNumber);
        }

        [UnitOfWork]
        public virtual async Task BuildWorkProcessProblemDailyReportAsync(DateTime staticDate)
        {
            await _reportRepository.BuildWorkProcessProblemDailyReportAsync(staticDate);
        }



        /// <summary>
        /// 生产工序一次性通过率报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task BuildWorkProcessOnePassRateReportAsync(DateTime staticDate, string materialNumber = "D02.001%")
        {
            await _reportRepository.BuildWorkProcessOnePassRateReportAsync(staticDate, materialNumber);
        }

        /// <summary>
        /// 生产前置准备工序日报
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task BuildPrepaireWorkProcessDayReportsAsync(DateTime staticDate)
        {
            await _reportRepository.BuildPrepaireWorkProcessDayReportsAsync(staticDate);
        }

        /// <summary>
        /// 查询工序日报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<WorkProcessCapacityDailyReportRecordDto>> LoadStationCacityDailyRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<WorkProcessCapacityDailyReportRecordDto> returnData = new PageData<WorkProcessCapacityDailyReportRecordDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _stationCapacityDailyReportRecordRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.WorkStationId != null, p => p.WorkStationId == conditon.WorkStationId)
                        .WhereIf(conditon.WorkProcessId != null, p => p.WorkProcessId == conditon.WorkProcessId)
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<WorkProcessCapacityDailyReportRecordDto>>(query.OrderByDescending(p => p.StaticDate).AsNoTracking().Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData;
        }

        public ProductQuantity LoadWorkOrderCompletionStatus(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            ProductQuantity returnData = new ProductQuantity();
            var conditon = where.Condition;
            conditon.ParseTime();
            returnData.CancalNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已取消).Count();
            returnData.ClosedNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已关闭).Count();
            returnData.StartNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已下发).Count();
            returnData.ProduceNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.生产中).Count();
            returnData.NotStartedNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.未开始).Count();
            return returnData;
        }

        /// <summary>
        /// 查询工序统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<WorkProcessCapacityStaticReportDto>> LoadStationCacityStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<WorkProcessCapacityStaticReportDto> returnData = new PageData<WorkProcessCapacityStaticReportDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _stationCapacityDailyReportRecordRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.WorkStationId != null, p => p.WorkStationId == conditon.WorkStationId)
                        .WhereIf(conditon.WorkProcessId != null, p => p.WorkProcessId == conditon.WorkProcessId)
                        ;
            var groupByQuery = query.GroupBy(p => new
            {
                p.MaterialId,
                p.MaterialName,
                p.MaterialNumber,
                p.ProductLineId,
                p.ProductLineName,
                p.WorkStationId,
                p.WorkStationName,
                p.WorkProcessId,
                p.WorkProcessName
            }).Select(p => new WorkProcessCapacityStaticReportDto()
            {

                MaterialId = p.Key.MaterialId,
                MaterialName = p.Key.MaterialName,
                MaterialNumber = p.Key.MaterialNumber,
                ProductLineId = p.Key.ProductLineId,
                ProductLineName = p.Key.ProductLineName,
                WorkStationId = p.Key.WorkStationId,
                WorkStationName = p.Key.WorkStationName,
                WorkProcessId = p.Key.WorkProcessId,
                WorkProcessName = p.Key.WorkProcessName,
                InputCount = p.Sum(p => p.InputCount),
                FinishedCount = p.Sum(p => p.FinishedCount),
            });

            returnData.Total = await groupByQuery.CountAsync();
            returnData.List = groupByQuery.AsNoTracking().Skip(where.SkipCount).Take(where.PageSize).ToList();

            return returnData;
        }

        /// <summary>
        /// 查询产线统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<ProductLineCapacityDailyReportRecordDto>> LoadProductLineCacityStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<ProductLineCapacityDailyReportRecordDto> returnData = new PageData<ProductLineCapacityDailyReportRecordDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _productLineDialyReportRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<ProductLineCapacityDailyReportRecordDto>>(query.AsNoTracking().OrderByDescending(p => p.StaticDate).Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData;

        }

        /// <summary>
        /// 查询工序问题统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<WorkProcessProblemDailyReportRecordDto>> LoadWorkProcessProblemStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<WorkProcessProblemDailyReportRecordDto> returnData = new PageData<WorkProcessProblemDailyReportRecordDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _problemDailyReportRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        .WhereIf(!string.IsNullOrEmpty(conditon.ProblemCategoryCode), p => p.QualityProblemNumber.StartsWith(conditon.ProblemCategoryCode))
                        .WhereIf(conditon.ProblemDefineId != null, p => p.ProblemDefineId == conditon.ProblemDefineId)
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<WorkProcessProblemDailyReportRecordDto>>(query.AsNoTracking().OrderByDescending(p => p.StaticDate).Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData; ;
        }


        public async Task<DDImportantInfoDto> LoadDDImportantInfoAsync(EntityDto<string> snInfo)
        {
            var dataInfo = await _viewddImportantInfosRep.GetAll().FirstOrDefaultAsync(p => p.BelongMaterialBatchNumber == snInfo.Id && p.IsAudited == true);
            if (dataInfo != null)
            {
                var dtoData = ObjectMapper.Map<DDImportantInfoDto>(dataInfo);
                dtoData.ProduceDateTime = StackSerialNumberGenerator.ParseProductDateTime(dtoData.BelongMaterialBatchNumber);
                return dtoData;
            }

            return null;
        }


        /// <summary>
        /// 加载电堆重要信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<DDImportantInfoDto>> LoadDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<DDImportantInfoDto> returnData = new PageData<DDImportantInfoDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _viewddImportantInfosRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.RecordDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.RecordDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialIds != null && conditon.MaterialIds.Count() > 0, p => conditon.MaterialIds.Contains(p.MaterialId))
                        .WhereIf(conditon.ProductLineId != null, p => p.BelongProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.IsAudited != null, p => p.IsAudited == conditon.IsAudited)
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.BelongMaterialBatchNumber.Contains(conditon.KeyWord) || p.BelongOrderNumber.Contains(conditon.KeyWord) || p.ProjectNumber.Contains(conditon.KeyWord))
                        .WhereIf(conditon.Level != null && conditon.Level.Count > 0, p => p.Level != null && conditon.Level.Contains(p.Level.Value))
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.MaterialNumber))
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.SupplierBatchNumber))
                        .WhereIf(conditon.IsInStock != null, p => p.IsInStock == conditon.IsInStock)
                        ;

            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<DDImportantInfoDto>>(query.AsNoTracking().OrderByDescending(p => p.RecordDate).Skip(where.SkipCount).Take(where.PageSize).ToList());
            returnData.List.ForEach(p =>
            {
                p.ProduceDateTime = StackSerialNumberGenerator.ParseProductDateTime(p.BelongMaterialBatchNumber);
            });

            return returnData; ;
        }

        /// <summary>
        /// 加载电堆工序一次通过率报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<WorkProcessOnePassRateReportDto>> LoadWorkProcessOnePassRateReportRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<WorkProcessOnePassRateReportDto> returnData = new PageData<WorkProcessOnePassRateReportDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _workProcessOnePassRateReportRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.WorkProcessId != null, p => p.WorkProcessId == conditon.WorkProcessId)
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<WorkProcessOnePassRateReportDto>>(query.AsNoTracking().OrderByDescending(p => p.StaticDate).Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData;
        }


        /// <summary>
        /// 加载电堆工序一次通过率报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<PrepaireWorkProcessDayReportDto>> LoadPrepaireWorkProcessDayReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<PrepaireWorkProcessDayReportDto> returnData = new PageData<PrepaireWorkProcessDayReportDto>();
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _prepaireWorkProcessDayReportRep
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.WorkOrderNumber.Contains(conditon.KeyWord) || p.MaterialName.Contains(conditon.KeyWord) || p.MaterialNumber.Contains(conditon.KeyWord))
                        .WhereIf(conditon.StartDate != null, p => p.StaticDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.StaticDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.WorkStationId != null, p => p.WorkStationId == conditon.WorkStationId)
                        .WhereIf(conditon.ProductLineId != null, p => p.ProductLineId == conditon.ProductLineId)
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<PrepaireWorkProcessDayReportDto>>(query.AsNoTracking().OrderByDescending(p => p.StaticDate).Skip(where.SkipCount).Take(where.PageSize).ToList());

            return returnData;
        }

        public async Task<PageData<OrderMaterialProduceStatuDto>> LoadOrderMaterialProduceStatuReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            PageData<OrderMaterialProduceStatuDto> returnData = new PageData<OrderMaterialProduceStatuDto>();
            var conditon = where.Condition;
            conditon.ParseTime();

            var overyDays = int.Parse(SettingManager.GetSettingValue(AppSettingNames.OverDayConfing));

            List<ProduceStatusEnum> expectionStatus = new List<ProduceStatusEnum>() { ProduceStatusEnum.异常, ProduceStatusEnum.异常处置, ProduceStatusEnum.返修中 };
            List<ProduceStatusEnum> produceStatuses = new List<ProduceStatusEnum>() { ProduceStatusEnum.生产中, ProduceStatusEnum.异常, ProduceStatusEnum.异常处置, ProduceStatusEnum.返修中 };
            var onlyQueryFinineed = conditon.ProduceStatus.Count == 1 && conditon.ProduceStatus.Contains(ProduceStatusEnum.已完成);
            var onlyQueryProducing = conditon.ProduceStatus.Count > 0 && conditon.ProduceStatus.Except(produceStatuses).Count() == 0;
            var onlyQueryyException = conditon.ProduceStatus.Count > 0 && conditon.ProduceStatus.Except(expectionStatus).Count() == 0;
            var normalQuery = !onlyQueryFinineed && !onlyQueryProducing && !onlyQueryyException;
            var query = _ordreMaterialStatuRep
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.WorkOrderNumber.Contains(conditon.KeyWord) || p.ProjectNumber.Contains(conditon.KeyWord) || p.MaterialBatchNumber.Contains(conditon.KeyWord) || p.MaterialName.Contains(conditon.KeyWord) || p.ProjectName.Contains(conditon.KeyWord))
                        .WhereIf(normalQuery && conditon.StartDate != null, p => p.StartTime >= conditon.StartDate)
                        .WhereIf(normalQuery && conditon.EndDate != null, p => p.StartTime <= conditon.EndDate)
                        .WhereIf(onlyQueryFinineed, p => p.EndTime >= conditon.StartDate && p.EndTime <= conditon.EndDate)
                        .WhereIf(!string.IsNullOrEmpty(conditon.ProductCategory), p => p.MaterialNumber.StartsWith(conditon.ProductCategory))
                        .WhereIf(conditon.StayTime > 0, p => p.StayTime >= conditon.StayTime)
                        //.WhereIf(onlyQueryProducing, p => p.LastUpdateTime >= conditon.StartDate && p.LastUpdateTime <= conditon.EndDate)
                        //.WhereIf(onlyQueryyException, p => p.LastUpdateTime >= conditon.StartDate && p.LastUpdateTime <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialInfoId == conditon.MaterialId)
                        .WhereIf(conditon.WorkStationId != null, p => p.CurrentWorkStationId == conditon.WorkStationId)
                        .WhereIf(conditon.ProductLineId > 0, p => p.CurrentProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.ProduceStatus != null && conditon.ProduceStatus.Count > 0, p => conditon.ProduceStatus.Contains(p.ProduceStatus))
                        ;
            returnData.Total = await query.CountAsync();
            returnData.List = ObjectMapper.Map<List<OrderMaterialProduceStatuDto>>(query.AsNoTracking().OrderByDescending(p => p.LastUpdateTime).ThenBy(p => p.StartTime).Skip(where.SkipCount).Take(where.PageSize).ToList());
            var defautlTeantId = AbpSession.TenantId == null ? 1 : AbpSession.TenantId;
            using (var uow = this.UnitOfWorkManager.Current.SetTenantId(defautlTeantId))
            {
                AbpSession.Use(defautlTeantId, null);
                var configInfo = await SettingManager.GetSettingValueAsync(AppSettingNames.BigScreenMaterialNameReplaceConfig);
                var materialInfo = new List<string>();
                if (configInfo != null && !string.IsNullOrEmpty(configInfo))
                {
                    materialInfo = configInfo.Split(',').ToList();
                }

                returnData.List.ForEach(p =>
                {
                    p.IsOverDay = (DateTime.Now - p.LastUpdateTime.Value).TotalDays > overyDays && p.ProduceStatus == ProduceStatusEnum.生产中;
                    // 2024-09-05  客制化名称显示
                    if (materialInfo != null && materialInfo.Count > 0)
                    {
                        p.MaterialName = p.MaterialName.Replace(materialInfo[0], materialInfo[1]);
                    }
                });
            }
            return returnData;
        }

        public SNInStockInfoDto LoadERPInStockInfo(string snNumber)
        {
            return ObjectMapper.Map<SNInStockInfoDto>(_erpRepostiory.GetSNInStockInfo(snNumber));
        }

        public async Task<List<OrderMaterialProduceStatuExportDto>> LoadOrderMaterialProduceStatuExportReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {

            var conditon = where.Condition;
            conditon.ParseTime();
            List<ProduceStatusEnum> expectionStatus = new List<ProduceStatusEnum>() { ProduceStatusEnum.异常, ProduceStatusEnum.异常处置, ProduceStatusEnum.返修中 };
            List<ProduceStatusEnum> produceStatuses = new List<ProduceStatusEnum>() { ProduceStatusEnum.生产中, ProduceStatusEnum.异常, ProduceStatusEnum.异常处置, ProduceStatusEnum.返修中 };
            var onlyQueryFinineed = conditon.ProduceStatus.Count == 1 && conditon.ProduceStatus.Contains(ProduceStatusEnum.已完成);
            var onlyQueryProducing = conditon.ProduceStatus.Count > 0 && conditon.ProduceStatus.Except(produceStatuses).Count() == 0;
            var onlyQueryyException = conditon.ProduceStatus.Count > 0 && conditon.ProduceStatus.Except(expectionStatus).Count() == 0;
            var normalQuery = !onlyQueryFinineed && !onlyQueryProducing && !onlyQueryyException;
            var query = _ordreMaterialStatuRep
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.WorkOrderNumber.Contains(conditon.KeyWord) || p.ProjectNumber.Contains(conditon.KeyWord) || p.MaterialBatchNumber.Contains(conditon.KeyWord) || p.MaterialName.Contains(conditon.KeyWord) || p.ProjectName.Contains(conditon.KeyWord))
                        .WhereIf(normalQuery && conditon.StartDate != null, p => p.StartTime >= conditon.StartDate)
                        .WhereIf(normalQuery && conditon.EndDate != null, p => p.StartTime <= conditon.EndDate)
                        .WhereIf(onlyQueryFinineed, p => p.EndTime >= conditon.StartDate && p.EndTime <= conditon.EndDate)
                        .WhereIf(conditon.StayTime > 0, p => p.StayTime >= conditon.StayTime)
                        //.WhereIf(onlyQueryProducing, p => p.LastUpdateTime >= conditon.StartDate && p.LastUpdateTime <= conditon.EndDate)
                        //.WhereIf(onlyQueryyException, p => p.LastUpdateTime >= conditon.StartDate && p.LastUpdateTime <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialInfoId == conditon.MaterialId)
                        .WhereIf(conditon.WorkStationId != null, p => p.CurrentWorkStationId == conditon.WorkStationId)
                        .WhereIf(conditon.ProductLineId > 0, p => p.CurrentProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.ProduceStatus != null && conditon.ProduceStatus.Count > 0, p => conditon.ProduceStatus.Contains(p.ProduceStatus));

            return ObjectMapper.Map<List<OrderMaterialProduceStatuExportDto>>(
               await query.AsNoTracking()
                .OrderByDescending(p => p.LastUpdateTime)
                .ThenBy(p => p.StartTime).ToListAsync());
        }

        public List<PrepaireWorkProcessDayReportDto> LoadPadPrepaireWorkProcessReport(DateTime staticDate)
        {
            List<PrepaireWorkProcessDayReportDto> result = new List<PrepaireWorkProcessDayReportDto>();
            if (staticDate.Date == DateTime.Now.Date)
            {
                result = ObjectMapper.Map<List<PrepaireWorkProcessDayReportDto>>(_reportRepository.QueryToadyPadPrepaireWorkProcessReport(staticDate));
            }
            else
            {
                result = _prepaireWorkProcessDayReportRep.GetAll().Where(p => p.StaticDate == staticDate).GroupBy(p =>
                 new
                 {
                     p.MaterialName,
                     p.MaterialNumber,
                     p.ProductLineName,
                     p.WorkStationName,
                     p.CutMaterialUnitName
                 }).Select(p => new PrepaireWorkProcessDayReportDto()
                 {
                     MaterialName = p.Key.MaterialName,
                     MaterialNumber = p.Key.MaterialNumber,
                     ProductLineName = p.Key.ProductLineName,
                     WorkStationName = p.Key.WorkStationName,
                     FinishedCount = p.Sum(p => p.FinishedCount),
                     CutMaterialUnitName = p.Key.CutMaterialUnitName,
                 }).ToList();
            }

            return result;
        }

        public List<WorkProcessCapacityDailyReportRecordDto> LoadDayWorkProcessReport(ReportQueryConditonDto reportQueryConditon)
        {
            List<WorkProcessCapacityDailyReportRecordDto> result = new List<WorkProcessCapacityDailyReportRecordDto>();
            if (reportQueryConditon.StartDate == DateTime.Now.Date)
            {
                result = ObjectMapper.Map<List<WorkProcessCapacityDailyReportRecordDto>>(_reportRepository.QueryToadyPadWorkProcessCapacityReport(reportQueryConditon.StartDate.Value));

                if (reportQueryConditon.ProductLineId > 0)
                {
                    result = result.Where(p => p.ProductLineId == reportQueryConditon.ProductLineId).ToList();
                }
                else
                {
                    result = result.GroupBy(p => new { p.WorkProcessName }).Select(p => new WorkProcessCapacityDailyReportRecordDto()
                    {
                        WorkProcessName = p.Key.WorkProcessName,
                        //ProductLineName = p.Key.ProductLineName,
                        //WorkStationName = p.Key.WorkStationName,
                        FinishedCount = p.Sum(p => p.FinishedCount),
                        InputCount = p.Sum(p => p.InputCount),

                    }).ToList();
                }
            }
            else
            {
                result = _stationCapacityDailyReportRecordRep.GetAll()
                    .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                    .Where(p => p.StaticDate == reportQueryConditon.StartDate).GroupBy(p =>
                 new
                 {
                     //p.ProductLineName,
                     p.WorkStationName,
                     p.WorkProcessName
                 }).Select(p => new WorkProcessCapacityDailyReportRecordDto()
                 {
                     WorkProcessName = p.Key.WorkProcessName,
                     //ProductLineName = p.Key.ProductLineName,
                     WorkStationName = p.Key.WorkStationName,
                     FinishedCount = p.Sum(p => p.FinishedCount),
                     InputCount = p.Sum(p => p.InputCount),
                 }).ToList();
            }

            return result;
        }

        public async Task<ProductSummaryDto> LoadProductSummaryReportAsync(ReportQueryConditonDto where)
        {
            where.ParseTime();
            if (where.StartDate == null)
            {
                where.StartDate = DateTime.Now.AddMonths(-1).Date;
            }

            if (where.EndDate == null)
            {
                where.StartDate = DateTime.Now.Date;
            }


            ProductSummaryDto productSummaryDto = new ProductSummaryDto();

            var query = _ordreMaterialStatuRep.GetAll()
                .WhereIf(where.ProductLineId != null && where.ProductLineId > 0, p => p.CurrentProductLineId == where.ProductLineId)
                .WhereIf(!string.IsNullOrEmpty(where.ProductCategory), p => p.MaterialNumber.StartsWith(where.ProductCategory));

            //productSummaryDto.OutputCount = await query.Where(p => p.EndTime <= where.EndDate && p.EndTime >= where.StartDate && p.ProduceStatus == ProduceStatusEnum.已完成).CountAsync();
            productSummaryDto.OutputCount = (int)await query.Where(p => p.EndTime <= where.EndDate && p.EndTime >= where.StartDate && p.ProduceStatus == ProduceStatusEnum.已完成)
               .SumAsync(a => a.CurrentMatrialCount);
            //productSummaryDto.InputCount = await query.Where(p => p.StartTime <= where.EndDate && p.StartTime >= where.StartDate).CountAsync();
            productSummaryDto.InputCount = (int)await query.Where(p => p.StartTime <= where.EndDate && p.StartTime >= where.StartDate).SumAsync(a => a.CurrentMatrialCount);

            productSummaryDto.IssuedCount = (int)_workOrderInfoRep.GetAllIncluding(p => p.MaterialInfo)
                .Where(p => p.WorkOrderStatu != WorkOrderStatuEnum.已取消)
                .WhereIf(where.ProductLineId != null && where.ProductLineId > 0, p => p.ProduceLineId == where.ProductLineId)
                .Where(p => p.CreationTime <= where.EndDate && p.CreationTime >= where.StartDate)
                .WhereIf(!string.IsNullOrEmpty(where.ProductCategory), p => p.MaterialInfo.MaterialNumber.StartsWith(where.ProductCategory))
                .Sum(p => p.ProduceCount);

            //productSummaryDto.ScrapCount = await query.Where(p => p.ProduceStatus == ProduceStatusEnum.报废 && p.LastUpdateTime <= where.EndDate && p.LastUpdateTime >= where.StartDate).CountAsync();
            productSummaryDto.ScrapCount = (int)await query.Where(p => p.ProduceStatus == ProduceStatusEnum.报废 && p.LastUpdateTime <= where.EndDate && p.LastUpdateTime >= where.StartDate).SumAsync(a => a.CurrentMatrialCount);
            productSummaryDto.QulityProblemCount = await _viewProblemRecordRep.GetAll()
                .WhereIf(where.ProductLineId != null && where.ProductLineId > 0, p => p.ProductLineId == where.ProductLineId)
                .WhereIf(!string.IsNullOrEmpty(where.ProductCategory), p => p.MaterialNumber.StartsWith(where.ProductCategory))
                .Where(p => p.CreationTime <= where.EndDate && p.CreationTime >= where.StartDate).CountAsync();

            //productSummaryDto.ProducingCount = await query.Where(p => p.ProduceStatus == ProduceStatusEnum.生产中 || p.ProduceStatus == ProduceStatusEnum.异常处置 || p.ProduceStatus == ProduceStatusEnum.异常 || p.ProduceStatus == ProduceStatusEnum.返修中).CountAsync();
            productSummaryDto.ProducingCount = (int)await query.Where(p => p.ProduceStatus == ProduceStatusEnum.生产中 || p.ProduceStatus == ProduceStatusEnum.异常处置 || p.ProduceStatus == ProduceStatusEnum.异常 || p.ProduceStatus == ProduceStatusEnum.返修中)
                .SumAsync(a => a.CurrentMatrialCount);
            //productSummaryDto.ExceptionCount = await query.Where(p => (p.ProduceStatus == ProduceStatusEnum.异常处置 || p.ProduceStatus == ProduceStatusEnum.异常 || p.ProduceStatus == ProduceStatusEnum.返修中)).CountAsync();
            productSummaryDto.ExceptionCount = (int)await query.Where(p => (p.ProduceStatus == ProduceStatusEnum.异常处置 || p.ProduceStatus == ProduceStatusEnum.异常 || p.ProduceStatus == ProduceStatusEnum.返修中))
                .SumAsync(a => a.CurrentMatrialCount);

            return productSummaryDto;
        }

        public List<DayWorkProcessProblemStaticsDto> LoadDayWorkProcessProblemStatics(ReportQueryConditonDto reportQueryConditon)
        {
            var query = _viewProblemRecordRep.GetAll()
                 .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                 .WhereIf(reportQueryConditon.EndDate != null, p => p.CreationTime < reportQueryConditon.EndDate)
                 .WhereIf(reportQueryConditon.StartDate != null, p => p.CreationTime >= reportQueryConditon.StartDate)
                 ;
            if (reportQueryConditon.ProductLineId > 0)
            {
                return query.GroupBy(p => new { p.ProductLineName, p.WorkProcessName }).Select(p => new DayWorkProcessProblemStaticsDto()
                {
                    ProductLineName = p.Key.ProductLineName,
                    WorkProcess = p.Key.WorkProcessName,
                    ProblemCount = p.Count(),
                }).ToList();
            }
            else
            {
                return query.GroupBy(p => p.WorkProcessName).Select(p => new DayWorkProcessProblemStaticsDto()
                {
                    ProductLineName = "",
                    WorkProcess = p.Key,
                    ProblemCount = p.Count(),
                }).ToList();
            }

        }

        public List<DayProblemStaticsDto> LoadDayProblemStatics(ReportQueryConditonDto reportQueryConditon)
        {
            var query = _viewProblemRecordRep.GetAll()
            .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
            .WhereIf(reportQueryConditon.EndDate != null, p => p.CreationTime < reportQueryConditon.EndDate)
            .WhereIf(reportQueryConditon.StartDate != null, p => p.CreationTime >= reportQueryConditon.StartDate);

            return query.GroupBy(p => p.ProbleName).Select(p => new DayProblemStaticsDto()
            {
                ProblemName = p.Key,
                ProblemCount = p.Count(),
            }).ToList();

        }

        public List<WorkProcessAvgTimeStaticsDto> LoadDayWorkProcessAvgTimeStatics(ReportQueryConditonDto reportQueryConditon)
        {

            reportQueryConditon.ParseTime();
            var query = _workProcessOperatorLogRep.GetAll().WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                .WhereIf(reportQueryConditon.EndDate != null, p => p.EndTime < reportQueryConditon.EndDate)
                .WhereIf(reportQueryConditon.StartDate != null, p => p.StartTime >= reportQueryConditon.StartDate);

            return query.GroupBy(p => p.WorkProcessName).Select(p => new WorkProcessAvgTimeStaticsDto()
            {
                CostSeconds = (decimal)p.Average(d => d.CostTimeSeconds),
                WorkProcessName = p.Key,
            }).ToList();

        }

        public List<ProductLineCapacityDailyReportRecordDto> LoadProductLineCacityStaticData(ReportQueryConditonDto reportQueryConditon)
        {
            var query = _productLineDialyReportRep.GetAll()
                 .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                 .WhereIf(reportQueryConditon.EndDate != null, p => p.StaticDate < reportQueryConditon.EndDate)
                 .WhereIf(reportQueryConditon.StartDate != null, p => p.StaticDate >= reportQueryConditon.StartDate);

            return query.GroupBy(p => p.StaticDate).Select(p => new ProductLineCapacityDailyReportRecordDto()
            {
                StaticDate = p.Key,
                InputCount = p.Sum(d => d.InputCount),
                FinishedCount = p.Sum(d => d.FinishedCount)
            }).ToList();
        }

        public List<ProductLineCapacityDailyReportRecordDto> LoadProductCategoryOutputConfig(ReportQueryConditonDto reportQueryConditon)
        {
            var query = _productLineDialyReportRep.GetAll()
                 .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                 .WhereIf(reportQueryConditon.EndDate != null, p => p.StaticDate < reportQueryConditon.EndDate)
                 .WhereIf(reportQueryConditon.StartDate != null, p => p.StaticDate >= reportQueryConditon.StartDate);

            return query.GroupBy(p => p.MaterialName).Select(p => new ProductLineCapacityDailyReportRecordDto()
            {
                MaterialName = p.Key,
                FinishedCount = p.Sum(d => d.FinishedCount)
            }).ToList();
        }

        public ProductQuantityDto LoadWorkOrderCompletionStatus(ReportQueryConditonDto reportQueryConditon)
        {
            ProductQuantityDto returnData = new ProductQuantityDto();
            reportQueryConditon.ParseTime();
            returnData.CancalNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已取消).Count();
            returnData.ClosedNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已关闭).Count();
            returnData.IssuedNumb = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已下发).Count();
            returnData.ProduceNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.生产中).Count();
            returnData.PausedNumb = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.已暂停).Count();
            returnData.NotStartedNum = _workOrderInfoRep.GetAll().Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.未开始).Count();
            return returnData;
        }

        public ProblemRecordStaticDto LoadQulitityProblemDealedStatic(ReportQueryConditonDto reportQueryConditon)
        {
            var query = _viewProblemRecordRep.GetAll()
                  .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                  .WhereIf(reportQueryConditon.EndDate != null, p => p.CreationTime < reportQueryConditon.EndDate)
                  .WhereIf(reportQueryConditon.StartDate != null, p => p.CreationTime >= reportQueryConditon.StartDate);
            ProblemRecordStaticDto result = new ProblemRecordStaticDto()
            {
                CloseCount = query.Where(p => p.IsClosed == true).Count(),
                UnCloseCount = query.Where(p => p.IsClosed == false).Count(),
            };

            return result;
        }

        public List<WorkProcessCapacityDailyReportRecordDto> LoadWorkProcessStayProductStatic(ReportQueryConditonDto reportQueryConditon)
        {
            var fqcWorkProcess = _workProcessRep.GetAll().Where(p => p.WorkProcessType == WorkProcessTypeEnum.FQC).Select(p => p.Id).ToList();
            var query = _ordreMaterialStatuRep.GetAll()
                .Where(p => !fqcWorkProcess.Contains(p.CurrentWorkProcessId))
                .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.CurrentProductLineId == reportQueryConditon.ProductLineId);
            //.WhereIf(reportQueryConditon.EndDate != null, p => p.LastUpdateTime < reportQueryConditon.EndDate)
            //.WhereIf(reportQueryConditon.StartDate != null, p => p.LastUpdateTime >= reportQueryConditon.StartDate);

            var fqcQuery = _ordreMaterialStatuRep.GetAll()
                .Where(p => fqcWorkProcess.Contains(p.CurrentWorkProcessId) && p.ProduceStatus != ProduceStatusEnum.已完成)
                .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.CurrentProductLineId == reportQueryConditon.ProductLineId);

            var fqcInfo = fqcQuery.GroupBy(p => p.ProcessName).Select(p => new WorkProcessCapacityDailyReportRecordDto()
            {
                WorkProcessName = p.Key,
                FinishedCount = p.Count(),
            }).ToList();

            var normalProcessInfo = query.GroupBy(p => p.ProcessName).Select(p => new WorkProcessCapacityDailyReportRecordDto()
            {
                WorkProcessName = p.Key,
                FinishedCount = p.Count(),
            }).ToList();

            return fqcInfo.Concat(normalProcessInfo).ToList();
        }

        public ProblemDealedStaticDto DefectHandlingProgressStatus(ReportQueryConditonDto reportQueryConditon)
        {
            reportQueryConditon.ParseTime();
            ProblemDealedStaticDto dealedStaticDto = new ProblemDealedStaticDto();
            var query = _viewProblemRecordRep.GetAll()
                .WhereIf(reportQueryConditon.ProductLineId > 0, p => p.ProductLineId == reportQueryConditon.ProductLineId)
                .Where(p => p.CreationTime >= reportQueryConditon.StartDate && p.CreationTime < reportQueryConditon.EndDate);


            dealedStaticDto.FinishedCount = query.Where(p => p.IsClosed == true).Count();
            dealedStaticDto.UnFinishedCount = query.Where(p => p.IsClosed == false).Count();

            return dealedStaticDto;

        }

        public List<DDImportantInfoExportDto> LoadExportDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _viewddImportantInfosRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.RecordDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.RecordDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialIds != null && conditon.MaterialIds.Count() > 0, p => conditon.MaterialIds.Contains(p.MaterialId))
                        .WhereIf(conditon.ProductLineId != null, p => p.BelongProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.Level != null && conditon.Level.Count > 0, p => p.Level != null && conditon.Level.Contains(p.Level.Value))
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.MaterialNumber))
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.SupplierBatchNumber))
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.BelongMaterialBatchNumber.Contains(conditon.KeyWord) || p.BelongOrderNumber.Contains(conditon.KeyWord) || p.ProjectNumber.Contains(conditon.KeyWord))
                        .WhereIf(conditon.IsInStock != null, p => p.IsInStock == conditon.IsInStock)
                        ;

            var reuslt = this.ObjectMapper.Map<List<DDImportantInfoExportDto>>(query.ToList());

            //using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            //{
            //    reuslt.ForEach(p =>
            //    {
            //        var qrCodeData = qrGenerator.CreateQrCode(p.BelongMaterialBatchNumber, QRCodeGenerator.ECCLevel.H);
            //        BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            //        p.QrCodeImg = Convert.ToBase64String(qrCode.GetGraphic(20));
            //    });
            //}
            return reuslt;
        }


        public async Task<DDImportantInfoWordExportDto> LoadExportDDImportantInfosAsync(long id)
        {
            var data = await _viewddImportantInfosRep.FirstOrDefaultAsync(p => p.Id == id);

            var exportData = this.ObjectMapper.Map<DDImportantInfoWordExportDto>(data);
            var orderInfo = await _workOrderInfoRep.FirstOrDefaultAsync(p => p.OrderNumber == data.BelongOrderNumber);

            if (orderInfo.CustomerProductInfo != null)
            {
                // 2024-09-05 客户化信息定制
                if (!string.IsNullOrEmpty(orderInfo.CustomerProductInfo.MaterialName))
                {
                    exportData.MatreialName = orderInfo.CustomerProductInfo.MaterialName;
                }
            }



            return exportData;
        }

        public async Task<bool> AuidtDDImportantInfoAsync(DDImportantInfoDto importantInfoDto)
        {
            if (importantInfoDto.Level == null)
            {
                throw new UserFriendlyException("未设置电堆等级信息,审核失败");
            }

            var dataInfo = _ddImportantInfosRep.FirstOrDefault(p => p.Id == importantInfoDto.Id);
            if (dataInfo.IsAudited)
            {
                throw new UserFriendlyException("该电堆信息已审核完成，请勿更改！");
            }

            if (dataInfo.MaterialRecordSimplyInfos == null || dataInfo.MaterialRecordSimplyInfos.Count == 0)
            {
                var materialList = _recordMaterialRep.GetAll().Where(p => p.ProductBatchNumber == dataInfo.BelongMaterialBatchNumber).Select(p => new MaterialRecordSimplyInfo()
                {
                    MaterialNumber = p.InputMaterialNumber,
                    MatreialName = p.InputMaterialName,
                    BatchNo = p.BatchNo,
                    Supplier = p.Supplier,
                    WarehousingTime = p.WarehousingTime,

                }).ToList();

                dataInfo.SetMaterialRecordInfo(materialList);
            }

            dataInfo.AuditorId = AbpSession.UserId;
            var userInfo = await UserManager.FindByIdAsync(dataInfo.AuditorId.GetValueOrDefault());
            dataInfo.IsAudited = true;
            dataInfo.AuditeDate = DateTime.Now;
            dataInfo.Level = importantInfoDto.Level;
            dataInfo.Auditor = userInfo.Name;
            dataInfo.Remark = importantInfoDto.Remark;
            this.CurrentUnitOfWork.SaveChanges();
            return true;
        }

        public async Task<PageData<View_BatchMaterialUsedReportDto>> LoadBatchMaterialUsedReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            ReportQueryConditonDto reportQueryConditonDto = where.Condition;
            reportQueryConditonDto.ParseTime();
            var query = _batchMaterialUsedRep.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(reportQueryConditonDto.SupplierBatchNumber), p => p.BatchNo == reportQueryConditonDto.SupplierBatchNumber)
                 .WhereIf(!string.IsNullOrEmpty(reportQueryConditonDto.MaterialNumber), p => p.MaterialNumber == reportQueryConditonDto.MaterialNumber)
                 .WhereIf(reportQueryConditonDto.StartDate != null, p => p.LastWarningTime >= reportQueryConditonDto.StartDate)
                 .WhereIf(reportQueryConditonDto.EndDate != null, p => p.LastWarningTime <= reportQueryConditonDto.EndDate)
                 .WhereIf(!string.IsNullOrEmpty(reportQueryConditonDto.KeyWord), p => p.BatchNo.Contains(reportQueryConditonDto.KeyWord));

            var pageData = new PageData<View_BatchMaterialUsedReportDto>()
            {
                List = ObjectMapper.Map<List<View_BatchMaterialUsedReportDto>>(await query.OrderByDescending(p => p.LastWarningTime)
                .ThenBy(p => p.IsOverUsed)
                .ThenBy(p => p.MaterialNumber)
                .ThenBy(p => p.BatchNo)
                .Skip(where.SkipCount).Take(where.PageSize).ToListAsync()),
                Total = await query.CountAsync(),
            };

            return pageData;
        }

        /// <summary>
        /// 前置物料售后销售报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<PageData<WorkProcessMaterialRecordDto>> LoadRepairedInputMaterial(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            ReportQueryConditonDto reportQueryConditonDto = where.Condition;
            reportQueryConditonDto.ParseTime();

            var processIds = _workProcessRep.GetAll().Where(p => p.WorkProcessType == WorkProcessTypeEnum.前置物料准备工序).Select(p => p.Id).ToList();
            var query = _recordMaterialRep.GetAll()
                .Where(p => processIds.Contains(p.WorkProcessId))// 前置物料工序
                .Where(p => p.IsRepairedInput == true)
                .WhereIf(reportQueryConditonDto.ProductLineId > 0, p => p.ProductLineId == reportQueryConditonDto.ProductLineId)
                .WhereIf(reportQueryConditonDto.MaterialId > 0, p => p.InputMaterilId == reportQueryConditonDto.MaterialId)
                .WhereIf(reportQueryConditonDto.WorkProcessId > 0, p => p.WorkProcessId == reportQueryConditonDto.WorkProcessId)
                .WhereIf(reportQueryConditonDto.StartDate != null, p => p.CreateTime >= reportQueryConditonDto.StartDate)
                .WhereIf(reportQueryConditonDto.EndDate != null, p => p.CreateTime <= reportQueryConditonDto.EndDate)
                .GroupBy(p => new { p.ProductLineId, p.WorkProcessId, p.WorkProcessName, p.InputMaterilId, p.InputMaterialName, p.InputMaterialNumber, p.InputUnitName, p.BOMUnitName })
                .Select(p => new WorkProcessMaterialRecordDto()
                {
                    ProductLineId = p.Key.ProductLineId,
                    InputMaterilId = p.Key.InputMaterilId,
                    InputMaterialName = p.Key.InputMaterialName,
                    InputMaterialNumber = p.Key.InputMaterialNumber,
                    WorkProcessId = p.Key.WorkProcessId,
                    WorkProcessName = p.Key.WorkProcessName,
                    InputUnitName = p.Key.InputUnitName,
                    InputMaterialCount = p.Sum(d => d.InputMaterialCount),
                    BOMMaterialCount = p.Sum(d => d.BOMMaterialCount),
                    BOMUnitName = p.Key.BOMUnitName
                });

            var pageData = new PageData<WorkProcessMaterialRecordDto>()
            {
                Total = await query.CountAsync(),
                List = query.Skip(where.SkipCount).Take(where.PageSize).OrderBy(p => p.InputMaterilId).ToList()
            };

            var prdouctLineIds = pageData.List.Select(p => p.ProductLineId).ToList();
            var productlineNames = _prodcutLineRep.GetAll().Where(p => prdouctLineIds.Contains(p.Id)).Select(p => new ProductLineDto()
            {
                ProductLineName = p.ProductLineName,
                Id = p.Id,
            });

            pageData.List.ForEach(p =>
            {
                p.ProductLineName = productlineNames.FirstOrDefault(d => d.Id == p.ProductLineId).ProductLineName;
            });

            return pageData;
        }

        public async Task<List<WorkOrderMaterilCostItem>> LoadKeyMaterilCostByWorkOrderNumberAsync(string workOrderNumber)
        {
            List<WorkOrderMaterilCostItem> result = new List<WorkOrderMaterilCostItem>();
            // 加载工单BOM信息
            var workOrderInfo = await _workOrderInfoRep.FirstOrDefaultAsync(p => p.OrderNumber == workOrderNumber);
            var processIds = _workProcessRep.GetAll().Where(p => p.WorkProcessType == WorkProcessTypeEnum.前置物料准备工序).Select(p => p.Id).ToList();
            if (workOrderInfo != null)
            {
                var data = _bomUnitManager.GetWorkOrderBomItems(workOrderInfo.WorkOrderBomId.GetValueOrDefault());
                var ScreenId = _materialCategoryManager.ScreenImportant(data.Select(p => p.InputMaterialId).ToList());
                var keyInfo = ObjectMapper.Map<List<BomItemDto>>(data.Where(p => ScreenId.Contains(p.InputMaterialId)).ToList());


                var staticCostInfo = _recordMaterialRep.GetAll()
                    .Where(p => p.OrderNumber == workOrderNumber && processIds.Contains(p.WorkProcessId) && p.IsRepairedInput == false)
                    .GroupBy(p => new { p.OrderNumber, p.InputMaterilId, p.InputUnitName, p.WorkProcessName })
                    .Select(p => new WorkProcessMaterialRecordDto()
                    {
                        OrderNumber = p.Key.OrderNumber,
                        InputMaterilId = p.Key.InputMaterilId,
                        InputMaterialCount = p.Sum(d => d.InputMaterialCount),
                        InputUnitName = p.Key.InputUnitName,
                        WorkProcessName = p.Key.WorkProcessName,
                        BOMMaterialCount = p.Sum(d => d.BOMMaterialCount)
                    }).ToList();

                //var staticCostInfo = _materialBatchNumberRep.GetAll()
                //     .Where(p => p.FromOrderNumber == workOrderNumber && p.CreateWorkStationName == "裁切工位")
                //     .GroupBy(p => new { p.FromOrderNumber, p.MaterialId, p.WrapUniteName })
                //     .Select(p => new MaterialBatchNumberDto()
                //     {
                //         FromOrderNumber = p.Key.FromOrderNumber,
                //         MaterialId = p.Key.MaterialId,
                //         MatrialCount = p.Sum(d => d.MatrialCount),
                //         WrapUniteName = p.Key.WrapUniteName,
                //         BOMMaterialCount = p.Sum(d => d.BOMMaterialCount)
                //     }).ToList();

                foreach (var item in keyInfo)
                {
                    var staticItem = staticCostInfo.Where(p => p.InputMaterilId == item.FormMaterialId).ToList();
                    if (staticItem == null)
                    {
                        staticItem = new List<WorkProcessMaterialRecordDto> { new WorkProcessMaterialRecordDto() { InputMaterialCount = 0, } };
                    }

                    foreach (var itemDetial in staticItem)
                    {
                        result.Add(new WorkOrderMaterilCostItem()
                        {
                            FormMaterialId = item.FormMaterialId,
                            FormMaterialName = item.FormMaterialName,
                            FormMaterialNumber = item.FormMaterialNumber,
                            WorkProcessName = itemDetial.WorkProcessName,
                            Specification = item.Specification,
                            WorkOrderCount = item.FormCount * workOrderInfo.ProduceCount,
                            UnitName = item.UnitName,
                            MatrialCount = itemDetial.InputMaterialCount.GetValueOrDefault(),
                            WrapUniteName = string.IsNullOrEmpty(itemDetial.InputUnitName) ? item.UnitName : itemDetial.InputUnitName,
                            BOMMaterialCount = item.UnitName == itemDetial.InputUnitName ? itemDetial.InputMaterialCount.GetValueOrDefault() : itemDetial.BOMMaterialCount.GetValueOrDefault(),
                            BOMUnitName = item.UnitName,
                            WorkOrderNumber = workOrderNumber,
                        });
                    }
                }
            }

            return result;
        }

        public async Task<List<StockDDExportDto>> LoadExportStockDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var conditon = where.Condition;
            conditon.ParseTime();
            var query = _viewddImportantInfosRep
                        .GetAll()
                        .WhereIf(conditon.StartDate != null, p => p.RecordDate >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.RecordDate <= conditon.EndDate)
                        .WhereIf(conditon.MaterialId != null, p => p.MaterialId == conditon.MaterialId)
                        .WhereIf(conditon.ProductLineId != null, p => p.BelongProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.Level != null && conditon.Level.Count > 0, p => p.Level != null && conditon.Level.Contains(p.Level.Value))
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.MaterialNumber))
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => WLDDbFunctionsExtension.JsonQuery(p.ExtensionData, "$.MaterialRecordInfos").Contains(conditon.SupplierBatchNumber))
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.BelongMaterialBatchNumber.Contains(conditon.KeyWord) || p.BelongOrderNumber.Contains(conditon.KeyWord) || p.ProjectNumber.Contains(conditon.KeyWord))
                        .WhereIf(conditon.IsInStock != null, p => p.IsInStock == conditon.IsInStock)
                        ;

            var reuslt = this.ObjectMapper.Map<List<StockDDExportDto>>(await query.ToListAsync());
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                reuslt.ForEach(p =>
                {
                    var byteData = QRCoder.BitmapByteQRCodeHelper.GetQRCode(p.BelongMaterialBatchNumber, QRCodeGenerator.ECCLevel.H, 20);
                    p.QrCodeImg = Convert.ToBase64String(byteData);
                });
            }

            return reuslt;
        }

        public async Task<PageData<ProductConstructMaterialInfoDto>> LoadProductConstructMaterialInfos(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var conditon = where.Condition;
            PageData<ProductConstructMaterialInfoDto> pageData = new PageData<ProductConstructMaterialInfoDto>();
            if (string.IsNullOrEmpty(conditon.SupplierBatchNumber))
            {
                throw new UserFriendlyException("请输入查询的物料批次号");
            }

            var query = _productConstructMaterialInfoRep
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => p.InputMaterialNumber == conditon.MaterialNumber)
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => p.BatchNo == conditon.SupplierBatchNumber)
                        .WhereIf(conditon.ProductLineId > 0, p => p.CurrentProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.WorkProcessId > 0, p => p.CurrentWorkProcessId == conditon.WorkProcessId)
                        .WhereIf(conditon.WorkStationId > 0, p => p.CurrentWorkStationId == conditon.WorkStationId);

            pageData.Total = query.Count();
            var reuslt = this.ObjectMapper.Map<List<ProductConstructMaterialInfoDto>>(await query.Skip(where.SkipCount).Take(where.PageSize).ToListAsync());

            pageData.List = reuslt;
            return pageData;
        }

        public async Task<List<ProductConstructMaterialInfoExportDto>> ExportProductConstructMaterialInfos(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var conditon = where.Condition;
            conditon.ParseTime();
            if (string.IsNullOrEmpty(conditon.SupplierBatchNumber))
            {
                throw new UserFriendlyException("请输入查询的物料批次号");
            }

            var query = _productConstructMaterialInfoRep
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => p.InputMaterialNumber == conditon.MaterialNumber)
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => p.BatchNo == conditon.SupplierBatchNumber)
                        .WhereIf(conditon.WorkProcessId > 0, p => p.CurrentWorkProcessId == conditon.WorkProcessId)
                        .WhereIf(conditon.ProductLineId > 0, p => p.CurrentProductLineId == conditon.ProductLineId)
                        .WhereIf(conditon.WorkStationId > 0, p => p.CurrentWorkStationId == conditon.WorkStationId);

            var reuslt = this.ObjectMapper.Map<List<ProductConstructMaterialInfoExportDto>>(await query.ToListAsync());

            return reuslt;
        }

        public async Task<PageData<PrepareUserWorkStaticDto>> LoadPrepareUserWorkStaticAsync(JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where)
        {
            var condition = where.Condition as PrepareUserWorkStaticQueryCondtionDto;
            condition.ParseTime();

            var query = _prePareUserWorkStaticRep.GetAll()
                .WhereIf(condition.StartDate != null, p => p.CreationDate >= condition.StartDate)
                .WhereIf(condition.EndDate != null, p => p.CreationDate < condition.EndDate)
                .WhereIf(condition.KeyWord != null, p => p.OperatorName.Contains(condition.KeyWord))
                .WhereIf(condition.OperatorId > 0, p => p.OperatorId == condition.OperatorId)
                .WhereIf(condition.OrgId != null && condition.OrgId.Count > 0, p => condition.OrgId.Contains(p.OrgId));

            query = query.GroupBy(p => new { p.OperatorId, p.OperatorName, p.MaterialName, p.MaterialNumber, p.OrgId, p.OrgName, p.WrapUniteName, p.WorkStationName }).Select(p => new View_PrepareUserWorkStatic()
            {
                OperatorId = p.Key.OperatorId,
                OperatorName = p.Key.OperatorName,
                MaterialName = p.Key.MaterialName,
                MaterialNumber = p.Key.MaterialNumber,
                WorkStationName = p.Key.WorkStationName,
                OrgId = p.Key.OrgId,
                OrgName = p.Key.OrgName,
                WrapUniteName = p.Key.WrapUniteName,
                MatrialCount = p.Sum(p => p.MatrialCount),
            });

            query = query.OrderBy(p => p.OperatorId).ThenBy(p => p.OrgId).ThenBy(p => p.MaterialNumber);
            PageData<PrepareUserWorkStaticDto> result = new PageData<PrepareUserWorkStaticDto>()
            {
                List = ObjectMapper.Map<List<PrepareUserWorkStaticDto>>(await query.Skip(where.SkipCount).Take(where.PageSize).ToListAsync()),
                Total = query.Count()
            };


            return result;
        }

        public async Task<PageData<DDTestDayKPIDto>> LoadDDTestDayKPIAsync(JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where)
        {
            var condition = where.Condition as PrepareUserWorkStaticQueryCondtionDto;
            condition.ParseTime();
            var query = _DDTestDayKPIRep.GetAll()
                .WhereIf(condition.StartDate != null, p => p.StaticDate >= condition.StartDate)
                .WhereIf(condition.EndDate != null, p => p.StaticDate < condition.EndDate)
                .WhereIf(condition.KeyWord != null, p => p.OperatorName.Contains(condition.KeyWord))
                .WhereIf(condition.OperatorId > 0, p => p.OperatorId == condition.OperatorId);

            query = query.GroupBy(p => new { p.OperatorId, p.OperatorName, p.MaterialName, p.MaterialNumber }).Select(p => new View_DDTestDayKPI()
            {
                OperatorId = p.Key.OperatorId,
                OperatorName = p.Key.OperatorName,
                MaterialName = p.Key.MaterialName,
                MaterialNumber = p.Key.MaterialNumber,
                TestAmounts = p.Sum(p => p.TestAmounts),
                TestCount = p.Sum(p => p.TestCount),
                TestDDCount = p.Sum(p => p.TestDDCount)
            });

            query = query.OrderBy(p => p.OperatorId).ThenBy(p => p.TestAmounts);
            PageData<DDTestDayKPIDto> result = new PageData<DDTestDayKPIDto>()
            {
                List = ObjectMapper.Map<List<DDTestDayKPIDto>>(await query.Skip(where.SkipCount).Take(where.PageSize).ToListAsync()),
                Total = query.Count()
            };

            return result;

        }

        public PageData<DDWeekOnePassRateReportDto> LoadDDWeekOnePassRateReport(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var condition = where.Condition as ReportQueryConditonDto;
            condition.ParseTime();
            List<DDWeekOnePassRateReport> result = _reportRepository.QueryDDWeekOnePassRateReport(condition.StartDate.Value, condition.EndDate.Value, condition.WorkProcessId.GetValueOrDefault());
            PageData<DDWeekOnePassRateReportDto> pageData = new PageData<DDWeekOnePassRateReportDto>()
            {
                List = ObjectMapper.Map<List<DDWeekOnePassRateReportDto>>(result),
                Total = result.Count()
            };

            return pageData;
        }

        public PageData<OrgProductProcessWorkLoadReportDto> LoadOrgProductProcessWorkLoadReport(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var condition = where.Condition as ReportQueryConditonDto;
            condition.ParseTime();
            List<OrgProductProcessWorkLoadReport> result = _reportRepository.QueryOrgProductProcessWorkLoadReport(condition.StartDate.Value, condition.EndDate.Value, condition.WorkProcessId.GetValueOrDefault());
            PageData<OrgProductProcessWorkLoadReportDto> pageData = new PageData<OrgProductProcessWorkLoadReportDto>()
            {
                List = ObjectMapper.Map<List<OrgProductProcessWorkLoadReportDto>>(result),
                Total = result.Count()
            };

            return pageData;
        }

        public async Task<List<WorkOrderFinishedInfoDto>> LoadWorkOrderFinishedInfoAsync(ReportQueryConditonDto where)
        {
            using (UnitOfWorkManager.Current.SetTenantId(1))
            {
                var query = _workOrderInfoRep.GetAll()
                    .WhereIf(where.ProductLineId > 0, p => p.ProduceLineId == where.ProductLineId)
                    .Where(p => p.WorkOrderStatu == WorkOrderStatuEnum.生产中)
                    ;
                var result = await query.GroupBy(p => p.ProduceLineId).Select(p => new WorkOrderFinishedInfoDto()
                {
                    FinishedCount = p.Sum(d => d.FinishedCount),
                    ProduceCount = p.Sum(d => d.ProduceCount),
                    ProductLineId = p.Key
                }).ToListAsync();

                return result;
            }
        }

        public async Task<List<ProductLineCapacityYearReportRecord>> QueryProductLineYearStaticReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {

            return await _reportDapperRepository.QueryProductLineCapacityYearReportRecord(where.Condition.StartDate, where.Condition.EndDate, where.Condition.ProductLineId);
        }


        public async Task<PageData<MaterialDiscardRecordDTO>> LoadMaterialDiscardRecordReportAsync(JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where)
        {
            PageData<MaterialDiscardRecordDTO> pagedResult = new PageData<MaterialDiscardRecordDTO>();
            IQueryable<View_MaterialDiscardRecord> queryInfo = CreateDiscardMaterialQuery(where);
            pagedResult.Total = await queryInfo.CountAsync();
            var dataResult = queryInfo.OrderByDescending(p => p.RecordDate).Skip(where.SkipCount).Take(where.PageSize).ToList();
            pagedResult.List = ObjectMapper.Map<List<MaterialDiscardRecordDTO>>(dataResult);

            return pagedResult;
        }

        public async Task<List<MaterialDiscardRecordExportDTO>> ExportMaterialDiscardRecordReportAsync(JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where)
        {
            List<MaterialDiscardRecordExportDTO> pagedResult = new List<MaterialDiscardRecordExportDTO>();
            IQueryable<View_MaterialDiscardRecord> queryInfo = CreateDiscardMaterialQuery(where);

            pagedResult = ObjectMapper.Map<List<MaterialDiscardRecordExportDTO>>(await queryInfo.ToListAsync());

            return pagedResult;
        }

        private IQueryable<View_MaterialDiscardRecord> CreateDiscardMaterialQuery(JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where)
        {
            var condition = where.Condition as DiscardRecordReportCondtionDto;
            condition.ParseTime();
            var queryInfo = _viewMaterialDiscardRep.GetAll()
                  .WhereIf(!string.IsNullOrEmpty(condition.MaterialNumber), p => p.MaterialNumber == condition.MaterialNumber)
                  .WhereIf(condition.WorkProcessId > 0, p => p.BelongWorkProcessId == condition.WorkProcessId)
                  .WhereIf(condition.ProductLineId > 0, p => p.BelongProductLineId == condition.ProductLineId)
                  .WhereIf(condition.DiscardType != null, p => p.DiscardType == condition.DiscardType)
                  .WhereIf(condition.StartDate != null, p => p.RecordDate >= condition.StartDate)
                  .WhereIf(condition.EndDate != null, p => p.RecordDate <= condition.EndDate)
                  .WhereIf(!string.IsNullOrEmpty(condition.KeyWord), p => p.Supplier.Contains(condition.KeyWord) || p.WorkOrderNumber.Contains(condition.KeyWord))
                  ;
            return queryInfo;
        }

        public async Task<PageData<ERPInStockInfoOperateRecordDTO>> LoadBatchOperatorRecord(JHTPageAjaxResquest<ReportQueryConditonDto> where)
        {
            var conditon = where.Condition;
            PageData<ERPInStockInfoOperateRecordDTO> pageData = new PageData<ERPInStockInfoOperateRecordDTO>();
            conditon.ParseTime();

            var query = _instockOperateRecord
                        .GetAll()
                        .WhereIf(!string.IsNullOrEmpty(conditon.MaterialNumber), p => p.MaterialNumber == conditon.MaterialNumber)
                        .WhereIf(!string.IsNullOrEmpty(conditon.SupplierBatchNumber), p => p.BatchNo == conditon.SupplierBatchNumber)
                        .WhereIf(conditon.StartDate != null, p => p.OperateTime >= conditon.StartDate)
                        .WhereIf(conditon.EndDate != null, p => p.OperateTime <= conditon.EndDate)
                        .WhereIf(!string.IsNullOrEmpty(conditon.KeyWord), p => p.Operator.Contains(conditon.KeyWord));

            pageData.Total = query.Count();
            var reuslt = this.ObjectMapper.Map<List<ERPInStockInfoOperateRecordDTO>>(await query.OrderByDescending(p => p.Id).Skip(where.SkipCount).Take(where.PageSize).ToListAsync());

            pageData.List = reuslt;
            return pageData;
        }

        public class ProductQuantity
        {
            public int CancalNum { get; set; }
            public int ClosedNum { get; set; }
            public int StartNum { get; set; }
            public int ProduceNum { get; set; }
            public int NotStartedNum { get; set; }
        }
    }
}

