using Abp.AutoMapper;
using WLD.SimpleMes.Sessions.Dto;

namespace WLD.SimpleMes.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}

