using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ITLearning.Frontend.Web.Core.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IPermissionsProvider _permissionsProvider;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, IPermissionsProvider permissionsProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _permissionsProvider = permissionsProvider;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var userModel = new User
            {
                UserName = model.Login,
                Email = model.Email
            };

            var createUserResult = await _userManager.CreateAsync(userModel, model.Password);

            if (createUserResult.Succeeded)
            {
                var addRolesResult = await _userManager.AddToRolesAsync(userModel, _permissionsProvider.GetBasicRoles());
                var addClaimsResult = await _userManager.AddClaimsAsync(userModel, _permissionsProvider.GetBasicClaims());

                if (addRolesResult.Succeeded && addClaimsResult.Succeeded)
                {
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                }
                else
                {
                    List<IdentityError> errors = new List<IdentityError>();
                    errors.AddRange(addRolesResult.Errors);
                    errors.AddRange(addClaimsResult.Errors);

                    return IdentityResult.Failed(errors.ToArray());
                }
            }
            else
            {
                return IdentityResult.Failed(createUserResult.Errors.ToArray());
            }

            return IdentityResult.Success;
        }

        public async Task SignInAsync(LoginModel model)
        {
            var userModel = new User
            {
                UserName = model.Login
            };

            await _signInManager.SignInAsync(userModel, isPersistent: false);
        }
    }
}