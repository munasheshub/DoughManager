using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoughManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class dropDiscrepancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscrepancyRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscrepancyRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    DeleterId = table.Column<int>(type: "int", nullable: true),
                    StaffOnDutyNavigationId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscrepancyAmount = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    StaffOnDuty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffOnDutyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscrepancyRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscrepancyRecords_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscrepancyRecords_Accounts_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscrepancyRecords_Accounts_StaffOnDutyNavigationId",
                        column: x => x.StaffOnDutyNavigationId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyRecords_CreatorId",
                table: "DiscrepancyRecords",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyRecords_DeleterId",
                table: "DiscrepancyRecords",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyRecords_StaffOnDutyNavigationId",
                table: "DiscrepancyRecords",
                column: "StaffOnDutyNavigationId");
        }
    }
}
