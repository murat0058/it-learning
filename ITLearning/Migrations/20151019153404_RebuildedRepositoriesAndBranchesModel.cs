using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITLearning.Migrations
{
    public partial class RebuildedRepositoriesAndBranchesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("GitBranchEntity");
            migrationBuilder.DropTable("GitRepositoryEntity");
            migrationBuilder.CreateTable(
                name: "GitRepository",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsAnonymousPushAllowed = table.Column<bool>(nullable: false),
                    IsBare = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitRepository", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "GitBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    IsVisible = table.Column<bool>(nullable: false),
                    LastSHA = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RepositoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitBranch_GitRepository_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "GitRepository",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("GitBranch");
            migrationBuilder.DropTable("GitRepository");
            migrationBuilder.CreateTable(
                name: "GitRepositoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitRepositoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitRepositoryEntity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "GitBranchEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CanPull = table.Column<bool>(nullable: false),
                    CanPush = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RepositoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitBranchEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GitBranchEntity_GitRepositoryEntity_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "GitRepositoryEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GitBranchEntity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
        }
    }
}
