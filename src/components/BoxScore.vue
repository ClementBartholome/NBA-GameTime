<template>
  <div v-if="boxScore" class="box-score">
    <div class="team">
      <div class="team-header">
        <img :src="homeTeamInfo.logo" alt="Team Logo" />
        <h2>{{ homeTeamInfo.teamName }}</h2>
        <span :class="{ 'winning-score': homeTeamScore > awayTeamScore, score: true }">{{
          homeTeamScore
        }}</span>
      </div>
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Player</th>
              <th>PTS</th>
              <th>REB</th>
              <th>AST</th>
              <th>MIN</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(player, index) in homeTeam" :key="index">
              <router-link :to="'/player/' + player.player.id" class="player-link"
                ><td class="player-name">
                  {{ player.player.first_name }} {{ player.player.last_name }}
                </td></router-link
              >
              <td v-if="player.min != 0">{{ player.pts }}</td>
              <td v-if="player.min != 0">{{ player.reb }}</td>
              <td v-if="player.min != 0">{{ player.ast }}</td>
              <td v-if="player.min != 0">{{ player.min }}</td>
              <td v-else colspan="4">DNP</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="team">
      <div class="team-header">
        <span :class="{ score: true, 'winning-score': awayTeamScore > homeTeamScore }">{{
          awayTeamScore
        }}</span>
        <h2>{{ visitorTeamInfo.teamName }}</h2>
        <img :src="visitorTeamInfo.logo" alt="Team logo" />
      </div>
      <div class="table-container">
        <table>
          <thead>
            <tr>
              <th>Player</th>
              <th>PTS</th>
              <th>REB</th>
              <th>AST</th>
              <th>MIN</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(player, index) in awayTeam" :key="index">
              <router-link :to="'/player/' + player.player.id" class="player-link"
                ><td class="player-name">
                  {{ player.player.first_name }} {{ player.player.last_name }}
                </td></router-link
              >
              <td v-if="player.min != 0">{{ player.pts }}</td>
              <td v-if="player.min != 0">{{ player.reb }}</td>
              <td v-if="player.min != 0">{{ player.ast }}</td>
              <td v-if="player.min != 0">{{ player.min }}</td>
              <td v-else colspan="4">DNP</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
  <div v-else><h2>Games has not started yet</h2></div>
</template>

<script setup lang="ts">
import { useTeamStore } from '../stores/TeamsStore'
import { convertTeamName } from '../util/NbaUtil'
import { getBoxScoreByGameId } from '../util/NbaApi'
import { computed, ref, onMounted } from 'vue'

interface BoxScore {
  player: {
    first_name: string
    last_name: string
    id: number
  }
  game: {
    home_team_score: number
    visitor_team_score: number
  }
  pts: number
  reb: number
  ast: number
  min: number
  team: {
    abbreviation: string
  }
}

const props = defineProps({
  gameId: String,
  homeTeamName: String,
  visitorTeamName: String
})

const boxScore = ref<BoxScore[]>([])
const homeTeam = ref<BoxScore[]>([])
const awayTeam = ref<BoxScore[]>([])
const homeTeamScore = ref(0)
const awayTeamScore = ref(0)

onMounted(async () => {
  await fetchBoxScore()
})

const fetchBoxScore = async () => {
  const boxScoreData = await getBoxScoreByGameId(props.gameId)
  boxScore.value = boxScoreData
  console.log(boxScore.value)

  // Check if props.homeTeamName and props.visitorTeamName are defined
  const homeTeamName = props.homeTeamName || '' // Provide a default value if it's undefined
  const visitorTeamName = props.visitorTeamName || '' // Provide a default value if it's undefined

  // Get home team players from the boxScore
  homeTeam.value = boxScore.value.filter(
    (player) => player.team.abbreviation === convertTeamName(homeTeamName)
  )

  // Get away team players from the boxScore
  awayTeam.value = boxScore.value.filter(
    (player) => player.team.abbreviation === convertTeamName(visitorTeamName)
  )

  homeTeamScore.value = homeTeam.value[0].game.home_team_score
  awayTeamScore.value = awayTeam.value[0].game.visitor_team_score

  // Sort players by minutes played
  homeTeam.value.sort((a, b) => b.min - a.min)
  awayTeam.value.sort((a, b) => b.min - a.min)

  // Remove the leading zero for minutes played < 10
  homeTeam.value.forEach((player) => {
    player.min = player.min < 10 ? player.min.slice(1) : player.min
  })

  awayTeam.value.forEach((player) => {
    player.min = player.min < 10 ? player.min.slice(1) : player.min
  })
}

const teamStore = useTeamStore()
// Convert gameId to a number
const gameIdNumber = parseInt(props.gameId || '0', 10)
// Get this game homeTeamInfo from the store
const homeTeamInfo = computed(() => {
  const teamInfo = teamStore.getTeamInfo(gameIdNumber)
  return teamInfo ? teamInfo.homeTeamInfo : null
})

// Get this game visitorTeamInfo from the store
const visitorTeamInfo = computed(() => {
  const teamInfo = teamStore.getTeamInfo(gameIdNumber)
  return teamInfo ? teamInfo.visitorTeamInfo : null
})
</script>

<style scoped>
.starter {
  background-color: #e0ffe0; /* Light green for starters */
}

.score {
  background: white;
  display: flex;
  justify-content: center;
  flex-direction: column;
  height: 2.5rem;
  width: 4rem;
  text-align: center;
  font-weight: bold;
}

.winning-score {
  background: black;
  color: white;
}

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
  font-size: 1.5rem;
}

.team span {
  font-size: 1.2rem;
}

.player-link {
  color: black;
  display: contents;
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

.player-name:hover {
  opacity: 0.7;
}

@media (max-width: 1024px) {
  .box-score {
    flex-direction: column;
  }

  .team {
    margin-bottom: 20px;
  }
  .team:last-of-type .team-header {
    flex-direction: row-reverse;
  }
}

@media (max-width: 768px) {
  .box-score {
    padding: 0;
  }
}
</style>
