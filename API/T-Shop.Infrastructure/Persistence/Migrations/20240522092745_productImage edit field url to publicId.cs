using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class productImageeditfieldurltopublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "table_product_review_image",
                newName: "ImagePublicID");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "table_product_image",
                newName: "ImagePublicID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePublicID",
                table: "table_product_review_image",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePublicID",
                table: "table_product_image",
                newName: "ImageUrl");
        }
    }
}
