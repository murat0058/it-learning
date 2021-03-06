using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ITLearning.Backend.Database;

namespace ITLearning.Backend.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20151228192621_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.ErrorLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("ErrorMessage");

                    b.Property<int>("ErrorSource");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.GitBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsVisible");

                    b.Property<string>("LastSHA");

                    b.Property<string>("Name");

                    b.Property<int?>("RepositoryId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.GitRepository", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAnonymousPushAllowed");

                    b.Property<bool>("IsBare");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("SourceGitRepositoryId");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("TaskInstanceId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Password");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.JunctionTables.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GroupId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<string>("Description");

                    b.Property<int?>("GroupId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Language");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.TaskInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CodeReviewExist");

                    b.Property<DateTime>("FinishDate");

                    b.Property<bool>("IsFinished");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.TaskInstanceReview", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("ArchitectureRate");

                    b.Property<int>("CleanCodeRate");

                    b.Property<string>("Comment");

                    b.Property<int>("OptymizationRate");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int?>("GitRepositoryId");

                    b.Property<string>("ImageName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.GitBranch", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.GitRepository")
                        .WithMany()
                        .HasForeignKey("RepositoryId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.GitRepository", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.GitRepository")
                        .WithMany()
                        .HasForeignKey("SourceGitRepositoryId");

                    b.HasOne("ITLearning.Backend.Database.Entities.Task")
                        .WithOne()
                        .HasForeignKey("ITLearning.Backend.Database.Entities.GitRepository", "TaskId");

                    b.HasOne("ITLearning.Backend.Database.Entities.TaskInstance")
                        .WithOne()
                        .HasForeignKey("ITLearning.Backend.Database.Entities.GitRepository", "TaskInstanceId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.Group", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.JunctionTables.UserGroup", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.Task", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.TaskInstance", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.Task")
                        .WithMany()
                        .HasForeignKey("TaskId");

                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.TaskInstanceReview", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.TaskInstance")
                        .WithOne()
                        .HasForeignKey("ITLearning.Backend.Database.Entities.TaskInstanceReview", "Id");
                });

            modelBuilder.Entity("ITLearning.Backend.Database.Entities.User", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.GitRepository")
                        .WithMany()
                        .HasForeignKey("GitRepositoryId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("ITLearning.Backend.Database.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
