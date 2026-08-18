using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.LineSideWarehouse
{
    public class LineSideMaterialInfoBomItem : Entity<long>
    {
        /// <summary>
        /// 所属线边库物料ID
        /// </summary>
        public long LineSideMaterialInfoId { get; set; }

        /// <summary>
        /// 所属线边库物料
        /// </summary>
        public LineSideMaterialInfo LineSideMaterialInfo { get; set; }

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

        /// <summary>
        /// 构成配比值  1个FEF需要多少个其他物料
        /// 检查配比值是否符合条件
        /// </summary>
        public decimal FormMaterialAmount { get; set; }

    }
}
