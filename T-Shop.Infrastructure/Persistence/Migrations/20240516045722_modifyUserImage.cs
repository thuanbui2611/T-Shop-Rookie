using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace T_Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyUserImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("37f97fc7-2d2b-4f84-bbbb-bce1774ec71c"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("dc1437fc-a249-47b9-a754-b263466ea70d"));

            migrationBuilder.DropColumn(
                name: "avatar",
                table: "table_users");

            migrationBuilder.AddColumn<Guid>(
                name: "avatar_id",
                table: "table_users",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("03f51320-ccd1-4941-9109-e3403b47a0e0"), null, "Admin", "ADMIN" },
                    { new Guid("908f9f61-8352-4d62-aa25-c8a5459f2955"), null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_table_users_avatar_id",
                table: "table_users",
                column: "avatar_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_table_users_table_image_avatar_id",
                table: "table_users",
                column: "avatar_id",
                principalTable: "table_image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_table_users_table_image_avatar_id",
                table: "table_users");

            migrationBuilder.DropIndex(
                name: "IX_table_users_avatar_id",
                table: "table_users");

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("03f51320-ccd1-4941-9109-e3403b47a0e0"));

            migrationBuilder.DeleteData(
                table: "table_roles",
                keyColumn: "Id",
                keyValue: new Guid("908f9f61-8352-4d62-aa25-c8a5459f2955"));

            migrationBuilder.DropColumn(
                name: "avatar_id",
                table: "table_users");

            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "table_users",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "table_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("37f97fc7-2d2b-4f84-bbbb-bce1774ec71c"), null, "User", "USER" },
                    { new Guid("dc1437fc-a249-47b9-a754-b263466ea70d"), null, "Admin", "ADMIN" }
                });
        }
    }
}
