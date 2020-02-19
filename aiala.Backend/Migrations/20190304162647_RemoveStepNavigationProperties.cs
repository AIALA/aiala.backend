using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class RemoveStepNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_TaskId",
                schema: "aiala",
                table: "ScheduledSteps");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps");

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
        }
    }
}
