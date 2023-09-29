<template>
  <AppHeader />
  <main>
    <!-- <h1>Box Score for the game {{ gameId }}</h1> -->
    <BoxScore
      :playerStats="playerStats"
      :homeTeamName="homeTeamName"
      :visitorTeamName="visitorTeamName"
      :gameId="gameId"
    />
  </main>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import BoxScore from '../components/BoxScore.vue'
import AppHeader from '../components/AppHeader.vue'
import { getSingleBoxScore } from '../util/NbaUtil'
import { useGamesStore } from '../stores/GamesStore'

const route = useRoute()

const gameId = computed(() => {
  // If the gameId is an array, return the first element
  return Array.isArray(route.params.gameId) ? route.params.gameId[0] : route.params.gameId
})

const homeTeamName = computed(() => {
  return Array.isArray(route.params.homeTeamName)
    ? route.params.homeTeamName[0]
    : route.params.homeTeamName
})

const visitorTeamName = computed(() => {
  return Array.isArray(route.params.visitorTeamName)
    ? route.params.visitorTeamName[0]
    : route.params.visitorTeamName
})

const gamesBoxScores = useGamesStore().gamesBoxScores

const playerStats = computed(() => {
  if (gamesBoxScores && gamesBoxScores.length > 0) {
    // Get the single box score for the specified teams
    const singleBoxScore = getSingleBoxScore(
      gamesBoxScores,
      homeTeamName.value,
      visitorTeamName.value
    )
    // Return the player statistics for the game
    return singleBoxScore[0].PlayerGames
  }
  // If no data is available, return an empty array
  return []
})
</script>
