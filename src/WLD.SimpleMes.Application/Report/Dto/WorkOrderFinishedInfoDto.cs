using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    public class WorkOrderFinishedInfoDto
    {
        public decimal ProduceCount { get; set; }

        public decimal FinishedCount { get; set; }

        public long? ProductLineId { get; set; }

        public decimal FinishedRate
        {
            get
            {
                return FinishedCount / ProduceCount;
            }
        }
    }
}
