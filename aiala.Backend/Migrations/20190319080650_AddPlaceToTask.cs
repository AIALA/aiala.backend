using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddPlaceToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Revision",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "aiala",
                table: "ScheduledTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaceId",
                schema: "aiala",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduledPlace",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PictureId = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledPlace_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalSchema: "aiala",
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledPlace_ScheduledTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "aiala",
                        principalTable: "ScheduledTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PlaceId",
                schema: "aiala",
                table: "Tasks",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPlace_PlaceId",
                schema: "aiala",
                table: "ScheduledPlace",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPlace_TaskId",
                schema: "aiala",
                table: "ScheduledPlace",
                column: "TaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Places_PlaceId",
                schema: "aiala",
                table: "Tasks",
                column: "PlaceId",
                principalSchema: "aiala",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Places_PlaceId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ScheduledPlace",
                schema: "aiala");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PlaceId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "aiala",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Revision",
                schema: "aiala",
                table: "Tasks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true);
        }
    }
}
