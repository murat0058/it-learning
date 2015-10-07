using ITLearning.Frontend.Web.DAL;
using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ITLearning.Frontend.Web.Core.Identity.Providers;

namespace ITLearning.Frontend.Web.Core.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static void EnsureRolesCreated(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<AppDbContext>();
            var permissionsProvider = app.ApplicationServices.GetService<IPermissionsProvider>();

            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole<int>>>();
            foreach (var role in permissionsProvider.GetBasicRoles())
            {
                if (!roleManager.RoleExistsAsync(role.ToUpper()).Result)
                {
                    roleManager.CreateAsync(new IdentityRole<int> { Name = role });
                }
            }
        }
    }
}
