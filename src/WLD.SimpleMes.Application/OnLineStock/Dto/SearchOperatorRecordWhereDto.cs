using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.LineSideWarehouse;

namespace WLD.SimpleMes.OnLineStock.Dto
{
    public class SearchOperatorRecordWhereDto
    {
        /// <summary>
        /// 任务单编号\物料名称
        /// </summary>
        public string KeyWord { get; set; }


        public string[] DateRange { get; set; }
        /// <summary>
        /// 操作车间
        /// </summary>
        public long? OperatorWorkShopId { get; set; }

        /// <summary>
        /// 操作人员
        /// </summary>
        public long? OpertaorId { get; set; }

        public DateTime? StartOperatorTime { get; set; }

        public DateTime? EndOperatorTime { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public StockOperatoerType? StockOperatoerType { get; set; }

        /// </summary>
        public void ParseTime()
        {
            if (DateRange != null && DateRange.Length > 1)
            {
                if (DateTime.TryParse(DateRange[0], out var startDate))
                {
                    this.StartOperatorTime = startDate.Date;
                }

                if (DateTime.TryParse(DateRange[1], out var endDate))
                {
                    this.EndOperatorTime = endDate.Date.AddDays(1);
                }
            }
            else
            {
                this.StartOperatorTime = null;
                this.EndOperatorTime = null;
            }
        }
    }
}
