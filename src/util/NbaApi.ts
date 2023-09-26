import axios from 'axios'

interface TeamInfo {
  logo: string
  primaryColor: string
  teamName: string
}

const apiKey = 'c8d641b0d5f54e6ba908f0066da32747'

export async function fetchGames(currentDate: string) {
  try {
    const cachedData = localStorage.getItem(currentDate)

    if (cachedData) {
      return JSON.parse(cachedData)
    } else {
      const response = await axios.get('https://www.balldontlie.io/api/v1/games', {
        params: {
          start_date: currentDate,
          end_date: currentDate
        }
      })

      const games = response.data.data as any[]

      // Store fetched data in local cache
      const simplifiedGames = games.map((game) => ({
        id: game.id,
        home_team: {
          name: game.home_team.name,
          full_name: game.home_team.full_name
        },
        visitor_team: {
          name: game.visitor_team.name,
          full_name: game.visitor_team.full_name
        },
        home_team_score: game.home_team_score,
        visitor_team_score: game.visitor_team_score
      }))

      localStorage.setItem(currentDate, JSON.stringify(simplifiedGames))
      console.log(simplifiedGames)
      return simplifiedGames
    }
  } catch (error) {
    console.error('Error fetching NBA games:', error)
    // Handle the error or return an empty array if needed
    return []
  }
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
      `https://api.sportsdata.io/v3/nba/scores/json/teams?key=${apiKey}`
      // 1000 calls per month limit
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Find the team by name in the API response
      const team = response.data.find((t: any) => t.Name === teamName)

      if (team) {
        // Create a TeamInfo object with logo and primaryColor
        const teamInfo: TeamInfo = {
          logo: team.WikipediaLogoUrl,
          primaryColor: team.PrimaryColor,
          teamName: team.Key
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
    primaryColor: 'ffffff',
    teamName: 'Undefined'
  }
}

// Function to get NBA standings
export async function getStandings(season: number) {
  // Check if standings data is already stored in localStorage
  const storedInfo = localStorage.getItem(`standings_${season}`)

  if (storedInfo) {
    // If found, parse and return the stored standings data
    return JSON.parse(storedInfo)
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/Standings/${season}?key=${apiKey}`
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Map the response data to only include the necessary fields
      const standings = response.data.map((team: any) => ({
        City: team.City,
        Name: team.Name,
        Wins: team.Wins,
        Losses: team.Losses,
        Percentage: team.Percentage,
        LastTenWins: team.LastTenWins,
        LastTenLosses: team.LastTenLosses,
        Streak: team.Streak
      }))
      console.log(standings)
      return standings
    }
  } catch (error) {
    console.error('Error fetching NBA standings:', error)
  }

  // Return an empty array if no standings data is found or an error occurs
  return []
}

export async function getBoxScores(currentDate: string) {
  try {
    const storedInfo = localStorage.getItem(`boxscores_${currentDate}`)

    if (storedInfo) {
      return JSON.parse(storedInfo)
    } else {
      const response = await axios.get(
        `https://api.sportsdata.io/v3/nba/stats/json/BoxScores/${currentDate}?key=${apiKey}`
        // Note: 1000 calls per month limit
      )

      if (Array.isArray(response.data) && response.data.length > 0) {
        const boxScores = response.data

        // Simplify the box scores data
        const simplifiedBoxScores = boxScores.map((boxScore) => ({
          Game: {
            HomeTeam: boxScore.Game.HomeTeam,
            AwayTeam: boxScore.Game.AwayTeam,
            GameID: boxScore.Game.GameID
          },
          PlayerGames: boxScore.PlayerGames.map((playerGame: any) => ({
            Name: playerGame.Name,
            Minutes: playerGame.Minutes,
            Points: playerGame.Points,
            Rebounds: playerGame.Rebounds,
            Assists: playerGame.Assists,
            HomeOrAway: playerGame.HomeOrAway,
            Started: playerGame.Started
          }))
        }))

        localStorage.setItem(`boxscores_${currentDate}`, JSON.stringify(simplifiedBoxScores))
        console.log(simplifiedBoxScores)
        return simplifiedBoxScores
      }
    }
  } catch (error) {
    console.error('Error fetching NBA box scores:', error)
  }

  // Return an empty array if no box scores data is found
  return []
}
