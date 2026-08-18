using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTOrganzations.Dto
{
    /// <summary>
    /// 组织关系
    /// </summary>
    [AutoMap(typeof(JHTOrganzation.JHTOrganzation))]
    public class JHTOrganzationDto : EntityDto<long>
    {
        // <summary>
        /// 编码
        /// </summary>
        [StringLength(95)]
        public string Code { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        public long SortNumber { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        [MaxLength(50)]
        public string ShortName { get; set; }

        //public List<long> DepartTags { get; set; }
    }
}

