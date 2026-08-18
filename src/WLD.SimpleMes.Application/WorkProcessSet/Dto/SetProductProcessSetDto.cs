using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSet.Dto
{
    public class SetProductProcessSetDto : EntityDto<long>
    {
        public long MaterialInfoId { get; set; }
        public long BelongWorkProcessSetId { get; set; }
        public bool IsCurrent { get; set; }
    }
}
