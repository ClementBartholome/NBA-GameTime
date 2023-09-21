<template>
  <div class="game-card" v-if="game">
    <div class="home-team" :style="{ backgroundColor: homeTeamColor }">
      <img :src="homeTeamLogo" alt="Home team logo" />
      <h2>{{ game.home_team.full_name }}</h2>
      <span
        class="score"
        v-if="game.home_team_score > 0"
        :class="{ winner: isHomeWinner(game.home_team_score, game.visitor_team_score) }"
        >{{ game.home_team_score }}</span
      >
    </div>
    <div class="away-team" :style="{ backgroundColor: visitorTeamColor }">
      <img :src="visitorTeamLogo" alt="Visiting team logo" />
      <h2>{{ game.visitor_team.full_name }}</h2>
      <span
        class="score"
        v-if="game.visitor_team_score > 0"
        :class="{ winner: isAwayWinner(game.home_team_score, game.visitor_team_score) }"
        >{{ game.visitor_team_score }}</span
      >
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'
import { getTeamInfo } from '../util/NbaApi'
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

    const playerStats = ref([])

    onMounted(async () => {
      if (props.game) {
        const homeTeamInfo = await getTeamInfo(props.game.home_team.name)
        // console.log(homeTeamInfo)
        const visitorTeamInfo = await getTeamInfo(props.game.visitor_team.name)

        homeTeamName.value = homeTeamInfo.teamName
        visitorTeamName.value = visitorTeamInfo.teamName
        homeTeamLogo.value = homeTeamInfo.logo
        visitorTeamLogo.value = visitorTeamInfo.logo
        homeTeamColor.value = '#' + homeTeamInfo.primaryColor
        visitorTeamColor.value = '#' + visitorTeamInfo.primaryColor

        const boxScore = getSingleBoxScore(
          props.gamesBoxScores,
          homeTeamName.value,
          visitorTeamName.value
        )
        playerStats.value = boxScore[0].PlayerGames
        console.log(playerStats.value)
      }
    })

    return {
      homeTeamLogo,
      visitorTeamLogo,
      homeTeamColor,
      visitorTeamColor,
      homeTeamName,
      visitorTeamName
    }
  },
  methods: {
    isHomeWinner(homeScore: number, awayScore: number) {
      return homeScore > awayScore
    },
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
