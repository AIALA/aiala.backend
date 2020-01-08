using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "aiala");

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTimeOffset>(nullable: false),
                    Revision = table.Column<long>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "directory",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Tenants_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "directory",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AuthorId",
                schema: "aiala",
                table: "Tasks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GroupId",
                schema: "aiala",
                table: "Tasks",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "aiala");
        }
    }
}
