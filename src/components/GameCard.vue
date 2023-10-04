<template>
  <router-link :to="'/boxscore/' + gameId + `-` + homeTeamName + `-` + visitorTeamName">
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
import { fetchAndSaveTeamInfo, getBoxScoreByGameId } from '../util/NbaApi'
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
const gameId = ref(0)

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

    const singleBoxScore = await getBoxScoreByGameId(props.game.gameId)
    // console.log(singleBoxScore)

    gameId.value = singleBoxScore[0].game.id

    // Check if the game teams info already exists in the store
    const existingTeamInfo = teamStore.teamsInfo.find((info) => info.gameId === gameId.value)

    // If it doesn't exist, associate the teams information with the game and add it to the store
    if (!existingTeamInfo) {
      const teamInfo = {
        gameId: gameId.value,
        homeTeamInfo,
        visitorTeamInfo
      }
      teamStore.addTeamInfo(teamInfo)
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
