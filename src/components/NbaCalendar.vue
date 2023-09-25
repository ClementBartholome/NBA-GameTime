<template>
  <section class="calendar">
    <div class="calendar-input">
      <label for="calendar-input" class="calendar-icon" @click="toggleCalendar">
        <span>{{ currentMonth }}</span>
        <i class="far fa-calendar"></i>
      </label>
      <input
        type="text"
        id="calendar-input"
        v-model="currentDate"
        @change="updateGames()"
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
      :selectedWeek="selectedWeek"
      :formattedDate="formattedDate"
      @selectDay="selectDay"
      @prevWeek="prevWeek"
      @nextWeek="nextWeek"
    />

    <ul class="game-list">
      <li v-for="game in games" :key="game.id">
        <GameCard :game="game" :gamesBoxScores="gamesBoxScores" />
      </li>
    </ul>
  </section>
</template>

<script lang="ts">
import DatePicker from './DatePicker.vue'
import CalendarPicker from './CalendarPicker.vue'
import GameCard from './GameCard.vue'
import { fetchGames, getBoxScores } from '../util/NbaApi'

export default {
  components: {
    DatePicker,
    CalendarPicker,
    GameCard
  },
  data() {
    return {
      games: [] as any[], // Data for the NBA games.
      gamesBoxScores: [], // Data for the NBA games box scores.
      currentDate: '2023-05-09', // Base date
      selectedWeek: [] as string[], // The days of the week that is selected.
      showCalendar: false // Boolean to show or hide the calendar.
    }
  },
  watch: {
    currentDate(newDate, oldDate) {
      if (newDate !== oldDate) {
        this.updateGames()
        this.updateGamesBoxScores(newDate)
      }
    }
  },
  computed: {
    // Format the current date for display
    formattedDate() {
      const dateObj = new Date(this.currentDate)
      const dayOfWeek = dateObj.toLocaleDateString('en-US', { weekday: 'short' })
      const dayOfMonth = dateObj.getDate()
      return `${dayOfWeek} ${dayOfMonth}`
    },
    currentMonth() {
      const dateObj = new Date(this.currentDate)

      return dateObj.toLocaleDateString('en-US', { year: 'numeric', month: 'long' })
    }
  },
  mounted() {
    this.updateGames()
    this.updateSelectedWeek()
    this.updateGamesBoxScores(this.currentDate)
  },
  methods: {
    async updateGames() {
      const games = await fetchGames(this.currentDate)
      this.games = games
    },
    async updateGamesBoxScores(date: string) {
      const gamesBoxScores = await getBoxScores(date)
      console.log(gamesBoxScores)
      this.gamesBoxScores = gamesBoxScores
    },
    updateSelectedWeek() {
      // Update the selected week's days based on the current date
      const selectedDate = new Date(this.currentDate)
      const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']

      this.selectedWeek = []

      // Get the day of the week for the current date (0 = Sunday, 1 = Monday, etc.)
      const dayOfWeek = selectedDate.getDay()

      // Calculate the date for the first day of the week (Sunday) using the current date
      const sundayDate = new Date(selectedDate)
      sundayDate.setDate(selectedDate.getDate() - dayOfWeek)

      // Fill selectedWeek with the days of the week
      for (let i = 0; i < 7; i++) {
        const day = new Date(sundayDate)
        day.setDate(sundayDate.getDate() + i)
        this.selectedWeek.push(daysOfWeek[day.getDay()] + ' ' + day.getDate())
      }
    },
    selectDate(selectedDate: any) {
      // Close the calendar and update currentDate
      this.showCalendar = false
      this.currentDate = selectedDate
      // Fetch NBA games for the new date
      this.updateGames()
      this.updateSelectedWeek()
    },
    toggleCalendar() {
      console.log(this.currentDate)
      console.log(this.games)
      // Toggle the calendar visibility
      this.showCalendar = !this.showCalendar
    },
    selectDay(day: string) {
      // Extract the day number from the selected day string
      const dayParts = day.split(' ')
      const dayNumber = parseInt(dayParts[1], 10)

      // Create a new date with the selected day
      const selectedDate = new Date(this.currentDate)
      selectedDate.setDate(dayNumber)

      // Update currentDate with the selected date
      this.currentDate = selectedDate.toISOString().split('T')[0]

      // Fetch NBA games for the new date
      this.updateGames()
    },
    prevWeek() {
      // Go back one week by subtracting 7 days from currentDate
      const prevDate = new Date(this.currentDate)
      prevDate.setDate(prevDate.getDate() - 7)
      this.currentDate = prevDate.toISOString().split('T')[0]

      // Fetch NBA games for the new week and update selectedWeek
      this.updateGames()
      this.updateSelectedWeek()
    },
    nextWeek() {
      // Advance one week by adding 7 days to currentDate
      const nextDate = new Date(this.currentDate)
      nextDate.setDate(nextDate.getDate() + 7)
      this.currentDate = nextDate.toISOString().split('T')[0]

      // Fetch NBA games for the new week and update selectedWeek
      this.updateGames()
      this.updateSelectedWeek()
    }
  }
}
</script>

<style scoped>
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
  margin-right: 10px; /* Adjust the spacing between the icon and input field as needed */
  font-size: 1.25rem; /* Adjust the icon size as needed */
  color: #888; /* Adjust the icon color as needed */
  display: flex;
  align-items: center;
  cursor: pointer;
  gap: 4rem;
}

#calendar-input {
  display: none; /* Hide the input field */
}

.game-list {
  display: flex;
  flex-wrap: wrap;
}
</style>
