using Abp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.SAPSDK.Configuation
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                  new SettingDefinition(AppSettingNames.SAPInterfaceUrl, "",scopes:SettingScopes.Application|SettingScopes.Tenant),
            };
        }
    }
}
