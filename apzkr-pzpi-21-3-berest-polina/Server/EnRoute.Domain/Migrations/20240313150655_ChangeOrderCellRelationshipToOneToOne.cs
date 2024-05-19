using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnRoute.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderCellRelationshipToOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_AssignedCellId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedCellId",
                table: "Orders",
                column: "AssignedCellId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_AssignedCellId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AssignedCellId",
                table: "Orders",
                column: "AssignedCellId");
        }
    }
}
