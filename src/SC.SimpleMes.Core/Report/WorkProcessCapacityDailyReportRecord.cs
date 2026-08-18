using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report
{
    /// <summary>
    /// 工位产能统计报表
    /// </summary>
    public class WorkProcessCapacityDailyReportRecord : Entity<long>
    {
        public long MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public long ProductLineId { get; set; }
        public string ProductLineName { get; set; }
        
        public long WorkStationId { get; set; }

        public string WorkStationName { get; set; }

        public long WorkProcessId { get; set; }

        public string WorkProcessName { get; set; }
        public decimal InputCount { get; set; }

        public decimal FinishedCount { get; set; }

        public DateTime StaticDate { get; set; }

        public DateTime DataDate { get; set; }
    }
}
