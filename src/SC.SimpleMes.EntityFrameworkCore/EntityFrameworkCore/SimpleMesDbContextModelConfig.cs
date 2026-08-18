using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SC.SimpleMes.Authorization.Roles;
using SC.SimpleMes.Authorization.Users;
using SC.SimpleMes.BOM;
using SC.SimpleMes.DynamicForms;
using SC.SimpleMes.LineSideWarehouse;
using SC.SimpleMes.Material;
using SC.SimpleMes.MultiTenancy;
using SC.SimpleMes.QualityControl;
using SC.SimpleMes.WorkOrder;
using SC.SimpleMes.WorkProcess;
using SC.SimpleMes.WorkStation;

namespace SC.SimpleMes.EntityFrameworkCore
{
    public partial class SimpleMesDbContext
    {
        protected const int StringLongLength = 512;
        protected const int StringMiddleLength = 255;
        protected const int StringShortLength = 80;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region 基础模块

            modelBuilder.Ignore<UserLoginAttempt>();
            modelBuilder.Ignore<AuditLog>();
            modelBuilder.Entity<ViewUser>().ToView("ViewUser");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>(string.Empty);
            modelBuilder.Entity<User>(d =>
            {
                d.Property(p => p.PhoneNumber).IsRequired(false);
                d.Property(p => p.Surname).IsRequired(false);
                d.Property(p => p.EmailAddress).HasMaxLength(User.MaxEmailAddressLength).IsRequired(false);
            });
            modelBuilder.Entity<JHTOrganzation.JHTOrganzation>().ToTable("OrganizationUnits");

            #endregion

            #region 工厂建模

            modelBuilder.Entity<WorkShopInfo>(d =>
            {
                d.Property(p => p.WorkShopNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkShopName).IsRequired(true).HasMaxLength(StringShortLength);
                d.HasIndex(p => p.WorkShopNumber).IsUnique(false);
            });

            modelBuilder.Entity<ProductLine>(d =>
            {
                d.Property(p => p.ProductLineNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProductLineName).IsRequired(true).HasMaxLength(StringShortLength);
                d.HasIndex(p => p.ProductLineNumber).IsUnique(false);
                d.HasOne(p => p.BelongWorkShop).WithMany(p => p.ProductLines).HasForeignKey(p => p.BelongWorkShopId);
            });

            modelBuilder.Entity<WorkStationInfo>(d =>
            {
                d.Property(p => p.WorkStationNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.WorkStationName).IsRequired(true).HasMaxLength(StringShortLength);
                d.HasIndex(p => p.WorkStationNumber).IsUnique(false);
                d.Property(p => p.IsShared).HasDefaultValue(false);
                d.HasOne(p => p.BelongProductLine).WithMany(d => d.ManageWorkStations).HasForeignKey(p => p.BelongProductLineId);
            });

            #endregion

            #region 工艺建模

