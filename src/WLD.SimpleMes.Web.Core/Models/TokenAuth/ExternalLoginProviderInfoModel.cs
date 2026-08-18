using Abp.AutoMapper;
using WLD.SimpleMes.Authentication.External;

namespace WLD.SimpleMes.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

