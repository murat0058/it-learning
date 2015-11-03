using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace ITLearning.Migrations
{
    public partial class TasksGroupEtcModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsDeleted", table: "GitRepository");
            migrationBuilder.DropColumn(name: "IsPublic", table: "GitRepository");
            migrationBuilder.DropColumn(name: "SourceRepositoryName", table: "GitRepository");
            migrationBuilder.DropTable("UserGitRepository");
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "TaskCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCategory", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "TaskInstance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GitRepositoryId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstance_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstance_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "TaskInstanceReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    TaskInstanceId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInstanceReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskInstanceReview_TaskInstance_TaskInstanceId",
                        column: x => x.TaskInstanceId,
                        principalTable: "TaskInstance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskInstanceReview_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Task",
                nullable: false);
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Task",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleOnlyInGroup",
                table: "Task",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<int>(
                name: "TaskCategoryId",
                table: "Task",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "SourceGitRepositoryId",
                table: "GitRepository",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "TaskInstanceId",
                table: "GitRepository",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_GitRepository_GitRepository_SourceGitRepositoryId",
                table: "GitRepository",
                column: "SourceGitRepositoryId",
                principalTable: "GitRepository",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_GitRepository_TaskInstance_TaskInstanceId",
                table: "GitRepository",
                column: "TaskInstanceId",
                principalTable: "TaskInstance",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Task_Group_GroupId",
                table: "Task",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_TaskCategoryId",
                table: "Task",
                column: "TaskCategoryId",
                principalTable: "TaskCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GitRepository_GitRepository_SourceGitRepositoryId", table: "GitRepository");
            migrationBuilder.DropForeignKey(name: "FK_GitRepository_TaskInstance_TaskInstanceId", table: "GitRepository");
            migrationBuilder.DropForeignKey(name: "FK_Task_Group_GroupId", table: "Task");
            migrationBuilder.DropForeignKey(name: "FK_Task_TaskCategory_TaskCategoryId", table: "Task");
            migrationBuilder.DropColumn(name: "GroupId", table: "Task");
            migrationBuilder.DropColumn(name: "IsVisibleOnlyInGroup", table: "Task");
            migrationBuilder.DropColumn(name: "TaskCategoryId", table: "Task");
            migrationBuilder.DropColumn(name: "SourceGitRepositoryId", table: "GitRepository");
            migrationBuilder.DropColumn(name: "TaskInstanceId", table: "GitRepository");
            migrationBuilder.DropTable("Event");
            migrationBuilder.DropTable("UserGroup");
            migrationBuilder.DropTable("TaskCategory");
            migrationBuilder.DropTable("TaskInstanceReview");
            migrationBuilder.DropTable("Group");
            migrationBuilder.DropTable("TaskInstance");
            migrationBuilder.CreateTable(
                name: "UserGitRepository",
                columns: table => new
                {
                    GitRepositoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGitRepository", x => new { x.GitRepositoryId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGitRepository_GitRepository_GitRepositoryId",
                        column: x => x.GitRepositoryId,
                        principalTable: "GitRepository",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGitRepository_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Task",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GitRepository",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "GitRepository",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<string>(
                name: "SourceRepositoryName",
                table: "GitRepository",
                nullable: true);
        }
    }
}
