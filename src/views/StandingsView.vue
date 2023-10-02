<template>
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
    <div class="table-container">
      <table class="standings-table">
        <thead>
          <tr>
            <th>Rank</th>
            <th>Team</th>
            <th>Wins</th>
            <th>Losses</th>
            <th>Win%</th>
            <th>L10</th>
            <th>Streak</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(team, index) in sortedStandings" :key="index">
            <td>{{ index + 1 }}</td>
            <td class="team">
              <img :src="teamLogos[team.name]" alt="Team Logo" class="team-logo" />
              <span>{{ team.city }} {{ team.name }}</span>
            </td>
            <td>{{ team.wins }}</td>
            <td>{{ team.losses }}</td>
            <td>{{ team.percentage }}</td>
            <td>{{ team.lastTenWins }}-{{ team.lastTenLosses }}</td>
            <td>
              {{ team.streak > 0 ? 'W' + team.streak : 'L' + Math.abs(team.streak) }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>

<script setup lang="ts">
import AppHeader from '../components/AppHeader.vue'
import { ref, onMounted, computed } from 'vue'
import { fetchAndSaveStandings, getStandingsFromDb, getTeamInfoFromDb } from '../util/NbaApi'

interface TeamStandings {
  season: number
  city: string
  name: string
  wins: number
  losses: number
  percentage: number
  lastTenWins: number
  lastTenLosses: number
  streak: number
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
  await fetchAndSaveStandings(selectedSeason.value)
  const fetchedStandings = await getStandingsFromDb(selectedSeason.value)
  if (fetchedStandings) {
    standings.value = fetchedStandings

    // Fetch team logos and store them with team names as keys
    await Promise.all(
      fetchedStandings.map(async (team: any) => {
        const logo = await getTeamInfoFromDb(team.name).then((info) => info.logo)
        teamLogos.value[team.name] = logo
        return logo
      })
    )
  }
}

// Call the fetchStandings function when the component is mounted
onMounted(fetchStandings)

// Sort standings based on win percentage
const sortedStandings = computed(() => {
  return [...standings.value].sort((a, b) => b.percentage - a.percentage)
})
</script>

<style scoped>
main h1,
main label {
  padding: 1rem;
}
.table-container {
  width: 100%;
  overflow-x: auto; /* Enable horizontal scrolling */
  margin-bottom: 2rem; /* Add some margin to separate from the header */
}

/* Fix the "Rank" and "Team" columns to the left */
/* .standings-table {
  width: auto;
  position: relative;
}

.standings-table thead th:nth-child(-n + 3),
.standings-table tbody td:nth-child(-n + 3) {
  position: -webkit-sticky;
  position: sticky;
  left: 0;
  z-index: 1;
  background-color: #181818;
} */

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
  vertical-align: middle;
  margin-right: 10px;
}

.team span {
  display: inline-block;
  width: 170px;
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

.standings-table tbody tr:last-child td {
  border-bottom: none;
}

.standings-table th {
  background-color: #071a21;
  color: #fff;
  font-weight: bold;
}

@media (max-width: 768px) {
  .standings-table th,
  .standings-table td {
    padding: 4px 8px;
  }
  .team-logo {
    max-height: 40px;
    max-width: 40px;
  }
}
</style>
