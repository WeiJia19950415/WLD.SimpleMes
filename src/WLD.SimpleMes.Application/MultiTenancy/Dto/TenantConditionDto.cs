using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.MultiTenancy.Dto
{
    public class TenantConditionDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 归属区域
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsActive { get; set; }
    }
}

