using ITLearning.Backend.Business.Factories;
using ITLearning.Backend.Business.Services;
using ITLearning.Backend.DataAccess.Repositories;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Factories;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Common;
using ITLearning.Frontend.Web.Core.Identity.Providers;
using ITLearning.Frontend.Web.Core.Identity.Services;
using ITLearning.Shared.Configs;
using ITLearning.Shared.Formatters;
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
            services.AddTransient<IStartupPermissionsProvider, StartupPermissionsProvider>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IGroupsService, GroupsService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<IPermissionsService, PermissionsService>();
            services.AddTransient<INewsRepository, StaticFilesNewsRepository>();
            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITasksRepository, TasksRepository>();
            services.AddTransient<IGroupsRepository, GroupsRepository>();
            services.AddTransient<IPermissionsRepository, PermissionsRepository>();
            #endregion

            #region Utils
            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
            #endregion

            #region Providers
            services.AddTransient<IAppConfigurationProvider, AppConfigurationProvider>();
            #endregion

            #region Configs
            services.Configure<SourceControlRestApiConfiguration>(configuration.GetSection("SourceControlRestApiConfiguration"));
            services.Configure<PathsConfiguration>(configuration.GetSection("Paths"));
            services.Configure<DisqusConfiguration>(configuration.GetSection("Disqus"));
            services.Configure<DatabaseConfiguration>(x => x.ConnectionString = configuration["Data:DefaultConnection:ConnectionString"]);
            #endregion

            services.AddTransient<IWebClientFactory, WebClientFactory>();
            services.AddTransient<IStaticProvidersConfigurator, StaticProvidersConfigurator>();
        }
    }
}