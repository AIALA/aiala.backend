using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class DirectoryPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "directory");

            migrationBuilder.RenameTable(
                name: "UserSettings",
                schema: "aiala",
                newName: "UserSettings",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "aiala",
                newName: "Users",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "TenantSettings",
                schema: "aiala",
                newName: "TenantSettings",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Tenants",
                schema: "aiala",
                newName: "Tenants",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "SubscriptionTypes",
                schema: "aiala",
                newName: "SubscriptionTypes",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                schema: "aiala",
                newName: "Subscriptions",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "SubscriptionActivations",
                schema: "aiala",
                newName: "SubscriptionActivations",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "SubscriptionActivationCodes",
                schema: "aiala",
                newName: "SubscriptionActivationCodes",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "SettingTypes",
                schema: "aiala",
                newName: "SettingTypes",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Registrations",
                schema: "aiala",
                newName: "Registrations",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "PermissionGroups",
                schema: "aiala",
                newName: "PermissionGroups",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "PermissionGroupAssignments",
                schema: "aiala",
                newName: "PermissionGroupAssignments",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "PermissionAssignments",
                schema: "aiala",
                newName: "PermissionAssignments",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Apps",
                schema: "aiala",
                newName: "Apps",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "AccountSettings",
                schema: "aiala",
                newName: "AccountSettings",
                newSchema: "directory");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "aiala",
                newName: "Accounts",
                newSchema: "directory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserSettings",
                schema: "directory",
                newName: "UserSettings",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "directory",
                newName: "Users",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "TenantSettings",
                schema: "directory",
                newName: "TenantSettings",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Tenants",
                schema: "directory",
                newName: "Tenants",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "SubscriptionTypes",
                schema: "directory",
                newName: "SubscriptionTypes",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                schema: "directory",
                newName: "Subscriptions",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "SubscriptionActivations",
                schema: "directory",
                newName: "SubscriptionActivations",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "SubscriptionActivationCodes",
                schema: "directory",
                newName: "SubscriptionActivationCodes",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "SettingTypes",
                schema: "directory",
                newName: "SettingTypes",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Registrations",
                schema: "directory",
                newName: "Registrations",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "PermissionGroups",
                schema: "directory",
                newName: "PermissionGroups",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "PermissionGroupAssignments",
                schema: "directory",
                newName: "PermissionGroupAssignments",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "PermissionAssignments",
                schema: "directory",
                newName: "PermissionAssignments",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Apps",
                schema: "directory",
                newName: "Apps",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "AccountSettings",
                schema: "directory",
                newName: "AccountSettings",
                newSchema: "aiala");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "directory",
                newName: "Accounts",
                newSchema: "aiala");
        }
    }
}
