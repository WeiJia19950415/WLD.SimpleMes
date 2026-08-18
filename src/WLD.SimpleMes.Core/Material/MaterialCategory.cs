using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Material
{
    /// <summary>
    /// 物料分类表
    /// </summary>
    public class MaterialCategory : Entity<long>, IMayHaveTenant
    {
        public const char CategoryCodeSepator = '.';

        public const char CategorNameSepator = '-';

        /// <summary>
        /// 电堆默认分组编码
        /// </summary>
        public static string DefaultStacksCategoryCode = "D01100";

        public int? TenantId { get; set; }

        /// <summary>
        /// 物料分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 全分类名称【包含父级分类的名称】
        /// </summary>
        public string FullCategoryName { get; set; }

        /// <summary>
        /// 物料分类代码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 物料分类描述
        /// </summary>
        public string CategoryDescription { get; set; }

        /// <summary>
        /// 是否未为关键物料
        /// </summary>
        public bool IsKeyMaterial { get; set; }

        /// <summary>
        /// 父级分类
        /// </summary>
        public long? ParentCategoryId { get; set; }

        public List<MaterialInfo> MaterialInfos { get; set; }

        public MaterialBatchNumberRuler BatchNumberRuler { get; set; }
        /// <summary>
        /// 获取父级分类编码
        /// </summary>
        /// <returns></returns>
        public static string GetParentCode(string categoryCode)
        {
            if (!string.IsNullOrEmpty(categoryCode) && categoryCode.IndexOf(CategoryCodeSepator) > 0)
            {
                return categoryCode.Substring(0, categoryCode.LastIndexOf(CategoryCodeSepator));
            }

            return string.Empty;
        }

        public static string GetRootParentCode(string categoryCode)
        {
            if (!string.IsNullOrEmpty(categoryCode))
            {
                return categoryCode.Substring(0, categoryCode.IndexOf(CategoryCodeSepator) - 1);
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取全部分类
        /// </summary>
        /// <param name="parentCatogryFullName"></param>
        /// <returns></returns>
        public static string GetFullCatgoryName(string parentCatogryFullName, string catgoryName)
        {
            return $"{parentCatogryFullName}{CategorNameSepator}{catgoryName}";
        }
    }
}
