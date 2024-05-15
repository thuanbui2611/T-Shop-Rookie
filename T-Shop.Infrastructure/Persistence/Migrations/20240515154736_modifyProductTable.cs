using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_table_product_FK_color_id",
                table: "table_product");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("5e81d9e9-a4d7-4687-a874-2fe128efc243"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("e5c9d242-4296-48e7-a56f-4ffefb6022c0"));

            migrationBuilder.RenameColumn(
                name: "Configuration",
                table: "table_product",
                newName: "Variant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "table_users",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "table_users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "table_product",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("37f97fc7-2d2b-4f84-bbbb-bce1774ec71c"), null, "User", "USER" },
                    { new Guid("dc1437fc-a249-47b9-a754-b263466ea70d"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_color_id_FK_model_id_Variant",
                table: "table_product",
                columns: new[] { "FK_color_id", "FK_model_id", "Variant" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_table_product_FK_color_id_FK_model_id_Variant",
                table: "table_product");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("37f97fc7-2d2b-4f84-bbbb-bce1774ec71c"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("dc1437fc-a249-47b9-a754-b263466ea70d"));

            migrationBuilder.RenameColumn(
                name: "Variant",
                table: "table_product",
                newName: "Configuration");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "table_users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "table_users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "table_product",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5e81d9e9-a4d7-4687-a874-2fe128efc243"), null, "User", "USER" },
                    { new Guid("e5c9d242-4296-48e7-a56f-4ffefb6022c0"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_color_id",
                table: "table_product",
                column: "FK_color_id");
        }
    }
}
