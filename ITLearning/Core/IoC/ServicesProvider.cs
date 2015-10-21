using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace ITLearning.Frontend.Web.Core.IoC
{
    public static class ServicesProvider
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPermissionsProvider, PermissionsProvider>();
            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
        }
    }
}
