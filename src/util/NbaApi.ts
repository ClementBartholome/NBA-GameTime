import axios from 'axios'
import config from '@/config/config'

const apiKey = config.apiKey

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

  const fetchedGames = response.data.data as any[]
  console.log(fetchedGames)

  if (fetchedGames === undefined || fetchedGames.length === 0) {
    return []
  }

  const transformedGamesData = fetchedGames.map((game) => ({
    gameId: game.id,
    homeTeamName: game.home_team.name,
    homeTeamFullName: game.home_team.full_name,
    visitorTeamName: game.visitor_team.name,
    visitorTeamFullName: game.visitor_team.full_name,
    homeTeamScore: game.home_team_score,
    visitorTeamScore: game.visitor_team_score,
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
      const fetchedTeam = response.data.find((t: any) => t.Name === teamName)
      console.log(teamName)
      console.log(fetchedTeam)

      if (fetchedTeam) {
        const teamInfo = {
          logo: fetchedTeam.WikipediaLogoUrl,
          primaryColor: fetchedTeam.PrimaryColor,
          teamKey: fetchedTeam.Key,
          teamName: fetchedTeam.Name
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

export async function fetchAndSaveStandings(season: number) {
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
    return standings
  } catch (error) {
    console.error('Error fetching NBA standings:', error)
    return []
  }
}

/*
 ** PLAYER DATA
 **
 */

export async function fetchAndSavePlayerInfo(playerId: number) {
  const existingPlayer = await getPlayerInfoFromDb(playerId)

  if (existingPlayer.id) {
    return existingPlayer
  }
  try {
    const response = await axios.get(`https://www.balldontlie.io/api/v1/players/${playerId}`)
    const fetchedPlayer = response.data
    const playerPhoto = await getPlayerPhoto(fetchedPlayer.last_name, fetchedPlayer.first_name)
    const playerInfo = {
      playerId: fetchedPlayer.id,
      firstName: fetchedPlayer.first_name,
      lastName: fetchedPlayer.last_name,
      position: fetchedPlayer.position,
      teamName: fetchedPlayer.team.name,
      playerPhoto: playerPhoto
    }
    await axios.post('http://localhost:5068/api/players', playerInfo, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    return playerInfo
  } catch (error) {
    console.error('Error fetching player info:', error)
  }
  return []
}

export async function getPlayerPhoto(playerLastName: string, playerFirstName: string) {
  try {
    const storedInfo = localStorage.getItem('players_full_list')

    if (storedInfo) {
      const playersData = JSON.parse(storedInfo)
      const fetchedPlayer = playersData.find(
        (p: any) => p.LastName === playerLastName && p.FirstName === playerFirstName
      )

      if (fetchedPlayer) {
        return fetchedPlayer.PhotoUrl || 'https://us.v-cdn.net/6022045/uploads/defaultavatar.png'
      }
    } else {
      const response = await axios.get(
        `https://api.sportsdata.io/v3/nba/scores/json/Players?key=${apiKey}`
      )

      localStorage.setItem('players_full_list', JSON.stringify(response.data))

      if (Array.isArray(response.data) && response.data.length > 0) {
        const fetchedPlayer = response.data.find(
          (p: any) => p.LastName === playerLastName && p.FirstName === playerFirstName
        )

        if (fetchedPlayer) {
          return fetchedPlayer.PhotoUrl || 'https://us.v-cdn.net/6022045/uploads/defaultavatar.png'
        }
      }
    }

    // If no player photo is found, return the generic URL
    return 'https://us.v-cdn.net/6022045/uploads/defaultavatar.png'
  } catch (error) {
    console.error('Error fetching player photo:', error)
    // If an error occurs, return the generic URL
    return 'https://us.v-cdn.net/6022045/uploads/defaultavatar.png'
  }
}

export async function getPlayerInfoFromDb(playerId: number) {
  try {
    const response = await axios.get(`http://localhost:5068/api/players/byId?playerid=${playerId}`)
    const player = response.data
    return player
  } catch (error) {
    console.error('Player is not yet added to database:', error)
    return []
  }
}

export async function fetchAndSavePlayerSeasonAvg(playerId: number, season: number) {
  const existingStats = await getPlayerSeasonAvgFromDb(playerId, season)
  if (existingStats.id) {
    return existingStats
  }
  try {
    const response = await axios.get(
      `https://www.balldontlie.io/api/v1/season_averages?player_ids[]=${playerId}&season=${season}`
    )
    const fetchedStats = response.data
    const playerSeasonAvg = {
      playerId: fetchedStats.data[0].player_id,
      season: fetchedStats.data[0].season,
      gamesPlayed: fetchedStats.data[0].games_played,
      minutes: parseFloat(fetchedStats.data[0].min),
      points: fetchedStats.data[0].pts,
      rebounds: fetchedStats.data[0].reb,
      offensiveRebounds: fetchedStats.data[0].oreb,
      defensiveRebounds: fetchedStats.data[0].dreb,
      assists: fetchedStats.data[0].ast,
      blocks: fetchedStats.data[0].blk,
      steals: fetchedStats.data[0].stl,
      turnovers: fetchedStats.data[0].turnover,
      fieldGoalsAttempted: fetchedStats.data[0].fga,
      fieldGoalsMade: fetchedStats.data[0].fgm,
      fieldGoalsPercentage: fetchedStats.data[0].fg_pct,
      threePointersAttempted: fetchedStats.data[0].fg3a,
      threePointersMade: fetchedStats.data[0].fg3m,
      threePointersPercentage: fetchedStats.data[0].fg3_pct,
      freeThrowsAttempted: fetchedStats.data[0].fta,
      freeThrowsMade: fetchedStats.data[0].ftm,
      freeThrowsPercentage: fetchedStats.data[0].ft_pct,
      fouls: fetchedStats.data[0].pf
    }
    await axios.post('http://localhost:5068/api/playerseasonaverages', playerSeasonAvg, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    return playerSeasonAvg
  } catch (error) {
    console.error('Player season averages not yet added', error)
    return []
  }
}

export async function getPlayerSeasonAvgFromDb(playerId: number, season: number) {
  try {
    const response = await axios.get(
      `http://localhost:5068/api/playerseasonaverages/byId?playerid=${playerId}&season=${season}`
    )
    const stats = response.data
    return stats
  } catch (error) {
    console.error('Error fetching player season averages:', error)
    return []
  }
}

/* BOX SCORES DATA
 **
 **
 */

export async function getBoxScoresByDate(currentDate: string) {
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

export async function getBoxScoreByGameId(gameID: any) {
  try {
    const storedInfo = localStorage.getItem(`boxscore_${gameID}`)

    if (storedInfo) {
      return JSON.parse(storedInfo)
    } else {
      const response = await axios.get(
        `https://www.balldontlie.io/api/v1/stats?game_ids[]=${gameID}&per_page=40`
      )

      if (Array.isArray(response.data.data) && response.data.data.length > 0) {
        const boxScore = response.data.data
        // console.log(boxScore)
        localStorage.setItem(`boxscore_${gameID}`, JSON.stringify(boxScore))
        return boxScore
      }
    }
  } catch (error) {
    console.error('Error fetching NBA box scores:', error)
    return []
  }
}
