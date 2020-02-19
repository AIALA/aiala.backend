using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddScheduledTasksDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultDuration",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduledSteps",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ScheduledTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledSteps_ScheduledTasks_ScheduledTaskId",
                        column: x => x.ScheduledTaskId,
                        principalSchema: "aiala",
                        principalTable: "ScheduledTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSteps_ScheduledTaskId",
                schema: "aiala",
                table: "ScheduledSteps",
                column: "ScheduledTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledSteps",
                schema: "aiala");

            migrationBuilder.DropColumn(
                name: "DefaultDuration",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                schema: "aiala",
                table: "ScheduledTasks");
        }
    }
}
