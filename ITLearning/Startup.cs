using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Dnx.Runtime;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL;
using Microsoft.AspNet.Authentication.Cookies;
using Autofac;
using Autofac.Framework.DependencyInjection;
using System;

namespace ITLearning.Frontend.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");
                
            builder.AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext, int>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            builder.RegisterModule(new FrontendModule());
            
            // Populate the services.
            builder.Populate(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();

            //TODO: Is this necessary?
            app.UseCookieAuthentication((p) => new CookieAuthenticationOptions
            {
                LoginPath = "/Account/Login"
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Landing}/{action=Index}/{id?}");
            });
        }
    }
}
