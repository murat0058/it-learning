using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace ITLearning.Frontend.Web.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<GitRepository> GitRepositories { get; set; }
        public DbSet<GitBranch> GitBranches { get; set; }
        public DbSet<GitRepositoryUser> GitRepositoryUsers { get; set; }
        public DbSet<ErrorLogs> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region GitRepositoryEntity
            builder.Entity<GitRepository>()
                .HasKey(p => p.Id);
            builder.Entity<GitRepository>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion

            #region GitBranchEntity
            builder.Entity<GitBranch>()
                .HasKey(p => p.Id);
            builder.Entity<GitBranch>()
                .Property(p => p.Name)
                .IsRequired();
            #endregion

            #region GitRepositoryUser
            builder.Entity<GitRepositoryUser>()
                .HasKey(x => new { x.GitRepositoryId, x.UserId });
            #endregion

            #region ErrorLogs
            builder.Entity<ErrorLogs>()
                .HasKey(p => p.Id);
            #endregion
        }
    }
}