using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace ITLearning.Frontend.Web.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<RepositoryEntity> Repositories { get; set; }
        public DbSet<BranchEntity> Branches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region RepositoryEntity

            builder.Entity<RepositoryEntity>()
                .Key(p => p.Id);

            builder.Entity<RepositoryEntity>()
                .Property(p => p.Name)
                .Required();

            #endregion

            #region BranchEntity

            builder.Entity<BranchEntity>()
                .Key(p => p.Id);

            builder.Entity<BranchEntity>()
                .Property(p => p.Name)
                .Required();

            #endregion
        }
    }
}