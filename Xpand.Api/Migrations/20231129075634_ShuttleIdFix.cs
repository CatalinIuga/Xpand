using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xpand.Api.Migrations
{
    /// <inheritdoc />
    public partial class ShuttleIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Teams_TeamId",
                table: "Planets");

            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Shuttles_ShuttleId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ShuttleId",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Shuttles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Shuttles_TeamId",
                table: "Shuttles",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Teams_TeamId",
                table: "Planets",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Shuttles_Teams_TeamId",
                table: "Shuttles",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Teams_TeamId",
                table: "Planets");

            migrationBuilder.DropForeignKey(
                name: "FK_Robots_Teams_TeamId",
                table: "Robots");

            migrationBuilder.DropForeignKey(
                name: "FK_Shuttles_Teams_TeamId",
                table: "Shuttles");

            migrationBuilder.DropIndex(
                name: "IX_Shuttles_TeamId",
                table: "Shuttles");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Shuttles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ShuttleId",
                table: "Teams",
                column: "ShuttleId",
                unique: true,
                filter: "[ShuttleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Teams_TeamId",
                table: "Planets",
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
    }
}
