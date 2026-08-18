using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report
{
    public interface IReportRepository
    {
        Task<List<long>> GetTodayProductLineMaterialAsync(DateTime staticDate, string materialNumber = "D02.001");

        /// <summary>
        /// 工序产能统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        Task BuildWorkProcessCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001");

        /// <summary>
        /// 产线统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        Task BuildProductLineCapacityDailyReportAsync(DateTime staticDate, long materialId, long firstWorkProcessId, long lastWorkProcessId);


        /// <summary>
        /// 工序质量统计报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        Task BuildWorkProcessProblemDailyReportAsync(DateTime staticDate);

        /// <summary>
        /// 工序一次性通过率报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <param name="materialNumber"></param>
        /// <returns></returns>
        Task BuildWorkProcessOnePassRateReportAsync(DateTime staticDate, string materialNumber);

        /// <summary>
        /// 前置物料报表
        /// </summary>
        /// <param name="staticDate"></param>
        /// <returns></returns>
        Task BuildPrepaireWorkProcessDayReportsAsync(DateTime staticDate);
        List<PrepaireWorkProcessDayReport> QueryToadyPadPrepaireWorkProcessReport(DateTime staticDate);
        List<WorkProcessCapacityDailyReportRecord> QueryToadyPadWorkProcessCapacityReport(DateTime staticDate);


        List<DDWeekOnePassRateReport> QueryDDWeekOnePassRateReport(DateTime startDate, DateTime endDate, long workProcessId = 19);
        List<OrgProductProcessWorkLoadReport> QueryOrgProductProcessWorkLoadReport(DateTime startDate, DateTime endDate, long workProcessId);

       
    }
}
