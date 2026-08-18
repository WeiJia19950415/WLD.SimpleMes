using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.WorkOrder
{
    /// <summary>
    /// 工单BOM项
    /// </summary>
    public class WorkOrderBomItem : Entity<long>, ICreationAudited, IHasCreationTime
    {
        /// <summary>
        /// 归属的工单BOM
        /// </summary>
        public long BelongWorkOrderBomId { get; set; }

        /// <summary>
        /// 所属的工序
        /// </summary>
        public long BelongWorkProcessId { get; set; }

        /// <summary>
        /// 所属工序
        /// </summary>
        public WorkProcessInfo BelongWorkProcess { get; set; }

        /// <summary>
        /// 投入的物料
        /// </summary>
        public long InputMaterialId { get; set; }

        /// <summary>
        /// 投入物料信息
        /// </summary>
        public MaterialInfo InputMaterial { get; set; }

        /// <summary>
        /// 投入数量
        /// </summary>
        public decimal InputMaterialCount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public long? CreatorUserId { get; set; }
    }
}
