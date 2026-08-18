using Abp.Application.Features;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SC.SimpleMes.Authorization;
using SC.SimpleMes.JHTFeature;
using Castle.MicroKernel.Registration;

namespace SC.SimpleMes
{
    [DependsOn(
        typeof(SimpleMesCoreModule),
        typeof(AbpAutoMapperModule))]
    public class SimpleMesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SimpleMesAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SimpleMesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            // 替换原有的功能检查
            if (IocManager.IsRegistered<IFeatureChecker>())
            {
                IocManager.IocContainer.Register(
                    Component.For<IFeatureChecker>().Named("JHTFeatureChecker").ImplementedBy<JHTFeatureChecker>().IsDefault()
                    );
            }
        }
    }
}

