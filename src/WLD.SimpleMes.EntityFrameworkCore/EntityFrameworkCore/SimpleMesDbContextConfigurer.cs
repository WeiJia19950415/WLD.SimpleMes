using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    public static class SimpleMesDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SimpleMesDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<SimpleMesDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<LogReportDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<LogReportDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<ReportDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<ReportDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<K3ERPDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString)
                .ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }

        public static void Configure(DbContextOptionsBuilder<K3ERPDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).ConfigureWarnings(b => b.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
        }
    }
}

