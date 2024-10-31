using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier for the image."),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the image."),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The path to the image.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9ba35cb3-852f-4bd8-8700-8af77441ee30"), null, "Broker", "BROKER" },
                    { new Guid("f50214ac-27e7-4ee6-ae69-da5b1630d374"), null, "User", "USER" },
                    { new Guid("faeec210-22ab-457d-821f-9eef8d75c7dc"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ba35cb3-852f-4bd8-8700-8af77441ee30"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f50214ac-27e7-4ee6-ae69-da5b1630d374"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("faeec210-22ab-457d-821f-9eef8d75c7dc"));

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
    }
}
