using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.Web;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SimpleMesDbContextFactory : IDesignTimeDbContextFactory<SimpleMesDbContext>
    {
        public SimpleMesDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SimpleMesDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SimpleMesDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SimpleMesConsts.ConnectionStringName));

            return new SimpleMesDbContext(builder.Options);
        }
    }

    public class LogReportDbContextFactory : IDesignTimeDbContextFactory<LogReportDbContext>
    {
        public LogReportDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LogReportDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var connectionString = configuration.GetConnectionString(SimpleMesConsts.LogReportConnectionStringName);
            SimpleMesDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SimpleMesConsts.LogReportConnectionStringName));

            return new LogReportDbContext(builder.Options);
        }

    }

    public class ReportDbContextFactory : IDesignTimeDbContextFactory<ReportDbContext>
    {
        public ReportDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ReportDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var connectionString = configuration.GetConnectionString(SimpleMesConsts.ReportConnectionStringName);
            SimpleMesDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SimpleMesConsts.ReportConnectionStringName));

            return new ReportDbContext(builder.Options);
        }

    }

    public class K3ERPDbContextFactory : IDesignTimeDbContextFactory<K3ERPDbContext>
    {
        public K3ERPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<K3ERPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var connectionString = configuration.GetConnectionString(SimpleMesConsts.K3ERPDbConnectionStringName);
            SimpleMesDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SimpleMesConsts.K3ERPDbConnectionStringName));

            return new K3ERPDbContext(builder.Options);
        }

    }
}

