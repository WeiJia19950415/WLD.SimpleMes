using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.OnLineStock.Dto
{
    public class LineSideMaterialInfoBomItemDto
    {
        /// <summary>
        /// 所属线边库物料ID
        /// </summary>
        public long LineSideMaterialInfoId { get; set; }

        /// <summary>
        /// 构成物料的分类ID
        /// </summary>
        public long FormMaterialCategoryId { get; set; }

        /// <summary>
        /// 物料分类编号
        /// </summary>
        public string FormMaterialCategoryNumber { get; set; }

        /// <summary>
        /// 物料分类名称
        /// </summary>
        public string FormMaterialCategoryName { get; set; }

        public decimal FormMaterialAmount { get; set; }
    }
}
