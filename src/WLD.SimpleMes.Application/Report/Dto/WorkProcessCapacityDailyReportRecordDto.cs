using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    public class WorkProcessCapacityDailyReportRecordDto : EntityDto<long>
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
