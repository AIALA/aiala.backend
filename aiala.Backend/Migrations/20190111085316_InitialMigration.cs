using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "aiala");

            migrationBuilder.CreateTable(
                name: "Apps",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingTypes",
                schema: "aiala",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    TypeId = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingTypes", x => new { x.Key, x.TypeId });
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TenantType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActiveAccountId = table.Column<Guid>(nullable: true),
                    ExternalUserId = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTypes",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Features = table.Column<string>(nullable: true),
                    AppId = table.Column<Guid>(nullable: true),
                    SubscriptionLengthInDays = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTypes_Apps_AppId",
                        column: x => x.AppId,
                        principalSchema: "aiala",
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AppId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionGroups_Apps_AppId",
                        column: x => x.AppId,
                        principalSchema: "aiala",
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionGroups_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantSettings",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSettings_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "aiala",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "aiala",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "aiala",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "aiala",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedAccountId = table.Column<Guid>(nullable: true),
                    CreatedSubscriptionId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TenantType = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    ApprovalRequired = table.Column<bool>(nullable: false),
                    Approved = table.Column<DateTimeOffset>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovalToken = table.Column<string>(nullable: true),
                    Confirmed = table.Column<DateTimeOffset>(nullable: true),
                    Completed = table.Column<DateTimeOffset>(nullable: true),
                    SubscriptionTypeId = table.Column<Guid>(nullable: true),
                    ConfirmationToken = table.Column<string>(nullable: false),
                    Values = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalSchema: "aiala",
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionActivations",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DefaultTenantType = table.Column<int>(nullable: false),
                    DefaultTenantId = table.Column<Guid>(nullable: true),
                    SubscriptionTypeId = table.Column<Guid>(nullable: true),
                    ActivationType = table.Column<int>(nullable: false),
                    ApprovalRequired = table.Column<bool>(nullable: false),
                    Approvers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionActivations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionActivations_Tenants_DefaultTenantId",
                        column: x => x.DefaultTenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionActivations_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalSchema: "aiala",
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    SubscriptionTypeId = table.Column<Guid>(nullable: true),
                    ValidFrom = table.Column<DateTimeOffset>(nullable: true),
                    ValidTo = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalSchema: "aiala",
                        principalTable: "SubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    SettingTypeKey = table.Column<string>(nullable: true),
                    SettingTypeTypeId = table.Column<string>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSettings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "aiala",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountSettings_SettingTypes_SettingTypeKey_SettingTypeTypeId",
                        columns: x => new { x.SettingTypeKey, x.SettingTypeTypeId },
                        principalSchema: "aiala",
                        principalTable: "SettingTypes",
                        principalColumns: new[] { "Key", "TypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionAssignments",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PermissionGroupId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: true),
                    PermissionType = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTimeOffset>(nullable: true),
                    ValidTo = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionAssignments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "aiala",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionAssignments_PermissionGroups_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "aiala",
                        principalTable: "PermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionAssignments_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "aiala",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroupAssignments",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroupAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionGroupAssignments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "aiala",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionGroupAssignments_PermissionGroups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "aiala",
                        principalTable: "PermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionActivationCodes",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActivationKey = table.Column<string>(nullable: true),
                    ValidFrom = table.Column<DateTimeOffset>(nullable: true),
                    ValidTo = table.Column<DateTimeOffset>(nullable: true),
                    Activated = table.Column<DateTimeOffset>(nullable: true),
                    SubscriptionActivationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionActivationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionActivationCodes_SubscriptionActivations_SubscriptionActivationId",
                        column: x => x.SubscriptionActivationId,
                        principalSchema: "aiala",
                        principalTable: "SubscriptionActivations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                schema: "aiala",
                table: "Accounts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                schema: "aiala",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_AccountId",
                schema: "aiala",
                table: "AccountSettings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "aiala",
                table: "AccountSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionAssignments_AccountId",
                schema: "aiala",
                table: "PermissionAssignments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionAssignments_PermissionGroupId",
                schema: "aiala",
                table: "PermissionAssignments",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionAssignments_TenantId",
                schema: "aiala",
                table: "PermissionAssignments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupAssignments_AccountId",
                schema: "aiala",
                table: "PermissionGroupAssignments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupAssignments_GroupId",
                schema: "aiala",
                table: "PermissionGroupAssignments",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroups_AppId",
                schema: "aiala",
                table: "PermissionGroups",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroups_TenantId",
                schema: "aiala",
                table: "PermissionGroups",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_SubscriptionTypeId",
                schema: "aiala",
                table: "Registrations",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionActivationCodes_SubscriptionActivationId",
                schema: "aiala",
                table: "SubscriptionActivationCodes",
                column: "SubscriptionActivationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionActivations_DefaultTenantId",
                schema: "aiala",
                table: "SubscriptionActivations",
                column: "DefaultTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionActivations_SubscriptionTypeId",
                schema: "aiala",
                table: "SubscriptionActivations",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                schema: "aiala",
                table: "Subscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                schema: "aiala",
                table: "Subscriptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_AppId",
                schema: "aiala",
                table: "SubscriptionTypes",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettings_TenantId",
                schema: "aiala",
                table: "TenantSettings",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "aiala",
                table: "TenantSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                schema: "aiala",
                table: "UserSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_SettingTypeKey_SettingTypeTypeId",
                schema: "aiala",
                table: "UserSettings",
                columns: new[] { "SettingTypeKey", "SettingTypeTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSettings",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "PermissionAssignments",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "PermissionGroupAssignments",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Registrations",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "SubscriptionActivationCodes",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "TenantSettings",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "UserSettings",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "PermissionGroups",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "SubscriptionActivations",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "SettingTypes",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "SubscriptionTypes",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Apps",
                schema: "aiala");
        }
    }
}
