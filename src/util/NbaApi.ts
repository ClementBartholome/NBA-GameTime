import axios from 'axios'

export async function getTeamLogo(teamName: string) {
  // Vérifiez d'abord si le logo est déjà présent dans le localStorage
  const storedLogo = localStorage.getItem(`${teamName}_logo`)

  if (storedLogo) {
    return storedLogo
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/teams?key=c8d641b0d5f54e6ba908f0066da32747`
      // 1000 calls per month
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Recherchez l'équipe par son nom dans la réponse
      const team = response.data.find((t: any) => t.Name === teamName)

      if (team) {
        // Stockez le logo dans le localStorage pour une utilisation future
        localStorage.setItem(`${teamName}_logo`, team.WikipediaLogoUrl)
        return team.WikipediaLogoUrl
      }
    }
  } catch (error) {
    console.error("Erreur lors de la récupération du logo de l'équipe :", error)
  }

  // if no logo found, return default logo
  return 'https://www.1min30.com/wp-content/uploads/2018/03/logo-NBA.jpg'
}
