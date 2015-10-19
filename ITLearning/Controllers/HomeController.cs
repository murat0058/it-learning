using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using System;

namespace ITLearning.Frontend.Web.Controllers
{
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.HomeController)]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
