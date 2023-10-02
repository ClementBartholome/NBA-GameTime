using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

[Table("players_season_averages")]
public class PlayerSeasonAverages
{
    public int Id { get; set; }
    public int PlayerId { get; set; }

    public int Season { get; set; }
    public int GamesPlayed { get; set; }
    public decimal Minutes { get; set; }
    public decimal Points { get; set; }
    public decimal Rebounds { get; set; }
    public decimal OffensiveRebounds { get; set; }
    public decimal DefensiveRebounds { get; set; }
    public decimal Assists { get; set; }
    public decimal Blocks { get; set; }
    public decimal Steals { get; set; }
    public decimal Turnovers { get; set; }
    public decimal FieldGoalsAttempted { get; set; }
    public decimal FieldGoalsMade { get; set; }
    public decimal FieldGoalsPercentage { get; set; }
    public decimal ThreePointersAttempted { get; set; }
    public decimal ThreePointersMade { get; set; }
    public decimal ThreePointersPercentage { get; set; }
    public decimal FreeThrowsAttempted { get; set; }
    public decimal FreeThrowsMade { get; set; }
    public decimal FreeThrowsPercentage { get; set; }
    public decimal Fouls { get; set; }

    public PlayerSeasonAverages() {
        PlayerId = 0;
        Season = 0;
        GamesPlayed = 0;
        Minutes = 0;
        Points = 0;
        Rebounds = 0;
        OffensiveRebounds = 0;
        DefensiveRebounds = 0;
        Assists = 0;
        Blocks = 0;
        Steals = 0;
        Turnovers = 0;
        FieldGoalsAttempted = 0;
        FieldGoalsMade = 0;
        FieldGoalsPercentage = 0;
        ThreePointersAttempted = 0;
        ThreePointersMade = 0;
        ThreePointersPercentage = 0;
        FreeThrowsAttempted = 0;
        FreeThrowsMade = 0;
        FreeThrowsPercentage = 0;
        Fouls = 0;
    }
}