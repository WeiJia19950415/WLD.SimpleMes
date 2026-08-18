using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.ERPSyncTask
{
    public class ERPSyncTask : FullAuditedEntity<long>
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public SyncType SyncType { get;set; }

        /// <summary>
        /// 调用的存储过程
        /// </summary>
        public string StoredName { get; set; }

        /// <summary>
        /// 调用的存储过程参数
        /// </summary>
        public string StoredParameter { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public SyncState SyncState { get; set; }

        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 任务执行时间
        /// </summary>
        public DateTime? ImplementTime { get; set; }

        /// <summary>
        /// 失败信息
        /// </summary>
        public string FailMassage { get; set; }
    }

    [Description("任务类型")]
    public enum SyncType
    {
        [Description("物料分组")]
        MaterialCategory = 1,
        [Description("物料")]
        Material = 2,
        [Description("入库单")]
        Warehousing = 3,
    }
    [Description("任务状态")]
    public enum SyncState
    {
        [Description("执行失败")]
        fail = -1,
        [Description("待执行")]
        StayComplete = 0,
        [Description("执行完成")]
        Complete = 1,
    }
}
