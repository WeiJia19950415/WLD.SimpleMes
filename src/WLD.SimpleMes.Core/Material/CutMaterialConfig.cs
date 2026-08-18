using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 裁切物料配置
    /// </summary>
    public class CutMaterialConfig : Entity<long>
    {
        /// <summary>
        /// 使用到的产品Id
        /// </summary>
        public long? UsedProductId { get; set; }

        /// <summary>
        /// 使用到的产品编码【支持按类型】
        /// </summary>
        public string ProductMaterialNumber { get; set; }

        /// <summary>
        /// 使用到的产品信息
        /// </summary>
        public MaterialInfo? UsedProduct { get; set; }

        /// <summary>
        /// 配置的物料编码或分类
        /// </summary>
        public string ConfigMaterialNumber { get; set; }

        /// <summary>
        /// 配置的物料名称
        /// </summary>
        public string ConfigMaterialName { get; set; }

        /// <summary>
        /// 物料配置的计量单位
        /// </summary>
        public string ConfigMaterialUnitName { get; set; }

        /// <summary>
        /// 裁切成的规格型号
        /// </summary>
        public string CutSpecification { get; set; }

        /// <summary>
        /// 裁切后的计量单位
        /// </summary>
        public string CutUnitName { get; set; }

        /// <summary>
        /// 单位换算比，及裁切后的单位通过规格型号计算出的值与物料单位换算关系
        /// 1张 =  xx多少平方米
        /// </summary>
        public decimal ConversionRatio { get; set; }
    }
}
