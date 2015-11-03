using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace ITLearning.Frontend.Web.Core.IoC
{
    public static class ServicesProvider
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPermissionsProvider, PermissionsProvider>();
            #endregion

            #region Utils
            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
            #endregion

            #region Configs
            services.Configure<PathsConfiguration>(configuration.GetSection("Paths"));
            #endregion
        }
    }
}