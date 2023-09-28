<template>
  <div class="box-score">
    <div class="team">
      <h2>{{ homeTeamName }}</h2>
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
      <h2>{{ visitorTeamName }}</h2>
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

<script lang="ts">
interface PlayerStats {
  Name: string
  Minutes: number
  Points: number
  Rebounds: number
  Assists: number
  HomeOrAway: string
  Started: number
}

export default {
  props: {
    playerStats: Array<PlayerStats>,
    homeTeamName: String,
    visitorTeamName: String
  },
  computed: {
    homeTeamPlayers() {
      return this.playerStats
        ? this.playerStats.filter((player) => player.HomeOrAway === 'HOME')
        : []
    },
    visitorTeamPlayers() {
      return this.playerStats
        ? this.playerStats.filter((player) => player.HomeOrAway === 'AWAY')
        : []
    },
    sortedHomeTeamPlayers() {
      return this.sortPlayersByStarted(this.homeTeamPlayers)
    },
    sortedVisitorTeamPlayers() {
      return this.sortPlayersByStarted(this.visitorTeamPlayers)
    }
  },
  methods: {
    sortPlayersByStarted(players: PlayerStats[]) {
      // Sort players by Started (0 or 1), then by Minutes
      return players.slice().sort((a, b) => {
        if (a.Started === b.Started) {
          return b.Minutes - a.Minutes
        }
        return b.Started - a.Started
      })
    }
  }
}
</script>

<style scoped>
/* General Box Score Styling */
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
.team h2 {
  margin: 0;
  font-size: 24px;
  text-align: center;
  background-color: #f0f0f0;
  padding: 10px;
  border-bottom: 2px solid #333;
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
