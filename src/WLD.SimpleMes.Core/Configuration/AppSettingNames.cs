namespace WLD.SimpleMes.Configuration
{
    public static class AppSettingNames
    {
        public const string UiTheme = "App.UiTheme";
        public const string WebTitleName = "WebTitleName";
        public const string WebLogoImg = "WebLogoImg";
        public const string WebBgImg = "WebBgImg";
        public const string FactoryCode = "FactoryCode";
        /// <summary>
        /// 能否重复使用物料批次号
        /// </summary>
        public const string CanReusedBatchNumber = "CanReusedBatchNumber";

        /// <summary>
        /// 标准工序能否修改工序物料信息
        /// </summary>
        public const string CanStandWorkProcessModifyMaterialInfo = "StandWorkProcessCanModifyMaterialInfo";

        public const string ShiftInfo = "ShiftInfo";

        /// <summary>
        /// 能否产线混用
        /// </summary>
        public const string CanMixedProductLine = "CanMixedProductLine";

        /// <summary>
        /// 测试报表配置
        /// </summary>
        public const string DDTestReportConfig = "DDTestReportConfig";

        /// <summary>
        /// 电堆打印配置固定参数
        /// </summary>
        public const string DDPrintFixedInfos = "DDPrintFixedInfos";

        /// <summary>
        /// 电堆测试机器配置信息
        /// </summary>
        public const string DDTestMachineConfig = "DDTestMachineConfig";

        /// <summary>
        /// 配置超期天数警告
        /// </summary>
        public const string OverDayConfing = "超期天数";

        /// <summary>
        /// BOM计算中忽略检查数量的单位
        /// </summary>
        public const string IgnoreBomUniteName= "IgnoreBomUniteName";

        /// <summary>
        /// 大屏展示的物料名称
        /// </summary>
        public const string BigScreenMaterialNameReplaceConfig = "替换的物料名称(原名称,现名称)";


    }
}

