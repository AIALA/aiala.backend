using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiala.Backend.Migrations
{
    public partial class UpdateDirectoryAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Activities_PictureId1",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PictureId1",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "directory",
                table: "Registrations",
                newName: "ExternalUserId");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                schema: "directory",
                table: "Accounts",
                newName: "Invitation_Message");

            migrationBuilder.RenameColumn(
                name: "InvitationId",
                schema: "directory",
                table: "Accounts",
                newName: "Invitation_InviterId");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                schema: "directory",
                table: "Accounts",
                newName: "Invitation_ConfirmationToken");

            migrationBuilder.AddColumn<string>(
                name: "Culture",
                schema: "directory",
                table: "Users",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Culture",
                schema: "directory",
                table: "Tenants",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                schema: "directory",
                table: "Tenants",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Invitation_Accepted",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Invitation_Created",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Invitation_Declined",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Invitation_Resent",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Invitation_ValidUntil",
                schema: "directory",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Invitation_Status",
                schema: "directory",
                table: "Accounts",
                nullable: false,
                computedColumnSql: @"CAST
                    (
                    CASE
                        WHEN [Invitation_Declined] IS NOT NULL THEN 3
                        WHEN [Invitation_Accepted] IS NOT NULL THEN 4
                        WHEN [Invitation_ValidUntil] IS NOT NULL AND [Invitation_ValidUntil] < GETUTCDATE() THEN 2
                        WHEN [Invitation_Created] IS NOT NULL THEN 1
                        ELSE 0
                    END AS INT
                    )");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities",
                column: "PictureId",
                principalSchema: "aiala",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Pictures_PictureId",
                schema: "aiala",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Culture",
                schema: "directory",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Culture",
                schema: "directory",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Region",
                schema: "directory",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Invitation_Accepted",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Invitation_Created",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Invitation_Declined",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Invitation_Resent",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Invitation_Status",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Invitation_ValidUntil",
                schema: "directory",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ExternalUserId",
                schema: "directory",
                table: "Registrations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Invitation_Message",
                schema: "directory",
                table: "Accounts",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "Invitation_InviterId",
                schema: "directory",
                table: "Accounts",
                newName: "InvitationId");

            migrationBuilder.RenameColumn(
                name: "Invitation_ConfirmationToken",
                schema: "directory",
                table: "Accounts",
                newName: "Firstname");

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

            migrationBuilder.AddColumn<Guid>(
                name: "PictureId1",
                schema: "aiala",
                table: "Activities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invitations",
                schema: "directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Accepted = table.Column<DateTimeOffset>(nullable: true),
                    ConfirmationToken = table.Column<string>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Culture = table.Column<string>(nullable: true),
                    Declined = table.Column<DateTimeOffset>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    InviterId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Resent = table.Column<int>(nullable: false),
                    ValidUntil = table.Column<DateTimeOffset>(nullable: false)
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
    }
}
