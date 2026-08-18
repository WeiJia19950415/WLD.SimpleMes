using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder.DTO
{
    public class IssuedWorkOrderInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 生产车间id
        /// </summary>
        public long ProduceWorkShopId { get; set; }
        
        /// <summary>
        /// 生产产线Id
        /// </summary>
        public long ProduceLineId { get; set; }
    }
}
