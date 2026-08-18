using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 替代料关系
    /// </summary>
    public class MaterialReplaceRelation : Entity<long>
    {
        /// <summary>
        /// BOM编码
        /// </summary>
        public string FApplicableBOM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Fstatus { get; set; }

        /// <summary>
        /// 半成品ID
        /// </summary>
        public long Fapplicableltem { get; set; }

        /// <summary>
        /// 半成品编码
        /// </summary>
        public string FapplicableItenCode { get; set; }
        /// <summary>
        /// 原材料ID
        /// </summary>
        public long FItemID { get; set; }

        /// <summary>
        /// 原材料编码
        /// </summary>
        public string FItenCode { get; set; }

        /// <summary>
        /// 替代原材料ID
        /// </summary>
        public long FsubsItemID { get; set; }

        /// <summary>
        /// 替代原材料编码
        /// </summary>
        public string FsubsItenlode { get; set; }

        /// <summary>
        /// 替代比例
        /// </summary>
        public float Frate { get; set; }

    }
}
