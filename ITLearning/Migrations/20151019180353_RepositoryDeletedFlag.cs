using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ITLearning.Migrations
{
    public partial class RepositoryDeletedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GitRepository",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsDeleted", table: "GitRepository");
        }
    }
}
