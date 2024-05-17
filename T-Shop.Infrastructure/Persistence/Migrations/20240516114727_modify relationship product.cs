using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyrelationshipproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("6a030151-ab91-4493-8e79-3d7b6d9a191d"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("74b66054-8c32-43f7-a83f-4abec006e93f"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("979288ab-57be-4bee-8b4d-38236346705a"), null, "User", "USER" },
                    { new Guid("a7e9e7cd-b630-4be8-a0a0-18c52bc4de3f"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("979288ab-57be-4bee-8b4d-38236346705a"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("a7e9e7cd-b630-4be8-a0a0-18c52bc4de3f"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6a030151-ab91-4493-8e79-3d7b6d9a191d"), null, "User", "USER" },
                    { new Guid("74b66054-8c32-43f7-a83f-4abec006e93f"), null, "Admin", "ADMIN" }
                });
        }
    }
}
