using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UserManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "aiala");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "directory",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationId",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppNumber",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    ValidUntil = table.Column<DateTimeOffset>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ConfirmationToken = table.Column<string>(nullable: true),
                    Culture = table.Column<string>(nullable: true),
                    Resent = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Accepted = table.Column<DateTimeOffset>(nullable: true),
                    Declined = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InvitationId",
                schema: "directory",
                table: "Accounts",
                column: "InvitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Invitations_InvitationId",
                schema: "directory",
                table: "Accounts",
                column: "InvitationId",
                principalSchema: "directory",
                principalTable: "Invitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                schema: "directory",
                table: "Accounts",
                column: "UserId",
                principalSchema: "directory",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Invitations_InvitationId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Invitations",
                schema: "directory");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_InvitationId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Firstname",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InvitationId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Lastname",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoleType",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "WhatsAppNumber",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                schema: "directory",
                table: "Accounts",
                column: "UserId",
                principalSchema: "directory",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
