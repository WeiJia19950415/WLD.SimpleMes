using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl.Dto
{
    public class ProblemCategoryDto : EntityDto<long>
    {
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
        /// 父级分类
        /// </summary>
        public long? ParentCategoryId { get; set; }

        public string ParentCategoryCode { get; set; }
    }
}
