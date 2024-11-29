using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_stats_api.Migrations
{
    /// <inheritdoc />
    public partial class AddAwardColumnsToPlayerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "allDefensive",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "allNBA",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "allStar",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "champion",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dpoy",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fmvp",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "mvp",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "olympicBronze",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "olympicGold",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "olympicSilver",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allDefensive",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "allNBA",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "allStar",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "champion",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "dpoy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "fmvp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "mvp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "olympicBronze",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "olympicGold",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "olympicSilver",
                table: "Players");
        }
    }
}
