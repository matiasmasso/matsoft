using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Api.Migrations
{
    /// <inheritdoc />
    public partial class CascadeCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_Apps_AppId",
                table: "AppRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppEnrollments_Users_UserId",
                table: "UserAppEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AppRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_EnrollmentId",
                table: "UserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoles_Apps_AppId",
                table: "AppRoles",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppEnrollments_Users_UserId",
                table: "UserAppEnrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AppRoles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_EnrollmentId",
                table: "UserRoles",
                column: "EnrollmentId",
                principalTable: "UserAppEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_Apps_AppId",
                table: "AppRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppEnrollments_Users_UserId",
                table: "UserAppEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AppRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserAppEnrollments_EnrollmentId",
                table: "UserRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoles_Apps_AppId",
                table: "AppRoles",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppEnrollments_Apps_AppId",
                table: "UserAppEnrollments",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppEnrollments_Users_UserId",
                table: "UserAppEnrollments",
                column: "UserId",
                principalTable: "Users",
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
    }
}
