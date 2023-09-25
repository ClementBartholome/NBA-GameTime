<template>
  <div class="box-score">
    <div class="team">
      <h2>Home team</h2>
      <ul>
        <li v-for="(player, index) in sortedHomeTeamPlayers" :key="index" class="player">
          <div class="player-info">
            <h3>{{ player.Name }}</h3>
            <template v-if="player.Minutes === 0">
              <p>DNP</p>
            </template>
            <template v-else>
              <p>Points: {{ player.Points }}</p>
              <p>Rebounds: {{ player.Rebounds }}</p>
              <p>Assists: {{ player.Assists }}</p>
              <p>Minutes : {{ player.Minutes }}</p>
            </template>
          </div>
        </li>
      </ul>
    </div>
    <div class="team">
      <h2>Away team</h2>
      <ul>
        <li v-for="(player, index) in sortedVisitorTeamPlayers" :key="index" class="player">
          <div class="player-info">
            <h3>{{ player.Name }}</h3>
            <template v-if="player.Minutes === 0">
              <p>DNP</p>
            </template>
            <template v-else>
              <p>Points: {{ player.Points }}</p>
              <p>Rebounds: {{ player.Rebounds }}</p>
              <p>Assists: {{ player.Assists }}</p>
              <p>Minutes : {{ player.Minutes }}</p>
            </template>
          </div>
        </li>
      </ul>
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
    playerStats: Array<PlayerStats>
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
      // Starters are displayed first (Started = 1), bench players then (Started = 0)
      return players.slice().sort((a, b) => b.Started - a.Started)
    }
  }
}
</script>

<style scoped>
.box-score {
  display: flex;
  justify-content: space-between;
  background-color: #f0f0f0;
  padding: 20px;
  border: 1px solid #ccc;
  margin: 20px;
  color: black;
}

.team {
  flex: 1;
  background-color: #fff;
  padding: 10px;
  border: 1px solid #ddd;
}

.player {
  margin-bottom: 10px;
}

.player-info {
  background-color: #f9f9f9;
  padding: 10px;
  border: 1px solid #ccc;
}
</style>
