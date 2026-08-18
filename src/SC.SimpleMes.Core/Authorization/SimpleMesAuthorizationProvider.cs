using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SC.SimpleMes.Authorization
{
    public class SimpleMesAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            #region 01基础功能模块[基础配置+日志+定时任务]
            var userPermission = context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            userPermission.CreateChildPermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            userPermission.CreateChildPermission(PermissionNames.Pages_Users_ResetPassWord, L("ResetPassWord"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_Job, L("Job"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_JournalLog, L("JournalLog"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Setting, L("Setting"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_OrgMange, L("OrgMange"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            #endregion

            #region 02产线建模
            
            // 车间管理
            context.CreatePermission(PermissionNames.Pages_WorkShopMange, L("WorkShopMangage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            
            var staionPermission = context.CreatePermission(PermissionNames.Page_WorkStationManage, L("WorkStationManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            staionPermission.CreateChildPermission(PermissionNames.Page_CofingWorkStationUser, L("CofingWorkStationUser"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            staionPermission.CreateChildPermission(PermissionNames.Page_CofingWorkStation, L("CofingWorkStation"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            var productLinePermission = context.CreatePermission(PermissionNames.Page_ProductLineManange, L("ProductLineManange"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            productLinePermission.CreateChildPermission(PermissionNames.Page_EditProductLine, L("EditProductLine"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            productLinePermission.CreateChildPermission(PermissionNames.Page_CofingProductLineUser, L("CofingProductLineUser"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            #endregion

            #region 03 工艺管理
            // 工艺建模
            var materialPermission = context.CreatePermission(PermissionNames.Page_Material, L("MaterialManager"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            materialPermission.CreateChildPermission(PermissionNames.Page_MaterialInfo, L("MaterialInfoManage"));
            materialPermission.CreateChildPermission(PermissionNames.Page_MaterialInfoSupplier, L("MaterialInfoSupplier"));
            materialPermission.CreateChildPermission(PermissionNames.Page_Material_Category, L("MaterialCategoryManage"));
            materialPermission.CreateChildPermission(PermissionNames.Page_Material_Ruler, L("MaterialRulerManage"));
            materialPermission.CreateChildPermission(PermissionNames.Page_CutMaterialConfig, L("CutMaterialConfig"));

            context.CreatePermission(PermissionNames.Page_BomManager, L("BomManager"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_ProcessManage, L("ProcessManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_ProcessSetManage, L("ProcessSetManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_SetBomManager, L("SetBomManager"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_ProudctProcessSetManage, L("ProudctProcessSetManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            #endregion

            #region 04 工单管理
            var workWorderManagePermission = context.CreatePermission(PermissionNames.Page_WorkOrderManage, L("WorkOrderManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Issues, L("WorkOrderManage_Issues"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Cancel, L("WorkOrderManage_Cancel"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderInfoManage, L("WorkOrderInfoManage"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Recover, L("WorkOrderManage_Recover"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Revert, L("WorkOrderManage_Revert"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Pause, L("WorkOrderManage_Pause"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_Close, L("WorkOrderManage_Close"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_SNManage, L("WorkOrderManage_SNManage"));
            workWorderManagePermission.CreateChildPermission(PermissionNames.Page_WorkOrderManage_SetCustomerProductInfo, L("SetCustomerProductInfo"));

            // 序列号管理
            var snManagePermission = context.CreatePermission(PermissionNames.Page_SNBatcNumberManage, L("SNBatcNumberManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            snManagePermission.CreateChildPermission(PermissionNames.Page_SNBatcNumberInfoManage, L("SNBatcNumberInfoManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            snManagePermission.CreateChildPermission(PermissionNames.Page_SNBatcNumberManage_ManuallyInsert, L("SNBatcNumberManage_ManuallyInsert"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            snManagePermission.CreateChildPermission(PermissionNames.Page_SNBatcNumberInfoDel, L("SNBatcNumberInfoDel"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            #endregion

            #region 05 动态表单管理
            context.CreatePermission(PermissionNames.Page_WorkPrcessFormManage, L("WorkPrcessFormManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            #endregion

            #region 06 质量管理
            var qualityManage = context.CreatePermission(PermissionNames.Page_QualityManage, L("QualityManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManage_Category, L("QualityManage_Category"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManage_ProblemDefine, L("QualityManage_ProblemDefine"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_ProblemDeal, L("QualityManage_ProblemDeal"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_ProblemJudge, L("QualityManage_ProblemJudge"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_IPQC, L("QualityManage_IPQC"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_QC, L("QualityManage_QC"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_Retest, L("QualityManager_Retest"));
            qualityManage.CreateChildPermission(PermissionNames.Page_QualityManager_UpdateFormInfo, L("QualityManager_UpdateFormInfo"));
            #endregion

            #region 07 ERP入库单信息管理
            context.CreatePermission(PermissionNames.Page_BatchNoByInStockInfo, L("BatchNoByInStockInfo"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_BatchNoByInStockInfo_ProductSN, L("BatchNoByInStockInfo_ProductSN"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_BatchNoByInStockInfo_Nameplate, L("BatchNoByInStockInfo_Nameplate"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            #endregion

            #region 08 PAD端操作权限
            context.CreatePermission(PermissionNames.Page_PadOperation, L("PadOperation"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Page_PadReport, L("PadReport"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            #endregion

            context.CreatePermission(PermissionNames.BaseInfo_Edit, L("BaseInfo_Edit"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Data_SinlgeOperatorRecord, L("SinlgeOperatorRecord"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            #region 10 报表权限管理

            var reportManage = context.CreatePermission(PermissionNames.Page_Report, L("ReportManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            reportManage.CreateChildPermission(PermissionNames.Report_StationCapacity, L("Report_StationCapacity"));
            reportManage.CreateChildPermission(PermissionNames.Report_ProductLineCapacity, L("Report_ProductLineCapacity"));
            reportManage.CreateChildPermission(PermissionNames.Report_QualitityRecord, L("Report_QualitityRecord"));
            reportManage.CreateChildPermission(PermissionNames.Report_QualitityReport, L("Report_QualitityReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_DDKeyInfoReport, L("Report_DDKeyInfoReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_AuditeDDKeyInfoReport, L("Report_AuditeDDKeyInfoReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_WorkProcessOnePassRate, L("Report_WorkProcessOnePassRate"));
            reportManage.CreateChildPermission(PermissionNames.Report_PrepaireWorkPorcess, L("Report_PrepaireWorkPorcess"));
            reportManage.CreateChildPermission(PermissionNames.Report_OrderMaterialProduceStatuse, L("Report_OrderMaterialProduceStatuse"));
            reportManage.CreateChildPermission(PermissionNames.Report_OrderMaterialProduceStatuse_ProduceRecord, L("ProduceRecord"));
            reportManage.CreateChildPermission(PermissionNames.Report_DayBigScreen, L("Report_DayBigScreen"));
            reportManage.CreateChildPermission(PermissionNames.Report_MonthBigScreen, L("Report_MonthBigScreen"));
            reportManage.CreateChildPermission(PermissionNames.Report_BatchMaterialUsedReport, L("Report_BatchMaterialUsedReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_WorkOrderMaterialUsedReport, L("Report_WorkOrderMaterialUsedReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_ProductConstructMaterialInfos, L("Report_ProductConstructMaterialInfos"));
            reportManage.CreateChildPermission(PermissionNames.Report_DDTestDayKPI, L("Report_DDTestDayKPI"));
            reportManage.CreateChildPermission(PermissionNames.Report_PrepareUserWorkStatic, L("Report_PrepareUserWorkStatic"));
            reportManage.CreateChildPermission(PermissionNames.Report_DDWeekOnePassRate, L("Report_DDWeekOnePassRate"));
            reportManage.CreateChildPermission(PermissionNames.Report_OrgProductProcessWorkLoadReport, L("OrgProductProcessWorkLoadReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_RepairedMatreialReport, L("RepairedMatreialReport"));
            reportManage.CreateChildPermission(PermissionNames.Report_MatreialDiscardReport, L("MatreialDiscardReport"));

            #endregion


            #region 11 线边库权限管理

            var stockPermission = context.CreatePermission(PermissionNames.OnlineStock_StockManage, L("OnlineStock_StockManage"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            stockPermission.CreateChildPermission(PermissionNames.OnlineStock_StockManageRecord, L("OnlineStock_StockManageRecord"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            stockPermission.CreateChildPermission(PermissionNames.OnlineStock_StockRecordReport, L("OnlineStock_StockRecordReport"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            stockPermission.CreateChildPermission(PermissionNames.OnlineStock_StockReport, L("OnlineStock_StockReport"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
            stockPermission.CreateChildPermission(PermissionNames.OnlineStock_MaterialInfoManager, L("OnlineStock_MaterialInfoManager"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            #endregion

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SimpleMesConsts.LocalizationSourceName);
        }
    }
}



