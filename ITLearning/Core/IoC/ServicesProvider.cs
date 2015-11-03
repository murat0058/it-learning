using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.Contract.Providers.ViewModelProviders;
using ITLearning.Frontend.Web.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.Providers.Home;
using ITLearning.Frontend.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using System.Configuration;

namespace ITLearning.Frontend.Web.Core.IoC
{
    public static class ServicesProvider
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPermissionsProvider, PermissionsProvider>();
            services.AddTransient<INewsThumbnailsService, NewsThumbnailsService>();
            services.AddTransient<IUserBasicDataViewModelProvider, UserBasicDataViewModelProvider>();
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
