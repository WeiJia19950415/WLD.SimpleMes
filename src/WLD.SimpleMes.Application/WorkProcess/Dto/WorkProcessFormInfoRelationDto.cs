using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcess.Dto
{
    public class WorkProcessFormInfoRelationDto : EntityDto<long>
    {
        public string FormsName { get; set; }

        public long BelongWorkProcessId { get; set; }
        public long BelongFormInfoId { get; set; }

        public FormUseTypeEnum FormUseType { get; set; }

        public bool IsEnabled { get; set; }
    }
}
