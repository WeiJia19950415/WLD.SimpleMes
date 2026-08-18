using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.QualityControl;

namespace WLD.SimpleMes.Report.Dto
{
    public class DiscardRecordReportCondtionDto: ReportQueryConditonDto
    {
        public DiscardTypeEnum? DiscardType { get; set; }
    }
}
