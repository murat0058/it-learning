using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using ITLearning.Frontend.Web.DAL.Model;

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
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
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

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Login, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
        }

        public async System.Threading.Tasks.Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}