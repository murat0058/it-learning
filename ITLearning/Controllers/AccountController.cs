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
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Login(LoginViewModel model)
        {
            return View(model ?? new LoginViewModel());
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginViewModel)
        {
            LoginModel model = Mapper.Map<LoginModel>(loginViewModel);

            var validator = new LoginModelValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                ModelState.ApplyValidationFailures(result.Errors);
            }
            else
            {
                var signInResult = await _identityService.LoginAsync(model);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Nie znaleziono użytkownika {loginViewModel.Login}");
                }
            }

            loginViewModel.Password = string.Empty;
            return View(loginViewModel);
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
                ModelState.ApplyValidationFailures(result.Errors);
            }
            else
            {
                var signUpResult = await _identityService.SignUpAsync(model);

                if (signUpResult.Succeeded)
                {
                    // Valid & Success
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Valid & Error during creating user
                    foreach (var error in signUpResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(signUpViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _identityService.LogoutAsync();

            return RedirectToAction("Index", "Landing");
        }

        public IActionResult Unauthorized(string returnUrl)
        {
            ModelState.AddModelError(string.Empty, "Musisz być zalogowany lub posiadać odpowiednie uprawnienia aby przejść dalej.");

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View("Login", model);
        }
    }
}