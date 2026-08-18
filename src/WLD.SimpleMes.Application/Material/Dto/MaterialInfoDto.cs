using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    public class MaterialInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 材料编号
        /// </summary>
        public string MaterialNumber { get; set; }

        public string ShowMaterialNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(CategoryCode))
                {
                    return this.MaterialNumber.Replace(this.CategoryCode, "").TrimStart('.');
                }

                return this.MaterialNumber;
            }
        }

        /// <summary>
        /// 材料类型
        /// </summary>
        public MaterialTypeEnum MaterialType { get; set; }

        public int? TenantId { get; set; }

        /// <summary>
        /// 单位名称 例：个、盒、箱
        /// </summary>
        public string UnitName { get; set; }

        public string Specification { get; set; }

        /// <summary>
        /// 归属分类
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 归属分类全称
        /// </summary>
        public string CategoryName { get; set; }

        public long? BelongCategoryId { get; set; }

    }
}
