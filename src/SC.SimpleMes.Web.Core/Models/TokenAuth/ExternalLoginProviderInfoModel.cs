using Abp.AutoMapper;
using SC.SimpleMes.Authentication.External;

namespace SC.SimpleMes.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

