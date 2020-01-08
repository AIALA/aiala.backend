using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class StorePictureIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureId",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                schema: "aiala",
                table: "Tasks",
                nullable: true);
        }
    }
}
