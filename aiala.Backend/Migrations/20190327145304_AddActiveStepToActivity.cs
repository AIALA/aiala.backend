using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddActiveStepToActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActiveStepId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActiveStepId",
                schema: "aiala",
                table: "Activities",
                column: "ActiveStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ScheduledSteps_ActiveStepId",
                schema: "aiala",
                table: "Activities",
                column: "ActiveStepId",
                principalSchema: "aiala",
                principalTable: "ScheduledSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ScheduledSteps_ActiveStepId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ActiveStepId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActiveStepId",
                schema: "aiala",
                table: "Activities");
        }
    }
}
