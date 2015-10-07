using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ITLearning.Frontend.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (Authorized())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Landing");
            }
        }
    }
}
