using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_stats_api.Migrations
{
    /// <inheritdoc />
    public partial class MakeSeasonCompKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamStats",
                table: "TeamStats");

            migrationBuilder.AlterColumn<string>(
                name: "Season",
                table: "TeamStats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamStats",
                table: "TeamStats",
                columns: new[] { "TeamId", "PerMode", "Season" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamStats",
                table: "TeamStats");

            migrationBuilder.AlterColumn<string>(
                name: "Season",
                table: "TeamStats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamStats",
                table: "TeamStats",
                columns: new[] { "TeamId", "PerMode" });
        }
    }
}
