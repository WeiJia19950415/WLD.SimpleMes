using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.Material;

namespace SC.SimpleMes.BOM
{
    /// <summary>
    /// Bom信息
    /// </summary>
    public class BomInfo : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 所属物料
        /// </summary>
        public long MaterialId { get; set; }

        /// <summary>
        /// 所属物料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        public string MaterialName { get; set; }

        public MaterialInfo Material { get; set; }

        /// <summary>
        /// 版本编号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 是否未当前使用BOM
        /// </summary>
        public bool IsCurrent { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// Bom内容
        /// </summary>

        public List<BomItemInfo> BomItems { get; set; }
    }
}
