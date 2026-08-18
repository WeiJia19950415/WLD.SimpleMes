using Abp.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report
{
    public interface IReportDapperRepository : IDapperRepository<WorkProcessCapacityDailyReportRecord, long>
    {
        Task<List<ProductLineCapacityYearReportRecord>> QueryProductLineCapacityYearReportRecord(DateTime? startDate, DateTime? endDate, long? productLineId);
    }
}
