using ITLearning.Frontend.Web.DAL.Model;
using ITLearning.Frontend.Web.DAL.Model.JunctionTables;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ITLearning.Frontend.Web.DAL
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<TaskInstance> TaskInstances { get; set; }
        public DbSet<TaskInstanceReview> TaskInstanceReviews { get; set; }
        public DbSet<GitRepository> GitRepositories { get; set; }
        public DbSet<GitBranch> GitBranches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Task
            builder.Entity<Task>()
                .HasKey(p => p.Id);
            #endregion

            #region TaskCategory
            builder.Entity<TaskCategory>()
                .HasKey(p => p.Id);
            #endregion

            #region TaskInstance
            builder.Entity<TaskInstance>()
                .HasKey(p => p.Id);
            #endregion

            #region TaskInstanceReview
            builder.Entity<TaskInstanceReview>()
                .HasKey(p => p.Id);
            #endregion

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

            #region Group
            builder.Entity<Group>()
                .HasKey(p => p.Id);
            #endregion

            #region UserGroup
            builder.Entity<UserGroup>()
                .HasKey(x => new { x.UserId, x.GroupId });
            #endregion

            #region Event
            builder.Entity<Event>()
                .HasKey(p => p.Id);
            #endregion

            #region ErrorLog
            builder.Entity<ErrorLog>()
                .HasKey(p => p.Id);
            #endregion
        }
    }
}