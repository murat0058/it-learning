using ITLearning.Frontend.Web.Contract.Configs;
using ITLearning.Frontend.Web.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Frontend.Web.Services;
using ITLearning.Frontend.Web.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using ITLearning.Frontend.Web.Contract.DAL.Repositories;
using ITLearning.Frontend.Web.DAL.Respoitories;
using ITLearning.Frontend.Web.Contract.Providers.ModelProviders;
using ITLearning.Frontend.Web.Providers.ModelProviders;
using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.Contract.Providers;
using ITLearning.Frontend.Web.Providers;

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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<INewsProvider, StaticFilesNewsProvider>();
            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion

            #region Utils
            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
            #endregion

            #region Providers
            services.AddTransient<IAppConfigurationProvider, AppConfigurationProvider>();
            #endregion

            #region Configs
            services.Configure<PathsConfiguration>(configuration.GetSection("Paths"));
            services.Configure<DatabaseConfiguration>(x => x.ConnectionString = configuration["Data:DefaultConnection:ConnectionString"]);
            #endregion
        }
    }
}