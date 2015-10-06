using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Threading.Tasks;
using System.Linq;
using ITLearning.Frontend.Web.Core.Identity.Validators;
using ITLearning.Frontend.Web.Common.Extensions;
using AutoMapper;

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

        public IActionResult SignUp(SignUpViewModel model)
        {
            return View(model ?? new SignUpViewModel());
        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task<IActionResult> SignUpPost(SignUpViewModel signUpViewModel)
        {
            SignUpModel model = Mapper.Map<SignUpModel>(signUpViewModel);

            var validator = new SignUpModelValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                ModelState.FillModelStateErrors(result.Errors);
                return View(signUpViewModel);
            }
            else
            {
                await _identityService.SignUpAsync(model);
                return RedirectToAction("SignUp");
            }

        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}