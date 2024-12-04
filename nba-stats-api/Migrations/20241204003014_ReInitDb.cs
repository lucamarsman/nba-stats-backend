using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_stats_api.Migrations
{
    /// <inheritdoc />
    public partial class ReInitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxscores",
                columns: table => new
                {
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAffiliation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftRound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DraftNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonExperience = table.Column<int>(type: "int", nullable: true),
                    PointsPerGame = table.Column<double>(type: "float", nullable: true),
                    ReboundsPerGame = table.Column<double>(type: "float", nullable: true),
                    AssistsPerGame = table.Column<double>(type: "float", nullable: true),
                    PlayerImpactEstimate = table.Column<double>(type: "float", nullable: true),
                    JerseyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsNBAPlayer = table.Column<bool>(type: "bit", nullable: true),
                    IsGreatest75 = table.Column<bool>(type: "bit", nullable: true),
                    allDefensive = table.Column<int>(type: "int", nullable: false),
                    dpoy = table.Column<int>(type: "int", nullable: false),
                    allNBA = table.Column<int>(type: "int", nullable: false),
                    allStar = table.Column<int>(type: "int", nullable: false),
                    mvp = table.Column<int>(type: "int", nullable: false),
                    fmvp = table.Column<int>(type: "int", nullable: false),
                    champion = table.Column<int>(type: "int", nullable: false),
                    olympicBronze = table.Column<int>(type: "int", nullable: false),
                    olympicSilver = table.Column<int>(type: "int", nullable: false),
                    olympicGold = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PerMode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    SeasonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    Assists = table.Column<float>(type: "real", nullable: false),
                    Rebounds = table.Column<float>(type: "real", nullable: false),
                    DefensiveRebounds = table.Column<float>(type: "real", nullable: false),
                    OffensiveRebounds = table.Column<float>(type: "real", nullable: false),
                    FreeThrowsAttempted = table.Column<float>(type: "real", nullable: false),
                    FreeThrowsMade = table.Column<float>(type: "real", nullable: false),
                    FreeThrowPercentage = table.Column<float>(type: "real", nullable: false),
                    FieldGoalsAttempted = table.Column<float>(type: "real", nullable: false),
                    FieldGoalsMade = table.Column<float>(type: "real", nullable: false),
                    FieldGoalPercentage = table.Column<float>(type: "real", nullable: false),
                    ThreePointersAttempted = table.Column<float>(type: "real", nullable: false),
                    ThreePointersMade = table.Column<float>(type: "real", nullable: false),
                    ThreePointerPercentage = table.Column<float>(type: "real", nullable: false),
                    Steals = table.Column<float>(type: "real", nullable: false),
                    Blocks = table.Column<float>(type: "real", nullable: false),
                    Turnovers = table.Column<float>(type: "real", nullable: false),
                    Minutes = table.Column<float>(type: "real", nullable: false),
                    PlusMinus = table.Column<float>(type: "real", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => new { x.PlayerId, x.PerMode, x.Season });
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearFounded = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PerMode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<float>(type: "real", nullable: false),
                    Assists = table.Column<float>(type: "real", nullable: false),
                    Rebounds = table.Column<float>(type: "real", nullable: false),
                    DefensiveRebounds = table.Column<float>(type: "real", nullable: false),
                    OffensiveRebounds = table.Column<float>(type: "real", nullable: false),
                    FreeThrowsAttempted = table.Column<float>(type: "real", nullable: false),
                    FreeThrowsMade = table.Column<float>(type: "real", nullable: false),
                    FreeThrowPercentage = table.Column<float>(type: "real", nullable: false),
                    FieldGoalsAttempted = table.Column<float>(type: "real", nullable: false),
                    FieldGoalsMade = table.Column<float>(type: "real", nullable: false),
                    FieldGoalPercentage = table.Column<float>(type: "real", nullable: false),
                    ThreePointersAttempted = table.Column<float>(type: "real", nullable: false),
                    ThreePointersMade = table.Column<float>(type: "real", nullable: false),
                    ThreePointerPercentage = table.Column<float>(type: "real", nullable: false),
                    Steals = table.Column<float>(type: "real", nullable: false),
                    Blocks = table.Column<float>(type: "real", nullable: false),
                    Turnovers = table.Column<float>(type: "real", nullable: false),
                    Minutes = table.Column<float>(type: "real", nullable: false),
                    PlusMinus = table.Column<float>(type: "real", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => new { x.TeamId, x.PerMode, x.Season });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxscores");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TeamStats");
        }
    }
}
