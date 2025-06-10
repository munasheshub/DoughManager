using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedProductBatchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ClosingGas",
                table: "ProductBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ClosingZesaUnits",
                table: "ProductBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ProductBatches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OpeningGas",
                table: "ProductBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OpeningZesaUnits",
                table: "ProductBatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionProcesses",
                table: "ProductBatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ProductBatches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingGas",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "ClosingZesaUnits",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "OpeningGas",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "OpeningZesaUnits",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "ProductionProcesses",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ProductBatches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductBatches");
        }
    }
}
