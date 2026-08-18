using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SC.SimpleMes.MultiTenancy;

namespace SC.SimpleMes.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}

