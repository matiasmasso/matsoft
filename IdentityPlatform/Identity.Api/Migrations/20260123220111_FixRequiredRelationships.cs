using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixRequiredRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnrollmentId",
                table: "UserRoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles",
                columns: new[] { "EnrollmentId", "RoleId" },
                unique: true,
                filter: "[EnrollmentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnrollmentId",
                table: "UserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_EnrollmentId_RoleId",
                table: "UserRoles",
                columns: new[] { "EnrollmentId", "RoleId" },
                unique: true);
        }
    }
}
