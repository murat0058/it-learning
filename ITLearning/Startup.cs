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
using ITLearning.Frontend.Web.Core.IoC;
using ITLearning.Frontend.Web.Common.Mappings;
using ITLearning.Frontend.Web.Core.Identity.Extensions;
using Microsoft.AspNet.Identity;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Common;
using System;
using Microsoft.AspNet.Http;

namespace ITLearning.Frontend.Web
{
    public class Startup
    {
        private string _initialError = "";

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            try
            {
            }
            catch (Exception e)
            {
                _initialError = e.ToString();
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            if (_initialError != "")
            {
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(_initialError);
                });
                return;
            }

            try
            {
            }
            catch (Exception e)
            {
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(e.ToString());
                });
                return;
            }
        }

        //public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(appEnv.ApplicationBasePath)
        //        .AddJsonFile("config.json");

        //    builder.AddEnvironmentVariables();

        //    builder.AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

        //    if (env.IsDevelopment())
        //    {
        //        builder.AddUserSecrets();
        //    }
        //    Configuration = builder.Build();
        //}

        //public IConfiguration Configuration { get; set; }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    //services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();

        //    //services.AddEntityFramework()
        //    //    .AddSqlServer()
        //    //    .AddDbContext<AppDbContext>(options =>
        //    //        options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

        //    //services.AddIdentity<User, IdentityRole<int>>()
        //    //    .AddEntityFrameworkStores<AppDbContext, int>()
        //    //    .AddDefaultTokenProviders();

        //    services.AddMvc().AddMvcOptions(options =>
        //    {
        //        options.Filters.Add(new AuthorizeClaimAttribute());
        //    });

        //    //ServicesProvider.RegisterServices(services);
        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        //{
        //    //MappingsProvider.ConfigureMappings();

        //    //app.UseIISPlatformHandler();
        //    app.UseDeveloperExceptionPage();

        //    //if (env.IsDevelopment())
        //    //{
        //    //    loggerFactory.MinimumLevel = LogLevel.Critical;
        //    //    loggerFactory.AddConsole();

        //    //    app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
        //    //}


        //    app.UseStaticFiles();
        //    //app.UseIdentity();
        //    //app.EnsureRolesCreated();

        //    //app.UseCookieAuthentication((p) => new CookieAuthenticationOptions
        //    //{
        //    //    LoginPath = "/Account/Login"
        //    //});

        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Landing}/{action=Index}/{id?}");
        //    });
        //}
    }
}
