using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.Core.Identity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task SignUpAsync(SignUpModel model)
        {
            var userModel = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Login,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(userModel, model.Password);

            if (result.Succeeded)
            {
                var claim = new Claim(ClaimType.Login.ToString(), ClaimValue.Read.ToString());

                await _userManager.AddClaimAsync(userModel, claim);
                await _signInManager.SignInAsync(userModel, false);
            }
        }
    }
}
