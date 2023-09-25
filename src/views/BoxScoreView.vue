<template>
  <AppHeader />
  <main>
    <h1 @click="fetchData()">Box Score for the game {{ gameId }}</h1>
    <BoxScore :playerStats="playerStats" />
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
      return this.$route.params.gameId
    },
    homeTeamName() {
      return this.$route.params.homeTeamName
    },
    visitorTeamName() {
      return this.$route.params.visitorTeamName
    },
    gamesBoxScores() {
      return useGamesStore().gamesBoxScores
    },
    playerStats() {
      if (this.gamesBoxScores && this.gamesBoxScores.length > 0) {
        const singleBoxScore = getSingleBoxScore(
          this.gamesBoxScores,
          this.homeTeamName,
          this.visitorTeamName
        )
        return singleBoxScore[0].PlayerGames
      }
      return []
    }
  },
  methods: {
    async fetchData() {
      console.log(this.gamesBoxScores)
      console.log(this.homeTeamName)
      console.log(this.visitorTeamName)
    }
  }
}
</script>
