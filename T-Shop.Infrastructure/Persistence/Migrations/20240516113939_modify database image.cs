using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifydatabaseimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_image_table_image_PK_FK_image_id",
                table: "table_product_image");

            migrationBuilder.DropForeignKey(
                name: "FK_table_product_image_table_product_PK_FK_product_id",
                table: "table_product_image");

            migrationBuilder.DropForeignKey(
                name: "FK_table_users_table_image_avatar_id",
                table: "table_users");

            migrationBuilder.DropTable(
                name: "table_image");

            migrationBuilder.DropIndex(
                name: "IX_table_users_avatar_id",
                table: "table_users");

            migrationBuilder.DropIndex(
                name: "IX_table_product_image_PK_FK_image_id",
                table: "table_product_image");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("41508c25-8802-4fc9-9ff8-1dd9264c80d9"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("80d96eb4-3783-4f5b-99b1-9a202010c6fe"));

            migrationBuilder.DropColumn(
                name: "avatar_id",
                table: "table_users");

            migrationBuilder.RenameColumn(
                name: "PK_FK_image_id",
                table: "table_product_image",
                newName: "ImageID");

            migrationBuilder.RenameColumn(
                name: "PK_FK_product_id",
                table: "table_product_image",
                newName: "ProductID");

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "table_users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "table_product_image",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6a030151-ab91-4493-8e79-3d7b6d9a191d"), null, "User", "USER" },
                    { new Guid("74b66054-8c32-43f7-a83f-4abec006e93f"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_image_table_product_ProductID",
                table: "table_product_image",
                column: "ProductID",
                principalTable: "table_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_image_table_product_ProductID",
                table: "table_product_image");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("6a030151-ab91-4493-8e79-3d7b6d9a191d"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("74b66054-8c32-43f7-a83f-4abec006e93f"));

            migrationBuilder.DropColumn(
                name: "avatar",
                table: "table_users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "table_product_image");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "table_product_image",
                newName: "PK_FK_image_id");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "table_product_image",
                newName: "PK_FK_product_id");

            migrationBuilder.AddColumn<Guid>(
                name: "avatar_id",
                table: "table_users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "table_image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    PublicID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_image", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("41508c25-8802-4fc9-9ff8-1dd9264c80d9"), null, "User", "USER" },
                    { new Guid("80d96eb4-3783-4f5b-99b1-9a202010c6fe"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_users_avatar_id",
                table: "table_users",
                column: "avatar_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_table_product_image_PK_FK_image_id",
                table: "table_product_image",
                column: "PK_FK_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_image_table_image_PK_FK_image_id",
                table: "table_product_image",
                column: "PK_FK_image_id",
                principalTable: "table_image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_image_table_product_PK_FK_product_id",
                table: "table_product_image",
                column: "PK_FK_product_id",
                principalTable: "table_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_users_table_image_avatar_id",
                table: "table_users",
                column: "avatar_id",
                principalTable: "table_image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
