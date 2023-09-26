using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema; 

public class Team
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public string FullName { get; set; }

    public Team()
    {
        Name = "";
        FullName = "";
    }
}
