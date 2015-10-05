using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(SignUpModel model);
    }
}