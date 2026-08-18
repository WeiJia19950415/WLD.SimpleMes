using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.DynamicForms.DTO
{
    public class FormTemplateBasicInfoDto : EntityDto<long>
    {

        public string CreatorName { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormsName { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

       public bool IsCurrent { get; set; }
    }
}
