using Abp.Dapper.Repositories;
using Abp.Data;
using Abp.Domain.Uow;
using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.K3DBInfo;
using SC.SimpleMes.Report;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.EntityFrameworkCore.Repositories
{
    public class ReportDapperRepository : DapperEfRepositoryBase<ReportDbContext, WorkProcessCapacityDailyReportRecord, long>, IReportDapperRepository
    {
        public ReportDapperRepository(IActiveTransactionProvider activeTransactionProvider, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(activeTransactionProvider, currentUnitOfWorkProvider)
        {
        }

        public async Task<List<ProductLineCapacityYearReportRecord>> QueryProductLineCapacityYearReportRecord(DateTime? startDate, DateTime? endDate, long? productLineId)
        {
            // 按月进行统计
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select  [ProductLineId],StaticMonth,StaticYear,Sum(InputCount) as InputCount,SUM(FinishedCount) as FinishedCount from ");
            stringBuilder.Append(" ( SELECT [ProductLineId],InputCount,FinishedCount,DATEPART(MONTH,[StaticDate]) as StaticMonth,DATEPART(Year,[StaticDate]) as StaticYear ");
            stringBuilder.Append(" FROM [ProductLineCapacityDailyReportRecords] where StaticDate>=@startDate and StaticDate<=@endDate ");
            if (productLineId > 0)
            {
                stringBuilder.Append(" and ProductLineId=@productLineId");
            }

            stringBuilder.Append(" ) as T group by ProductLineId,StaticMonth,StaticYear order by StaticYear,StaticMonth");

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@startDate", startDate.GetValueOrDefault());
            dynamicParameters.Add("@endDate", endDate.GetValueOrDefault());
            dynamicParameters.Add("@productLineId", productLineId.GetValueOrDefault());

            return (await this.GetConnection().QueryAsync<ProductLineCapacityYearReportRecord>(stringBuilder.ToString(), dynamicParameters, this.GetActiveTransaction())).ToList();
        }
    }
}
