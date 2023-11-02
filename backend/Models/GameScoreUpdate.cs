using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class GameScoreUpdate
{
    public int HomeTeamScore { get; set; }
    public int VisitorTeamScore { get; set; }

    public GameScoreUpdate() {
        HomeTeamScore = 0;
        VisitorTeamScore = 0;
    }
}
