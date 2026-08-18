using System.Collections.Generic;

namespace WLD.SimpleMes.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}

