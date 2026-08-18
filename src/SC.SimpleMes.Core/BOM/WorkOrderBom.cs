using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkOrder;

namespace SC.SimpleMes.BOM
{
    /// <summary>
    /// 工单BOM
    /// </summary>
    public class WorkOrderBom : Entity<long>
    {
        public long WorkOrderId { get; set; }
        /// <summary>
        /// 生产工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        
        /// <summary>
        /// 生产工单
        /// </summary>
        public WorkOrderInfo WorkOrderInfo { get; set; }


        /// <summary>
        /// 所属物料
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 引用的工艺BOM
        /// </summary>
        public long WorkProcessSetBomId { get; set; }

        /// <summary>
        /// 引用的工艺Bom
        /// </summary>
        public WorkProcessSetBom WorkProcessSetBom { get; set; }
    }
}
