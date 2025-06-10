using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTake_Products_ProductId",
                table: "StockTake");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTake",
                table: "StockTake");

            migrationBuilder.RenameTable(
                name: "StockTake",
                newName: "StockTakes");

            migrationBuilder.RenameIndex(
                name: "IX_StockTake_ProductId",
                table: "StockTakes",
                newName: "IX_StockTakes_ProductId");

            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt9PM",
                table: "StockTakes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt2PM",
                table: "StockTakes",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTakes",
                table: "StockTakes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTakes_Products_ProductId",
                table: "StockTakes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTakes_Products_ProductId",
                table: "StockTakes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTakes",
                table: "StockTakes");

            migrationBuilder.RenameTable(
                name: "StockTakes",
                newName: "StockTake");

            migrationBuilder.RenameIndex(
                name: "IX_StockTakes_ProductId",
                table: "StockTake",
                newName: "IX_StockTake_ProductId");

            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt9PM",
                table: "StockTake",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "StockLevelAt2PM",
                table: "StockTake",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTake",
                table: "StockTake",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTake_Products_ProductId",
                table: "StockTake",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
