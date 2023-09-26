// using System;
// using System.ComponentModel.DataAnnotations; 
// using System.ComponentModel.DataAnnotations.Schema; 

// public class BoxScore
// {
//     [Key]
//     public int Id { get; set; }

//     public Game Game { get; set; }
//     public List<PlayerStats> PlayerGames { get; set; }

//     public BoxScore()
//     {
//         Game = new Game();
//         PlayerGames = new List<PlayerStats>();
//     }
// }