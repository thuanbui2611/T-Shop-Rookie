using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editTypeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_brand_table_type_TypeID",
                table: "table_brand");

            migrationBuilder.DropIndex(
                name: "IX_table_brand_TypeID",
                table: "table_brand");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("2446e3b5-3208-41ed-8843-0f588d893e7c"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("b5f229fe-f9cb-4fbd-a24c-bee1d95fbae6"));

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "table_brand");

            migrationBuilder.AddColumn<Guid>(
                name: "FK_type_id",
                table: "table_product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c4f90011-5387-40c8-927e-cb139a9137bd"), null, "Admin", "ADMIN" },
                    { new Guid("d402bf97-3fd2-4580-bc52-60e1ea56ab1f"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_type_id",
                table: "table_product",
                column: "FK_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_table_type_FK_type_id",
                table: "table_product",
                column: "FK_type_id",
                principalTable: "table_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_table_type_FK_type_id",
                table: "table_product");

            migrationBuilder.DropIndex(
                name: "IX_table_product_FK_type_id",
                table: "table_product");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("c4f90011-5387-40c8-927e-cb139a9137bd"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("d402bf97-3fd2-4580-bc52-60e1ea56ab1f"));

            migrationBuilder.DropColumn(
                name: "FK_type_id",
                table: "table_product");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeID",
                table: "table_brand",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2446e3b5-3208-41ed-8843-0f588d893e7c"), null, "Admin", "ADMIN" },
                    { new Guid("b5f229fe-f9cb-4fbd-a24c-bee1d95fbae6"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_brand_TypeID",
                table: "table_brand",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_table_brand_table_type_TypeID",
                table: "table_brand",
                column: "TypeID",
                principalTable: "table_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
