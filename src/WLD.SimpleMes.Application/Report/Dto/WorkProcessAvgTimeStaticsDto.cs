using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    public class WorkProcessAvgTimeStaticsDto
    {
        public string ProductLineName { get; set; }

        public string WorkProcessName { get; set; }

        public decimal CostSeconds { get; set; }

        public decimal CostMinutes
        {
            get
            {
                return CostSeconds / 60;
            }
        }
    }
}
