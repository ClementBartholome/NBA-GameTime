<template>
  <AppHeader />
  <main>
    <!-- <h1>Box Score for the game {{ gameId }}</h1> -->
    <BoxScore
      :playerStats="playerStats"
      :homeTeamName="homeTeamName"
      :visitorTeamName="visitorTeamName"
    />
  </main>
</template>

<script lang="ts">
import BoxScore from '../components/BoxScore.vue'
import AppHeader from '../components/AppHeader.vue'
import { getSingleBoxScore } from '../util/NbaUtil'
import { useGamesStore } from '../stores/GamesStore'

export default {
  components: {
    BoxScore,
    AppHeader
  },
  computed: {
    gameId() {
      // If the gameId is an array, return the first element
      return Array.isArray(this.$route.params.gameId)
        ? this.$route.params.gameId[0]
        : this.$route.params.gameId
    },
    homeTeamName() {
      return Array.isArray(this.$route.params.homeTeamName)
        ? this.$route.params.homeTeamName[0]
        : this.$route.params.homeTeamName
    },
    visitorTeamName() {
      return Array.isArray(this.$route.params.visitorTeamName)
        ? this.$route.params.visitorTeamName[0]
        : this.$route.params.visitorTeamName
    },
    gamesBoxScores() {
      return useGamesStore().gamesBoxScores
    },
    playerStats() {
      if (this.gamesBoxScores && this.gamesBoxScores.length > 0) {
        // Get the single box score for the specified teams
        const singleBoxScore = getSingleBoxScore(
          this.gamesBoxScores,
          this.homeTeamName,
          this.visitorTeamName
        )
        // Return the player statistics for the game
        return singleBoxScore[0].PlayerGames
      }
      // If no data is available, return an empty array
      return []
    }
  }
}
</script>
