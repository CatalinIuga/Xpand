using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xpand.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Captains",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Yoda", "4213" });

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageName", "Name", "RobotsCount", "Status" },
                values: new object[] { "On this planet we live.", "earth.png", "Earth", 2, "OK" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Id", "Description", "ImageName", "Name", "RobotsCount", "Status", "TeamId" },
                values: new object[,]
                {
                    { 2, "This one planet we can live.", "mars.png", "Mars", 2, "OK", 1 },
                    { 3, "No description yet :/", "jupiter.png", "Jupiter", 0, "TODO", null },
                    { 4, "This one planet we can't live.", "saturn.png", "Saturn", 2, "NotOK", 1 },
                    { 5, "No description yet :/", "uranus.png", "Uranus", 0, "TODO", null },
                    { 6, "No description yet :/", "neptune.png", "Neptune", 0, "TODO", null },
                    { 7, "No description yet :/", "pluto.png", "Pluto", 0, "TODO", null },
                    { 8, "No description yet :/", "mercury.png", "Mercury", 0, "TODO", null },
                    { 9, "No description yet :/", "venus.png", "Venus", 0, "TODO", null }
                });

            migrationBuilder.InsertData(
                table: "Robots",
                columns: new[] { "Id", "Name", "TeamId" },
                values: new object[] { 2, "C3PO", 1 });

            migrationBuilder.InsertData(
                table: "Shuttles",
                columns: new[] { "Id", "Name", "TeamId" },
                values: new object[] { 1, "Lambda T-4a", 1 });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Team Yoda's");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Robots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shuttles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Captains",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Luke Skywalker", "No password yet :/" });

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageName", "Name", "RobotsCount", "Status" },
                values: new object[] { "No description yet :/", "default.png", "Luke Skywalker", 0, "TODO" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Lambda");
        }
    }
}
