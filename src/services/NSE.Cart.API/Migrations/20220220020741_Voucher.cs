using Microsoft.EntityFrameworkCore.Migrations;

namespace NSE.Cart.API.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CartCustomer",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherUsed",
                table: "CartCustomer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "CartCustomer",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "CartCustomer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeDiscount",
                table: "CartCustomer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueDiscount",
                table: "CartCustomer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CartCustomer");

            migrationBuilder.DropColumn(
                name: "VoucherUsed",
                table: "CartCustomer");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "CartCustomer");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CartCustomer");

            migrationBuilder.DropColumn(
                name: "TypeDiscount",
                table: "CartCustomer");

            migrationBuilder.DropColumn(
                name: "ValueDiscount",
                table: "CartCustomer");
        }
    }
}
