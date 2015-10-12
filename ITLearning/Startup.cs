﻿using Microsoft.AspNet.Builder;
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
using ITLearning.Frontend.Web.Core.IoC;
using ITLearning.Frontend.Web.Common.Mappings;
using ITLearning.Frontend.Web.Core.Identity.Extensions;
using Microsoft.AspNet.Identity;
using ITLearning.Frontend.Web.Core.Identity.Attributes;

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

            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add(new AuthorizeClaimAttribute());
            });

            ServicesProvider.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            MappingsProvider.ConfigureMappings();

            if (env.IsDevelopment())
            {
                loggerFactory.MinimumLevel = LogLevel.Critical;
                loggerFactory.AddConsole();

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                //TODO logger
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.EnsureRolesCreated();

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
