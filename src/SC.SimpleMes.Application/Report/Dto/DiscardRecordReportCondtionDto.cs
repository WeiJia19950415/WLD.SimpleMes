using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.QualityControl;

namespace SC.SimpleMes.Report.Dto
{
    public class DiscardRecordReportCondtionDto: ReportQueryConditonDto
    {
        public DiscardTypeEnum? DiscardType { get; set; }
    }
}
