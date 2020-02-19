using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CorrelationId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TimeCreated = table.Column<DateTimeOffset>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "directory",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ActivityId",
                schema: "directory",
                table: "Accounts",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TenantId",
                schema: "aiala",
                table: "Activities",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Activities_ActivityId",
                schema: "directory",
                table: "Accounts",
                column: "ActivityId",
                principalSchema: "aiala",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Activities_ActivityId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "aiala");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ActivityId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                schema: "directory",
                table: "Accounts");
        }
    }
}
