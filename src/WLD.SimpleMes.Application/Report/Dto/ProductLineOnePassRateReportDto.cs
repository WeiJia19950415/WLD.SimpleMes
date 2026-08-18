using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    public class ProductLineOnePassRateReportDto : EntityDto<long>
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
