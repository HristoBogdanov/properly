using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProperlyAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7d24a848-29d3-4165-9506-5d58f2da9915"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d69187c3-e619-4f1b-b264-985e9c0e4386"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eebe6971-8ba7-46ac-b5f0-b9658c11a594"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the category"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "The title of the category"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates whether the category is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the feature"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "The title of the feature"),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The path to the icon of the feature"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates whether the feature is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the property"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "The title of the property"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false, comment: "The description of the property"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The address of the property"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "The price of the property"),
                    ForSale = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates if the property is for sale"),
                    ForRent = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates if the property is for rent"),
                    Bedrooms = table.Column<int>(type: "int", nullable: false, comment: "The number of bedrooms of the property"),
                    Bathrooms = table.Column<int>(type: "int", nullable: false, comment: "The number of bathrooms of the property"),
                    IsFurnished = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates if the property is furnished"),
                    Area = table.Column<int>(type: "int", nullable: false, comment: "The total area of the property in square meters"),
                    YearOfConstruction = table.Column<int>(type: "int", nullable: false, comment: "The year of construction of the property"),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of the property owner"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Flag that indicates whether the property is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesCategories",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the property"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesCategories", x => new { x.PropertyId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PropertiesCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesCategories_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesFeatures",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the property"),
                    FeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the feature")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesFeatures", x => new { x.PropertyId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PropertiesFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesFeatures_Properties_PropertyId",
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
                    { new Guid("2fcca831-9c8f-48c3-9324-8c7d364ec9cb"), null, "User", "USER" },
                    { new Guid("3a737f58-026e-4456-8ff4-e9473faeeec4"), null, "Broker", "BROKER" },
                    { new Guid("f08154d1-eb9d-44dd-ba33-96d0e7294a52"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesCategories_CategoryId",
                table: "PropertiesCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesFeatures_FeatureId",
                table: "PropertiesFeatures",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertiesCategories");

            migrationBuilder.DropTable(
                name: "PropertiesFeatures");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Properties");

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
                    { new Guid("7d24a848-29d3-4165-9506-5d58f2da9915"), null, "User", "USER" },
                    { new Guid("d69187c3-e619-4f1b-b264-985e9c0e4386"), null, "Admin", "ADMIN" },
                    { new Guid("eebe6971-8ba7-46ac-b5f0-b9658c11a594"), null, "Broker", "BROKER" }
                });
        }
    }
}
