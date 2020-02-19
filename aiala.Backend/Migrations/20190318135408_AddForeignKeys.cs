using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Tasks_AppTaskId",
                schema: "aiala",
                table: "Steps");

            migrationBuilder.RenameColumn(
                name: "AppTaskId",
                schema: "aiala",
                table: "Steps",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_AppTaskId",
                schema: "aiala",
                table: "Steps",
                newName: "IX_Steps_TaskId");

            migrationBuilder.RenameColumn(
                name: "ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSteps_ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                newName: "IX_ScheduledSteps_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                column: "TaskId",
                principalSchema: "aiala",
                principalTable: "ScheduledTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Tasks_TaskId",
                schema: "aiala",
                table: "Steps",
                column: "TaskId",
                principalSchema: "aiala",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "ScheduledSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Tasks_TaskId",
                schema: "aiala",
                table: "Steps");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                schema: "aiala",
                table: "Steps",
                newName: "AppTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_TaskId",
                schema: "aiala",
                table: "Steps",
                newName: "IX_Steps_AppTaskId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                newName: "ScheduledTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSteps_TaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                newName: "IX_ScheduledSteps_ScheduledTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                column: "ScheduledTaskId",
                principalSchema: "aiala",
                principalTable: "ScheduledTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Tasks_AppTaskId",
                schema: "aiala",
                table: "Steps",
                column: "AppTaskId",
                principalSchema: "aiala",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
