using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace ITLearning.Migrations
{
    public partial class TaskEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Task",
                nullable: false);
        }
    }
}
