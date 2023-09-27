<template>
  <router-link :to="'/boxscore/' + gameID + `-` + homeTeamName + `-` + visitorTeamName">
    <div class="game-card" v-if="game">
      <div class="home-team" :style="{ backgroundColor: homeTeamColor }">
        <img :src="homeTeamLogo" alt="Home team logo" />
        <h2>{{ game.home_team_full_name }}</h2>
        <span
          class="score"
          v-if="game.home_team_score > 0"
          :class="{ winner: isHomeWinner(game.home_team_score, game.visitor_team_score) }"
          >{{ game.home_team_score }}</span
        >
      </div>
      <div class="away-team" :style="{ backgroundColor: visitorTeamColor }">
        <img :src="visitorTeamLogo" alt="Visiting team logo" />
        <h2>{{ game.visitor_team_full_name }}</h2>
        <span
          class="score"
          v-if="game.visitor_team_score > 0"
          :class="{ winner: isAwayWinner(game.home_team_score, game.visitor_team_score) }"
          >{{ game.visitor_team_score }}</span
        >
      </div>
    </div>
  </router-link>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'
import { fetchAndSaveTeamInfo } from '../util/NbaApi'
import { getSingleBoxScore } from '../util/NbaUtil'

export default {
  props: {
    game: Object,
    gamesBoxScores: Array
  },
  setup(props) {
    const homeTeamName = ref('')
    const visitorTeamName = ref('')
    const homeTeamLogo = ref('')
    const visitorTeamLogo = ref('')
    const homeTeamColor = ref('')
    const visitorTeamColor = ref('')

    const gameID = ref(0)

    onMounted(async () => {
      // console.log(props.game)
      // Check if game data and gamesBoxScores are available and not empty
      if (props.game && props.gamesBoxScores && props.gamesBoxScores.length > 0) {
        // Fetch home team information
        const homeTeamInfo = await fetchAndSaveTeamInfo(props.game.home_team_name)
        // Fetch visitor team information
        const visitorTeamInfo = await fetchAndSaveTeamInfo(props.game.visitor_team_name)

        // Set home team properties
        homeTeamName.value = homeTeamInfo.teamKey
        homeTeamLogo.value = homeTeamInfo.logo
        homeTeamColor.value = '#' + homeTeamInfo.primaryColor

        // Set visitor team properties
        visitorTeamName.value = visitorTeamInfo.teamKey
        visitorTeamLogo.value = visitorTeamInfo.logo
        visitorTeamColor.value = '#' + visitorTeamInfo.primaryColor

        // console.log(homeTeamName.value)
        // console.log(props.gamesBoxScores)

        // Get the single box score for the specified teams
        const boxScore = getSingleBoxScore(
          props.gamesBoxScores,
          homeTeamName.value,
          visitorTeamName.value
        )

        // Check if boxScore data is structured correctly
        if (boxScore && boxScore[0] && boxScore[0].Game && boxScore[0].Game.GameID) {
          gameID.value = boxScore[0].Game.GameID
          console.log(boxScore[0])
          console.log(gameID.value)
        } else {
          console.error('La structure des données de boxScore[0] est incorrecte.')
        }
      }
    })

    return {
      homeTeamLogo,
      visitorTeamLogo,
      homeTeamColor,
      visitorTeamColor,
      homeTeamName,
      visitorTeamName,
      gameID
    }
  },
  methods: {
    // Determine if the home team is the winner
    isHomeWinner(homeScore: number, awayScore: number) {
      return homeScore > awayScore
    },
    // Determine if the away team is the winner
    isAwayWinner(homeScore: number, awayScore: number) {
      return awayScore > homeScore
    }
  }
}
</script>

<style scoped>
.game-card {
  display: flex;
  justify-content: center;
  gap: 1.5rem;
  border: 1px solid #ccc;
  padding: 1rem;
  margin: 1rem;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  color: #ffffff;
  transition: transform 0.2s ease-in-out;
}

.game-card h2 {
  font-size: 1.3rem;
}

.game-card img {
  background-color: white;
  padding: 0.5rem;
  height: 100px;
  width: 130px;
  border-radius: 20px;
}

.home-team,
.away-team {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
  border-radius: 5px;
  width: 275px;
}

.game-card:hover {
  transform: scale(1.02);
}

.score {
  font-size: 1.5rem;
}

.winner {
  font-weight: 700;
}
</style>
