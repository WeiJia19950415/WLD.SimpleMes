using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.QualityControl
{
    /// <summary>
    /// 质量问题定义
    /// </summary>
    public class QualityProblemDefine : Entity<long>
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
        /// 所属问题分类
        /// </summary>
        public ProblemCategory ProblemCategory { get; set; }

        public string GetOwnCode()
        {
            if (!string.IsNullOrEmpty(QualityProblemNumber))
            {
                return QualityProblemNumber.Substring(QualityProblemNumber.LastIndexOf(ProblemCategory.CategoryCodeSepator) + 1);
            }
            return string.Empty;
        }
    }
}
