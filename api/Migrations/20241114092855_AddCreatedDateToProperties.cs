using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedDateToProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7675ebf3-f186-4da8-93e3-05aafc241306"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2cdec91-7ab2-4778-b5d4-4d78aaa218eb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b48b4c9e-4953-47f2-a82f-f44d27270e01"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Properties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7675ebf3-f186-4da8-93e3-05aafc241306"), null, "User", "USER" },
                    { new Guid("b2cdec91-7ab2-4778-b5d4-4d78aaa218eb"), null, "Broker", "BROKER" },
                    { new Guid("b48b4c9e-4953-47f2-a82f-f44d27270e01"), null, "Admin", "ADMIN" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Properties");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7675ebf3-f186-4da8-93e3-05aafc241306"), null, "User", "USER" },
                    { new Guid("b2cdec91-7ab2-4778-b5d4-4d78aaa218eb"), null, "Broker", "BROKER" },
                    { new Guid("b48b4c9e-4953-47f2-a82f-f44d27270e01"), null, "Admin", "ADMIN" }
                });
        }
    }
}
