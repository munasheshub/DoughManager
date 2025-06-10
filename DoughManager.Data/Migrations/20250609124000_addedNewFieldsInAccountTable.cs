using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedNewFieldsInAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetCode",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetCodeExpTime",
                table: "Accounts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetCode",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResetCodeExpTime",
                table: "Accounts");
        }
    }
}
