using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddEmergencyContactToSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseTaskContacts",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "EmergencyContact1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "EmergencyContact2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_Accounts_EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "EmergencyContact1Id",
                principalSchema: "directory",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_Accounts_EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "EmergencyContact2Id",
                principalSchema: "directory",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_Accounts_EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_Accounts_EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "EmergencyContact1Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "EmergencyContact2Id",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "UseTaskContacts",
                schema: "aiala",
                table: "ScheduledTasks");
        }
    }
}
