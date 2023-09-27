using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Team
{


    public int id { get; set; }

    public string logo { get; set; }
    public string primaryColor { get; set; }
    public string teamKey { get; set; }
    public string teamName { get; set; }

    public Team()
    {
        logo = "";
        primaryColor = "";
        teamKey = "";
        teamName = "";
    }
}
