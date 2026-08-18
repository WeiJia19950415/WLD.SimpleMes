using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.WorkOrder.DTO
{
    public class WorkOrderPickingMaterilInfoDto
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// 工单数量
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        public decimal PickingCount { get; set; }

        /// <summary>
        /// 领料单位
        /// </summary>
        public string UniteName { get; set; }
    }
}
