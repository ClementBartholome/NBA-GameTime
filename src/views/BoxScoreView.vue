<template>
  <AppHeader />
  <main>
    <BoxScore :gameId="gameId" :homeTeamName="homeTeamName" :visitorTeamName="visitorTeamName" />
  </main>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import BoxScore from '../components/BoxScore.vue'
import AppHeader from '../components/AppHeader.vue'

const route = useRoute()

const gameId = computed(() => {
  // If the gameId is an array, return the first element
  // gameId = string | string[]
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
</script>
