public class Game
{
    public int id { get; set; }
    public int gameID { get; set;}
    public string home_team_name { get; set; }
    public string home_team_full_name { get; set; }
    public string visitor_team_name { get; set; }
    public string visitor_team_full_name { get; set; }
    public int home_team_score { get; set; }
    public int visitor_team_score { get; set; }

    public Game() {
        id = 0;
        gameID = 0;
        home_team_name = "";
        home_team_full_name = "";
        visitor_team_name = "";
        visitor_team_full_name = "";
        home_team_score = 0;
        visitor_team_score = 0;
    }
}
