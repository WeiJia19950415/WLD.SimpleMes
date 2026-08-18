using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report
{
    /// <summary>
    /// 产线一次性通过率报表
    /// </summary>
    public class ProductLineOnePassRateReport : Entity<long>
    {
        public long MaterialId { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public long ProductLineId { get; set; }
        public string ProductLineName { get; set; }

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
