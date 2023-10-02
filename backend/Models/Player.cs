using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Player 
{
    public int id { get; set; }
    public int PlayerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; }
    public string PlayerPhoto { get; set; }

    [ForeignKey("Team")]
    public string TeamName { get; set; }

    public Team? Team { get; set; }

    public Player() {
        PlayerId = 0;
        FirstName = "";
        LastName = "";
        Position = "";
        PlayerPhoto = "";
    }
}
