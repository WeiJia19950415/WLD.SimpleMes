using Abp.Application.Features;
using Abp.Localization;

namespace SC.SimpleMes.Authorization
{
    /// <summary>
    /// 配置应用授权
    /// </summary>
    public class SimpleMesFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
   

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SimpleMesConsts.LocalizationSourceName);
        }
    }
}

