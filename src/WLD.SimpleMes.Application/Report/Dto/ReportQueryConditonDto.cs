using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkOrder;
using static WLD.SimpleMes.DynamicForms.DDImportantInfos;

namespace WLD.SimpleMes.Report.Dto
{
    public class ReportQueryConditonDto
    {
        public string KeyWord { get; set; }

        public string[] DateRange { get; set; }

        public long? ProductLineId { get; set; }

        public long? MaterialId { get; set; }

        public string MaterialNumber { get; set; }

        public long? WorkStationId { get; set; }

        public long? ProblemDefineId { get; set; }

        public long? WorkProcessId { get; set; }

        public string ProblemCategoryCode { get; set; }

        public string SupplierBatchNumber { get; set; }
        public List<LevelEnum> Level { get; set; }

        public string ProductCategory { get; set; }

        public long[] MaterialIds { get; set; }
        /// <summary>
        /// 是否入库
        /// </summary>
        public int? IsInStock { get; set; }

        /// <summary>
        /// 超量使用
        /// </summary>
        public bool? IsOverUsed { get; set; }

        public bool? IsAudited { get; set; }

        /// <summary>
        /// 滞留天数
        /// </summary>
        public int? StayTime { get; set; }

        public List<ProduceStatusEnum> ProduceStatus { get; set; }
        /// <summary>
        /// 解析时间
        /// </summary>
        public void ParseTime()
        {
            if (DateRange != null && DateRange.Length > 1)
            {
                if (DateTime.TryParse(DateRange[0], out var startDate))
                {
                    this.StartDate = startDate.Date;
                }

                if (DateTime.TryParse(DateRange[1], out var endDate))
                {
                    this.EndDate = endDate.Date.AddDays(1);
                }
            }
            else
            {
                this.StartDate = null;
                this.EndDate = null;
            }
        }

        public DateTime? StartDate
        {
            get; set;
        }

        public DateTime? EndDate
        {
            get; set;
        }
    }
}
