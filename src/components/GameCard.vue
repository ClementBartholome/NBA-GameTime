<template>
  <router-link :to="'/boxscore/' + gameID + `-` + homeTeamName + `-` + visitorTeamName">
    <div class="game-card" v-if="game">
      <div class="home-team" :style="{ backgroundColor: homeTeamColor }">
        <img :src="homeTeamLogo" alt="Home team logo" />
        <h2>{{ game.homeTeamFullName }}</h2>
        <span
          class="score"
          v-if="game.homeTeamScore > 0"
          :class="{ winner: isHomeWinner(game.homeTeamScore, game.visitorTeamScore) }"
          >{{ game.homeTeamScore }}</span
        >
      </div>
      <div class="away-team" :style="{ backgroundColor: visitorTeamColor }">
        <img :src="visitorTeamLogo" alt="Visiting team logo" />
        <h2>{{ game.visitorTeamFullName }}</h2>
        <span
          class="score"
          v-if="game.visitorTeamScore > 0"
          :class="{ winner: isAwayWinner(game.homeTeamScore, game.visitorTeamScore) }"
          >{{ game.visitorTeamScore }}</span
        >
      </div>
    </div>
  </router-link>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { fetchAndSaveTeamInfo, getCorrectBoxScore } from '../util/NbaApi'
import { useTeamStore } from '../stores/TeamsStore'

const props = defineProps({
  game: Object,
  gamesBoxScores: Array
})

const homeTeamName = ref('')
const visitorTeamName = ref('')
const homeTeamLogo = ref('')
const visitorTeamLogo = ref('')
const homeTeamColor = ref('')
const visitorTeamColor = ref('')
const gameID = ref(0)

const teamStore = useTeamStore()

onMounted(async () => {
  if (props.game && props.gamesBoxScores && props.gamesBoxScores.length > 0) {
    // Fetch home team information
    const homeTeamInfo = await fetchAndSaveTeamInfo(props.game.homeTeamName)
    // Fetch visitor team information
    const visitorTeamInfo = await fetchAndSaveTeamInfo(props.game.visitorTeamName)

    // Set home team properties
    homeTeamName.value = homeTeamInfo.teamKey
    homeTeamLogo.value = homeTeamInfo.logo
    homeTeamColor.value = '#' + homeTeamInfo.primaryColor

    // Set visitor team properties
    visitorTeamName.value = visitorTeamInfo.teamKey
    visitorTeamLogo.value = visitorTeamInfo.logo
    visitorTeamColor.value = '#' + visitorTeamInfo.primaryColor

    const correctBoxScore = await getCorrectBoxScore(props.game.gameId)
    // console.log(correctBoxScore)

    gameID.value = correctBoxScore[0].game.id
    // console.log(gameID.value)

    // Get the single box score for the specified teams
    // const boxScore = getSingleBoxScore(
    //   props.gamesBoxScores,
    //   homeTeamName.value,
    //   visitorTeamName.value
    // )

    // Check if boxScore data is structured correctly
    // if (boxScore[0].Game.GameID) {
    //   gameID.value = boxScore[0].Game.GameID
    //   console.log(boxScore[0])
    // } else {
    //   console.error('La structure des données de boxScore[0] est incorrecte.')
    // }

    const existingTeamInfo = teamStore.teamsInfo.find((info) => info.gameId === gameID.value)

    // If it doesn't exist, add it to the store
    if (!existingTeamInfo) {
      const teamInfo = {
        gameId: gameID.value,
        homeTeamInfo,
        visitorTeamInfo
      }

      // Add team information to the store
      teamStore.addTeamInfo(teamInfo)
      // console.log(teamStore.teamsInfo)
    }
  }
})

// Determine if the home team is the winner
const isHomeWinner = (homeScore: number, awayScore: number) => {
  return homeScore > awayScore
}
// Determine if the away team is the winner
const isAwayWinner = (homeScore: number, awayScore: number) => {
  return awayScore > homeScore
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
  height: 270px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  color: #ffffff;
  transition: transform 0.2s ease-in-out;
}

.game-card h2 {
  font-size: 1.3rem;
  text-align: center;
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
  justify-content: space-between;
  padding: 1rem;
  border-radius: 5px;
  width: 50%;
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

@media (max-width: 500px) {
  .game-card {
    gap: 0;
    padding: 0;
    height: 200px;
  }
  .game-card img {
    width: 100%;
    height: 80px;
  }
  .home-team,
  .away-team {
    border-radius: 0;
  }
}
</style>
