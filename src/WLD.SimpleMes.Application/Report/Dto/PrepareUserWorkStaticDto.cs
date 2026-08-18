using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Report.Dto
{
    /// <summary>
    /// 前置工序加工人员绩效报表
    /// </summary>
    public class PrepareUserWorkStaticDto
    {
        public long OperatorId { get; set; }

        public string OperatorName { get; set; }

        public string OrgName { get; set; }

        public long? OrgId { get; set; }

        public long WorkStationId { get; set; }

        public string WorkStationName { get; set; }
        public long ProductlineId { get; set; }

        public string ProductLineName { get; set; }

        public string MaterialName { get; set; }

        public string MaterialNumber { get; set; }

        public decimal MatrialCount { get; set; }

        public string WrapUniteName { get; set; }

        public DateTime CreationDate { get; set; }
    }

    public class PrepareUserWorkStaticQueryCondtionDto : ReportQueryConditonDto
    {
        public long? OperatorId { get; set; }
        public List<long> OrgId { get; set; }
    }
}
