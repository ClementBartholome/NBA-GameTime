<template>
  <div>
    <AppHeader />
    <main>
      <h1>NBA Regular Season Standings</h1>
      <label for="seasonSelect">Select Season:</label>
      <select
        id="seasonSelect"
        class="season-selector"
        v-model="selectedSeason"
        @change="fetchStandings"
      >
        <option v-for="year in availableSeasons" :key="year" :value="year">{{ year }}</option>
      </select>

      <table class="standings-table">
        <thead>
          <tr>
            <th>Rank</th>
            <th>Team</th>
            <th>Wins</th>
            <th>Losses</th>
            <th>Win %</th>
            <th>Last 10</th>
            <th>Streak</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(team, index) in sortedStandings" :key="index">
            <td>{{ index + 1 }}</td>
            <td class="team">
              <img :src="teamLogos[team.Name]" alt="Team Logo" class="team-logo" />
              <span>{{ team.City }} {{ team.Name }}</span>
            </td>
            <td>{{ team.Wins }}</td>
            <td>{{ team.Losses }}</td>
            <td>{{ team.Percentage }}</td>
            <td>{{ team.LastTenWins }} - {{ team.LastTenLosses }}</td>
            <td>
              {{ team.Streak > 0 ? 'W' + team.Streak : 'L' + Math.abs(team.Streak) }}
            </td>
          </tr>
        </tbody>
      </table>
    </main>
  </div>
</template>

<script setup lang="ts">
import AppHeader from '../components/AppHeader.vue'
import { ref, onMounted, computed } from 'vue'
import { getStandings, getTeamInfo } from '../util/NbaApi'

// Define the data structure for team standings
interface TeamStandings {
  City: string
  Name: string
  Wins: number
  Losses: number
  Percentage: number
  LastTenWins: number
  LastTenLosses: number
  Streak: number
}

// Define and initialize reactive variables
const standings = ref<TeamStandings[]>([])
const selectedSeason = ref<number>(2023) // Default selected season
const availableSeasons = ref<number[]>([
  2023, 2022, 2021, 2020, 2019, 2018, 2017, 2016, 2015, 2014, 2013, 2012, 2011, 2010, 2009
])

const teamLogos = ref<{ [key: string]: string }>({}) // Store team logos with team names as keys

// Function to fetch standings for the selected season
const fetchStandings = async () => {
  const fetchedStandings = await getStandings(selectedSeason.value)
  if (fetchedStandings) {
    standings.value = fetchedStandings

    // Fetch team logos and store them with team names as keys
    await Promise.all(
      fetchedStandings.map(async (team: any) => {
        const logo = await getTeamInfo(team.Name).then((info) => info.logo)
        teamLogos.value[team.Name] = logo
        return logo
      })
    )
  }
}

// Call the fetchStandings function when the component is mounted
onMounted(fetchStandings)

// Compute sorted standings based on win percentage
const sortedStandings = computed(() => {
  return [...standings.value].sort((a, b) => b.Percentage - a.Percentage)
})
</script>

<style scoped>
.season-selector {
  padding: 0.5rem 2rem 0.5rem 0.75rem;
  line-height: 1.25;
  border-radius: 0.25rem;
  font-size: 0.75rem;
  cursor: pointer;
  font-weight: 700;
  text-overflow: ellipsis;
  color: #343232;
  margin: 1rem;
}

.team-logo {
  height: 60px;
  width: 80px;
  background: white;
  padding: 0.2rem;
  border-radius: 20px;
}
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

.standings-table td.team {
  display: flex;
  align-items: center;
  gap: 10px;
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
