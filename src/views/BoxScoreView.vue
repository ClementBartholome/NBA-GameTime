<template>
  <AppHeader />
  <main>
    <h1>Box Score for the game {{ gameId }}</h1>
    <BoxScore :playerStats="playerStats" />
  </main>
</template>

<script lang="ts">
import BoxScore from '../components/BoxScore.vue'
import AppHeader from '../components/AppHeader.vue'
import { getSingleBoxScore } from '../util/NbaUtil'

export default {
  components: {
    BoxScore,
    AppHeader
  },
  computed: {
    gameId() {
      return this.$route.params.gameId
    }
  }
}
</script>
