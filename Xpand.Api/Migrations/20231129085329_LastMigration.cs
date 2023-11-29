using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xpand.Api.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Captains",
                keyColumn: "Id",
                keyValue: 1,
                column: "TeamId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShuttleId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Captains",
                keyColumn: "Id",
                keyValue: 1,
                column: "TeamId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShuttleId",
                value: null);
        }
    }
}
