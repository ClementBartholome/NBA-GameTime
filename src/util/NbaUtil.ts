export function getSingleBoxScore(
  gamesData: any,
  homeTeamName: string | string[],
  awayTeamName: string | string[]
) {
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

// Convert team abbreviations to match the ones used in the box score
export function convertTeamName(teamName: string) {
  switch (teamName) {
    case 'PHO':
      return 'PHX'
    case 'NY':
      return 'NYK'
    case 'GS':
      return 'GSW'
    case 'NO':
      return 'NOP'
    case 'SA':
      return 'SAS'
    default:
      return teamName
  }
}

export function convertPosition(position: string) {
  switch (position) {
    case 'G':
      return 'Guard'
    case 'F':
      return 'Forward'
    case 'C':
      return 'Center'
    case 'F-C':
      return 'Forward-Center'
    case 'C-F':
      return 'Center-Forward'
  }
}
