using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class WorkOrderBomDto : EntityDto<long>
    {
        public long WorkOrderId { get; set; }
        /// <summary>
        /// 生产工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        /// <summary>
        /// 所属物料
        /// </summary>
        public long MaterialId { get; set; }
        /// <summary>
        /// 引用的工艺BOM
        /// </summary>
        public long WorkProcessSetBomId { get; set; }

        /// <summary>
        /// 相关的工艺BOM
        /// </summary>
        public List<WorkOrderBomItemDto> OrderBomItemDtos
        {
            get;
            set;
        }
    }
}
