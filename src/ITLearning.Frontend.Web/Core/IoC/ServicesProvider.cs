using ITLearning.Backend.Business.Providers;
using ITLearning.Backend.Business.Services;
using ITLearning.Backend.DataAccess.Repositories;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Shared.Configs;
using ITLearning.Shared.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITLearning.Frontend.Web.Core.IoC
{
    public static class ServicesProvider
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPermissionsProvider, PermissionsProvider>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IGroupsService, GroupsService>();
            services.AddTransient<ITasksService, TasksService>();

            services.AddTransient<INewsProvider, StaticFilesNewsProvider>();
            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGroupsRepository, GroupsRepository>();
            #endregion

            #region Utils
            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
            #endregion

            #region Providers
            services.AddTransient<IAppConfigurationProvider, AppConfigurationProvider>();
            #endregion

            #region Configs
            services.Configure<PathsConfiguration>(configuration.GetSection("Paths"));
            services.Configure<DisqusConfiguration>(configuration.GetSection("Disqus"));
            services.Configure<DatabaseConfiguration>(x => x.ConnectionString = configuration["Data:DefaultConnection:ConnectionString"]);
            #endregion
        }
    }
}