using Abp.AspNetCore.Mvc.ViewComponents;

namespace WLD.SimpleMes.Web.Views
{
    public abstract class SimpleMesViewComponent : AbpViewComponent
    {
        protected SimpleMesViewComponent()
        {
            LocalizationSourceName = SimpleMesConsts.LocalizationSourceName;
        }
    }
}

