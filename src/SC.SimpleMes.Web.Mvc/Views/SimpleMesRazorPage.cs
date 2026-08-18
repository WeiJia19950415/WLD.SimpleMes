using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace WLD.SimpleMes.Web.Views
{
    public abstract class SimpleMesRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected SimpleMesRazorPage()
        {
            LocalizationSourceName = SimpleMesConsts.LocalizationSourceName;
        }
    }
}

