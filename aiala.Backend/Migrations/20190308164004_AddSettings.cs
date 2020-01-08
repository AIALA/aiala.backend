using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSettings",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "TenantSettings",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "UserSettings",
                schema: "directory");

            migrationBuilder.DropTable(
                name: "SettingTypes",
                schema: "directory");

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings",
                schema: "directory");

            migrationBuilder.CreateTable(
                name: "SettingTypes",
                schema: "directory",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    TypeId = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingTypes", x => new { x.Key, x.TypeId });
                });

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSettings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "directory",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "directory",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantSettings",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSettings_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "directory",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "directory",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "directory",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "directory",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_AccountId",
                schema: "directory",
                table: "AccountSettings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "directory",
                table: "AccountSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettings_TenantId",
                schema: "directory",
                table: "TenantSettings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "directory",
                table: "TenantSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                schema: "directory",
                table: "UserSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "directory",
                table: "UserSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });
        }
    }
}
