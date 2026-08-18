using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class WorkOrderStatuStaticDto
    {
        public int ProducingCount { get; set; }
         public int IssuedCount { get; set; }
        public int CancelCount { get; set; }
        public int ClosedCount { get; set; }
        public int PauseCount { get; set; }
    }
}
