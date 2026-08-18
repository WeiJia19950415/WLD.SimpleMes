using Abp.Application.Services.Dto;
using JHT.Abp.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.DTO;
using WLD.SimpleMes.DynamicForms.DTO;
using WLD.SimpleMes.QualityControl.Dto;
using WLD.SimpleMes.Report.Dto;
using WLD.SimpleMes.WorkOrder.DTO;
using WLD.SimpleMes.WorkProcess.Dto;
using static WLD.SimpleMes.Report.ReportAppService;

namespace WLD.SimpleMes.Report
{
    public interface IReportAppService
    {
        /// <summary>
        /// 生成工位统计报表
        /// </summary>
        /// <param name="staticDate">统计日期</param>
        /// <param name="materialNumber">物料编码</param>
        Task BuildWorkProcessCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001");

        SNInStockInfoDto LoadERPInStockInfo(string snNumber);
        /// <summary>
        /// 生成产线统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        Task BuildProductLineCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001");


        /// <summary>
        /// 加载生产看板-前置物料统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        List<PrepaireWorkProcessDayReportDto> LoadPadPrepaireWorkProcessReport(DateTime staticDate);


        /// <summary>
        /// 工序质量统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        Task BuildWorkProcessProblemDailyReportAsync(DateTime staticDate);


        /// <summary>
        /// 生成前置物料准备工序报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        Task BuildPrepaireWorkProcessDayReportsAsync(DateTime staticDate);

        /// <summary>
        /// 工序一次性通过率报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        Task BuildWorkProcessOnePassRateReportAsync(DateTime staticDate, string materialNumber = "D02.001");
        List<WorkProcessCapacityDailyReportRecordDto> LoadDayWorkProcessReport(ReportQueryConditonDto reportQueryConditonDto);

        /// <summary>
        /// 查询统计记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<WorkProcessCapacityDailyReportRecordDto>> LoadStationCacityDailyRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        List<DayWorkProcessProblemStaticsDto> LoadDayWorkProcessProblemStatics(ReportQueryConditonDto reportQueryConditon);


        /// <summary>
        /// 查询汇总统计记录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<WorkProcessCapacityStaticReportDto>> LoadStationCacityStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 查询产线统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<ProductLineCapacityDailyReportRecordDto>> LoadProductLineCacityStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);


        /// <summary>
        /// 查询工序问题分类统计
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<WorkProcessProblemDailyReportRecordDto>> LoadWorkProcessProblemStaticRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        List<DayProblemStaticsDto> LoadDayProblemStatics(ReportQueryConditonDto reportQueryConditon);

        /// <summary>
        /// 查询电堆关键性能数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<DDImportantInfoDto>> LoadDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 查询工序一次性通过率报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<WorkProcessOnePassRateReportDto>> LoadWorkProcessOnePassRateReportRecordsAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);


        /// <summary>
        /// 获取工序加工平均时长统计
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        List<WorkProcessAvgTimeStaticsDto> LoadDayWorkProcessAvgTimeStatics(ReportQueryConditonDto reportQueryConditon);


        /// <summary>
        /// 查询前置物料准备工序统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<PrepaireWorkProcessDayReportDto>> LoadPrepaireWorkProcessDayReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);


        /// <summary>
        /// 加载大屏产品的产能统计报表
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        List<ProductLineCapacityDailyReportRecordDto> LoadProductLineCacityStaticData(ReportQueryConditonDto reportQueryConditon);


        /// <summary>
        /// 查询电堆当前状态信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<OrderMaterialProduceStatuDto>> LoadOrderMaterialProduceStatuReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        Task<List<OrderMaterialProduceStatuExportDto>> LoadOrderMaterialProduceStatuExportReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        /// <summary>
        /// 获取生产概况的简要数据信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<ProductSummaryDto> LoadProductSummaryReportAsync(ReportQueryConditonDto where);
        List<ProductLineCapacityDailyReportRecordDto> LoadProductCategoryOutputConfig(ReportQueryConditonDto reportQueryConditon);
        ProductQuantityDto LoadWorkOrderCompletionStatus(ReportQueryConditonDto reportQueryConditon);
        ProblemRecordStaticDto LoadQulitityProblemDealedStatic(ReportQueryConditonDto reportQueryConditon);

