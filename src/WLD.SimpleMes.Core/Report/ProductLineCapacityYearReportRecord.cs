using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report
{
    public class ProductLineCapacityYearReportRecord
    {
        public long ProductLineId { get; set; }
        public decimal InputCount { get; set; }

        public decimal FinishedCount { get; set; }

        public int StaticMonth { get; set; }
        
        public int StaticYear { get; set; }

    }
}
