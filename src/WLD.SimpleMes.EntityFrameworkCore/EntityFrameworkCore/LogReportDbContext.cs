using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.EntityFrameworkCore;
using WLD.SimpleMes.JHTLog;
using WLD.SimpleMes.Log;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.Report;
using static WLD.SimpleMes.DynamicForms.DDImportantInfos;
using WLD.SimpleMes.LineSideWarehouse;
using WLD.SimpleMes.Common;
using WLD.SimpleMes.WorkOrder;
using Newtonsoft.Json;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    /// <summary>
    /// 日志报表数据库
    /// </summary>
    public class LogReportDbContext : AbpDbContext
    {
        protected const int StringLongLength = 512;
        protected const int StringMiddleLength = 255;
        protected const int StringShortLength = 50;

        public LogReportDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDbFunction(() => WLDDbFunctionsExtension.JsonQuery(default(string), default(string)));
            modelBuilder.Entity<JHTAuditLog>().ToTable("AuditLogs");
            modelBuilder.Entity<WorkProcessMaterialRecord>(d =>
            {
                d.HasIndex(p => p.OrderNumber).IsUnique(false);
                d.HasIndex(p => p.InputMaterialBatchNumber).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
                d.HasIndex(p => p.WorkProcessId).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);

                d.Property(p => p.OrderNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProductBatchNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialBatchNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialCount).HasPrecision(20, 3);
                d.Property(p => p.OutRangeCount).HasPrecision(20, 3);
                d.Property(p => p.InputMaterialName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputUnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkProcessName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BOMMaterialCount).HasPrecision(20, 3);
                d.Property(p => p.BOMUnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Supplier).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BatchNo).HasMaxLength(StringMiddleLength);
                d.Property(p => p.IsRepairedInput).HasDefaultValue(false);

            });


            modelBuilder.Entity<WorkProcessMaterialRecordHistory>(d =>
            {

                d.HasIndex(p => p.OrderNumber).IsUnique(false);
                d.HasIndex(p => p.InputMaterialBatchNumber).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
                d.HasIndex(p => p.WorkProcessId).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);

                d.Property(p => p.OrderNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProductBatchNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialBatchNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputMaterialCount).HasPrecision(20, 3);
                d.Property(p => p.OutRangeCount).HasPrecision(20, 3);
                d.Property(p => p.InputMaterialName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.InputUnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkProcessName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ChangeReason).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BOMMaterialCount).HasPrecision(20, 3);
                d.Property(p => p.BOMUnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Supplier).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BatchNo).HasMaxLength(StringMiddleLength);
                
            });

            modelBuilder.Entity<WorkProcessOperatorRecord>(d =>
            {
                d.HasIndex(p => p.OrderNumber).IsUnique(false);
                d.HasIndex(p => p.BatchNumber).IsUnique(false);
                d.HasIndex(p => p.WorkStationId).IsUnique(false);
                d.HasIndex(p => p.WorkProcessId).IsUnique(false);
                d.HasIndex(p => p.ProductLineId).IsUnique(false);
                d.HasIndex(p => p.CurrentOperatroAccountId).IsUnique(false);
                d.HasIndex(p => p.WorkProcessOperateType).IsUnique(false);

                d.Property(p => p.WorkStationName).HasMaxLength(StringShortLength);
                d.Property(p => p.BatchNumber).IsRequired().HasMaxLength(StringMiddleLength);
                d.Property(p => p.OrderNumber).IsRequired().HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkProcessName).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.IsRepaired).IsRequired(true).HasDefaultValue(0);
                d.Property(p => p.IsLastFqcRepaired).IsRequired(true).HasDefaultValue(0);
            });

            modelBuilder.Entity<ProblemRecord>(d =>
            {
                d.HasIndex(p => p.BatchMaterilaNumber).IsUnique(false);
                d.HasIndex(p => p.WorkOrderNumber).IsUnique(false);
                d.HasIndex(p => p.OnWorkProcessNumber).IsUnique(false);
                d.HasIndex(p => p.QualityProblemDefineNumber).IsUnique(false);
                d.HasIndex(p => p.QualityProblemDefineNumber).IsUnique(false);
                d.HasIndex(p => p.CreationTime).IsUnique(false);
                d.HasIndex(p => p.ResponsibleDepartmentId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);

                d.Property(p => p.WorkProcessName).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Createor).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.QualityProblemDefineNumber).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.DetailDescretion).IsRequired(true).HasMaxLength(StringLongLength);
                d.Property(p => p.OnWorkProcessNumber).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.WorkOrderNumber).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BatchMaterilaNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.DepartmentName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ReasonAnlysis).HasMaxLength(StringLongLength);
                d.Property(p => p.ProblemCount).HasDefaultValue(1);
                d.Property(p => p.CheckCount).HasDefaultValue(1);
                d.Property(p => p.UnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.AuditorName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WarpUnitName).HasMaxLength(StringMiddleLength);
            });

            modelBuilder.Entity<ProblemDealRecord>(d =>
            {
                d.HasIndex(p => p.DealTime).IsUnique(false);
                d.HasIndex(p => p.ProblemDealType).IsUnique(false);
                d.HasIndex(p => p.ProblemRecordId).IsUnique(false);
                d.HasIndex(p => p.OperatorId).IsUnique(false);

                d.Property(p => p.OperatorName).HasMaxLength(StringShortLength);
                d.Property(p => p.OperatorDescreption).HasMaxLength(StringLongLength);
            });


            modelBuilder.Entity<LineSideMaterialOperatorRecord>(d =>
            {
                d.HasIndex(p => p.OpertaorId).IsUnique(false);
                d.HasIndex(p => p.OperatorWorkShopId).IsUnique(false);
                d.HasIndex(p => p.WorkOrderNumber).IsUnique(false);
                d.HasIndex(p => p.LineSideMaterialInfoId).IsUnique(false);
                d.Property(p => p.OpertaorName).HasMaxLength(StringShortLength);
                d.Property(p => p.HandleUserName).HasMaxLength(StringShortLength);
                d.Property(p => p.ProjectName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProjectNumber).HasMaxLength(StringMiddleLength);
            });

            modelBuilder.Entity<View_ProblemRecord>(d =>
            {
                d.ToView("View_ProblemRecord");
            });

            modelBuilder.Entity<View_LineSideMaterialOperatorRecord>(d =>
            {
                d.ToView("View_LineSideMaterialOperatorRecord");
            });


            modelBuilder.Entity<View_BatchMaterialUsedReport>(d =>
            {
                d.ToView("View_BatchMaterialUsedReport");
            });


            modelBuilder.Entity<View_DDImportantInfos>(d =>
            {
                d.ToView("View_DDImportantInfo");
            });

            modelBuilder.Entity<View_MaterialBatchNumbers>(d =>
            {
                d.ToView("View_MaterialBatchNumbers");
            });

            modelBuilder.Entity<View_ProductConstructMaterialInfo>(d =>
            {
                d.ToView("View_ProductConstructMaterialInfo");

            });

            modelBuilder.Entity<View_OverUseWorkOrderInfo>(d =>
            {
                d.ToView("View_OverUseWorkOrderInfo");
            });

            modelBuilder.Entity<FormInfoRecord>(d =>
            {
                d.HasIndex(p => p.BelongMaterialBatchNumber).IsUnique(false);
                d.HasIndex(p => p.BelongOrderNumber).IsUnique(false);
                d.HasIndex(p => p.BelongWorkProcessNumber).IsUnique(false);
                d.HasIndex(p => p.BelongFormId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.OperatorUserId).IsUnique(false);
                d.HasIndex(p => p.OperatorTime).IsUnique(false);

                d.Property(p => p.BelongOrderNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Operator).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MatreialName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BelongProductLineName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BelongMaterialBatchNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BelongWorkProcessNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkProcessName).IsRequired(false).HasMaxLength(StringMiddleLength);
            });

            modelBuilder.Entity<MaterialBatchNumber>(d =>
            {
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.BatchNumber).IsUnique(true);
                d.HasIndex(p => p.CreationTime).IsUnique(false);
                d.HasIndex(p => p.FlowNumber).IsUnique(false);

                d.Ignore(p => p.IsLineMaterialInfo);
                d.Property(p => p.MaterialStatu).HasDefaultValue(MaterialStatuEnum.可用);
                d.Property(p => p.FromOrderNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BatchNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.CreatorIds).IsRequired(false).HasMaxLength(StringLongLength);
                d.Property(p => p.WrapUniteName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.FromErpBatchNumber).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BOMMaterialUnitName).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BOMMaterialCount).IsRequired(true).HasPrecision(20, 3);
            });

            modelBuilder.Entity<ERPInStockInfo>(d =>
           {

               d.HasIndex(p => p.WarehousingNumber).IsUnique(false);
               d.HasIndex(p => p.MaterialNumber).IsUnique(false);
               d.HasIndex(p => p.BatchNo).IsUnique(false);
               d.HasIndex(p => p.CreateTime).IsUnique(false);
               d.HasIndex(p => p.SourceType).IsUnique(false);
               d.HasIndex(p => p.WarehousingTime).IsUnique(false);

               d.Property(p => p.WarehousingNumber).IsRequired(true).HasMaxLength(StringShortLength);
               d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
               d.Property(p => p.Supplier).IsRequired(true).HasMaxLength(StringShortLength);
               d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
               d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
               d.Property(p => p.Specification).IsRequired(false).HasMaxLength(StringShortLength);
               d.Property(p => p.UnitName).IsRequired(false).HasMaxLength(StringShortLength);
               d.Property(p => p.BatchNo).IsRequired(true).HasMaxLength(StringMiddleLength);
               d.Property(p => p.ReceiptQuantity).IsRequired(true).HasPrecision(20, 3);
               d.Property(p => p.MaterialStatu).IsRequired(true).HasDefaultValue(MaterialStatuEnum.可用);
           });

            modelBuilder.Entity<BatchNumberPrintRecord>(d =>
            {
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.BatchNumber).IsUnique(false);
                d.HasIndex(p => p.PrintTime).IsUnique(false);
                d.HasIndex(p => p.OperatorId).IsUnique(false);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.PrintMachine).IsRequired(false).HasMaxLength(StringShortLength);
                d.Property(p => p.OperatorName).IsRequired(false).HasMaxLength(StringShortLength);
            });


            modelBuilder.Entity<DDImportantInfos>(d =>
            {
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.BelongMaterialBatchNumber).IsUnique(false);
                d.HasIndex(p => p.BelongProductLineId).IsUnique(false);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BelongProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.MatreialName).IsRequired(false).HasMaxLength(StringShortLength);
                d.Property(p => p.Remark).HasMaxLength(StringLongLength);
                d.Property(p => p.Auditor).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Checkor).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProjectNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProjectName).HasMaxLength(StringMiddleLength);

                d.Ignore(p => p.UploadUrls);
                d.Ignore(p => p.MaterialRecordSimplyInfos);
            });


            modelBuilder.Ignore<MaterialRecordSimplyInfo>();
            modelBuilder.Ignore<UploadUrlInfos>();


            modelBuilder.Entity<WarningOverUsedERPInStockInfo>(d =>
            {
                d.HasIndex(p => p.BatchNo).IsUnique(false);
                d.HasIndex(p => p.FirstWarningTime).IsUnique(false);

                d.Property(p => p.BatchNo).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ActualUseAmount).HasPrecision(18, 4);
                d.Property(p => p.FirstNoticeUser).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BelongDepartmentName).HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<WarningOverUsedWorkOrderRecord>(d =>
            {
                d.HasIndex(p => p.WorkOrderNumber).IsUnique(false);
                d.HasIndex(p => p.FirstWarningTime).IsUnique(false);

                d.Property(p => p.WorkOrderNumber).IsRequired().HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<MaterialDiscardRecord>(d =>
            {
                d.HasIndex(p => p.ProblemRecordId).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.RecordDate).IsUnique(false);

                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.BatchNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ErpBatchNumber).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.UnitName).HasMaxLength(StringMiddleLength);
                d.Property(p => p.DiscardType).IsRequired(true);
                d.Property(p => p.RecordUserId).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.DeiscardReasonDescreption).HasMaxLength(StringLongLength);
                d.Property(p => p.ProblemDefineNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkOrderNumber).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WrapUnitName).HasMaxLength(StringMiddleLength);
            });

            modelBuilder.Entity<View_MaterialDiscardRecord>().ToView("View_MaterialDiscardRecord");

            modelBuilder.Entity<ERPInStockInfoOperateRecord>(d =>
            {
                d.HasIndex(p => p.BatchNo).IsUnique(false);
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.HasIndex(p => p.OperateTime).IsUnique(false);

                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.OperateDesp).HasMaxLength(StringLongLength);
                d.Property(p => p.BatchNo).IsRequired(true).HasMaxLength(StringMiddleLength);
            });

            base.OnModelCreating(modelBuilder);
        }

        #region 工序操作记录

        public DbSet<WorkProcessMaterialRecord> WorkProcessMaterialRecords { get; set; }

        public DbSet<WorkProcessOperatorRecord> WorkProcessOperatorRecords { get; set; }

        public DbSet<ProblemRecord> ProblemRecords { get; set; }

        public DbSet<ProblemDealRecord> ProblemDealRecords { get; set; }

        public DbSet<FormInfoRecord> FormInfoRecords { get; set; }

        public DbSet<MaterialBatchNumber> MaterialBatchNumbers { get; set; }

        public DbSet<WorkProcessMaterialRecordHistory> WorkProcessMaterialRecordHistory { get; set; }
        #endregion

        /// <summary>
        /// 审计日志
        /// </summary>
        public DbSet<JHTAuditLog> AuditLogs { get; set; }

        /// <summary>
        /// 定时任务日志
        /// </summary>
        public DbSet<NquartzJobLog> NquartzJobLogs { get; set; }

        /// <summary>
        /// 登录尝试日志
        /// </summary>
        public DbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        public DbSet<BatchNumberPrintRecord> BatchNumberPrintRecords { get; set; }

        public DbSet<ERPInStockInfo> ERPInStockInfos { get; set; }

        public DbSet<View_ProblemRecord> View_ProblemRecord { get; set; }

        public DbSet<DDImportantInfos> DDImportantInfos { get; set; }

        public DbSet<View_OverUseWorkOrderInfo> OverUseWorkOrderInfos { get; set; }

        public DbSet<WarningOverUsedWorkOrderRecord> WarningOverUsedWorkOrderRecords { get; set; }


        public DbSet<WarningOverUsedERPInStockInfo> WarningOverUsedERPInStockInfos { get; set; }

        /// <summary>
        /// 线边库操作报表
        /// </summary>
        public DbSet<LineSideMaterialOperatorRecord> LineSideMaterialOperatorRecords { get; set; }

        /// <summary>
        /// 线边库操作记录
        /// </summary>
        public DbSet<View_LineSideMaterialOperatorRecord> View_LineSideMaterialOperatorRecord { get; set; }


        public DbSet<View_MaterialBatchNumbers> View_MaterialBatchNumbers { get; set; }
        /// <summary>
        /// 批次物料消耗使用情况
        /// </summary>
        public DbSet<View_BatchMaterialUsedReport> View_BatchMaterialUsedReports { get; set; }

        public DbSet<View_DDImportantInfos> View_DDImportantInfos { get; set; }

        public DbSet<View_ProductConstructMaterialInfo> view_ProductConstructMaterialInfos { get; set; }

        /// <summary>
        /// 报废统计记录报表
        /// </summary>
        public DbSet<MaterialDiscardRecord> MaterialDiscardRecord { get; set; }

        public DbSet<View_MaterialDiscardRecord> View_MaterialDiscardRecords { get; set; }

        /// <summary>
        /// 入库物料操作记录报表
        /// </summary>
        public DbSet<ERPInStockInfoOperateRecord> ERPInStockRecords { get; set; }
    }
}



