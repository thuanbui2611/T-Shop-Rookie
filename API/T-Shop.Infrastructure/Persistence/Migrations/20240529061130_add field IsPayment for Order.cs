using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T_Shop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addfieldIsPaymentforOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPayment",
                table: "table_order",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayment",
                table: "table_order");
        }
    }
}
