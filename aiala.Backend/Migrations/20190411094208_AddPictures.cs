using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pictures",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LastModifiedAt = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PicturesTags",
                schema: "aiala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Confidence = table.Column<float>(nullable: false),
                    PictureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicturesTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicturesTags_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalSchema: "aiala",
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicturesTags_PictureId",
                schema: "aiala",
                table: "PicturesTags",
                column: "PictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PicturesTags",
                schema: "aiala");

            migrationBuilder.DropTable(
                name: "Pictures",
                schema: "aiala");
        }
    }
}
