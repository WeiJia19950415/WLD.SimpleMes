using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WorkProcessSet.Dto
{
    public class ProductWorkProcessSetDto : EntityDto<long>
    {
        public long BelongWorkProcessSetId { get; set; }
        public string SetName { get; set; }
        public string Descreption { get; set; }
        public string SetVersion { get; set; }
        public long MaterialInfoId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialNumber { get; set; }
        public bool IsCurrent { get; set; }

        public int? TenantId { get; set; }
    }
}
