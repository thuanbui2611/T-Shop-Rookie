using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addproductreviewimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_table_product_image",
                table: "table_product_image");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("921b5402-9d4b-4171-978a-83dd77a85d70"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("95735f92-cd5f-4d74-b6f3-b39e8df42c25"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_product_image",
                table: "table_product_image",
                column: "ImageID");

            migrationBuilder.CreateTable(
                name: "table_product_review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductID = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionID = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_product_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_product_review_table_product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_product_review_table_transaction_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "table_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_product_review_image",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_product_review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_product_review_image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_table_product_review_image_table_product_review_FK_product_~",
                        column: x => x.FK_product_review_id,
                        principalTable: "table_product_review",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("55ea57a5-5eb0-446a-bd16-54de57738815"), null, "User", "USER" },
                    { new Guid("d2145b7c-6d76-4eb9-9e6c-ab101b754c72"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_product_image_FK_product_id",
                table: "table_product_image",
                column: "FK_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_review_ProductID",
                table: "table_product_review",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_review_TransactionID",
                table: "table_product_review",
                column: "TransactionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_product_review_image_FK_product_review_id",
                table: "table_product_review_image",
                column: "FK_product_review_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "table_product_review_image");

            migrationBuilder.DropTable(
                name: "table_product_review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_table_product_image",
                table: "table_product_image");

            migrationBuilder.DropIndex(
                name: "IX_table_product_image_FK_product_id",
                table: "table_product_image");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("55ea57a5-5eb0-446a-bd16-54de57738815"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("d2145b7c-6d76-4eb9-9e6c-ab101b754c72"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_table_product_image",
                table: "table_product_image",
                columns: new[] { "FK_product_id", "ImageID" });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("921b5402-9d4b-4171-978a-83dd77a85d70"), null, "Admin", "ADMIN" },
                    { new Guid("95735f92-cd5f-4d74-b6f3-b39e8df42c25"), null, "User", "USER" }
                });
        }
    }
}
