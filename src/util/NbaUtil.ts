export function getSingleBoxScore(gamesData: any, homeTeamName: string, awayTeamName: string) {
  // Utilisez la méthode filter pour filtrer les jeux en fonction du nom des équipes à domicile et à l'extérieur.
  const filteredGames = gamesData.filter((game: any) => {
    // console.log(game)
    return (
      (game.Game.HomeTeam === homeTeamName && game.Game.AwayTeam === awayTeamName) ||
      (game.Game.HomeTeam === awayTeamName && game.Game.AwayTeam === homeTeamName)
    )
  })

  return filteredGames
}
