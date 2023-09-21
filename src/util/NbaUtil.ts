export function getSingleBoxScore(gamesData: any, homeTeamName: string, awayTeamName: string) {
  // Filter method to find the game that matches the home and away team names
  const filteredGames = gamesData.filter((game: any) => {
    // console.log(game)
    return (
      (game.Game.HomeTeam === homeTeamName && game.Game.AwayTeam === awayTeamName) ||
      (game.Game.HomeTeam === awayTeamName && game.Game.AwayTeam === homeTeamName)
    )
  })

  return filteredGames
}
