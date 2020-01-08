using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class AddPictureActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureType",
                schema: "aiala",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                schema: "aiala",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId1",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_TenantId",
                schema: "aiala",
                table: "Pictures",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PictureId",
                schema: "aiala",
                table: "Activities",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PictureId1",
                schema: "aiala",
                table: "Activities",
                column: "PictureId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Pictures_PictureId1",
                schema: "aiala",
                table: "Activities",
                column: "PictureId1",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Tenants_TenantId",
                schema: "aiala",
                table: "Pictures",
                column: "TenantId",
                principalSchema: "directory",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Pictures_PictureId1",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Tenants_TenantId",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_TenantId",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Activities_PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_PictureId1",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PictureType",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "aiala",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                schema: "aiala",
                table: "Activities");
        }
    }
}
