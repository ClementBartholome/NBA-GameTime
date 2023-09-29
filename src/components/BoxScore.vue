<template>
  <div class="box-score">
    <div class="team">
      <div class="team-header">
        <img :src="homeTeamInfo.logo" alt="Team Logo" />
        <h2>{{ homeTeamInfo.teamName }}</h2>
      </div>
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th></th>
              <th>Player</th>
              <th>PTS</th>
              <th>REB</th>
              <th>AST</th>
              <th>MIN</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(player, index) in sortedHomeTeamPlayers" :key="index">
              <td>{{ player.Started ? 'Starter' : 'Bench' }}</td>
              <td>{{ player.Name }}</td>
              <td>{{ player.Points }}</td>
              <td>{{ player.Rebounds }}</td>
              <td>{{ player.Assists }}</td>
              <td>{{ player.Minutes }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="team">
      <div class="team-header">
        <h2>{{ visitorTeamInfo.teamName }}</h2>
        <img :src="visitorTeamInfo.logo" alt="Team logo" />
      </div>
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th></th>
              <th>Player</th>
              <th>PTS</th>
              <th>REB</th>
              <th>AST</th>
              <th>MIN</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(player, index) in sortedVisitorTeamPlayers" :key="index">
              <td>{{ player.Started ? 'Starter' : 'Bench' }}</td>
              <td>{{ player.Name }}</td>
              <td>{{ player.Points }}</td>
              <td>{{ player.Rebounds }}</td>
              <td>{{ player.Assists }}</td>
              <td>{{ player.Minutes }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useTeamStore } from '../stores/TeamsStore'
import { computed, defineProps } from 'vue'

interface PlayerStats {
  Name: string
  Minutes: number
  Points: number
  Rebounds: number
  Assists: number
  HomeOrAway: string
  Started: number
}

const props = defineProps({
  playerStats: Array<PlayerStats>,
  gameId: String
})

const homeTeamPlayers = computed(() =>
  props.playerStats ? props.playerStats.filter((player) => player.HomeOrAway === 'HOME') : []
)

const visitorTeamPlayers = computed(() =>
  props.playerStats ? props.playerStats.filter((player) => player.HomeOrAway === 'AWAY') : []
)

const sortPlayersByStarted = (players: PlayerStats[]) => {
  // Sort players by Started (0 or 1), then by Minutes
  return players.slice().sort((a, b) => {
    if (a.Started === b.Started) {
      return b.Minutes - a.Minutes
    }
    return b.Started - a.Started
  })
}

const sortedHomeTeamPlayers = computed(() => sortPlayersByStarted(homeTeamPlayers.value))
const sortedVisitorTeamPlayers = computed(() => sortPlayersByStarted(visitorTeamPlayers.value))

const teamStore = useTeamStore()

const gameIdNumber = parseInt(props.gameId || '0', 10)
const homeTeamInfo = computed(() => {
  const teamInfo = teamStore.getTeamInfo(gameIdNumber)
  return teamInfo ? teamInfo.homeTeamInfo : null
})

const visitorTeamInfo = computed(() => {
  const teamInfo = teamStore.getTeamInfo(gameIdNumber)
  return teamInfo ? teamInfo.visitorTeamInfo : null
})
</script>

<style scoped>
.box-score {
  display: flex;
  justify-content: space-between;
  background-color: #fff;
  padding: 20px;
  border: 1px solid #ddd;
  color: #333;
  font-family: Arial, sans-serif;
}

.team {
  flex: 1;
}

/* Team Headers */

.team-header {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 20px;
  margin: 0;
  text-align: center;
  background-color: #f0f0f0;
  padding: 10px;
  border-bottom: 2px solid #333;
}

.team-header img {
  height: 40px;
}
.team h2 {
  font-size: 24px;
}

/* Table Styling */
.table-container {
  width: 100%;
  overflow-x: auto;
  margin-top: 10px;
}
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

table th,
table td {
  padding: 8px 10px;
  text-align: center;
  border: 1px solid #ddd;
}

table th {
  background-color: #f0f0f0;
  font-weight: bold;
}

/* Did Not Play Styling */
td:empty::before {
  content: 'DNP';
  font-weight: bold;
  color: #999;
}

@media (max-width: 1024px) {
  .box-score {
    flex-direction: column;
  }

  .team {
    margin-bottom: 20px;
  }
}

@media (max-width: 768px) {
  .box-score {
    padding: 0;
  }
}
</style>
