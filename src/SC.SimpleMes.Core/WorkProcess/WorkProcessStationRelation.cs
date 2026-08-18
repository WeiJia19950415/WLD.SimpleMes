using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序与工位的关联关系
    /// </summary>
    public class WorkProcessStationRelation : Entity<long>
    {
        /// <summary>
        /// 关联工位
        /// </summary>
        public long BelongWorkStationId { get; set; }

        public WorkStationInfo BelongWorkStation { get; set; }

        /// <summary>
        /// 关联工序
        /// </summary>
        public long BelongWorkProcessId { get; set; }

        public WorkProcessInfo BelongWorkProcess { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; set; }
    }
}
