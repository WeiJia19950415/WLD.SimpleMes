using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using WLD.SimpleMes.Authorization.Roles;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.Localization;
using WLD.SimpleMes.Log;
using WLD.SimpleMes.MultiTenancy;
using WLD.SimpleMes.Timing;
using Castle.MicroKernel.Registration;
using Abp.Dapper;

namespace WLD.SimpleMes
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SimpleMesCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            SimpleMesLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = SimpleMesConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();

            Configuration.Localization.Languages.Add(new LanguageInfo("zh-Hans", "简体中文", "famfamfam-flags cn", true));

            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = SimpleMesConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleMesCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
            IocManager.IocContainer.Register(
           Component.For<Abp.Auditing.IAuditingStore>().Named("JHTAuditLogStore").ImplementedBy<JHTAuditLogStore>().IsDefault()
           );
        }
    }
}

