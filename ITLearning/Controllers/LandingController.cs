using Microsoft.AspNet.Mvc;

namespace ITLearning.Frontend.Web.Controllers
{
    [AllowAnonymous]
    public class LandingController : BaseController
    {
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
