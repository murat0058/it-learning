﻿using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Core.Identity.Validators;
using ITLearning.Frontend.Web.Common.Extensions;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using ITLearning.Frontend.Web.Controllers.Base;

namespace ITLearning.Frontend.Web.Controllers
{
    [AllowAnonymous]
    [Route("Account")]
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();

            return RedirectToAction("Index", "Landing");
        }

        [HttpGet("Unauthorized")]
        public IActionResult Unauthorized(string returnUrl)
        {
            ModelState.AddModelError(string.Empty, "Musisz być zalogowany lub posiadać odpowiednie uprawnienia aby przejść dalej.");

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View("Login", model);
        }

        [HttpPost("Login")]
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

        [HttpPost("SignUp")]
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
    }
}