using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UseTimeSpanForStartEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DefaultDuration",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Start",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "End",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "DefaultDuration",
                schema: "aiala",
                table: "ScheduledTasks",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
