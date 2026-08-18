using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.BOM.Dto
{
    public class BomItemDto
    {
        /// <summary>
        /// 构成物料Id
        /// </summary>
        public long FormMaterialId { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string FormMaterialNumber { get; set; }

        // <summary>
        /// 物料名称
        /// </summary>
        public string FormMaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 配比数量  
        /// </summary>
        public decimal FormCount { get; set; }

        /// <summary>
        /// 耗损系数
        /// </summary>
        public decimal LossFactor { get; set; }
    }
}
