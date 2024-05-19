using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnRoute.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountInStosk",
                table: "Goods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountInStosk",
                table: "Goods");
        }
    }
}
