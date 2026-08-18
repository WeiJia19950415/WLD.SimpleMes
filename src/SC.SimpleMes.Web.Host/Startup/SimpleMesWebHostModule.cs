using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SC.SimpleMes.Configuration;

namespace SC.SimpleMes.Web.Host.Startup
{
    [DependsOn(
       typeof(SimpleMesWebCoreModule))]
    public class SimpleMesWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SimpleMesWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleMesWebHostModule).GetAssembly());
        }
    }
}

