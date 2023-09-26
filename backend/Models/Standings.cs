using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Standings
{


    public int id { get; set; }

    public string City { get; set; }
    public string Name { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal Percentage { get; set; }
    public int LastTenWins { get; set; }
    public int LastTenLosses { get; set; }
    public string Streak { get; set; }

    public Standings() {
        City = "";
        Name = "";
        Wins = 0;
        Losses = 0;
        Percentage = 0;
        LastTenWins = 0;
        LastTenLosses = 0;
        Streak = "";
    }
}
