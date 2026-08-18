using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using WLD.SimpleMes.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    public class MultipleConnectionStringResolver : DefaultConnectionStringResolver
    {
        private readonly IConfigurationRoot _appConfiguration;
        public MultipleConnectionStringResolver(IAbpStartupConfiguration configuration, IWebHostEnvironment hostingEnvironment) : base(configuration)
        {
            _appConfiguration = AppConfigurations.Get(hostingEnvironment.ContentRootPath, hostingEnvironment.EnvironmentName);
        }
        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {

            if (args["DbContextConcreteType"] as Type == typeof(SimpleMesDbContext))
            {
                return _appConfiguration.GetConnectionString("Default");
            }

            if (args["DbContextConcreteType"] as Type == typeof(LogReportDbContext))
            {
                return _appConfiguration.GetConnectionString(SimpleMesConsts.LogReportConnectionStringName);
            }

            if (args["DbContextConcreteType"] as Type == typeof(ReportDbContext))
            {
                return _appConfiguration.GetConnectionString(SimpleMesConsts.ReportConnectionStringName);
            }


            if (args["DbContextConcreteType"] as Type == typeof(K3ERPDbContext))
            {
                return _appConfiguration.GetConnectionString(SimpleMesConsts.K3ERPDbConnectionStringName);
            }

            return base.GetNameOrConnectionString(args);
        }
    }
}

