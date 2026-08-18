using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class CreateUpdateWorkOrderInfoDto : EntityDto<long>
    {
        public string OrderNumber { get; set; }

        /// <summary>
        /// 来源工单：销售订单
        /// </summary>
        public string FromOrderNumber { get; set; }

        /// <summary>
        /// 生产产品Id
        /// </summary>
        public long MaterialInfoId { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public decimal ProduceCount { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }

        public DateTime DeliveryTime { get; set; }

        public int? TenantId { get; set; }
    }
}
