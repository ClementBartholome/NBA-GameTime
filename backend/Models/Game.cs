public class Game
{
    public int Id { get; set; }
    public Team HomeTeam { get; set; }
    public Team VisitorTeam { get; set; }
    public int HomeTeamScore { get; set; }
    public int VisitorTeamScore { get; set; }

    public Game() {
        Id = 0;
        HomeTeam = new Team();
        VisitorTeam = new Team();
        HomeTeamScore = 0;
        VisitorTeamScore = 0;
    }
}
