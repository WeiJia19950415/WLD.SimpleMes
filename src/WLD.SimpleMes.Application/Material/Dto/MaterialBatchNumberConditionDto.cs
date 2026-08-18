using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material.Dto
{
    public class MaterialBatchNumberConditionDto
    {
        public string KeyWord { get; set; }

        public long? BelongMaterialId { get; set; }

        public DateTime? CreationTime { get; set; }
        public bool? ShowAll { get; set; }

        public long? CreateStaionId { get; set; }

        /// <summary>
        /// 只显示产品信息
        /// </summary>
        public bool? OnlyShowProduct { get; set; }

        public long? ProductLineId { get; set; }

        public string[] DateRange { get; set; }

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

        public int? UsedState { get; set; }
    }
}
