using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xpand.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Captains_Teams_TeamId",
                table: "Captains");

            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots");

            migrationBuilder.AddColumn<int>(
                name: "ShuttleId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Shuttles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "ShuttleId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ShuttleId",
                table: "Teams",
                column: "ShuttleId",
                unique: true,
                filter: "[ShuttleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Captains_Teams_TeamId",
                table: "Captains",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Shuttles_ShuttleId",
                table: "Teams",
                column: "ShuttleId",
                principalTable: "Shuttles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Captains_Teams_TeamId",
                table: "Captains");

            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Shuttles_ShuttleId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ShuttleId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ShuttleId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Shuttles");

            migrationBuilder.AddForeignKey(
                name: "FK_Captains_Teams_TeamId",
                table: "Captains",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
