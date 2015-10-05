using ITLearning.Frontend.Web.Core.Identity.Models;
using Microsoft.AspNet.Identity;
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
            var user = new User { UserName = model.Login };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        }
    }
}