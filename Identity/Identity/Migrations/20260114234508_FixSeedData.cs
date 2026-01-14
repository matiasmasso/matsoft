using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "AppId",
                keyValue: new Guid("10dfb77f-3ee3-4585-8a14-c5b4f55674db"));

            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "AppId",
                keyValue: new Guid("9c2bc9c4-c39e-43ed-9140-c5aa7815a30a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("2435e660-9862-482c-bc9a-7a50992fb278"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("9ce1718c-1013-4ab5-ba80-d03b41454caa"));

            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "AppId", "Name" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Dashboard" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Reports" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Admin" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "AppId",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Apps",
                keyColumn: "AppId",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.InsertData(
                table: "Apps",
                columns: new[] { "AppId", "Name" },
                values: new object[,]
                {
                    { new Guid("10dfb77f-3ee3-4585-8a14-c5b4f55674db"), "Reports" },
                    { new Guid("9c2bc9c4-c39e-43ed-9140-c5aa7815a30a"), "Dashboard" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("2435e660-9862-482c-bc9a-7a50992fb278"), "User" },
                    { new Guid("9ce1718c-1013-4ab5-ba80-d03b41454caa"), "Admin" }
                });
        }
    }
}
