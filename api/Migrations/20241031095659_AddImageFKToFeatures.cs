using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFKToFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "Features");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "The unique identifier of the image of the feature");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7675ebf3-f186-4da8-93e3-05aafc241306"), null, "User", "USER" },
                    { new Guid("b2cdec91-7ab2-4778-b5d4-4d78aaa218eb"), null, "Broker", "BROKER" },
                    { new Guid("b48b4c9e-4953-47f2-a82f-f44d27270e01"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_ImageId",
                table: "Features",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Images_ImageId",
                table: "Features",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Images_ImageId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ImageId",
                table: "Features");

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

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Features");

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "The path to the icon of the feature");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("09ce8953-af8a-4c8f-9521-091947b425f1"), null, "User", "USER" },
                    { new Guid("39563165-9c35-4eb5-95bd-9fe630a12ed3"), null, "Admin", "ADMIN" },
                    { new Guid("f30092ef-8ac4-4179-a38b-c6c42a901d41"), null, "Broker", "BROKER" }
                });
        }
    }
}
