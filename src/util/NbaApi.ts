import axios from 'axios'

interface TeamInfo {
  logo: string
  primaryColor: string
}

// Get team information by name
export async function getTeamInfo(teamName: string) {
  // Check if team info is already stored in localStorage
  const storedInfo = localStorage.getItem(`${teamName}_info`)

  if (storedInfo) {
    // If found, parse and return the stored info
    return JSON.parse(storedInfo) as TeamInfo
  }

  try {
    // Fetch team data from the API
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/teams?key=c8d641b0d5f54e6ba908f0066da32747`
      // 1000 calls per month limit
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Find the team by name in the API response
      const team = response.data.find((t: any) => t.Name === teamName)

      if (team) {
        // Create a TeamInfo object with logo and primaryColor
        const teamInfo: TeamInfo = {
          logo: team.WikipediaLogoUrl,
          primaryColor: team.PrimaryColor
        }

        // Store the team info in localStorage
        localStorage.setItem(`${teamName}_info`, JSON.stringify(teamInfo))

        // Return the team info
        return teamInfo
      }
    }
  } catch (error) {
    console.error('Error fetching team information:', error)
  }

  // Return default values if no data is found
  return {
    logo: 'https://www.1min30.com/wp-content/uploads/2018/03/logo-NBA.jpg',
    primaryColor: 'ffffff'
  }
}

// Function to get NBA standings
export async function getStandings(season: number) {
  // Check if standings data is already stored in localStorage
  const storedInfo = localStorage.getItem(`standings_${season}`)

  if (storedInfo) {
    // If found, parse and return the stored standings data
    return JSON.parse(storedInfo) as TeamInfo[]
  }

  try {
    // Fetch NBA standings data from the API for the specified season
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/Standings/${season}?key=c8d641b0d5f54e6ba908f0066da32747`
      // Note: 1000 calls per month limit
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Store the standings data in localStorage for future use
      const standings = response.data
      localStorage.setItem(`standings_${season}`, JSON.stringify(standings))

      // Return the NBA standings data
      return standings
    }
  } catch (error) {
    console.error('Error fetching NBA standings:', error)
  }

  // Return an empty array if no standings data is found
  return []
}
