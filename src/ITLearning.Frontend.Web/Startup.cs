using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ITLearning.Backend.Database;
using ITLearning.Backend.Database.Entities;
using ITLearning.Frontend.Web.Core.IoC;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Extensions;
using Microsoft.AspNet.Authentication.Cookies;
using ITLearning.Shared.Mappings;

namespace ITLearning
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

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

            services.AddCaching();
            services.AddSession();

            ServicesProvider.RegisterServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            MappingsProvider.ConfigureMappings();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        //TODO - better production release scenario
                        serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
                    }
                }
                catch
                {
                }
            }

            app.UseStatusCodePagesWithRedirects("~/Home/Error/{0}");
            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseSession();
            app.EnsureRolesCreated();

            app.UseCookieAuthentication((p) => new CookieAuthenticationOptions
            {
                LoginPath = "/Account/Login"
            });

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
