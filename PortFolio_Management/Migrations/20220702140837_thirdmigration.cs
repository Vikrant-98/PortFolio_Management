using Microsoft.EntityFrameworkCore.Migrations;

namespace PortFolio_Management.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StocksPrice",
                table: "CustomerStocks");

            migrationBuilder.DropColumn(
                name: "MutualFundPrice",
                table: "CustomerMutualFunds");

            migrationBuilder.AddColumn<int>(
                name: "StocksQuantity",
                table: "CustomerStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MutualFundQuantity",
                table: "CustomerMutualFunds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StocksQuantity",
                table: "CustomerStocks");

            migrationBuilder.DropColumn(
                name: "MutualFundQuantity",
                table: "CustomerMutualFunds");

            migrationBuilder.AddColumn<double>(
                name: "StocksPrice",
                table: "CustomerStocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MutualFundPrice",
                table: "CustomerMutualFunds",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
