using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedRelationshipBetweenOrdersAndDispatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispatches_Products_ProductId",
                table: "Dispatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DispatchItems_Orders_SourceOrder",
                table: "DispatchItems");

            migrationBuilder.DropIndex(
                name: "IX_DispatchItems_SourceOrder",
                table: "DispatchItems");

            migrationBuilder.DropIndex(
                name: "IX_Dispatches_ProductId",
                table: "Dispatches");

            migrationBuilder.DropColumn(
                name: "SourceOrder",
                table: "DispatchItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Dispatches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceOrder",
                table: "DispatchItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Dispatches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DispatchItems_SourceOrder",
                table: "DispatchItems",
                column: "SourceOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_ProductId",
                table: "Dispatches",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatches_Products_ProductId",
                table: "Dispatches",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DispatchItems_Orders_SourceOrder",
                table: "DispatchItems",
                column: "SourceOrder",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
