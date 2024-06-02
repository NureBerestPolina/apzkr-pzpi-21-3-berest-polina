using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnRoute.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedCounterInstallRequestFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CounterInstallationRequests_OrganizationId",
                table: "CounterInstallationRequests",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CounterInstallationRequests_Organizations_OrganizationId",
                table: "CounterInstallationRequests",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CounterInstallationRequests_Organizations_OrganizationId",
                table: "CounterInstallationRequests");

            migrationBuilder.DropIndex(
                name: "IX_CounterInstallationRequests_OrganizationId",
                table: "CounterInstallationRequests");
        }
    }
}
