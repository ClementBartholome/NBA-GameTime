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
