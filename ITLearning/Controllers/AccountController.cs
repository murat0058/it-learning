using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.Core.Identity.Models;
using Microsoft.AspNet.Identity;
using ITLearning.Frontend.Web.Core.Identity.Services;

namespace ITLearning.Frontend.Web.Controllers
{
    public class AccountController : Controller
    {
        private IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel model)
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }

   
}
