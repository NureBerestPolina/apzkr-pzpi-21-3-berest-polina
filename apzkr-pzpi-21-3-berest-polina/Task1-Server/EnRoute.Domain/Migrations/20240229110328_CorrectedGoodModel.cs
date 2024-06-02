using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnRoute.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedGoodModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStosk",
                table: "Goods",
                newName: "AmountInStoсk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStoсk",
                table: "Goods",
                newName: "AmountInStosk");
        }
    }
}
