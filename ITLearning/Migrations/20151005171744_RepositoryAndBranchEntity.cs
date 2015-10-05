using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace ITLearning.Migrations
{
    public partial class RepositoryAndBranchEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepositoryEntity",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Name = table.Column<string>(isNullable: false),
                    UserId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepositoryEntity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "BranchEntity",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    CanPull = table.Column<bool>(isNullable: false),
                    CanPush = table.Column<bool>(isNullable: false),
                    Name = table.Column<string>(isNullable: false),
                    RepositoryId = table.Column<int>(isNullable: false),
                    UserId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchEntity_RepositoryEntity_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "RepositoryEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BranchEntity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BranchEntity");
            migrationBuilder.DropTable("RepositoryEntity");
        }
    }
}
