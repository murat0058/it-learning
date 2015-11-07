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
            //RecreateDatabaseData(app);
        }

        private void RecreateDatabaseData(IApplicationBuilder app)
        {
            using (var dbContext = new AppDbContext())
            {
                // Delete data
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE ErrorLog");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Event");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE GitBranch");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE GitRepository");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE UserGroup");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Group");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TaskInstanceReview");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TaskInstance");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TaskCategory");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Task");

                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetUserRoles");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetUserLogins");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetUserClaims");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetRoleClaims");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetRoles");
                dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE AspNetUsers");

                //var errorLogs = dbContext.ErrorLogs.ToList();
                //dbContext.ErrorLogs.RemoveRange(errorLogs);

                //var events = dbContext.Events.ToList();
                //dbContext.Events.RemoveRange(events);

                //var gitBranches = dbContext.GitBranches.ToList();
                //dbContext.GitBranches.RemoveRange(gitBranches);

                //var gitRepositories = dbContext.GitRepositories.ToList();
                //dbContext.GitRepositories.RemoveRange(gitRepositories);

                //var userGroups = dbContext.UserGroups.ToList();
                //dbContext.UserGroups.RemoveRange(userGroups);

                //var groups = dbContext.Groups.ToList();
                //dbContext.Groups.RemoveRange(groups);

                //var taskInstanceReviews = dbContext.TaskInstanceReviews.ToList();
                //dbContext.TaskInstanceReviews.RemoveRange(taskInstanceReviews);

                //var taskInstances = dbContext.TaskInstances.ToList();
                //dbContext.TaskInstances.RemoveRange(taskInstances);

                //var taskCategories = dbContext.TaskCategories.ToList();
                //dbContext.TaskCategories.RemoveRange(taskCategories);

                //var tasks = dbContext.Task.ToList();
                //dbContext.Task.RemoveRange(tasks);

                //var aspNetUserRoles = dbContext.UserRoles.ToList();
                //dbContext.UserRoles.RemoveRange(aspNetUserRoles);

                //var aspNetUserLogins = dbContext.UserLogins.ToList();
                //dbContext.UserLogins.RemoveRange(aspNetUserLogins);

                //var aspNetUserClaims = dbContext.UserClaims.ToList();
                //dbContext.UserClaims.RemoveRange(aspNetUserClaims);

                //var aspNetRoleClaims = dbContext.RoleClaims.ToList();
                //dbContext.RoleClaims.RemoveRange(aspNetRoleClaims);

                //var aspNetRoles = dbContext.Roles.ToList();
                //dbContext.Roles.RemoveRange(aspNetRoles);

                //var aspNetUsers = dbContext.Users.ToList();
                //dbContext.Users.RemoveRange(aspNetUsers);

                dbContext.SaveChanges();

                // Insert data
                app.EnsureRolesCreated();

                dbContext.Users.AddRange( // PasswordHash "MTIzNDU2" -> "123456"
                    new User() { UserName = "abystrek", NormalizedUserName = "ABYSTREK", PasswordHash = "MTIzNDU2", Email = "abystrek@itlearning.com", NormalizedEmail = "ABYSTREK@ITLEARNING.COM", FirstName = "Adrian", LastName = "Bystrek" },
                    new User() { UserName = "psmyrdek", NormalizedUserName = "PSMYRDEK", PasswordHash = "MTIzNDU2", Email = "psmyrdek@itlearning.com", NormalizedEmail = "PSMYRDEK@ITLEARNING.COM", FirstName = "Przemysław", LastName = "Smyrdek" }
                );

                dbContext.UserClaims.AddRange(
                    new IdentityUserClaim<int>() { UserId = dbContext.Users.First(x => x.UserName == "abystrek").Id, ClaimType = "Controller", ClaimValue = "HomeController" },
                    new IdentityUserClaim<int>() { UserId = dbContext.Users.First(x => x.UserName == "psmyrdek").Id, ClaimType = "Controller", ClaimValue = "HomeController" }
                );

                dbContext.UserRoles.AddRange(
                    new IdentityUserRole<int>() { UserId = dbContext.Users.First(x => x.UserName == "abystrek").Id, RoleId = dbContext.Roles.First(x => x.Name == "StandardUser").Id },
                    new IdentityUserRole<int>() { UserId = dbContext.Users.First(x => x.UserName == "psmyrdek").Id, RoleId = dbContext.Roles.First(x => x.Name == "StandardUser").Id }
                );

                dbContext.ErrorLogs.AddRange(
                    new ErrorLog() { ErrorSource = ErrorSource.PlatformApp, ErrorMessage = "PlatformApp current test error", Date = new DateTime() },
                    new ErrorLog() { ErrorSource = ErrorSource.PlatformApp, ErrorMessage = "PlatformApp past test error", Date = new DateTime().AddDays(-1) }
                );

                dbContext.Events.AddRange(
                    new Event() { Title = "Wydarzenie jutrzejsze", Description = "Opis wydarzenia jutrzejszego", Date = new DateTime().AddDays(1) },
                    new Event() { Title = "Wydarzenie dzisiejsze", Description = "Opis wydarzenia dzisiejszego", Date = new DateTime() },
                    new Event() { Title = "Wydarzenie wczorajsze", Description = "Opis wydarzenia wczorajszego", Date = new DateTime().AddDays(-1) }
                );

                dbContext.SaveChanges();
            }
        }
    }
}