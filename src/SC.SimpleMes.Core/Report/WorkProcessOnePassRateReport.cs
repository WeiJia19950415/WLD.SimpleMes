using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report
{
    public class WorkProcessOnePassRateReport : Entity<long>
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


        public decimal ExpectionCount { get; set; }

        public decimal FinishedCount { get; set; }

        /// <summary>
        /// 一次性通过率
        /// </summary>
        public decimal OnePassReate { get; set; }

        public DateTime StaticDate { get; set; }

        public DateTime DataDate { get; set; }
    }
}
