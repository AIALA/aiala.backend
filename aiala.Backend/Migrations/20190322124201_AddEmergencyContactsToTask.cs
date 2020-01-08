using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddEmergencyContactsToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseTaskContacts",
                schema: "aiala",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks",
                column: "EmergencyContact1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks",
                column: "EmergencyContact2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Accounts_EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks",
                column: "EmergencyContact1Id",
                principalSchema: "directory",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Accounts_EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks",
                column: "EmergencyContact2Id",
                principalSchema: "directory",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Accounts_EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Accounts_EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EmergencyContact1Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EmergencyContact2Id",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UseTaskContacts",
                schema: "aiala",
                table: "Tasks");
        }
    }
}
