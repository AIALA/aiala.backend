using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UpdatePortal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                schema: "directory",
                table: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Entity = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    Culture = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_Entity_Reference_Culture",
                schema: "directory",
                table: "Translations",
                columns: new[] { "Entity", "Reference", "Culture" },
                unique: true,
                filter: "[Entity] IS NOT NULL AND [Reference] IS NOT NULL AND [Culture] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                schema: "directory",
                table: "Subscriptions",
                column: "TenantId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                schema: "directory",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "Translations",
                schema: "directory");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Tenants_TenantId",
                schema: "directory",
                table: "Subscriptions",
                column: "TenantId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
