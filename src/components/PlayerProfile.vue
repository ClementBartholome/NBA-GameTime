<template>
  <section class="player-profile" :style="{ backgroundColor: '#' + playerTeamInfo.primaryColor }">
    <img :src="playerTeamInfo.logo" class="logo" :alt="playerInfo.teamName + ' logo'" />
    <img
      :src="playerInfo.playerPhoto"
      class="player-photo"
      :alt="playerInfo.firstName + ' ' + playerInfo.lastName + ' photo'"
    />

    <div class="player-info">
      <div class="flex gap-10">
        <p>{{ playerInfo.teamName }} | {{ convertPosition(playerInfo.position) }}</p>
      </div>
      <div class="player-name">
        <h1>{{ playerInfo.firstName }}</h1>
        <h1>{{ playerInfo.lastName }}</h1>
      </div>
    </div>
    <div class="averages">
      <div class="stat">
        <h2>PPG</h2>
        <p>{{ playerStats.points }}</p>
      </div>
      <div class="stat">
        <h2>RPG</h2>
        <p>{{ playerStats.rebounds }}</p>
      </div>
      <div class="stat">
        <h2>APG</h2>
        <p>{{ playerStats.assists }}</p>
      </div>
      <div class="stat">
        <h2>FG%</h2>
        <p>{{ playerStats.fieldGoalsPercentage }}</p>
      </div>
    </div>
    <div class="follow">
      <button>
        <svg
          xmlns="http://www.w3.org/2000/svg"
          height="16"
          width="16"
          viewBox="0 0 8.32 8.67"
          class="PlayerSummary_followIcon__4c0yj"
          data-no-icon="true"
          role="presentation"
          title="Follow Player Button Star"
          data-is-followed="false"
          data-is-disabled="false"
          data-has-team="true"
        >
          <path
            fill="currentColor"
            fill-rule="evenodd"
            d="M4 6L1.649 7.236l.449-2.618L.196 2.764l2.628-.382L4 0l1.176 2.382 2.628.382-1.902 1.854.45 2.618z"
          ></path>
        </svg>
        <span>Follow</span>
      </button>
    </div>
  </section>
</template>

<script setup lang="ts">
import { convertPosition } from '../util/NbaUtil'

interface PlayerInfo {
  playerId: number
  firstName: string
  lastName: string
  position: string
  teamName: string
  playerPhoto: string
}

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

interface PlayerTeamInfo {
  primaryColor: string
  logo: string
}

const props = defineProps<{
  playerStats: PlayerStats
  playerInfo: PlayerInfo
  playerTeamInfo: PlayerTeamInfo
}>()
</script>

<style scoped>
.player-profile {
  display: flex;
  padding: 20px;
  color: white;
  align-items: center;
  gap: 2rem;
  border-radius: 15px;
}

.logo {
  height: 100px;
  margin-bottom: 6rem;
}

.player-photo {
  object-fit: contain;
  height: 120px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 1);
  background: white;
  border-radius: 50%;
}

.player-name {
  text-transform: uppercase;
  font-size: 1.6rem;
}

.player-name h1 {
  line-height: 1;
}

.follow {
  margin-left: auto;
}

.follow button {
  padding: 0.5rem 3rem;
  border-radius: 9999px;
  font-weight: 700;
  font-size: 1.2rem;
  display: flex;
  align-items: center;
  color: white;
  background: transparent;
  border: 2px solid #fff;
  width: 100%;
  gap: 10px;
}

.averages {
  display: flex;
  gap: 2rem;
  width: 23%;
}
.stat {
  padding: 10px;
  background-color: white;
  border: 2px solid white;
  border-radius: 5px;
  text-align: center;
  width: 25%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  color: black;
}

.stat p {
  font-weight: 600;
  font-size: 1.2rem;
}
</style>
