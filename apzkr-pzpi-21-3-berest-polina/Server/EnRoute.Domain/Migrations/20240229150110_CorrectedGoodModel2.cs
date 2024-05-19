using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnRoute.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedGoodModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStoсk",
                table: "Goods",
                newName: "AmountInStock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountInStock",
                table: "Goods",
                newName: "AmountInStoсk");
        }
    }
}
