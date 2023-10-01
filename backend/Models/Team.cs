using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Team
{
    public int id { get; set; }
    public string Logo { get; set; }
    public string PrimaryColor { get; set; }
    public string TeamKey { get; set; }
    public string TeamName { get; set; }

    public Team()
    {
        Logo = "";
        PrimaryColor = "";
        TeamKey = "";
        TeamName = "";
    }
}
