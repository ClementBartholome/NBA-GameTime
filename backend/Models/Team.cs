using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Team
{
    [Key] 
    public string TeamName { get; set; }
    public int Id { get; set; }
    public string Logo { get; set; }
    public string PrimaryColor { get; set; }
    public string TeamKey { get; set; }

    public Team()
    {
        Logo = "";
        PrimaryColor = "";
        TeamKey = "";
        TeamName = "";
    }
}
