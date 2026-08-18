using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{

    public class WorkProcessOnePassRateReportDto : EntityDto<long>
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

        public string OnePassReateString
        {
            get
            {
                return string.Format($"{OnePassReate * 100}%");
            }
        }


  
        public DateTime StaticDate { get; set; }

        public DateTime DataDate { get; set; }
    }
}
