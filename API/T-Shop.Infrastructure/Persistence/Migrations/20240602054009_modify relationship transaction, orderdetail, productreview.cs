using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifyrelationshiptransactionorderdetailproductreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_review_table_transaction_TransactionID",
                table: "table_product_review");

            migrationBuilder.DropIndex(
                name: "IX_table_product_review_TransactionID",
                table: "table_product_review");

            migrationBuilder.RenameColumn(
                name: "TransactionID",
                table: "table_product_review",
                newName: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_review_OrderID_ProductID",
                table: "table_product_review",
                columns: new[] { "OrderID", "ProductID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_review_table_order_detail_OrderID_ProductID",
                table: "table_product_review",
                columns: new[] { "OrderID", "ProductID" },
                principalTable: "table_order_detail",
                principalColumns: new[] { "PK_FK_order_id", "PK_FK_product_id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_review_table_order_detail_OrderID_ProductID",
                table: "table_product_review");

            migrationBuilder.DropIndex(
                name: "IX_table_product_review_OrderID_ProductID",
                table: "table_product_review");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "table_product_review",
                newName: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_review_TransactionID",
                table: "table_product_review",
                column: "TransactionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_review_table_transaction_TransactionID",
                table: "table_product_review",
                column: "TransactionID",
                principalTable: "table_transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
