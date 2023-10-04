<template>
  <section class="player-stats">
    <h2>{{ playerStats.season }} - {{ playerStats.season + 1 }} Season Stats</h2>
    <table>
      <tr>
        <th>GP</th>
        <th>MIN</th>
        <th>PPG</th>
        <th>RPG</th>
        <th>APG</th>
        <th>FGM</th>
        <th>FGA</th>
        <th>FG%</th>
        <th>3PM</th>
        <th>3PA</th>
        <th>3P%</th>
        <th>SPG</th>
        <th>BPG</th>
        <th>TO</th>
        <th>ORB</th>
        <th>DRB</th>
        <th>FTM</th>
        <th>FTA</th>
        <th>FT%</th>
        <th>PF</th>
      </tr>
      <tr>
        <td>{{ playerStats.gamesPlayed }}</td>
        <td>{{ playerStats.minutes }}</td>
        <td>{{ playerStats.points }}</td>
        <td>{{ playerStats.rebounds }}</td>
        <td>{{ playerStats.assists }}</td>
        <td>{{ playerStats.fieldGoalsMade }}</td>
        <td>{{ playerStats.fieldGoalsAttempted }}</td>
        <td>{{ playerStats.fieldGoalsPercentage }}</td>
        <td>{{ playerStats.threePointersMade }}</td>
        <td>{{ playerStats.threePointersAttempted }}</td>
        <td>{{ playerStats.threePointersPercentage }}</td>
        <td>{{ playerStats.steals }}</td>
        <td>{{ playerStats.blocks }}</td>
        <td>{{ playerStats.turnovers }}</td>
        <td>{{ playerStats.offensiveRebounds }}</td>
        <td>{{ playerStats.defensiveRebounds }}</td>
        <td>{{ playerStats.freeThrowsMade }}</td>
        <td>{{ playerStats.freeThrowsAttempted }}</td>
        <td>{{ playerStats.freeThrowsPercentage }}</td>
        <td>{{ playerStats.fouls }}</td>
      </tr>
    </table>
  </section>
</template>

<script setup lang="ts">
interface PlayerStats {
  playerId: number
  season: number
  gamesPlayed: number
  minutes: number
  points: number
  rebounds: number
  offensiveRebounds: number
  defensiveRebounds: number
  assists: number
  blocks: number
  steals: number
  turnovers: number
  fieldGoalsAttempted: number
  fieldGoalsMade: number
  fieldGoalsPercentage: number
  threePointersAttempted: number
  threePointersMade: number
  threePointersPercentage: number
  freeThrowsAttempted: number
  freeThrowsMade: number
  freeThrowsPercentage: number
  fouls: number
}

const props = defineProps<{
  playerStats: PlayerStats
}>()
</script>

<style scoped>
.player-stats {
  margin-top: 2rem;
  background-color: white;
  color: black;
  border-radius: 15px;
  padding: 1rem;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  overflow: scroll;
}

.player-stats h2 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

table {
  width: 100%;
  border-collapse: collapse;
  font-size: 0.8rem;
}

table th,
table td {
  padding: 0.5rem 1rem;
  text-align: center;
  border: 1px solid black;
}

table th {
  background-color: #f2f2f2;
  font-weight: bold;
}

table tr:nth-child(even) {
  background-color: #f2f2f2;
}
</style>
