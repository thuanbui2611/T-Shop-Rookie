using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Shop.Infrastructure.Migrations
{
    public partial class categorySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "table_category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("028ac992-cc36-43c0-9fb7-d5226541ca7f"), "Category1" },
                    { new Guid("9cdacf2a-ef21-4d39-afa6-1bb10566b44a"), "Category2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("028ac992-cc36-43c0-9fb7-d5226541ca7f"));

            migrationBuilder.DeleteData(
                table: "table_category",
                keyColumn: "Id",
                keyValue: new Guid("9cdacf2a-ef21-4d39-afa6-1bb10566b44a"));
        }
    }
}
