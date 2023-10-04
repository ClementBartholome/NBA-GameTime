<template>
  <AppHeader />
  <main v-if="playerInfo && playerStats">
    <PlayerProfile
      :playerStats="playerStats"
      :playerInfo="playerInfo"
      :playerTeamInfo="playerTeamInfo"
    />
    <PlayerSeasonStats :playerStats="playerStats" />
  </main>
  <div v-else class="loader"></div>
</template>

<script setup lang="ts">
import AppHeader from '../components/AppHeader.vue'
import { computed, ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import {
  fetchAndSavePlayerInfo,
  getTeamInfoFromDb,
  fetchAndSavePlayerSeasonAvg
} from '../util/NbaApi'
import PlayerSeasonStats from '../components/PlayerSeasonStats.vue'
import PlayerProfile from '../components/PlayerProfile.vue'

interface PlayerInfo {
  playerId: number
  firstName: string
  lastName: string
  position: string
  teamName: string
  playerPhoto: string
}

const route = useRoute()

// Reactive variables
const playerInfo = ref<PlayerInfo>()
const playerTeamInfo = ref()
const playerStats = ref()

// Compute the playerId based on the route parameters
const playerId = computed(() => {
  const id = Array.isArray(route.params.playerId)
    ? parseInt(route.params.playerId[0])
    : parseInt(route.params.playerId)

  return id
})

onMounted(() => {
  fetchPlayer()
  fetchPlayerStats()
})

const fetchPlayer = async () => {
  // Fetch and save player information based on playerId
  const fetchedPlayer = await fetchAndSavePlayerInfo(playerId.value)
  playerInfo.value = fetchedPlayer
  // Fetch additional team information
  if (playerInfo.value) {
    const fetchPlayerTeamInfo = await getTeamInfoFromDb(playerInfo.value.teamName)
    playerTeamInfo.value = fetchPlayerTeamInfo
  }
}

const fetchPlayerStats = async () => {
  setTimeout(async () => {
    // Fetch and save player season statistics for the year 2022
    const fetchedPlayerStats = await fetchAndSavePlayerSeasonAvg(playerId.value, 2022)
    playerStats.value = fetchedPlayerStats
  }, 1000)
}
</script>

<style scoped>
main {
  padding: 20px;
}
.loader {
  margin: 0 auto;
}
</style>
