using ITLearning.Backend.Database.Entities;
using ITLearning.Backend.Database.Entities.JunctionTables;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ITLearning.Backend.Database
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Task> Tasks { get; set; }
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

            builder.Entity<Task>()
                .HasKey(p => p.Id);

            builder.Entity<TaskInstance>()
                .HasKey(p => p.Id);

            builder.Entity<TaskInstanceReview>()
                .HasKey(p => p.Id);

            builder.Entity<GitRepository>()
                .HasKey(p => p.Id);

            builder.Entity<GitBranch>()
                .HasKey(p => p.Id);

            builder.Entity<Group>()
                .HasKey(p => p.Id);

            builder.Entity<UserGroup>()
                .HasKey(p => p.Id);

            builder.Entity<Event>()
                .HasKey(p => p.Id);

            builder.Entity<ErrorLog>()
                .HasKey(p => p.Id);
        }
    }
}