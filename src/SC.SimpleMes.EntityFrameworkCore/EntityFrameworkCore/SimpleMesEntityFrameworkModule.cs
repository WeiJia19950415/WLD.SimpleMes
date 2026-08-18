using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using SC.SimpleMes.EntityFrameworkCore.Seed;
using Castle.MicroKernel.Registration;
using System.Reflection;
using System.Collections.Generic;
using Abp.Dapper;

namespace SC.SimpleMes.EntityFrameworkCore
{
    [DependsOn(
        typeof(SimpleMesCoreModule),typeof(AbpDapperModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class SimpleMesEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.IocContainer.Register(
                Component.For<IConnectionStringResolver>().Named("MultipleConnectionStringResolver").ImplementedBy<MultipleConnectionStringResolver>().IsDefault()
                );
            });

            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<SimpleMesDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });

                Configuration.Modules.AbpEfCore().AddDbContext<LogReportDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });

                Configuration.Modules.AbpEfCore().AddDbContext<ReportDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });

                Configuration.Modules.AbpEfCore().AddDbContext<K3ERPDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SimpleMesDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SimpleMesEntityFrameworkModule).GetAssembly());
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(SimpleMesEntityFrameworkModule).GetAssembly() });
            //DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(MyModule).GetAssembly() });

        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}

