using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.Common;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.Report;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    /// <summary>
    /// 日志库 读库
    /// </summary>
    public class ReportDbContext : AbpDbContext
    {
        protected const int StringLongLength = 512;
        protected const int StringMiddleLength = 255;
        protected const int StringShortLength = 50;

        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region 统计报表

            modelBuilder.Entity<WorkProcessCapacityDailyReportRecord>(d =>
            {
                d.ToTable("WorkProcessCapacityDailyReportRecord");
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkStationName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkProcessName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<ProductLineCapacityDailyReportRecord>(d =>
            {
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<WorkProcessProblemDailyReportRecord>(d =>
            {
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.ProblemDefineId).IsUnique(false);
                d.HasIndex(p => p.QualityProblemNumber).IsUnique(false);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkStationName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkProcessName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProbleName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.QualityProblemNumber).IsRequired(true).HasMaxLength(StringShortLength);
            });


            modelBuilder.Entity<ProductLineOnePassRateReport>(d =>
            {
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<WorkProcessOnePassRateReport>(d =>
            {
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkStationName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkProcessName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<PrepaireWorkProcessDayReport>(d =>
            {
                d.HasIndex(p => p.StaticDate).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);

                d.Property(p => p.WorkOrderNumber).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkStationName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<View_PrepareUserWorkStatic>(d =>
            {
                d.ToView("View_PrepareUserWorkStatic");
                d.HasKey(p => p.OperatorId);
                d.Property(p => p.Id).HasColumnName("OperatorId");
            });

            modelBuilder.Entity<View_DDTestDayKPI>(d =>
            {
                d.ToView("View_DDTestDayKPI");
                d.HasKey(p => p.OperatorId);
                d.Property(p => p.Id).HasColumnName("OperatorId");
            });

            modelBuilder.Entity<DDWeekOnePassRateReport>(d =>
            {
                d.Property(p => p.MaterialName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProductLineName).HasMaxLength(StringMiddleLength);

                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.DataDate).IsUnique(false);

            });

            modelBuilder.Entity<OrgProductProcessWorkLoadReport>(d =>
            {
                d.Property(p => p.MaterialName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProductLineName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkStationName).HasMaxLength(StringMiddleLength);

                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.MaterialInfoId).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.WorkProcessId).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
            });

            #endregion
        }

        #region 统计报表

        public DbSet<View_DDTestDayKPI> View_DDTestDayKPI { get; set; }
        public DbSet<View_PrepareUserWorkStatic> View_PrepareUserWorkStatic { get; set; }
        /// <summary>
        /// 工位产能统计报表
        /// </summary>
        public DbSet<WorkProcessCapacityDailyReportRecord> WorkProcessCapacityDailyReportRecord { get; set; }

        /// <summary>
        /// 产线产能统计报表
        /// </summary>
        public DbSet<ProductLineCapacityDailyReportRecord> ProductLineCapacityDailyReportRecords { get; set; }

        /// <summary>
        /// 产线质量报表
        /// </summary>
        public DbSet<WorkProcessProblemDailyReportRecord> WorkProcessProblemDailyReportRecords { get; set; }

        /// <summary>
        /// 前置物料准备工序日报
        /// </summary>
        public DbSet<PrepaireWorkProcessDayReport> PrepaireWorkProcessDayReports { get; set; }

        /// <summary>
        /// 工序一次性通过率报表
        /// </summary>
        public DbSet<WorkProcessOnePassRateReport> WorkProcessOnePassRateReports { get; set; }

        /// <summary>
        /// 产线一次性通过率报表
        /// </summary>
        public DbSet<ProductLineOnePassRateReport> ProductLineOnePassRateReports { get; set; }

        /// <summary>
        /// 电堆周一次性通过率报表
        /// </summary>
        public DbSet<DDWeekOnePassRateReport> DDWeekOnePassRateReports { get; set; }

        /// <summary>
        /// 产线工序加工量统计报表
        /// </summary>
        public DbSet<OrgProductProcessWorkLoadReport> OrgProductProcessWorkLoadReports { get; set; }


        #endregion
    }
}
