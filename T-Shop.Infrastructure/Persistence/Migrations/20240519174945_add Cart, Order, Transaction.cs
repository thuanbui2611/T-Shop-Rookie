using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCartOrderTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_model_table_brand_BrandID",
                table: "table_model");

            migrationBuilder.DropForeignKey(
                name: "FK_table_product_image_table_product_ProductID",
                table: "table_product_image");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("979288ab-57be-4bee-8b4d-38236346705a"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("a7e9e7cd-b630-4be8-a0a0-18c52bc4de3f"));

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "table_product_image",
                newName: "FK_product_id");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "table_model",
                newName: "FK_brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_model_BrandID",
                table: "table_model",
                newName: "IX_table_model_FK_brand_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "table_product",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "table_product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "table_cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    PaymentIntentID = table.Column<string>(type: "text", nullable: false),
                    ClientSecret = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_cart_items",
                columns: table => new
                {
                    PK_FK_cart_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_cart_items", x => new { x.PK_FK_cart_id, x.PK_FK_product_id });
                    table.ForeignKey(
                        name: "FK_table_cart_items_table_cart_PK_FK_cart_id",
                        column: x => x.PK_FK_cart_id,
                        principalTable: "table_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_cart_items_table_product_PK_FK_product_id",
                        column: x => x.PK_FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_order_detail",
                columns: table => new
                {
                    PK_FK_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_order_detail", x => new { x.PK_FK_order_id, x.PK_FK_product_id });
                    table.ForeignKey(
                        name: "FK_table_order_detail_table_order_PK_FK_order_id",
                        column: x => x.PK_FK_order_id,
                        principalTable: "table_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_order_detail_table_product_PK_FK_product_id",
                        column: x => x.PK_FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_transaction_table_order_FK_order_id",
                        column: x => x.FK_order_id,
                        principalTable: "table_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("008d2609-2d67-49eb-ad91-9329ff2e14ec"), null, "User", "USER" },
                    { new Guid("1770630c-cd71-4d87-8dbd-96e3d9e6c541"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_cart_items_PK_FK_product_id",
                table: "table_cart_items",
                column: "PK_FK_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_order_detail_PK_FK_product_id",
                table: "table_order_detail",
                column: "PK_FK_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_transaction_FK_order_id",
                table: "table_transaction",
                column: "FK_order_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_table_model_table_brand_FK_brand_id",
                table: "table_model",
                column: "FK_brand_id",
                principalTable: "table_brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_image_table_product_FK_product_id",
                table: "table_product_image",
                column: "FK_product_id",
                principalTable: "table_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_model_table_brand_FK_brand_id",
                table: "table_model");

            migrationBuilder.DropForeignKey(
                name: "FK_table_product_image_table_product_FK_product_id",
                table: "table_product_image");

            migrationBuilder.DropTable(
                name: "table_cart_items");

            migrationBuilder.DropTable(
                name: "table_order_detail");

            migrationBuilder.DropTable(
                name: "table_transaction");

            migrationBuilder.DropTable(
                name: "table_cart");

            migrationBuilder.DropTable(
                name: "table_order");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("008d2609-2d67-49eb-ad91-9329ff2e14ec"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("1770630c-cd71-4d87-8dbd-96e3d9e6c541"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "table_product");

            migrationBuilder.RenameColumn(
                name: "FK_product_id",
                table: "table_product_image",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "FK_brand_id",
                table: "table_model",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_table_model_FK_brand_id",
                table: "table_model",
                newName: "IX_table_model_BrandID");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "table_product",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("979288ab-57be-4bee-8b4d-38236346705a"), null, "User", "USER" },
                    { new Guid("a7e9e7cd-b630-4be8-a0a0-18c52bc4de3f"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_table_model_table_brand_BrandID",
                table: "table_model",
                column: "BrandID",
                principalTable: "table_brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_image_table_product_ProductID",
                table: "table_product_image",
                column: "ProductID",
                principalTable: "table_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
