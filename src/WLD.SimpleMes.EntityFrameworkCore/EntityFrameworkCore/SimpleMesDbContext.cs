using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using WLD.SimpleMes.Authorization.Roles;
using WLD.SimpleMes.Authorization.Users;
using WLD.SimpleMes.MultiTenancy;
using Abp.Authorization.Users;
using Abp.Auditing;
using WLD.SimpleMes.Material;
using WLD.SimpleMes.WorkProcess;
using WLD.SimpleMes.WorkStation;
using WLD.SimpleMes.BOM;
using WLD.SimpleMes.WorkOrder;
using WLD.SimpleMes.QualityControl;
using WLD.SimpleMes.DynamicForms;
using WLD.SimpleMes.LineSideWarehouse;

namespace WLD.SimpleMes.EntityFrameworkCore
{
    /// <summary>
    /// SimpleMes数据库上下文
    /// </summary>
    public partial class SimpleMesDbContext : AbpZeroDbContext<Tenant, Role, User, SimpleMesDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public SimpleMesDbContext(DbContextOptions<SimpleMesDbContext> options)
            : base(options)
        {

        }

        public DbSet<JHTOrganzation.JHTOrganzation> JHTOrganzations { get; set; }

        public DbSet<ViewUser> ViewUsers { get; set; }

        #region 工厂建模
        public DbSet<WorkShopInfo> WorkShopInfos { get; set; }

        public DbSet<ProductLine> ProductLines { get; set; }

        public DbSet<WorkStationInfo> WorkStationInfos { get; set; }

        public DbSet<WorkStationUserRelation> WorkStationUserRelations { get; set; }

        public DbSet<ProductLineUserRelation> ProductLineUserRelations { get; set; }

        #endregion

        #region 工艺建模

        public DbSet<MaterialInfo> MaterialInfos { get; set; }
        public DbSet<MaterialCategory> MaterialCategories { get; set; }

        public DbSet<CutMaterialConfig> CutMaterialConfig { get; set; }

        public DbSet<MaterialBatchNumberRuler> MaterialBatchNumberRulers { get; set; }
        public DbSet<MaterialReplaceRelation> MaterialReplaceRelations { get; set; }

        public DbSet<WorkProcessInfo> WorkProcessInfos { get; set; }

        public DbSet<WorkProcessSet> WorkProcessSets { get; set; }


        // 考虑下
        // public DbSet<WorkProcessSetDetail> WorkProcessSetDetails { get; set; }  

        public DbSet<WorkProcessSetProductRelation> WorkProcessSetProductRelations { get; set; }

        public DbSet<WorkProcessStationRelation> WorkProcessStationRelations { get; set; }

        public DbSet<WorkProcessFormInfoRelation> WorkProcessFormInfoRelations { get; set; }

        public DbSet<BomInfo> BomInfos { get; set; }

        public DbSet<BomItemInfo> BomItemInfos { get; set; }

        public DbSet<WorkProcessSetBom> WorkProcessSetBoms { get; set; }

        public DbSet<WorkProcessSetBomItem> WorkProcessSetBomItems { get; set; }

        public DbSet<WorkOrderBom> WorkOrderBoms { get; set; }

        public DbSet<WorkOrderBomItem> WorkOrderBomItems { get; set; }

        #endregion

        #region 质量控制

        public DbSet<ProblemCategory> ProblemCategories { get; set; }

        public DbSet<QualityProblemDefine> QualityProblemDefines { get; set; }

        #endregion

        #region 工单信息

        public DbSet<WorkOrderInfo> WorkOrderInfos { get; set; }

        public DbSet<OrderMaterialProduceStatu> OrderMaterialProduceStatuses { get; set; }

        public DbSet<View_OrderMaterialProduceStatuses> ViewOrderMaterialProduceStatuses { get; set; }

        #endregion

        #region 动态表单

        public DbSet<FormTemplateInfo> FormTemplateInfos { get; set; }

        #endregion


        #region ERP同步任务
        public DbSet<ERPSyncTask.ERPSyncTask> ERPSyncTasks { get; set; }
        #endregion

        #region 线边库

        public DbSet<LineSideMaterialInfo> LineSideMaterialInfos { get; set; }

        public DbSet<LineSideMaterialInfoBomItem> LineSideMaterialInfoBomItems { get; set; }

        #endregion
    }
}

