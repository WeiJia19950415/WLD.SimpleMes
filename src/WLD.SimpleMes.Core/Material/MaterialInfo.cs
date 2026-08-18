using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.BOM;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 物料信息表
    /// </summary>
    public class MaterialInfo : FullAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 材料编号
        /// </summary>
        public string MaterialNumber { get; private set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string MaterialCategoryCode { get; set; }

        public int? TenantId { get; set; }

        public long? BelongCategoryId { get; set; }

        public MaterialCategory BelongCategory { get; set; }

        public MaterialTypeEnum MaterialType { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 规格说明
        /// </summary>
        public string Specification { get; set; }

        public MaterialBatchNumberRuler MaterialBatchNumberRuler { get; set; }

        /// <summary>
        /// 对应BOM信息
        /// </summary>
        public List<BomInfo> BomInfos { get; set; }

    }

    public enum MaterialTypeEnum
    {
        原材料 = 1,
        在制品 = 2,
        半成品 = 3,
        成品 = 4
    }
}
