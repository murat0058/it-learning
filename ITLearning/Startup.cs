using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Dnx.Runtime;
using ITLearning.Frontend.Web.DAL;
using Microsoft.AspNet.Authentication.Cookies;
using ITLearning.Frontend.Web.Core.IoC;
using ITLearning.Frontend.Web.Common.Mappings;
using ITLearning.Frontend.Web.Core.Identity.Extensions;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.DAL.Model;
using ITLearning.Frontend.Web.Contract.Enums;
using System;
using System.Linq;

namespace ITLearning.Frontend.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");

            builder.AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ServicesProvider.RegisterServices(services, Configuration);

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
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            MappingsProvider.ConfigureMappings();

            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseIdentity();
            app.EnsureRolesCreated();

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

            // For testing purposes only
            RecreateDatabaseData(app);
        }

        private void RecreateDatabaseData(IApplicationBuilder app)
        {
            using (var dbContext = app.ApplicationServices.GetService<AppDbContext>())
            {
                dbContext.Database.ExecuteSqlCommand("DELETE FROM ErrorLog");
                //Do not forgot [] because of "Group" sql keyword
                dbContext.Database.ExecuteSqlCommand("DELETE FROM [Group]");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM Event");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM GitBranch");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM GitRepository");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM UserGroup");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM TaskInstanceReview");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM TaskInstance");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM TaskCategory");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM Task");

                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetUserRoles");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetUserLogins");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetUserClaims");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetRoleClaims");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetRoles");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM AspNetUsers");

                dbContext.SaveChanges();

                // Insert data

                app.EnsureRolesCreated();

                var firstUser = new User() { UserName = "abystrek", NormalizedUserName = "ABYSTREK", PasswordHash = "MTIzNDU2", Email = "abystrek@itlearning.com", NormalizedEmail = "ABYSTREK@ITLEARNING.COM", FirstName = "Adrian", LastName = "Bystrek" };
                var secondUser = new User() { UserName = "psmyrdek", NormalizedUserName = "PSMYRDEK", PasswordHash = "MTIzNDU2", Email = "psmyrdek@itlearning.com", NormalizedEmail = "PSMYRDEK@ITLEARNING.COM", FirstName = "Przemysław", LastName = "Smyrdek" };
                dbContext.Users.AddRange( // PasswordHash "MTIzNDU2" -> "123456"
                    firstUser,
                    secondUser
                );

                dbContext.SaveChanges();

                dbContext.UserClaims.AddRange(
                    new IdentityUserClaim<int>() { UserId = firstUser.Id, ClaimType = "Controller", ClaimValue = "HomeController" },
                    new IdentityUserClaim<int>() { UserId = secondUser.Id, ClaimType = "Controller", ClaimValue = "HomeController" }
                );

                dbContext.UserRoles.AddRange(
                    new IdentityUserRole<int>() { UserId = firstUser.Id, RoleId = dbContext.Roles.First(x => x.Name == "StandardUser").Id },
                    new IdentityUserRole<int>() { UserId = secondUser.Id, RoleId = dbContext.Roles.First(x => x.Name == "StandardUser").Id }
                );

                dbContext.ErrorLogs.AddRange(
                    new ErrorLog() { ErrorSource = ErrorSource.PlatformApp, ErrorMessage = "PlatformApp current test error", Date = DateTime.Now },
                    new ErrorLog() { ErrorSource = ErrorSource.PlatformApp, ErrorMessage = "PlatformApp past test error", Date = DateTime.Now.AddDays(-1) }
                );

                dbContext.Events.AddRange(
                    new Event() { Title = "Wydarzenie jutrzejsze", Description = "Opis wydarzenia jutrzejszego", Date = DateTime.Now.AddDays(1) },
                    new Event() { Title = "Wydarzenie dzisiejsze", Description = "Opis wydarzenia dzisiejszego", Date = DateTime.Now },
                    new Event() { Title = "Wydarzenie wczorajsze", Description = "Opis wydarzenia wczorajszego", Date = DateTime.Now.AddDays(-1) }
                );

                dbContext.SaveChanges();
            }
        }
    }
}