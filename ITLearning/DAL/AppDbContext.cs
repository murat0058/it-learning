using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace ITLearning.Frontend.Web.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<GitRepositoryEntity> GitRepositories { get; set; }
        public DbSet<GitBranchEntity> GitBranches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region GitRepositoryEntity

            builder.Entity<GitRepositoryEntity>()
                .HasKey(p => p.Id);
            builder.Entity<GitRepositoryEntity>()
                .Property(p => p.Name)
                .IsRequired();

            #endregion

            #region GitBranchEntity

            builder.Entity<GitBranchEntity>()
                .HasKey(p => p.Id);

            builder.Entity<GitBranchEntity>()
                .Property(p => p.Name)
                .IsRequired();

            #endregion
        }
    }
}