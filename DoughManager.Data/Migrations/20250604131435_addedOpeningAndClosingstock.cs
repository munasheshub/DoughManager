using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedOpeningAndClosingstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt9PM",
                table: "StockTake",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt2PM",
                table: "StockTake",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "ClosingStock",
                table: "StockTake",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OpeningStock",
                table: "StockTake",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingStock",
                table: "StockTake");

            migrationBuilder.DropColumn(
                name: "OpeningStock",
                table: "StockTake");

            migrationBuilder.AlterColumn<int>(
                name: "StockLevelAt9PM",
                table: "StockTake",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "StockLevelAt2PM",
                table: "StockTake",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
