using ITLearning.Frontend.Web.Core.Identity.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Services
{
    public interface IIdentityService
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<SignInResult> LoginAsync(LoginModel model);
        Task LogoutAsync();
    }
}