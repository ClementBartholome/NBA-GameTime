<template>
  <div>
    <AppHeader />
    <main>
      <h1>NBA 2022-2023 Regular Season Standings</h1>
      <table class="standings-table">
        <thead>
          <tr>
            <th>Rank</th>
            <th>Team</th>
            <th>Wins</th>
            <th>Losses</th>
            <th>Win %</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(team, index) in sortedStandings" :key="index">
            <td>{{ index + 1 }}</td>
            <td>{{ team.City }} {{ team.Name }}</td>
            <td>{{ team.Wins }}</td>
            <td>{{ team.Losses }}</td>
            <td>{{ team.Percentage }}</td>
          </tr>
        </tbody>
      </table>
    </main>
  </div>
</template>

<script setup lang="ts">
import AppHeader from '../components/AppHeader.vue'
import { ref, onMounted, computed } from 'vue'
import { getStandings } from '../util/NbaApi'

interface TeamStandings {
  City: string
  Name: string
  Wins: number
  Losses: number
  Percentage: number
}

const standings = ref<TeamStandings[]>([])

onMounted(async () => {
  const fetchedStandings = await getStandings()
  if (fetchedStandings) {
    standings.value = fetchedStandings
  }
})

const sortedStandings = computed(() => {
  return [...standings.value].sort((a, b) => b.Percentage - a.Percentage)
})
</script>

<style scoped>
.standings-table {
  width: 100%;
  border-collapse: collapse;
}

.standings-table th,
.standings-table td {
  padding: 8px 12px;
  text-align: left;
  border-bottom: 1px solid #ccc;
}

.standings-table tbody tr:last-child td {
  border-bottom: none;
}

.standings-table th {
  background-color: #071a21;
  color: #fff;
  font-weight: bold;
}
</style>
