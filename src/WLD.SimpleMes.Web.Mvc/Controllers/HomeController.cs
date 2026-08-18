using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using WLD.SimpleMes.Controllers;

namespace WLD.SimpleMes.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SimpleMesControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

