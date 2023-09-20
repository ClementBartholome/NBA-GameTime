import axios from 'axios'

interface TeamInfo {
  logo: string
  primaryColor: string
}

export async function getTeamInfo(teamName: string) {
  const storedInfo = localStorage.getItem(`${teamName}_info`)

  if (storedInfo) {
    return JSON.parse(storedInfo)
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/teams?key=c8d641b0d5f54e6ba908f0066da32747`
      // 1000 calls per month
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      const team = response.data.find((t: any) => t.Name === teamName)

      if (team) {
        const teamInfo: TeamInfo = {
          logo: team.WikipediaLogoUrl,
          primaryColor: team.PrimaryColor
        }
        localStorage.setItem(`${teamName}_info`, JSON.stringify(teamInfo))
        return teamInfo
      }
    }
  } catch (error) {
    console.error("Erreur lors de la récupération des informations de l'équipe :", error)
  }

  return {
    logo: 'https://www.1min30.com/wp-content/uploads/2018/03/logo-NBA.jpg',
    primaryColor: 'ffffff'
  }
}

export async function getStandings() {
  const storedInfo = localStorage.getItem(`standings`)

  if (storedInfo) {
    return JSON.parse(storedInfo)
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/Standings/2023?key=c8d641b0d5f54e6ba908f0066da32747`
      // 1000 calls per month
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      const standings = response.data
      localStorage.setItem(`standings`, JSON.stringify(standings))
      return standings
    }
  } catch (error) {
    console.error("Erreur lors de la récupération des informations de l'équipe :", error)
  }

  return []
}
