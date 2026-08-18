using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.QualityControl.Dto
{
    public class ProblemDefineDto : EntityDto<long>
    {
        /// <summary>
        /// 质量问题编号
        /// </summary>
        public string QualityProblemNumber { get; set; }

        /// <summary>
        /// 问题名称
        /// </summary>
        public string ProbleName { get; set; }

        /// <summary>
        /// 质量问题描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 质量分类Id
        /// </summary>
        public long? ProblemCategoryId { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 分类全称
        /// </summary>
        public string FullCategoryName { get; set; }

        public string ShowCategoryCode { get; set; }
    }
}
