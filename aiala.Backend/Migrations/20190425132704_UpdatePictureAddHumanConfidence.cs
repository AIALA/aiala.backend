using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UpdatePictureAddHumanConfidence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                schema: "aiala",
                table: "Pictures",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "PicturesTags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "Pictures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "PicturesTags");

            migrationBuilder.DropColumn(
                name: "HasHumanConfidence",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "aiala",
                table: "Pictures",
                newName: "LastModifiedAt");
        }
    }
}
