using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Game
{
    public int Id { get; set; }
    public int GameId { get; set;}
    public string HomeTeamName { get; set; }
    public string HomeTeamFullName { get; set; }
    public string VisitorTeamName { get; set; }
    public string VisitorTeamFullName { get; set; }
    public int HomeTeamScore { get; set; }
    public int VisitorTeamScore { get; set; }
    public string GameDate { get; set; }

    public Game() {
        GameId = 0;
        HomeTeamName = "";
        HomeTeamFullName = "";
        VisitorTeamName = "";
        VisitorTeamFullName = "";
        HomeTeamScore = 0;
        VisitorTeamScore = 0;
        GameDate = "";
    }
}

