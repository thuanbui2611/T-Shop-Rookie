using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Shop.Infrastructure.Migrations
{
    public partial class productSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("028ac992-cc36-43c0-9fb7-d5226541ca7f"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("9cdacf2a-ef21-4d39-afa6-1bb10566b44a"));

            migrationBuilder.InsertData(
                table: "table_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("204c08bd-b369-4b07-b102-16bb1de763f1"), "Category1" },
                    { new Guid("33665f30-e7b7-4b50-94cf-6c0ffc6b5c03"), "Category2" }
                });

            migrationBuilder.InsertData(
                table: "table_product",
                columns: new[] { "Id", "FK_category_id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("5ce09e64-67a9-47d9-9059-9e3ad9815794"), new Guid("33665f30-e7b7-4b50-94cf-6c0ffc6b5c03"), "This is test product", "Product2" },
                    { new Guid("5de23157-9d10-44b6-bbba-f4b833e384a6"), new Guid("204c08bd-b369-4b07-b102-16bb1de763f1"), "This is test product", "Product1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5ce09e64-67a9-47d9-9059-9e3ad9815794"));

            migrationBuilder.DeleteData(
                table: "table_product",
                keyColumn: "Id",
                keyValue: new Guid("5de23157-9d10-44b6-bbba-f4b833e384a6"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("204c08bd-b369-4b07-b102-16bb1de763f1"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("33665f30-e7b7-4b50-94cf-6c0ffc6b5c03"));

            migrationBuilder.InsertData(
                table: "table_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("028ac992-cc36-43c0-9fb7-d5226541ca7f"), "Category1" },
                    { new Guid("9cdacf2a-ef21-4d39-afa6-1bb10566b44a"), "Category2" }
                });
        }
    }
}
