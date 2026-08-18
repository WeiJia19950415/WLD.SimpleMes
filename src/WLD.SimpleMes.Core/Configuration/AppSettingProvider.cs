using System.Collections.Generic;
using Abp.Configuration;

namespace WLD.SimpleMes.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            string defatulDDMachineString = "[{ProductLineId:\"1\",TestMachineNumber:\"LS010199\"},{ProductLineId:\"2\",TestMachineNumber:\"lS010243\"}]";
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                 new SettingDefinition(AppSettingNames.WebTitleName, "全钒液流电池制造执行系统", scopes: SettingScopes.Application | SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.WebBgImg, "/Images/bg.jpg", scopes: SettingScopes.Application | SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.WebLogoImg, "/Images/logo.png", scopes: SettingScopes.Application | SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.FactoryCode, "L", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.CanReusedBatchNumber, "False", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.CanStandWorkProcessModifyMaterialInfo, "True", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.ShiftInfo, "", scopes: SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.CanMixedProductLine, "False", scopes: SettingScopes.Application | SettingScopes.Tenant),
                    new SettingDefinition(AppSettingNames.DDTestReportConfig, "", scopes: SettingScopes.Application | SettingScopes.Tenant),
                     new SettingDefinition(AppSettingNames.OverDayConfing, "1", scopes: SettingScopes.Application | SettingScopes.Tenant),
                     new SettingDefinition(AppSettingNames.DDPrintFixedInfos, "", scopes: SettingScopes.Application | SettingScopes.Tenant),
                      new SettingDefinition(AppSettingNames.DDTestMachineConfig,defatulDDMachineString, scopes: SettingScopes.Application | SettingScopes.Tenant),
                      new SettingDefinition(AppSettingNames.IgnoreBomUniteName,"平方米,平方厘米", scopes: SettingScopes.Application | SettingScopes.Tenant),
                      new SettingDefinition(AppSettingNames.BigScreenMaterialNameReplaceConfig,"", scopes: SettingScopes.Application | SettingScopes.Tenant),
            };
        }
    }
}


