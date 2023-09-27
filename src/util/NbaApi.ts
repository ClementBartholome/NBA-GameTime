import axios from 'axios'

const apiKey = 'c8d641b0d5f54e6ba908f0066da32747'

/* GAMES DATA
 **
 **
 */

export async function fetchAndSaveGames(currentDate: string) {
  const existingGames = await getGamesFromDb(currentDate)

  if (existingGames.length > 0) {
    // If games already exist in the database, return them
    return existingGames
  }

  const response = await axios.get('https://www.balldontlie.io/api/v1/games', {
    params: {
      start_date: currentDate,
      end_date: currentDate
    }
  })

  const games = response.data.data as any[]

  if (games === undefined || games.length === 0) {
    return []
  }

  const transformedGamesData = games.map((game) => ({
    gameID: game.id,
    home_team_name: game.home_team.name,
    home_team_full_name: game.home_team.full_name,
    visitor_team_name: game.visitor_team.name,
    visitor_team_full_name: game.visitor_team.full_name,
    home_team_score: game.home_team_score,
    visitor_team_score: game.visitor_team_score,
    gameDate: currentDate
  }))

  // Send the data to the backend
  await axios.post('http://localhost:5068/api/games', transformedGamesData, {
    headers: {
      'Content-Type': 'application/json'
    }
  })

  return transformedGamesData
}

export async function getGamesFromDb(currentDate: string) {
  try {
    const response = await axios.get(
      `http://localhost:5068/api/games/byDate?gameDate=${currentDate}`
    )

    const games = response.data as any[]

    return games
  } catch (error) {
    console.error('Error fetching NBA games:', error)
    // Handle the error or return an empty array if needed
    return []
  }
}

/* TEAMS DATA
 **
 **
 */

export async function fetchAndSaveTeamInfo(teamName: string) {
  const existingTeamInfo = await getTeamInfoFromDb(teamName)

  if (existingTeamInfo.teamName !== 'Undefined') {
    // If team information already exists in the database, return it
    return existingTeamInfo
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/teams?key=${apiKey}`
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      const team = response.data.find((t: any) => t.Name === teamName)
      console.log(teamName)
      console.log(team)

      if (team) {
        const teamInfo = {
          logo: team.WikipediaLogoUrl,
          primaryColor: team.PrimaryColor,
          teamKey: team.Key,
          teamName: team.Name
        }

        // Send the data to the backend
        await axios.post('http://localhost:5068/api/teams', teamInfo, {
          headers: {
            'Content-Type': 'application/json'
          }
        })
        console.log(teamInfo)
        return teamInfo
      }
    }
  } catch (error) {
    console.error('Error fetching team information:', error)
  }

  // If no team information is found or an error occurs, return default values
  return {
    logo: 'https://www.1min30.com/wp-content/uploads/2018/03/logo-NBA.jpg',
    primaryColor: 'ffffff',
    teamName: 'Undefined'
  }
}

export async function getTeamInfoFromDb(teamName: string) {
  try {
    const response = await axios.get(`http://localhost:5068/api/teams/${teamName}`)

    if (response.data) {
      return response.data
    }
  } catch (error) {
    console.error('Error fetching team information from DB:', error)
  }

  // If no team information is found or an error occurs, return default values
  return {
    logo: 'https://www.1min30.com/wp-content/uploads/2018/03/logo-NBA.jpg',
    primaryColor: 'ffffff',
    teamName: 'Undefined'
  }
}

/* STANDING DATA
 **
 **
 */

export async function getStandings(season: number) {
  const existingStandings = await getStandingsFromDb(season)

  if (existingStandings.length > 0) {
    // If standings already exist in the database, return them
    return existingStandings
  }

  try {
    const response = await axios.get(
      `https://api.sportsdata.io/v3/nba/scores/json/Standings/${season}?key=${apiKey}`
    )

    if (Array.isArray(response.data) && response.data.length > 0) {
      // Map the response data to only include the necessary fields
      const standings = response.data.map((team: any) => ({
        Season: team.Season,
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

      // Send the data to the backend
      await axios.post('http://localhost:5068/api/standings', standings, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      return standings
    }
  } catch (error) {
    console.error('Error fetching NBA standings:', error)
  }

  // Return an empty array if no standings data is found or an error occurs
  return []
}

export async function getStandingsFromDb(season: number) {
  try {
    const response = await axios.get(
      `http://localhost:5068/api/standings/bySeason?season=${season}`
    )

    const standings = response.data as any[]
    console.log(standings)
    return standings
  } catch (error) {
    console.error('Error fetching NBA standings:', error)
    // Handle the error or return an empty array if needed
    return []
  }
}

/* BOX SCORES DATA
 **
 **
 */

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
