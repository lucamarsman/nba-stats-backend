﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using nba_stats_api.Models;

#nullable disable

namespace nba_stats_api.Migrations
{
    [DbContext(typeof(NBAStatsDbContext))]
    [Migration("20241204004132_NullableBoxscoreFields")]
    partial class NullableBoxscoreFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Boxscore", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "GAME_ID");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "PLAYER_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("assists")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "AST");

                    b.Property<int?>("blocks")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "BLK");

                    b.Property<double?>("fieldGoalPercentage")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "FG_PCT");

                    b.Property<int?>("fieldGoalsAttempted")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "FGA");

                    b.Property<int?>("fieldGoalsMade")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "FGM");

                    b.Property<string>("gameComment")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "COMMENT");

                    b.Property<string>("minutes")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "MIN");

                    b.Property<string>("playerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "PLAYER_NAME");

                    b.Property<double?>("plusMinus")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "PLUS_MINUS");

                    b.Property<double?>("points")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "PTS");

                    b.Property<string>("position")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "START_POSITION");

                    b.Property<int?>("rebounds")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "REB");

                    b.Property<int?>("steals")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "STL");

                    b.Property<double?>("threePointerPercentage")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3_PCT");

                    b.Property<int?>("threePointersAttempted")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3A");

                    b.Property<int?>("threePointersMade")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3M");

                    b.Property<int?>("turnovers")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "TO");

                    b.HasKey("GameId", "PlayerId");

                    b.ToTable("Boxscores");
                });

            modelBuilder.Entity("Game", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<string>("MatchUp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Season")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<double?>("AssistsPerGame")
                        .HasColumnType("float");

                    b.Property<string>("BirthDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DraftNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DraftRound")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DraftYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsGreatest75")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsNBAPlayer")
                        .HasColumnType("bit");

                    b.Property<string>("JerseyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastAffiliation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("PlayerImpactEstimate")
                        .HasColumnType("float");

                    b.Property<double?>("PointsPerGame")
                        .HasColumnType("float");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ReboundsPerGame")
                        .HasColumnType("float");

                    b.Property<int?>("SeasonExperience")
                        .HasColumnType("int");

                    b.Property<string>("TeamAbbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("allDefensive")
                        .HasColumnType("int");

                    b.Property<int>("allNBA")
                        .HasColumnType("int");

                    b.Property<int>("allStar")
                        .HasColumnType("int");

                    b.Property<int>("champion")
                        .HasColumnType("int");

                    b.Property<int>("dpoy")
                        .HasColumnType("int");

                    b.Property<int>("fmvp")
                        .HasColumnType("int");

                    b.Property<int>("mvp")
                        .HasColumnType("int");

                    b.Property<int>("olympicBronze")
                        .HasColumnType("int");

                    b.Property<int>("olympicGold")
                        .HasColumnType("int");

                    b.Property<int>("olympicSilver")
                        .HasColumnType("int");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PlayerStat", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "PLAYER_ID");

                    b.Property<string>("PerMode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Season")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Assists")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "AST");

                    b.Property<float>("Blocks")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "BLK");

                    b.Property<float>("DefensiveRebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "DREB");

                    b.Property<float>("FieldGoalPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG_PCT");

                    b.Property<float>("FieldGoalsAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FGA");

                    b.Property<float>("FieldGoalsMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FGM");

                    b.Property<float>("FreeThrowPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FT_PCT");

                    b.Property<float>("FreeThrowsAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FTA");

                    b.Property<float>("FreeThrowsMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FTM");

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "GP");

                    b.Property<float>("Minutes")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "MIN");

                    b.Property<float>("OffensiveRebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "OREB");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "PLAYER_NAME");

                    b.Property<float>("PlusMinus")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "PLUS_MINUS");

                    b.Property<float>("Points")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "PTS");

                    b.Property<float>("Rebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "REB");

                    b.Property<string>("SeasonType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Steals")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "STL");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "TEAM_ID");

                    b.Property<float>("ThreePointerPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3_PCT");

                    b.Property<float>("ThreePointersAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3A");

                    b.Property<float>("ThreePointersMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3M");

                    b.Property<float>("Turnovers")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "TOV");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PlayerId", "PerMode", "Season");

                    b.ToTable("PlayerStats");
                });

            modelBuilder.Entity("TeamStat", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "TEAM_ID");

                    b.Property<string>("PerMode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Season")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Assists")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "AST");

                    b.Property<float>("Blocks")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "BLK");

                    b.Property<float>("DefensiveRebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "DREB");

                    b.Property<float>("FieldGoalPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG_PCT");

                    b.Property<float>("FieldGoalsAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FGA");

                    b.Property<float>("FieldGoalsMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FGM");

                    b.Property<float>("FreeThrowPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FT_PCT");

                    b.Property<float>("FreeThrowsAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FTA");

                    b.Property<float>("FreeThrowsMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FTM");

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "GP");

                    b.Property<float>("Minutes")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "MIN");

                    b.Property<float>("OffensiveRebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "OREB");

                    b.Property<float>("PlusMinus")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "PLUS_MINUS");

                    b.Property<float>("Points")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "PTS");

                    b.Property<float>("Rebounds")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "REB");

                    b.Property<string>("SeasonType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Steals")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "STL");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "TEAM_NAME");

                    b.Property<float>("ThreePointerPercentage")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3_PCT");

                    b.Property<float>("ThreePointersAttempted")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3A");

                    b.Property<float>("ThreePointersMade")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "FG3M");

                    b.Property<float>("Turnovers")
                        .HasColumnType("real")
                        .HasAnnotation("Relational:JsonPropertyName", "TOV");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TeamId", "PerMode", "Season");

                    b.ToTable("TeamStats");
                });

            modelBuilder.Entity("nba_stats_api.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("YearFounded")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
