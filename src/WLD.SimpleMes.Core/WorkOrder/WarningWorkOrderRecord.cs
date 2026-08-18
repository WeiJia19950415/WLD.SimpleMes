using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkOrder
{
    public class WarningOverUsedWorkOrderRecord : Entity<long>
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// 首次预警时间
        /// </summary>
        public DateTime FirstWarningTime { get; set; }

        /// <summary>
        /// 预警最后更新时间
        /// </summary>
        public DateTime LastWarningTime { get; set; }
    }
}
