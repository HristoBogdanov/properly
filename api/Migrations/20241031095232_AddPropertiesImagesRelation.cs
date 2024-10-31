using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesImagesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "PropertiesImages",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Inuqie identifier of the property"),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Inuqie identifier of the image")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesImages", x => new { x.PropertyId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_PropertiesImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("09ce8953-af8a-4c8f-9521-091947b425f1"), null, "User", "USER" },
                    { new Guid("39563165-9c35-4eb5-95bd-9fe630a12ed3"), null, "Admin", "ADMIN" },
                    { new Guid("f30092ef-8ac4-4179-a38b-c6c42a901d41"), null, "Broker", "BROKER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesImages_ImageId",
                table: "PropertiesImages",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertiesImages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("09ce8953-af8a-4c8f-9521-091947b425f1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("39563165-9c35-4eb5-95bd-9fe630a12ed3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f30092ef-8ac4-4179-a38b-c6c42a901d41"));

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
    }
}
