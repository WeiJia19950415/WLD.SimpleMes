using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class ConditonWorkOrderInfoDto
    {
        public string KeyWord { get; set; }
        /// <summary>
        /// 生产产品Id
        /// </summary>
        public long? MaterialInfoId { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public string[] PlanStartTimeRange { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public string[] PlanEndTimeRange { get; set; }

        public string[] DeliveryTimeRange { get; set; }

        public WorkOrderStatuEnum[] WorkOrderStatus { get; set; }



        public void AnalyseTime()
        {
            if (PlanStartTimeRange!=null&& PlanStartTimeRange.Length==2)
            {
                this.PlanStartTimeStart = DateTime.Parse(PlanStartTimeRange[0]);
                this.PlanStartTimeEnd = DateTime.Parse(PlanStartTimeRange[1]);
            }

            if (PlanEndTimeRange != null && PlanEndTimeRange.Length == 2)
            {
                this.PlanStartTimeStart = DateTime.Parse(PlanEndTimeRange[0]);
                this.PlanStartTimeEnd = DateTime.Parse(PlanEndTimeRange[1]);
            }


            if (DeliveryTimeRange != null && DeliveryTimeRange.Length == 2)
            {
                this.PlanStartTimeStart = DateTime.Parse(DeliveryTimeRange[0]);
                this.PlanStartTimeEnd = DateTime.Parse(DeliveryTimeRange[1]);
            }

          
        }

        public DateTime PlanStartTimeStart { get; set; }

        public DateTime PlanStartTimeEnd { get; set; }

        public DateTime PlanEndTimeStart { get; set; }

        public DateTime PlanEndTimeEnd { get; set; }

        public DateTime DeliveryTimeStart { get; set; }

        public DateTime DeliveryTimeEnd { get; set; }
    }
}
