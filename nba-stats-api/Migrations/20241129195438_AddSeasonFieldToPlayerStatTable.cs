using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_stats_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonFieldToPlayerStatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "PlayerStats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Season",
                table: "PlayerStats");
        }
    }
}
