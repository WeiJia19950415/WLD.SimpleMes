using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class WorkProcessCapacityStaticReportDto : EntityDto<string>
    {
        public new string Id
        {
            get
            {
                return $"{this.MaterialId}_{this.ProductLineId}_{this.WorkStationId}_{this.WorkProcessId}";
            }
        }

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
    }
}
