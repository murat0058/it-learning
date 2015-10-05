using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

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
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            SignUpModel signUpModel = new SignUpModel()
            {
                Email = model.Email,
                Login = model.Login,
                Password = model.Password,
                PasswordConfirmation = model.PasswordConfirmation
            };

            await _identityService.SignUpAsync(signUpModel);

            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}