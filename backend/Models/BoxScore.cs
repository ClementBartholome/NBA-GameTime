public class BoxScore
{
    public Game Game { get; set; }
    public List<PlayerStats> PlayerGames { get; set; }

    public BoxScore() {
        Game = new Game();
        PlayerGames = new List<PlayerStats>();
    }
}
