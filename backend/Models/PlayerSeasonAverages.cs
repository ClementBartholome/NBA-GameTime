using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class PlayerSeasonAverages
{
    public int Id { get; set; }
    public int PlayerId { get; set; }

    public int Minutes { get; set; }
    public int Points { get; set; }
    public int Rebounds { get; set; }
    public int OffensiveRebounds { get; set; }
    public int DefensiveRebounds { get; set; }
    public int Assists { get; set; }
    public int Blocks { get; set; }
    public int Steals { get; set; }
    public int Turnovers { get; set; }
    public int FieldGoalsAttempted { get; set; }
    public int FieldGoalsMade { get; set; }
    public int FieldGoalsPercentage { get; set; }
    public int ThreePointersAttempted { get; set; }
    public int ThreePointersMade { get; set; }
    public int ThreePointersPercentage { get; set; }
    public int FreeThrowsAttempted { get; set; }
    public int FreeThrowsMade { get; set; }
    public int FreeThrowsPercentage { get; set; }
    public int Fouls { get; set; }

    public PlayerSeasonAverages() {
        PlayerId = 0;
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