using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.SimpleMes.DynamicForms
{

    public class FormTemplateInfo : FullAuditedEntity<long>, IMayHaveTenant
    {
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
        /// 模版数据【JSON格式】
        /// </summary>
        public string TemplateData { get; set; }

        /// <summary>
        /// 创建人员姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 保存的实体对象表
        /// </summary>
        public string SaveEntityType { get; set; }

        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
