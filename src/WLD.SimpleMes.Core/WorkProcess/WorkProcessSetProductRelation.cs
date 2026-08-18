using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Material;

namespace WLD.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工艺与物料绑定关系
    /// </summary>
    public class WorkProcessSetProductRelation : FullAuditedEntity<long>
    {
        /// <summary>
        /// 归属产品Id
        /// </summary>
        public long MaterialInfoId { get; set; }

        /// <summary>
        /// 归属物料信息
        /// </summary>
        public MaterialInfo MaterialInfo { get; set; }

        /// <summary>
        /// 归属的工艺集合
        /// </summary>
        public long BelongWorkProcessSetId { get; set; }

        /// <summary>
        /// 归属的工艺集
        /// </summary>
        public WorkProcessSet BelongWorkProcessSet { get; set; }

        /// <summary>
        /// 是否为当前配置工艺
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
