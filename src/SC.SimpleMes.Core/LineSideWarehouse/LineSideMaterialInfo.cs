using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.LineSideWarehouse
{
    /// <summary>
    /// 线边库物料信息
    /// </summary>
    public class LineSideMaterialInfo : Entity<long>, ISoftDelete
    {
        public const string MaterialPrefix = "ZZ";
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 所属分类名称
        /// </summary>
        public string BelongCategoryNumber { get; set; }


        public bool IsDeleted { get; set; }

        /// <summary>
        /// 物料编码【遵循在制品编码规则】
        /// </summary>
        public string MaterialNumber { get; set; }

        /// <summary>
        /// BOM组成信息
        /// </summary>
        public List<LineSideMaterialInfoBomItem> BomItems { get; set; }
    }
}
