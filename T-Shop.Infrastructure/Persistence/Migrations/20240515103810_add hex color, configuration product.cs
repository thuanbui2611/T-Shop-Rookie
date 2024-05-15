using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addhexcolorconfigurationproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("c4f90011-5387-40c8-927e-cb139a9137bd"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("d402bf97-3fd2-4580-bc52-60e1ea56ab1f"));

            migrationBuilder.AddColumn<string>(
                name: "Configuration",
                table: "table_product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "table_product",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "HexColor",
                table: "table_color",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5e81d9e9-a4d7-4687-a874-2fe128efc243"), null, "User", "USER" },
                    { new Guid("e5c9d242-4296-48e7-a56f-4ffefb6022c0"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("5e81d9e9-a4d7-4687-a874-2fe128efc243"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("e5c9d242-4296-48e7-a56f-4ffefb6022c0"));

            migrationBuilder.DropColumn(
                name: "Configuration",
                table: "table_product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "table_product");

            migrationBuilder.DropColumn(
                name: "HexColor",
                table: "table_color");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c4f90011-5387-40c8-927e-cb139a9137bd"), null, "Admin", "ADMIN" },
                    { new Guid("d402bf97-3fd2-4580-bc52-60e1ea56ab1f"), null, "User", "USER" }
                });
        }
    }
}
