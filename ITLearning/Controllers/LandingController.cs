﻿using Microsoft.AspNet.Mvc;

namespace ITLearning.Frontend.Web.Controllers
{
    [AllowAnonymous]
    [Route("")]
    public class LandingController : BaseController
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if (IsAuthorized())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
