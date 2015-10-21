using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ITLearning.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GitRepository",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "GitRepositoryId",
                table: "AspNetUsers",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_User_GitRepository_GitRepositoryId",
                table: "AspNetUsers",
                column: "GitRepositoryId",
                principalTable: "GitRepository",
                principalColumn: "Id");
            migrationBuilder.AddForeignKey(
                name: "FK_GitRepository_User_UserId",
                table: "GitRepository",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_User_GitRepository_GitRepositoryId", table: "AspNetUsers");
            migrationBuilder.DropForeignKey(name: "FK_GitRepository_User_UserId", table: "GitRepository");
            migrationBuilder.DropColumn(name: "UserId", table: "GitRepository");
            migrationBuilder.DropColumn(name: "GitRepositoryId", table: "AspNetUsers");
        }
    }
}
