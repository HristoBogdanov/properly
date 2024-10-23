using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2fcca831-9c8f-48c3-9324-8c7d364ec9cb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a737f58-026e-4456-8ff4-e9473faeeec4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f08154d1-eb9d-44dd-ba33-96d0e7294a52"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("455f84d4-0f5f-43ed-8064-a804984e9284"), null, "User", "USER" },
                    { new Guid("b3664a81-a910-444e-bfd0-50f1f3ab593d"), null, "Broker", "BROKER" },
                    { new Guid("dc26e9a2-7643-4532-b3df-49e76070987e"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("455f84d4-0f5f-43ed-8064-a804984e9284"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3664a81-a910-444e-bfd0-50f1f3ab593d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc26e9a2-7643-4532-b3df-49e76070987e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2fcca831-9c8f-48c3-9324-8c7d364ec9cb"), null, "User", "USER" },
                    { new Guid("3a737f58-026e-4456-8ff4-e9473faeeec4"), null, "Broker", "BROKER" },
                    { new Guid("f08154d1-eb9d-44dd-ba33-96d0e7294a52"), null, "Admin", "ADMIN" }
                });
        }
    }
}
