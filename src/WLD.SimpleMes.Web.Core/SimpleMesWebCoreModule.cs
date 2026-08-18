using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using WLD.SimpleMes.Authentication.JwtBearer;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Abp.AspNetCore.Mvc.Results.Wrapping;
using Castle.MicroKernel.Registration;
using WLD.SimpleMes.Startup;
using JHT.AspNetCore.Quartz;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace WLD.SimpleMes
{
    [DependsOn(
        typeof(SimpleMesApplicationModule),
        typeof(SimpleMesEntityFrameworkModule),
        typeof(AbpAspNetCoreModule),
        typeof(JHTAspNetCoreQuartzModule),
        typeof(AbpAspNetCoreSignalRModule)
     )]
    public class SimpleMesWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SimpleMesWebCoreModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SimpleMesConsts.ConnectionStringName);

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            // 禁用直接使用Application 生成Controller
            //Configuration.Modules.AbpAspNetCore()
            //     .CreateControllersForAppServices(
            //         typeof(SimpleMesApplicationModule).GetAssembly()
            //     );

            // 序列化时的MVCTime格式，使用MVC自带的
            Configuration.Modules.AbpAspNetCore().UseMvcDateTimeFormatForAppServices = true;

            // 暂时禁用后台任务
            Configuration.Modules.AbpConfiguration.BackgroundJobs.IsJobExecutionEnabled = false;

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleMesWebCoreModule).GetAssembly());
            IocManager.Register<IExcelExporter, ExcelExporter>(Abp.Dependency.DependencyLifeStyle.Transient);
        }

        public override void PostInitialize()
        {
            IocManager.IocContainer.Register(
                Component.For<IAbpActionResultWrapperFactory>().Named("JHTAbpActionResultWrapperFactory").ImplementedBy<JHTAbpActionResultWrapperFactory>().IsDefault());

            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(SimpleMesWebCoreModule).Assembly);
        }
    }
}

