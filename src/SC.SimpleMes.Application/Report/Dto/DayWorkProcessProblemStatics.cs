using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class DayWorkProcessProblemStaticsDto
    {
        public string WorkProcess { get; set; }

        public string ProductLineName { get; set; }

        public decimal ProblemCount { get; set; }
    }
}
