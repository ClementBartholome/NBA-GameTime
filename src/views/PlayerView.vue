<template>
  <AppHeader />
  <main>
    <div v-if="playerInfo">
      <h1>{{ playerInfo.firstName }} {{ playerInfo.lastName }}</h1>
      <p>{{ playerInfo.position }}</p>
      <p>{{ playerInfo.teamName }}</p>
      <img :src="playerTeamInfo.logo" alt="" />
    </div>
    <div v-else>Loading player information...</div>
  </main>
</template>

<script setup lang="ts">
import AppHeader from '../components/AppHeader.vue'
import { computed, ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { fetchAndSavePlayerInfo, getTeamInfoFromDb } from '../util/NbaApi'

interface PlayerInfo {
  playerId: number
  firstName: string
  lastName: string
  position: string
  teamName: string
}

const route = useRoute()

const playerInfo = ref<PlayerInfo>()
const playerTeamInfo = ref()

const playerId = computed(() => {
  // If the gameId is an array, return the first element as a number
  const id = Array.isArray(route.params.playerId)
    ? parseInt(route.params.playerId[0])
    : parseInt(route.params.playerId)

  return id
})

onMounted(() => {
  fetchPlayer()
})

const fetchPlayer = async () => {
  const fetchedPlayer = await fetchAndSavePlayerInfo(playerId.value)
  playerInfo.value = fetchedPlayer
  if (playerInfo.value) {
    const fetchPlayerTeamInfo = await getTeamInfoFromDb(playerInfo.value.teamName)
    playerTeamInfo.value = fetchPlayerTeamInfo
    console.log(playerTeamInfo.value)
  }
}
</script>
