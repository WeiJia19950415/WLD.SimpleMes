using Abp.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WLD.SimpleMes.SAPSDK
{
    public class SAPSDKModule:AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _appConfiguration;

        public SAPSDKModule(IWebHostEnvironment env, IConfiguration appConfiguration)
        {
            _env = env;
            _appConfiguration = appConfiguration;
        }

        public override void PreInitialize()
        {
            base.PreInitialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
