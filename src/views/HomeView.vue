<template>
  <AppHeader />
  <main v-cloak>
    <NbaCalendar />
  </main>
</template>

<script setup lang="ts">
import NbaCalendar from '../components/NbaCalendar.vue'
import AppHeader from '../components/AppHeader.vue'
</script>

<style>
[v-cloak] {
  display: none;
}
</style>
