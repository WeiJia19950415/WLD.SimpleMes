using Abp.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Report;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    public class ReportRepository : ReportRepositoryBase<WorkProcessCapacityDailyReportRecord, long>, IReportRepository
    {
        public ReportRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<long>> GetTodayProductLineMaterialAsync(DateTime staticDate, string materialNumber)
        {
            var startDate = staticDate.Date;
            return await this.GetAll()
                .Where(p => p.StaticDate == startDate)
                .WhereIf(!string.IsNullOrEmpty(materialNumber), p => p.MaterialNumber.StartsWith(materialNumber))
                .GroupBy(p => p.MaterialId).Select(p => p.Key).ToListAsync();
        }

        public async Task BuildWorkProcessCapacityDailyReportAsync(DateTime staticDate, string materialNumber = "D02.001%")
        {

            var startDate = staticDate.Date;
            var endDate = startDate.AddDays(1).Date;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = startDate.ToString("yyyy-MM-dd"),
                ParameterName = "StartDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = endDate.ToString("yyyy-MM-dd"),
                ParameterName = "EndDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = materialNumber,
                ParameterName = "MaterialNumber"
            });

            // 支持数据重跑
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"delete from WorkProcessCapacityDailyReportRecord where StaticDate=@StartDate and MaterialNumber like @MaterialNumber", sqlParameters);
            // 支持插入数据
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"exec PROC_WorkProcessCapacityDailyReportRecord @StartDate,@EndDate,@MaterialNumber", sqlParameters);
        }


        public async Task BuildWorkProcessProblemDailyReportAsync(DateTime staticDate)
        {

            var startDate = staticDate.Date;
            var endDate = startDate.AddDays(1).Date;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = startDate.ToString("yyyy-MM-dd"),
                ParameterName = "StartDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = endDate.ToString("yyyy-MM-dd"),
                ParameterName = "EndDate"
            });

            // 支持数据重跑
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"delete from WorkProcessProblemDailyReportRecords where StaticDate=@StartDate", sqlParameters);
            // 支持插入数据
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"exec PROC_BuildProcessProblemDailyReport @StartDate,@EndDate", sqlParameters);
        }


        public async Task BuildPrepaireWorkProcessDayReportsAsync(DateTime staticDate)
        {

            var startDate = staticDate.Date;
            var endDate = startDate.AddDays(1).Date;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = startDate.ToString("yyyy-MM-dd"),
                ParameterName = "StartDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = endDate.ToString("yyyy-MM-dd"),
                ParameterName = "EndDate"
            });

            // 支持数据重跑
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"delete from PrepaireWorkProcessDayReports where StaticDate=@StartDate", sqlParameters);
            // 支持插入数据
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"exec PROC_PrepaireMaterialCount @StartDate,@EndDate", sqlParameters);
        }

        public async Task BuildProductLineCapacityDailyReportAsync(DateTime staticDate, long materialId, long firstWorkProcessId, long lastWorkProcessId)
        {
            var todayInfos = this.GetAll().Where(p => p.MaterialId == materialId && p.StaticDate == staticDate && (p.WorkProcessId == firstWorkProcessId || p.WorkProcessId == lastWorkProcessId)).ToList();
            var productLines = todayInfos.Select(p => p.ProductLineId).Distinct().ToList();
            List<ProductLineCapacityDailyReportRecord> productLinesRecords = new List<ProductLineCapacityDailyReportRecord>();
            foreach (var productLine in productLines)
            {
                var productLineInfo = todayInfos.FirstOrDefault(p => (p.WorkProcessId == firstWorkProcessId || p.WorkProcessId == lastWorkProcessId) && p.ProductLineId == productLine);
                var firstWorkProcess = todayInfos.FirstOrDefault(p => p.WorkProcessId == firstWorkProcessId && p.ProductLineId == productLine);
                var productLineInputCount = firstWorkProcess == null ? 0 : firstWorkProcess.InputCount;
                var lastWorkProcess = todayInfos.FirstOrDefault(p => p.WorkProcessId == lastWorkProcessId && p.ProductLineId == productLine);
                var prodcutLineFinisheCount = lastWorkProcess == null ? 0 : lastWorkProcess.FinishedCount;
                productLinesRecords.Add(new ProductLineCapacityDailyReportRecord()
                {
                    ProductLineId = productLine,
                    ProductLineName = productLineInfo.ProductLineName,
                    DataDate = DateTime.Now.Date,
                    StaticDate = productLineInfo.StaticDate,
                    FinishedCount = prodcutLineFinisheCount,
                    InputCount = productLineInputCount,
                    MaterialId = productLineInfo.MaterialId,
                    MaterialName = productLineInfo.MaterialName,
                    MaterialNumber = productLineInfo.MaterialNumber,
                });
            }

            await this.GetDbContext().Database.ExecuteSqlRawAsync($" delete from ProductLineCapacityDailyReportRecords where StaticDate='{staticDate.ToString("yyyy-MM-dd")}' and materialId={materialId}");
            await this.GetContext().ProductLineCapacityDailyReportRecords.AddRangeAsync(productLinesRecords);
        }

        public async Task BuildWorkProcessOnePassRateReportAsync(DateTime staticDate, string materialNumber)
        {
            var startDate = staticDate.Date;
            var endDate = startDate.AddDays(1).Date;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = startDate.ToString("yyyy-MM-dd"),
                ParameterName = "StartDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = endDate.ToString("yyyy-MM-dd"),
                ParameterName = "EndDate"
            });
            sqlParameters.Add(new SqlParameter()
            {
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = materialNumber,
                ParameterName = "MaterialNumber"
            });

            // 支持数据重跑
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"delete from WorkProcessOnePassRateReports where StaticDate=@StartDate and MaterialNumber like @MaterialNumber", sqlParameters);
            // 支持插入数据
            await this.GetDbContext().Database.ExecuteSqlRawAsync($"exec PROC_WorkProcessOnePassRateReportRecord @StartDate,@EndDate,@MaterialNumber", sqlParameters);
        }


        public List<PrepaireWorkProcessDayReport> QueryToadyPadPrepaireWorkProcessReport(DateTime staticDate)
        {
            var startDate = staticDate.ToString("yyyy-MM-dd");
            var endDate = staticDate.AddDays(1).Date.ToString("yyyy-MM-dd");
            string excuteSql = $"exec [PROC_GetTodayPrepareWorkProcesReport] '{startDate}','{endDate}'";
            return this.GetContext().PrepaireWorkProcessDayReports
                .FromSqlRaw(excuteSql)
                .ToList();
        }

        public List<WorkProcessCapacityDailyReportRecord> QueryToadyPadWorkProcessCapacityReport(DateTime staticDate)
        {
            var startDate = staticDate.ToString("yyyy-MM-dd");
            var endDate = staticDate.AddDays(1).Date.ToString("yyyy-MM-dd");
            string excuteSql = $"exec [PROC_GetTodayWorkProcessCapacityReport] '{startDate}','{endDate}'";
            return this.GetContext().WorkProcessCapacityDailyReportRecord
                .FromSqlRaw(excuteSql)
                .ToList();
        }

        public List<DDWeekOnePassRateReport> QueryDDWeekOnePassRateReport(DateTime startDate,DateTime endDate,long workProcessId)
        {
            var startDateString = startDate.ToString("yyyy-MM-dd");
            var endDateString = endDate.Date.ToString("yyyy-MM-dd");
            string excuteSql = $"exec [Proc_StaticOnePassRate] '{startDateString}','{endDateString}',{workProcessId}";
            return this.GetContext().DDWeekOnePassRateReports
                .FromSqlRaw(excuteSql)
                .ToList();
        }

        public List<OrgProductProcessWorkLoadReport> QueryOrgProductProcessWorkLoadReport(DateTime startDate, DateTime endDate, long workProcessId)
        {
            var startDateString = startDate.ToString("yyyy-MM-dd");
            var endDateString = endDate.Date.ToString("yyyy-MM-dd");
            string excuteSql = $"exec [Proc_OrgProductProcessWorkLoad] '{startDateString}','{endDateString}',{workProcessId}";
            return this.GetContext().OrgProductProcessWorkLoadReports
                .FromSqlRaw(excuteSql)
                .ToList();
        }

      
    }
}
