using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_stats_api.Migrations
{
    /// <inheritdoc />
    public partial class AddBoxscoreModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxscores",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    playerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gameComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minutes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assists = table.Column<int>(type: "int", nullable: false),
                    rebounds = table.Column<int>(type: "int", nullable: false),
                    blocks = table.Column<int>(type: "int", nullable: false),
                    turnovers = table.Column<int>(type: "int", nullable: false),
                    points = table.Column<double>(type: "float", nullable: false),
                    steals = table.Column<int>(type: "int", nullable: false),
                    plusMinus = table.Column<int>(type: "int", nullable: false),
                    fieldGoalsAttempted = table.Column<int>(type: "int", nullable: false),
                    fieldGoalsMade = table.Column<int>(type: "int", nullable: false),
                    fieldGoalPercentage = table.Column<double>(type: "float", nullable: false),
                    threePointersAttempted = table.Column<int>(type: "int", nullable: false),
                    threePointersMade = table.Column<int>(type: "int", nullable: false),
                    threePointerPercentage = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxscores", x => new { x.GameId, x.PlayerId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxscores");
        }
    }
}
