using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedStockTakeStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMorningShift",
                table: "StockTakeStatus",
                newName: "IsMorningShiftOpen");

            migrationBuilder.RenameColumn(
                name: "IsAfternoonShift",
                table: "StockTakeStatus",
                newName: "IsMorningShiftClosed");

            migrationBuilder.AddColumn<bool>(
                name: "IsAfternoonShiftClosed",
                table: "StockTakeStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAfternoonShiftOpen",
                table: "StockTakeStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAfternoonShiftClosed",
                table: "StockTakeStatus");

            migrationBuilder.DropColumn(
                name: "IsAfternoonShiftOpen",
                table: "StockTakeStatus");

            migrationBuilder.RenameColumn(
                name: "IsMorningShiftOpen",
                table: "StockTakeStatus",
                newName: "IsMorningShift");

            migrationBuilder.RenameColumn(
                name: "IsMorningShiftClosed",
                table: "StockTakeStatus",
                newName: "IsAfternoonShift");
        }
    }
}
