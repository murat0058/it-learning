using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;

namespace ITLearning.Frontend.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeClaim(Type = ClaimType.Login, Value = ClaimValue.Modify)]
        public IActionResult Login(string model)
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
