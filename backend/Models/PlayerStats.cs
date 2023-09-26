public class PlayerStats
{
    public string Name { get; set; }
    public int Minutes { get; set; }
    public int Points { get; set; }
    public int Rebounds { get; set; }
    public int Assists { get; set; }
    public string HomeOrAway { get; set; }
    public int Started { get; set; }

    public PlayerStats() {
        Name = "";
        Minutes = 0;
        Points = 0;
        Rebounds = 0;
        Assists = 0;
        HomeOrAway = "";
        Started = 0;
    }
}
