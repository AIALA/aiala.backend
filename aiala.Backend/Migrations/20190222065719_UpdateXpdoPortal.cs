using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UpdateXpdoPortal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Tenants_GroupId",
                schema: "aiala",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tenants_GroupId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RoleType",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "aiala",
                table: "Tasks",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_GroupId",
                schema: "aiala",
                table: "Tasks",
                newName: "IX_Tasks_TenantId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "aiala",
                table: "Days",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_GroupId",
                schema: "aiala",
                table: "Days",
                newName: "IX_Days_TenantId");

            migrationBuilder.AddColumn<Guid>(
                name: "InviterId",
                schema: "directory",
                table: "Invitations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Tenants_TenantId",
                schema: "aiala",
                table: "Days",
                column: "TenantId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tenants_TenantId",
                schema: "aiala",
                table: "Tasks",
                column: "TenantId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Tenants_TenantId",
                schema: "aiala",
                table: "Days");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tenants_TenantId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "InviterId",
                schema: "directory",
                table: "Invitations");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "aiala",
                table: "Tasks",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TenantId",
                schema: "aiala",
                table: "Tasks",
                newName: "IX_Tasks_GroupId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "aiala",
                table: "Days",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Days_TenantId",
                schema: "aiala",
                table: "Days",
                newName: "IX_Days_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Tenants_GroupId",
                schema: "aiala",
                table: "Days",
                column: "GroupId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tenants_GroupId",
                schema: "aiala",
                table: "Tasks",
                column: "GroupId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
