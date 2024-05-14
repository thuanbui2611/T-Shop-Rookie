using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_table_category_FK_category_id",
                table: "table_product");

            migrationBuilder.DropTable(
                name: "table_category");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("069283ad-326e-447b-be82-b7339c597b7a"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("49a74a73-30ed-41d0-90ae-00624bc85f48"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "table_product");

            migrationBuilder.RenameColumn(
                name: "FK_category_id",
                table: "table_product",
                newName: "FK_model_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_product_FK_category_id",
                table: "table_product",
                newName: "IX_table_product_FK_model_id");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "table_users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "table_users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "table_product",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "table_product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "FK_color_id",
                table: "table_product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsOnStock",
                table: "table_product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "table_color",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_color", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "table_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "table_product_image",
                columns: table => new
                {
                    PK_FK_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    PK_FK_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_product_image", x => new { x.PK_FK_product_id, x.PK_FK_image_id });
                    table.ForeignKey(
                        name: "FK_table_product_image_table_image_PK_FK_image_id",
                        column: x => x.PK_FK_image_id,
                        principalTable: "table_image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_table_product_image_table_product_PK_FK_product_id",
                        column: x => x.PK_FK_product_id,
                        principalTable: "table_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_brand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TypeID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_brand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_brand_table_type_TypeID",
                        column: x => x.TypeID,
                        principalTable: "table_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "table_model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    BrandID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_table_model_table_brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "table_brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2446e3b5-3208-41ed-8843-0f588d893e7c"), null, "Admin", "ADMIN" },
                    { new Guid("b5f229fe-f9cb-4fbd-a24c-bee1d95fbae6"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_product_FK_color_id",
                table: "table_product",
                column: "FK_color_id");

            migrationBuilder.CreateIndex(
                name: "IX_table_brand_TypeID",
                table: "table_brand",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_table_model_BrandID",
                table: "table_model",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_table_product_image_PK_FK_image_id",
                table: "table_product_image",
                column: "PK_FK_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_table_color_FK_color_id",
                table: "table_product",
                column: "FK_color_id",
                principalTable: "table_color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_table_model_FK_model_id",
                table: "table_product",
                column: "FK_model_id",
                principalTable: "table_model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_product_table_color_FK_color_id",
                table: "table_product");

            migrationBuilder.DropForeignKey(
                name: "FK_table_product_table_model_FK_model_id",
                table: "table_product");

            migrationBuilder.DropTable(
                name: "table_color");

            migrationBuilder.DropTable(
                name: "table_model");

            migrationBuilder.DropTable(
                name: "table_product_image");

            migrationBuilder.DropTable(
                name: "table_brand");

            migrationBuilder.DropTable(
                name: "table_image");

            migrationBuilder.DropTable(
                name: "table_type");

            migrationBuilder.DropIndex(
                name: "IX_table_product_FK_color_id",
                table: "table_product");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("2446e3b5-3208-41ed-8843-0f588d893e7c"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("b5f229fe-f9cb-4fbd-a24c-bee1d95fbae6"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "table_product");

            migrationBuilder.DropColumn(
                name: "FK_color_id",
                table: "table_product");

            migrationBuilder.DropColumn(
                name: "IsOnStock",
                table: "table_product");

            migrationBuilder.RenameColumn(
                name: "FK_model_id",
                table: "table_product",
                newName: "FK_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_table_product_FK_model_id",
                table: "table_product",
                newName: "IX_table_product_FK_category_id");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "table_users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "table_users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "table_product",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "table_product",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "table_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_table_category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("069283ad-326e-447b-be82-b7339c597b7a"), null, "Admin", "ADMIN" },
                    { new Guid("49a74a73-30ed-41d0-90ae-00624bc85f48"), null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_table_product_table_category_FK_category_id",
                table: "table_product",
                column: "FK_category_id",
                principalTable: "table_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
