using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;

namespace WLD.SimpleMes.BOM
{
    /// <summary>
    /// 工艺BOM信息
    /// </summary>
    public class WorkProcessSetBom : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 所属工艺Id
        /// </summary>
        public long BelongWorkProcessSetId { get; set; }

        /// <summary>
        /// 所属工艺
        /// </summary>
        public WorkProcessSet BelongWorkProcessSet { get; set; }

        /// <summary>
        /// 引用的标准BOMId
        /// </summary>
        public long ReferenceBomId { get; set; }

        /// <summary>
        /// 引用的BOM信息
        /// </summary>
        public BomInfo ReferenceBom { get; set; }

        /// <summary>
        /// 工艺版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 归属租户
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 引用该工艺BOM的订单BOM
        /// </summary>
        public List<WorkOrderBom> WorkOrderBoms { get; set; }

    }
}
