<template>
  <section class="calendar" v-cloak>
    <div class="calendar-input">
      <label for="calendar-input" class="calendar-icon" @click="toggleCalendar">
        <span>{{ currentMonth }}</span>
        <i class="far fa-calendar"></i>
      </label>
      <input
        type="text"
        id="calendar-input"
        v-model="currentDate"
        @change="updateGames"
        readonly
        style="display: none"
      />
      <calendar-picker
        v-if="showCalendar"
        :currentDate="currentDate"
        @select="selectDate"
      ></calendar-picker>
    </div>

    <date-picker
      :formatted-date="formattedDate"
      :selectedWeek="selectedWeek"
      @selectDay="selectDay"
      @prevWeek="prevWeek"
      @nextWeek="nextWeek"
    />
    <ul class="game-list">
      <span v-if="loading" class="loader"></span>
      <li v-else-if="games.length === 0">No games on this date</li>
      <li v-else v-for="game in games" :key="game.gameID">
        <GameCard :game="game" :gamesBoxScores="gamesBoxScores" />
      </li>
    </ul>
  </section>
</template>

<script setup lang="ts">
import DatePicker from './DatePicker.vue'
import CalendarPicker from './CalendarPicker.vue'
import GameCard from './GameCard.vue'
import { ref, onMounted, watch, computed } from 'vue'
import { fetchAndSaveGames, getGamesFromDb, getBoxScoresByDate } from '../util/NbaApi'

const games = ref([] as any[]) // Date for the games
const currentDate = ref('2023-05-09') // Base date
const selectedWeek = ref([] as string[]) // Days of the week that is selected
const showCalendar = ref(false) // Show the calendar or not
const loading = ref(false) // Loading state
const gamesBoxScores = ref([] as any[]) // Box scores for the games

// Format the date to display it in the date picker
const formattedDate = computed(() => {
  const dateObj = new Date(currentDate.value)
  const dayOfWeek = dateObj.toLocaleDateString('en-US', { weekday: 'short' })
  const dayOfMonth = dateObj.getDate()
  return `${dayOfWeek} ${dayOfMonth}` // = Mon 9, for example
})

// Format the date to display it next to the calendar picker
const currentMonth = computed(() => {
  const dateObj = new Date(currentDate.value)
  return dateObj.toLocaleDateString('en-US', { year: 'numeric', month: 'long' })
})

// const gamesBoxScores = computed(() => {
//   return useGamesStore().gamesBoxScores
// })

// On component mount, fetch the games, update the selected week and the games box scores
onMounted(() => {
  updateGames()
  updateSelectedWeek()
  updateGamesBoxScores(currentDate.value)
})

// Watch for changes in the current date and update the games
watch(currentDate, (newDate, oldDate) => {
  if (newDate !== oldDate) {
    updateGames()
    updateGamesBoxScores(newDate)
  }
})

const updateGames = async () => {
  try {
    loading.value = true
    await new Promise((resolve) => setTimeout(resolve, 1000))
    // Fetch games for the current date and save them in the database
    await fetchAndSaveGames(currentDate.value)
    const fetchedGames = await getGamesFromDb(currentDate.value)
    games.value = fetchedGames
  } catch (error) {
    console.error('Erreur lors de la mise à jour des matchs :', error)
  } finally {
    loading.value = false
  }
}

// Fetch NBA games box scores for the given date and store them
const updateGamesBoxScores = async (date: string) => {
  const fetchedGamesBoxScores = await getBoxScoresByDate(date)
  gamesBoxScores.value = fetchedGamesBoxScores
}

// Update the selected week based on the current date
const updateSelectedWeek = () => {
  const selectedDate = new Date(currentDate.value)
  const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']

  selectedWeek.value = []

  const dayOfWeek = selectedDate.getDay()
  const sundayDate = new Date(selectedDate)
  sundayDate.setDate(selectedDate.getDate() - dayOfWeek)

  for (let i = 0; i < 7; i++) {
    const day = new Date(sundayDate)
    day.setDate(sundayDate.getDate() + i)
    const formattedDay = daysOfWeek[day.getDay()] + ' ' + day.getDate()
    selectedWeek.value.push(formattedDay)
  }
}

// Select a date in the calendar picker
const selectDate = (selectedDate: any) => {
  showCalendar.value = false
  currentDate.value = selectedDate
  updateGames()
  updateSelectedWeek()
}

const toggleCalendar = () => {
  showCalendar.value = !showCalendar.value
}

// Select a day in the date picker
const selectDay = (day: string) => {
  const dayParts = day.split(' ')
  const dayNumber = parseInt(dayParts[1], 10)
  const selectedDate = new Date(currentDate.value)
  selectedDate.setDate(dayNumber)
  currentDate.value = selectedDate.toISOString().split('T')[0]
}

// Change the current date to the previous week
const prevWeek = () => {
  const prevDate = new Date(currentDate.value)
  prevDate.setDate(prevDate.getDate() - 7)
  currentDate.value = prevDate.toISOString().split('T')[0]
  updateSelectedWeek()
}

// Change the current date to the next week
const nextWeek = () => {
  const nextDate = new Date(currentDate.value)
  nextDate.setDate(nextDate.getDate() + 7)
  currentDate.value = nextDate.toISOString().split('T')[0]
  updateSelectedWeek()
}
</script>

<style scoped>
.calendar[v-cloak] {
  display: none;
}
.date-picker {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.date-picker button {
  background-color: transparent;
  border: none;
  cursor: pointer;
}

section.calendar {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 2rem;
}

.calendar-input {
  display: flex;
  align-items: center;
  justify-content: center;
}

.calendar-icon {
  margin-right: 10px;
  font-size: 1.25rem;
  color: #888;
  display: flex;
  align-items: center;
  cursor: pointer;
  gap: 4rem;
  position: relative;
  z-index: 9999;
}

#calendar-input {
  display: none;
}

.game-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.game-list li {
  width: 100%;
  max-width: 430px;
}

.date-picker {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.date-picker button {
  background-color: transparent;
  border: none;
  cursor: pointer;
}

section.calendar {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 2rem;
}

@media (max-width: 860px) {
  .game-list li {
    max-width: 600px;
  }
}
</style>