            modelBuilder.Entity<MaterialCategory>(d =>
            {
                d.Property(p => p.Id).IsRequired(true).ValueGeneratedNever();
                d.HasIndex(p => p.CategoryCode).IsUnique(false);
                d.Property(p => p.CategoryCode).HasMaxLength(StringMiddleLength);
                d.Property<string>(p => p.CategoryName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.FullCategoryName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.CategoryDescription).HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<MaterialInfo>(d =>
            {
                d.Property(p => p.Id).IsRequired(true).ValueGeneratedNever();
                d.HasIndex(p => p.MaterialNumber).IsUnique(false);
                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.UnitName).IsRequired(false).HasMaxLength(StringShortLength);
                d.Property(p => p.Specification).IsRequired(false).HasMaxLength(StringLongLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.HasOne(p => p.BelongCategory).WithMany(d => d.MaterialInfos).HasForeignKey(p => p.BelongCategoryId);
            });

            modelBuilder.Entity<CutMaterialConfig>(d =>
            {
                d.Property(p => p.Id).IsRequired(true);

                d.HasIndex(p => p.ConfigMaterialNumber).IsUnique(false);
                d.Property(p => p.ConfigMaterialName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ConfigMaterialUnitName).IsRequired(false).HasMaxLength(StringShortLength);
                d.Property(p => p.CutSpecification).IsRequired(true).HasMaxLength(StringLongLength);
                d.Property(p => p.CutUnitName).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.ConversionRatio).IsRequired(true).HasPrecision(15, 4);
                d.Property(p => p.ProductMaterialNumber).IsRequired(false).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ConfigMaterialNumber).HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<MaterialBatchNumberRuler>(d =>
            {
                d.Property(p => p.FlowNumberRuler).IsRequired().HasMaxLength(StringMiddleLength);
                d.HasOne(p => p.MaterialCategoryInfo).WithOne(d => d.BatchNumberRuler).HasForeignKey<MaterialBatchNumberRuler>(p => p.MaterialCategoryInfoId);
            });

            modelBuilder.Entity<WorkProcessInfo>(d =>
            {
                d.Property(p => p.ProcessNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ProcessName).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<WorkProcessSet>(d =>
            {
                d.Property(p => p.SetVersion).IsRequired(true).HasMaxLength(StringShortLength);
                d.Property(p => p.Descreption).HasMaxLength(StringMiddleLength);
                d.Property(p => p.ExtensionData).IsRequired(false).HasColumnType("text");
                d.Property(p => p.GraphData).IsRequired(false).HasColumnType("text");
            });

            modelBuilder.Entity<BomInfo>(d =>
            {
                d.Property(p => p.Id).ValueGeneratedNever();
                d.HasOne(p => p.Material).WithMany(d => d.BomInfos).HasForeignKey(p => p.MaterialId);

                d.Property(p => p.MaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.MaterialName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.Version).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<BomItemInfo>(d =>
            {
                d.HasOne(p => p.BelongBom).WithMany(d => d.BomItems).HasForeignKey(p => p.BelongBomId);

                d.Property(p => p.Specification).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.FormMaterialName).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(p => p.FormMaterialNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.Property(d => d.FormCount).HasPrecision(20, 3);
                d.Property(d => d.LossFactor).HasPrecision(20, 3);
            });

            modelBuilder.Entity<WorkProcessSetBom>(d =>
            {
                d.HasOne(p => p.BelongWorkProcessSet).WithMany(d => d.WorkProcessSetBoms).HasForeignKey(p => p.BelongWorkProcessSetId);
                d.Property(p => p.Version).IsRequired(true).HasMaxLength(StringShortLength);
            });

            modelBuilder.Entity<WorkProcessSetBomItem>(d =>
            {
                d.Property(p => p.InputMaterialCount).HasPrecision(20, 3);
            });

            modelBuilder.Entity<WorkOrderBom>(d =>
            {
                d.HasIndex(p => p.WorkOrderNumber).IsUnique(false);
                d.HasIndex(p => p.MaterialId).IsUnique(false);
                d.Property(p => p.WorkOrderNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                d.HasOne(p => p.WorkOrderInfo).WithOne(p => p.WorkOrderBom).HasForeignKey<WorkOrderBom>(p => p.WorkOrderId).OnDelete(DeleteBehavior.NoAction);
                d.HasOne(p => p.WorkProcessSetBom).WithMany(p => p.WorkOrderBoms).HasForeignKey(p => p.WorkProcessSetBomId);
            });

            modelBuilder.Entity<WorkOrderBomItem>(d =>
            {
                d.Property(p => p.InputMaterialCount).HasPrecision(20, 3);
            });

            #endregion

            #region 质量控制

            modelBuilder.Entity<ProblemCategory>(p =>
            {
                p.Property(p => p.CategoryCode).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.CategoryName).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.FullCategoryName).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.CategoryDescription).HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<QualityProblemDefine>(p =>
            {
                p.Property(p => p.QualityProblemNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.HasIndex(p => p.QualityProblemNumber);
                p.Property(p => p.ProbleName).IsRequired(true).HasMaxLength(StringShortLength);
                p.Property(p => p.Description).HasMaxLength(StringMiddleLength);
            });

            #endregion

            #region 工单信息

            modelBuilder.Entity<WorkOrderInfo>(p =>
            {
                p.Property(p => p.OrderNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.FromOrderNumber).IsRequired(false).HasMaxLength(StringMiddleLength);
                p.Property(p => p.ProduceCount).HasPrecision(20, 3);
                p.Property(p => p.ProdcuingCount).HasPrecision(20, 3);
                p.Property(p => p.FinishedCount).HasPrecision(20, 3);
                p.Property(p => p.ProjectName).HasMaxLength(StringLongLength);
                p.Property(p => p.ProjectNumber).HasMaxLength(StringLongLength);
                p.Property(p => p.Remark).HasMaxLength(StringLongLength);

                p.HasIndex(p => p.OrderNumber);
                p.HasIndex(p => new { p.PlanStartTime, p.PlanEndTime });
                p.HasIndex(p => p.WorkOrderStatu);
                p.HasIndex(p => p.ProjectNumber);

                p.Ignore(p => p.CustomerProductInfo);
                
            });

            modelBuilder.Entity<View_OrderMaterialProduceStatuses>(p =>
            {
                p.ToView("View_OrderMaterialProduceStatuses");
            });

            modelBuilder.Entity<OrderMaterialProduceStatu>(p =>
            {
                p.HasIndex(p => p.WorkOrderNumber);
                p.HasIndex(p => p.MaterialBatchNumber);
                p.HasIndex(p => p.ProduceStatus);
                p.HasIndex(p => p.LastUpdateTime);
                p.HasIndex(p => p.CurrentProductLineId);
                p.HasIndex(p => p.CurrentWorkProcessId);
                p.HasIndex(p => p.CurrentWorkStationId);

                p.Property(p => p.WorkOrderNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.MaterialBatchNumber).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.CurrentMatrialCount).IsRequired(true).HasDefaultValue(1);
                p.Ignore(p => p.IsRepairing);
            });

            modelBuilder.Entity<ProjectInfo>(p =>
            {
                p.Property(p => p.Id).ValueGeneratedNever();
                p.HasIndex(p => p.ProjectNumber);

                p.Property(p => p.ProjectNumber).HasMaxLength(StringMiddleLength);
                p.Property(p => p.ProjectName).HasMaxLength(StringMiddleLength);
                p.Property(p => p.ProjectFullName).HasMaxLength(StringMiddleLength);
            });

            #endregion

            #region 动态表单信息

            modelBuilder.Entity<FormTemplateInfo>(p =>
            {
                p.HasIndex(p => p.FormsName).IsUnique(false);
                p.Property(p => p.FormsName).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.Version).IsRequired(false).HasMaxLength(StringMiddleLength);
                p.Property(p => p.TemplateData).IsRequired(false).HasColumnType("text");
                p.Property(p => p.SaveEntityType).IsRequired(false).HasMaxLength(StringMiddleLength);
            });

            #endregion

            #region 同步任务

            modelBuilder.Entity<ERPSyncTask.ERPSyncTask>(p =>
            {
                p.HasIndex(p => p.SyncState);
                p.HasIndex(p => p.SyncType);
                p.HasIndex(p => p.CreateTime);

                p.Property(p => p.StoredName).IsRequired(true).HasMaxLength(StringMiddleLength);
                p.Property(p => p.StoredParameter).IsRequired(true).HasMaxLength(StringMiddleLength);
            });

            #endregion

            #region 线边库物料

            modelBuilder.Entity<LineSideMaterialInfo>(p =>
            {
                p.Property(p => p.MaterialName).HasMaxLength(StringMiddleLength);
                p.Property(p => p.UnitName).HasMaxLength(StringMiddleLength);
                p.HasMany(p => p.BomItems).WithOne(d => d.LineSideMaterialInfo).HasForeignKey(d => d.LineSideMaterialInfoId);
                p.Property(p=>p.MaterialNumber).HasMaxLength(StringMiddleLength);
                p.Property(p=>p.BelongCategoryNumber).HasMaxLength(StringMiddleLength);
            });


            modelBuilder.Entity<LineSideMaterialInfoBomItem>(p =>
            {
                p.Property(d => d.FormMaterialCategoryName).HasMaxLength(StringMiddleLength);
                p.Property(d => d.FormMaterialCategoryNumber).HasMaxLength(StringMiddleLength);
            });

            #endregion
        }
    }
}
