using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace WLD.SimpleMes.Localization
{
    public static class SimpleMesLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SimpleMesConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SimpleMesLocalizationConfigurer).GetAssembly(),
                        "WLD.SimpleMes.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}

