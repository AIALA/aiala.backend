using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddTaskLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTasks_Tasks_TaskId",
                schema: "aiala",
                table: "ScheduledTasks",
                column: "TaskId",
                principalSchema: "aiala",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTasks_Tasks_TaskId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "PictureId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);
        }
    }
}
