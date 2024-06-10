using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editrelationshiporderproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_order_detail_table_product_PK_FK_product_id",
                table: "table_order_detail");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("3eb7521c-235f-4265-ad65-8d9d18ea86e4"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("4f74d942-2b79-4510-8248-bc75bd5f0a89"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("921b5402-9d4b-4171-978a-83dd77a85d70"), null, "Admin", "ADMIN" },
                    { new Guid("95735f92-cd5f-4d74-b6f3-b39e8df42c25"), null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_table_order_detail_table_product_PK_FK_product_id",
                table: "table_order_detail",
                column: "PK_FK_product_id",
                principalTable: "table_product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_order_detail_table_product_PK_FK_product_id",
                table: "table_order_detail");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("921b5402-9d4b-4171-978a-83dd77a85d70"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("95735f92-cd5f-4d74-b6f3-b39e8df42c25"));

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3eb7521c-235f-4265-ad65-8d9d18ea86e4"), null, "Admin", "ADMIN" },
                    { new Guid("4f74d942-2b79-4510-8248-bc75bd5f0a89"), null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_table_order_detail_table_product_PK_FK_product_id",
                table: "table_order_detail",
                column: "PK_FK_product_id",
                principalTable: "table_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
