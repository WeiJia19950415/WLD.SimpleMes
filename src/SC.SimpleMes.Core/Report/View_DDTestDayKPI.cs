using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report
{
    public class View_DDTestDayKPI : Entity<long>
    {
        public long OperatorId { get; set; }

        public string OperatorName { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public decimal TestCount { get; set; }

        public decimal TestAmounts { get; set; }

        public decimal TestDDCount { get; set; }

        public DateTime StaticDate { get; set; }
    }
}
