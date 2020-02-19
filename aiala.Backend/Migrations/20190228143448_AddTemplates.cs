using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayTemplates",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTemplates_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "directory",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTaskTemplates",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    DayTemplateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledTaskTemplates_DayTemplates_DayTemplateId",
                        column: x => x.DayTemplateId,
                        principalSchema: "aiala",
                        principalTable: "DayTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledTaskTemplates_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "aiala",
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayTemplates_TenantId",
                schema: "aiala",
                table: "DayTemplates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTaskTemplates_DayTemplateId",
                schema: "aiala",
                table: "ScheduledTaskTemplates",
                column: "DayTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTaskTemplates_TaskId",
                schema: "aiala",
                table: "ScheduledTaskTemplates",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledTaskTemplates",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "DayTemplates",
                schema: "aiala");
        }
    }
}
