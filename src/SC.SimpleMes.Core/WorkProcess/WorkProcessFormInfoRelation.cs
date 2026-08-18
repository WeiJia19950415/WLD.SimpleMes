using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.SimpleMes.DynamicForms;

namespace SC.SimpleMes.WorkProcess
{
    /// <summary>
    /// 工序关联填报表单
    /// 同一个表单可以因为填报用途不同，在同一个工序中重复
    /// </summary>
    public class WorkProcessFormInfoRelation : Entity<long>
    {
        public long BelongWorkProcessId { get; set; }

        public WorkProcessInfo BelongWorkProcess { get; set; }

        public long BelongFormInfoId { get; set; }

        public FormTemplateInfo BelongFormInfo { get; set; }

        public FormUseTypeEnum FormUseType { get; set; }

        public bool IsEnabled { get; set; }
    }

    public enum FormUseTypeEnum
    {
        标准工序填报 = 1,
        巡检填报 = 2,
    }
}
