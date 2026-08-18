using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.JHTOrganzation
{
    public class JHTOrganzation : OrganizationUnit
    {
        /// <summary>
        /// 排序号
        /// </summary>
        public long SortNumber { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        [MaxLength(50)]
        public string ShortName { get; set; }

    }
}

