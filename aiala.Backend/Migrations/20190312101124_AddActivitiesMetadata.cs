using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddActivitiesMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Activities_ActivityId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ActivityId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "TimeCreated",
                schema: "aiala",
                table: "Activities",
                newName: "Metadata_TimeCreated");

            migrationBuilder.AddColumn<decimal>(
                name: "Metadata_Latitude",
                schema: "aiala",
                table: "Activities",
                type: "decimal(9, 6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Metadata_Longitude",
                schema: "aiala",
                table: "Activities",
                type: "decimal(9, 6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata_Latitude",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Metadata_Longitude",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Metadata_TimeCreated",
                schema: "aiala",
                table: "Activities",
                newName: "TimeCreated");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ActivityId",
                schema: "directory",
                table: "Accounts",
                column: "ActivityId");

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
    }
}
