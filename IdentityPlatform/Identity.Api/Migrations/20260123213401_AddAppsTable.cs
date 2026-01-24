using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAppsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_UserAppEnrollmentId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAppEnrollments_UserId_AppKey",
                table: "UserAppEnrollments");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "AppKey",
                table: "UserAppEnrollments");

            migrationBuilder.RenameColumn(
                name: "UserAppEnrollmentId",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserAppEnrollmentId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddColumn<Guid>(
                name: "EnrollmentId",
                table: "UserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "UserAppEnrollments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoles_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles",
                columns: new[] { "EnrollmentId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAppEnrollments_AppId",
                table: "UserAppEnrollments",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAppEnrollments_UserId_AppId",
                table: "UserAppEnrollments",
                columns: new[] { "UserId", "AppId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_AppId_Name",
                table: "AppRoles",
                columns: new[] { "AppId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apps_Key",
                table: "Apps",
                column: "Key",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AppRoles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_EnrollmentId",
                table: "UserRoles",
                column: "EnrollmentId",
                principalTable: "UserAppEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AppRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_EnrollmentId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserAppEnrollments_AppId",
                table: "UserAppEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_UserAppEnrollments_UserId_AppId",
                table: "UserAppEnrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "UserAppEnrollments");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoles",
                newName: "UserAppEnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_UserAppEnrollmentId");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppKey",
                table: "UserAppEnrollments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserAppEnrollments_UserId_AppKey",
                table: "UserAppEnrollments",
                columns: new[] { "UserId", "AppKey" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_UserAppEnrollmentId",
                table: "UserRoles",
                column: "UserAppEnrollmentId",
                principalTable: "UserAppEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
