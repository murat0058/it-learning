using ITLearning.Frontend.Web.Core.Identity;
using ITLearning.Frontend.Web.Core.Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
