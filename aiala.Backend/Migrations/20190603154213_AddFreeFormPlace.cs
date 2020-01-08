using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddFreeFormPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "FreeFormPlace",
                schema: "aiala",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FreeFormPlace",
                schema: "aiala",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