        /// <summary>
        /// 加载各工序停留产品数量信息
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        List<WorkProcessCapacityDailyReportRecordDto> LoadWorkProcessStayProductStatic(ReportQueryConditonDto reportQueryConditon);

        /// <summary>
        /// 各工序加工进度
        /// </summary>
        /// <param name="reportQueryConditon"></param>
        /// <returns></returns>
        ProblemDealedStaticDto DefectHandlingProgressStatus(ReportQueryConditonDto reportQueryConditon);

        /// <summary>
        /// 导出电堆测试信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        List<DDImportantInfoExportDto> LoadExportDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 导出电堆关键测试报告信息
        /// </summary>
        /// <param name="productBatchNumber"></param>
        /// <returns></returns>
        Task<DDImportantInfoWordExportDto> LoadExportDDImportantInfosAsync(long id);

        /// <summary>
        /// 审核电堆关键信息
        /// </summary>
        /// <param name="importantInfoDto"></param>
        /// <returns></returns>
        public Task<bool> AuidtDDImportantInfoAsync(DDImportantInfoDto importantInfoDto);
        Task<DDImportantInfoDto> LoadDDImportantInfoAsync(EntityDto<string> snInfo);


        Task<PageData<View_BatchMaterialUsedReportDto>> LoadBatchMaterialUsedReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 加载关键物料使用信息
        /// </summary>
        /// <param name="workOrderNumber">工单编码</param>
        /// <returns></returns>
        public Task<List<WorkOrderMaterilCostItem>> LoadKeyMaterilCostByWorkOrderNumberAsync(string workOrderNumber);

        /// <summary>
        /// 加载电堆入库导出信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<StockDDExportDto>> LoadExportStockDDImportantInfosAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 根据物料批次号信息追溯电堆信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<ProductConstructMaterialInfoDto>> LoadProductConstructMaterialInfos(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 根据物料批次号信息追溯导出电堆信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<ProductConstructMaterialInfoExportDto>> ExportProductConstructMaterialInfos(JHTPageAjaxResquest<ReportQueryConditonDto> where);

        /// <summary>
        /// 统计前置人员的绩效情况
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageData<PrepareUserWorkStaticDto>> LoadPrepareUserWorkStaticAsync(JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where);

        Task<PageData<DDTestDayKPIDto>> LoadDDTestDayKPIAsync(JHTPageAjaxResquest<PrepareUserWorkStaticQueryCondtionDto> where);


        PageData<DDWeekOnePassRateReportDto> LoadDDWeekOnePassRateReport(JHTPageAjaxResquest<ReportQueryConditonDto> where);


        PageData<OrgProductProcessWorkLoadReportDto> LoadOrgProductProcessWorkLoadReport(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        Task<List<WorkOrderFinishedInfoDto>> LoadWorkOrderFinishedInfoAsync(ReportQueryConditonDto where);
        Task<PageData<WorkProcessMaterialRecordDto>> LoadRepairedInputMaterial(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        Task<List<ProductLineCapacityYearReportRecord>> QueryProductLineYearStaticReportAsync(JHTPageAjaxResquest<ReportQueryConditonDto> where);
        Task<List<MaterialDiscardRecordExportDTO>> ExportMaterialDiscardRecordReportAsync(JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where);
        Task<PageData<MaterialDiscardRecordDTO>> LoadMaterialDiscardRecordReportAsync(JHTPageAjaxResquest<DiscardRecordReportCondtionDto> where);
        Task<PageData<ERPInStockInfoOperateRecordDTO>> LoadBatchOperatorRecord(JHTPageAjaxResquest<ReportQueryConditonDto> where);
    }
}
