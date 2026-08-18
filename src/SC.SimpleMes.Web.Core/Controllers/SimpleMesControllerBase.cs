using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SC.SimpleMes.Controllers
{
    public abstract class SimpleMesControllerBase: AbpController
    {
        protected SimpleMesControllerBase()
        {
            LocalizationSourceName = SimpleMesConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

