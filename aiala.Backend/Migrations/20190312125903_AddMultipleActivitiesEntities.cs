using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddMultipleActivitiesEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrelationId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Metadata_TimeCreated",
                schema: "aiala",
                table: "Activities",
                newName: "TimeCreated");

            migrationBuilder.RenameColumn(
                name: "Metadata_Longitude",
                schema: "aiala",
                table: "Activities",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Metadata_Latitude",
                schema: "aiala",
                table: "Activities",
                newName: "Latitude");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveTaskId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityData",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "aiala",
                table: "Activities",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                schema: "aiala",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "EmergencyId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StepId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActiveTaskId",
                schema: "aiala",
                table: "Activities",
                column: "ActiveTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StepId",
                schema: "aiala",
                table: "Activities",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TaskId",
                schema: "aiala",
                table: "Activities",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ScheduledTasks_ActiveTaskId",
                schema: "aiala",
                table: "Activities",
                column: "ActiveTaskId",
                principalSchema: "aiala",
                principalTable: "ScheduledTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ScheduledSteps_StepId",
                schema: "aiala",
                table: "Activities",
                column: "StepId",
                principalSchema: "aiala",
                principalTable: "ScheduledSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "Activities",
                column: "TaskId",
                principalSchema: "aiala",
                principalTable: "ScheduledTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ScheduledTasks_ActiveTaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ScheduledSteps_StepId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ActiveTaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StepId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActiveTaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ActivityData",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "EmergencyId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StepId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "TimeCreated",
                schema: "aiala",
                table: "Activities",
                newName: "Metadata_TimeCreated");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                schema: "aiala",
                table: "Activities",
                newName: "Metadata_Longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                schema: "aiala",
                table: "Activities",
                newName: "Metadata_Latitude");

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                schema: "aiala",
                table: "Activities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
