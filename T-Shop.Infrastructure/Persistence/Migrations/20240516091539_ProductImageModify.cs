using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductImageModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("03f51320-ccd1-4941-9109-e3403b47a0e0"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("908f9f61-8352-4d62-aa25-c8a5459f2955"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "table_product_image");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "table_product_image",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("41508c25-8802-4fc9-9ff8-1dd9264c80d9"), null, "User", "USER" },
                    { new Guid("80d96eb4-3783-4f5b-99b1-9a202010c6fe"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("41508c25-8802-4fc9-9ff8-1dd9264c80d9"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("80d96eb4-3783-4f5b-99b1-9a202010c6fe"));

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "table_product_image");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "table_product_image",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03f51320-ccd1-4941-9109-e3403b47a0e0"), null, "Admin", "ADMIN" },
                    { new Guid("908f9f61-8352-4d62-aa25-c8a5459f2955"), null, "User", "USER" }
                });
        }
    }
}
