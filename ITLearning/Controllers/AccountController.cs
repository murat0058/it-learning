using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Services;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.ViewModels.Identity;

namespace ITLearning.Frontend.Web.Controllers
{
    public class AccountController : BaseController
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
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }

   
}
