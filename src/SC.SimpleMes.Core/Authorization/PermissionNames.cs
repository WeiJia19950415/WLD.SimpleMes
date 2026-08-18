namespace SC.SimpleMes.Authorization
{
    public static class PermissionNames
    {
        #region 01基础配置管理
        /// <summary>
        /// 租户管理
        /// </summary>
        public const string Pages_Tenants = "Page.01_01_Tenants";

        /// <summary>
        /// 用户管理
        /// </summary>
        public const string Pages_Users = "Page.01_02_Users";

        /// <summary>
        /// 用户激活
        /// </summary>
        public const string Pages_Users_Activation = "Page.01_02_01_Users.Activation";

        /// <summary>
        /// 用户密码重置
        /// </summary>
        public const string Pages_Users_ResetPassWord = "Page.01_02_02_Users.ResetPassWord";

        /// <summary>
        /// 角色管理
        /// </summary>
        public const string Pages_Roles = "Page.01_03_Roles";

        /// <summary>
        /// 部门管理功能
        /// </summary>
        public const string Page_OrgMange = "Page.01_03_OrgMange";

        /// <summary>
        /// 系统配置管理
        /// </summary>
        public const string Pages_Setting = "Page.01_04_Setting";

        /// <summary>
        /// 日志管理
        /// </summary>
        public const string Pages_JournalLog = "Page.01_05_JournalLog";

        /// <summary>
        /// 调度任务管理
        /// </summary>
        public const string Pages_Job = "Page.01_05_Job";


        #endregion

        #region 02 产线建模

        /// <summary>
        ///  车间管理
        /// </summary>
        public const string Pages_WorkShopMange = "Page.02_01_WorkShopMangage";

        /// <summary>
        /// 产线管理
        /// </summary>
        public const string Page_ProductLineManange = "Page.02_02_ProductLineManange";

        /// <summary>
        /// 编辑产线信息
        /// </summary>
        public const string Page_EditProductLine = "Page.02_02_02_EditProductLine";

        /// <summary>
        /// 配置产线操作人员
        /// </summary>
        public const string Page_CofingProductLineUser = "Page.02_02_01_CofingUser";

        /// <summary>
        /// 工位管理
        /// </summary>
        public const string Page_WorkStationManage = "Page.02_03_WorkStationManage";

        /// <summary>
        /// 配置工序操作人员
        /// </summary>
        public const string Page_CofingWorkStationUser = "Page.02_03_01_CofingUser";

        /// <summary>
        /// 编辑工序信息
        /// </summary>
        public const string Page_CofingWorkStation = "Page.02_03_02_CofingWorkStation";

        #endregion

        #region 03工艺建模

        /// <summary>
        /// 物料管理
        /// </summary>
        public const string Page_Material = "Page.03_01_Material";

        /// <summary>
        /// 物料信息管理
        /// </summary>
        public const string Page_MaterialInfo = "Page.03_01_00_MaterialInfo";

        /// <summary>
        /// 物料供应商信息
        /// </summary>
        public const string Page_MaterialInfoSupplier = "Page.03_01_00_MaterialInfoSupplier";

        /// <summary>
        /// 物料分类管理
        /// </summary>
        public const string Page_Material_Category = "Page.03_01_01_Material_Category";

        /// <summary>
        /// 物料裁切配置管理
        /// </summary>
        public const string Page_CutMaterialConfig = "Page.03_01_02_01_CutMaterialConfig";

        /// <summary>
        /// 物料编码管理功能
        /// </summary>
        public const string Page_Material_Ruler = "Page.03_01_02_Material_Ruler";

        /// <summary>
        /// Bom管理
        /// </summary>
        public const string Page_BomManager = "Page.03_02_BomManager";

        /// <summary>
        /// 工序管理
        /// </summary>
        public const string Page_ProcessManage = "Page.03_03_WorkProcessManage";

        /// <summary>
        /// 工艺管理
        /// </summary>
        public const string Page_ProcessSetManage = "Page.03_04_WorkProcessSetManage";

        /// <summary>
        /// 工艺BOM管理
        /// </summary>
        public const string Page_SetBomManager = "Page.03_05_SetBomManager";

        /// <summary>
        /// 产品工艺管理
        /// </summary>
        public const string Page_ProudctProcessSetManage = "Page.03_06_ProudctProcessSetManage";

        #endregion

        #region 04 工单管理

        /// <summary>
        /// 工单管理页面
        /// </summary>
        public const string Page_WorkOrderManage = "Page.04_01_WorkOrderManage";


        /// <summary>
        /// 工单信息管理
        /// </summary>
        public const string Page_WorkOrderInfoManage = "Page.04_01_00_WorkOrderInfoManage";

        /// <summary>
        /// 工单下发
        /// </summary>
        public const string Page_WorkOrderManage_Issues = "Page.04_01_01_WorkOrderManage_Issues";

        /// <summary>
        /// 工单取消
        /// </summary>
        public const string Page_WorkOrderManage_Cancel = "Page.04_01_01_WorkOrderManage_Cancel";

        /// <summary>
        /// 撤回
        /// </summary>
        public const string Page_WorkOrderManage_Revert = "Page.04_01_01_WorkOrderManage_Revert";

        /// <summary>
        /// 暂停
        /// </summary>
        public const string Page_WorkOrderManage_Pause = "Page.04_01_01_WorkOrderManage_Pause";

        /// <summary>
        /// 恢复
        /// </summary>
        public const string Page_WorkOrderManage_Recover = "Page.04_01_01_WorkOrderManage_Recover";

        /// <summary>
        /// 关闭订单
        /// </summary>
        public const string Page_WorkOrderManage_Close = "Page.04_01_01_WorkOrderManage_Close";

        /// <summary>
        /// 工单编号管理
        /// </summary>
        public const string Page_WorkOrderManage_SNManage = "Page.04_01_01_WorkOrderManage_SNManage";

        /// <summary>
        /// 客制化产品信息显示
        /// </summary>
        public const string Page_WorkOrderManage_SetCustomerProductInfo = "Page.04_01_01_SetCustomerProductInfo";

        /// <summary>
        /// 序列号管理
        /// </summary>
        public const string Page_SNBatcNumberManage = "Page.04_02_01_SNBatcNumberManage";

        /// <summary>
        /// 序列号列表查询
        /// </summary>
        public const string Page_SNBatcNumberInfoManage = "Page.04_02_03_SNBatcNumberInfoManage";

        /// <summary>
        /// 批次号序列号删除
        /// </summary>
        public const string Page_SNBatcNumberInfoDel = "Page.04_02_04_SNBatcNumberInfoDel";


        /// <summary>
        /// 序列号人工插入
        /// </summary>
        public const string Page_SNBatcNumberManage_ManuallyInsert = "Page.04_02_02_ManuallyInsert";
        #endregion

        #region 05 动态表单管理
        public const string Page_WorkPrcessFormManage = "Page.05_01_WorkPrcessFormManage";
        #endregion

        #region 06 质量管理
        public const string Page_QualityManage = "Page.06_01_QualityManage";

        /// <summary>
        /// 质量分类管理
        /// </summary>
        public const string Page_QualityManage_Category = "Page.06_02_QualityManage_Category";

        /// <summary>
        /// 质量问题定义管理
        /// </summary>
        public const string Page_QualityManage_ProblemDefine = "Page.06_03_QualityManage_ProblemDefine";

        /// <summary>
        /// 异常处理
        /// </summary>
        public const string Page_QualityManager_ProblemDeal = "Page.06_04_QualityManage_ProblemDeal";

        /// <summary>
        /// 质量判定
        /// </summary>
        public const string Page_QualityManager_ProblemJudge = "Page.06_06_QualityManage_ProblemJudge";

        /// <summary>
        /// IPQC巡检
        /// </summary>
        public const string Page_QualityManager_IPQC = "Page.06_05_QualityManage_IPQC";

        /// <summary>
        /// QC质检权限
        /// </summary>
        public const string Page_QualityManager_QC = "Page.06_05_QualityManage_QC";

        /// <summary>
        /// 复测权限
        /// </summary>
        public const string Page_QualityManager_Retest = "Page.06_05_QualityManage_Retest";

        /// <summary>
        /// 更新填报表单数据
        /// </summary>
        public const string Page_QualityManager_UpdateFormInfo = "Page.06_05_QualityManage_UpdateFormInfo";

        #endregion

        #region 07 批次号打印
        /// <summary>
        /// 批次号打印
        /// </summary>
        public const string Page_BatchNoByInStockInfo = "Page.07_BatchNoByInStockInfo";
        public const string Page_BatchNoByInStockInfo_ProductSN = "Page.07_BatchNoByInStockInfo_ProductSN";
        public const string Page_BatchNoByInStockInfo_Nameplate = "Page.07_BatchNoByInStockInfo_Nameplate";
        #endregion

        #region 08 PAD端操作权限
        public const string Page_PadOperation = "Page.PadOperation";
        public const string Page_PadReport = "Page.PadReport";
        #endregion

        #region 09 特别权限

        public const string BaseInfo_Edit = "Page.BaseInfo_Edit";

        public const string Data_SinlgeOperatorRecord = "Data_SinlgeProduceRecord";
        #endregion

        #region 10 报表权限管理

        public const string Page_Report = "Page.10_Report";
        public const string Report_StationCapacity = "Page.10_01_Report_StationCapacity";// 工位产能统计报表
        public const string Report_ProductLineCapacity = "Page.10_02_Report_ProductLineCapacity";// 产线产能统计报表
        public const string Report_QualitityRecord = "Page.10_03_Report_QualitityRecord";// 产线产能统计报表
        public const string Report_QualitityReport = "Page.10_04_QualitityReport";// 质量统计报表
        public const string Report_DDKeyInfoReport = "Page.10_05_DDKeyInfoReport";// 电堆效能报表
        public const string Report_AuditeDDKeyInfoReport = "Page.10_05_AuditeDDKeyInfoReport";// 电堆效能报表

        public const string Report_WorkProcessOnePassRate = "Page.10_06_WorkProcessOnePassRate";// 电堆效能报表
        public const string Report_PrepaireWorkPorcess = "Page.10_07_PrepaireWorkPorcess";// 前置工序物料准备报表
        public const string Report_OrderMaterialProduceStatuse = "Page.10_08_OrderMaterialProduceStatuse";// 产品生产状态报表

        public const string Report_OrderMaterialProduceStatuse_ProduceRecord = "Page.10_08_OrderMaterialProduceStatuse_ProduceRecord";// 产品生产记录权限


        public const string Report_DayBigScreen = "Page.10_09_DayBigScreen";// 产品生产日大屏
        public const string Report_MonthBigScreen = "Page.10_09_MonthBigScreen";// 产品生产日大屏
        public const string Report_BatchMaterialUsedReport = "Page.10_10_BatchMaterialUsedReport";//批次物料使用情况报表
        public const string Report_WorkOrderMaterialUsedReport = "Page.10_11_WorkOrderMaterialUsedReport";//工单物料使用情况报表

        public const string Report_ProductConstructMaterialInfos = "Page.10_12_ProductConstructMaterialInfos";//工单物料使用情况报表

        /// <summary>
        /// 前置工序人员绩效统计报表
        /// </summary>
        public const string Report_PrepareUserWorkStatic = "Page.10_13_PrepareUserWorkStatic";//前置工序人员绩效统计报表


        /// <summary>
        /// 电堆测试绩效统计报表
        /// </summary>
        public const string Report_DDTestDayKPI = "Page.10_14_DDTestDayKPI";// 电堆测试绩效统计报表

        /// <summary>
        /// 电堆一次性通过率报表
        /// </summary>
        public const string Report_DDWeekOnePassRate = "Page.10_15_DDWeekOnePassRate";// 电堆一次性通过率报表

        /// <summary>
        /// 产品部门工序加工量统计报表
        /// </summary>
        public const string Report_OrgProductProcessWorkLoadReport = "Page.10_16_OrgProductProcessWorkLoadReport";//产品工序统计报表

        /// <summary>
        /// 售后物料报表
        /// </summary>
        public const string Report_RepairedMatreialReport = "Page.10_17_RepairedMatreialReport";// 售后物料报表

        /// <summary>
        /// 物料报废报表
        /// </summary>
        public const string Report_MatreialDiscardReport = "Page.10_18_MatreialDiscardReport";// 物料报废报表
        #endregion

        #region 11 线边库权限管理

        public const string OnlineStock_StockManage = "Page.11_StockManage";
        public const string OnlineStock_MaterialInfoManager = "Page.11_01_MaterialInfoManager";// 物料基础信息管理
        public const string OnlineStock_StockReport = "Page.11_02_StockReport";// 库存物料信息
        public const string OnlineStock_StockRecordReport = "Page.11_03_StockRecordRepor";// 库存信息记录报表
        public const string OnlineStock_StockManageRecord = "Page.11_04_StockManageRecord";// 库存信息增加记录操作

        

        #endregion
    }
}

