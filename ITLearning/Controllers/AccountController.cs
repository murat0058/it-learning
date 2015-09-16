using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Core.Identity.Services;
using System.Threading.Tasks;

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
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            await _identityService.SignUpAsync(model);
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }

   
}
