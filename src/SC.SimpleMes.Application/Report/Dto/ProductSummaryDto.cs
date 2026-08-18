using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.Report.Dto
{
    public class ProductSummaryDto
    {
        public int InputCount { get; set; }

        public string InputCountString
        {
            get
            {
                return InputCount.ToString().PadLeft(4, '0');
            }
        }

        public int OutputCount { get; set; }

        public string OutputCountString
        {
            get
            {
                return OutputCount.ToString().PadLeft(4, '0');
            }
        }

        public int ExceptionCount { get; set; }

        public string ExceptionCountString
        {
            get
            {
                return ExceptionCount.ToString().PadLeft(4, '0');
            }
        }
        public int ProducingCount { get; set; }

        public string ProducingCountString
        {
            get
            {
                return ProducingCount.ToString().PadLeft(4, '0');
            }
        }

        public int QulityProblemCount { get; set; }

        public string QulityProblemCountString
        {
            get
            {
                return QulityProblemCount.ToString().PadLeft(4, '0');
            }
        }

        public int ScrapCount
        {
            get; set;
        }

        public string ScrapCountString
        {
            get
            {
                return ScrapCount.ToString().PadLeft(4, '0');
            }
        }

        public int IssuedCount
        {
            get; set;
        }

        public string IssuedCountString
        {
            get
            {
                return IssuedCount.ToString().PadLeft(4, '0');
            }
        }
    }
}
