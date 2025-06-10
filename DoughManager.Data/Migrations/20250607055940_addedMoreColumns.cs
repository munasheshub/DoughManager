using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedMoreColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AfternoonShiftClosedTime",
                table: "StockTakeStatus",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AfternoonShiftOpenTime",
                table: "StockTakeStatus",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MorningShiftClosedTime",
                table: "StockTakeStatus",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MorningShiftOpenTime",
                table: "StockTakeStatus",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonShiftClosedTime",
                table: "StockTakeStatus");

            migrationBuilder.DropColumn(
                name: "AfternoonShiftOpenTime",
                table: "StockTakeStatus");

            migrationBuilder.DropColumn(
                name: "MorningShiftClosedTime",
                table: "StockTakeStatus");

            migrationBuilder.DropColumn(
                name: "MorningShiftOpenTime",
                table: "StockTakeStatus");
        }
    }
}
