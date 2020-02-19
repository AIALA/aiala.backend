using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddPictureToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PictureId",
                schema: "directory",
                table: "Accounts",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Pictures_PictureId",
                schema: "directory",
                table: "Accounts",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Pictures_PictureId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PictureId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PictureId",
                schema: "directory",
                table: "Accounts");
        }
    }
}
