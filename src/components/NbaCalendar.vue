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
import { useGamesStore } from '../stores/GamesStore'

export default {
  components: {
    DatePicker,
    CalendarPicker,
    GameCard
  },
  data() {
    return {
      games: [] as any[], // Data for the NBA games.
      currentDate: '2023-05-09', // Base date
      selectedWeek: [] as string[], // The days of the week that is selected.
      showCalendar: false // Boolean to show or hide the calendar.
    }
  },
  watch: {
    // Watch for changes in currentDate and trigger updates
    currentDate(newDate, oldDate) {
      if (newDate !== oldDate) {
        this.updateGames()
        this.updateGamesBoxScores(newDate)
      }
    }
  },
  computed: {
    gamesBoxScores() {
      return useGamesStore().gamesBoxScores
    },
    // Format the date to be displayed in the calendar
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
    // Initial setup when the component is mounted
    this.updateGames()
    this.updateSelectedWeek()
    this.updateGamesBoxScores(this.currentDate)
  },
  methods: {
    async updateGames() {
      try {
        // Fetch NBA games for the current date
        const games = await fetchGames(this.currentDate)
        this.games = games
        console.log(this.games)
      } catch (error) {
        console.error('Erreur lors de la mise à jour des matchs :', error)
      }
    },
    async updateGamesBoxScores(date: string) {
      // Fetch NBA games box scores for the given date
      const gamesBoxScores = await getBoxScores(date)
      // Update the store with the fetched data
      useGamesStore().setGamesBoxScores(gamesBoxScores)
    },
    updateSelectedWeek() {
      // Update the selected week's days based on the current date
      const selectedDate = new Date(this.currentDate) // Get the current date as a JavaScript Date object
      const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'] // Abbreviated day names

      this.selectedWeek = [] // Initialize an empty array to store the days of the selected week

      // Get the day of the week for the current date (0 = Sunday, 1 = Monday, etc.)
      const dayOfWeek = selectedDate.getDay()

      // Calculate the date for the first day of the week (Sunday) using the current date
      const sundayDate = new Date(selectedDate)
      sundayDate.setDate(selectedDate.getDate() - dayOfWeek)

      // Fill selectedWeek with the days of the week
      for (let i = 0; i < 7; i++) {
        const day = new Date(sundayDate)
        day.setDate(sundayDate.getDate() + i)
        // Returns a string like "Mon 1", "Tue 2", etc.
        const formattedDay = daysOfWeek[day.getDay()] + ' ' + day.getDate()
        this.selectedWeek.push(formattedDay) // Add the formatted day to the selectedWeek array
        // console.log(this.selectedWeek)
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
  margin-right: 10px;
  font-size: 1.25rem;
  color: #888;
  display: flex;
  align-items: center;
  cursor: pointer;
  gap: 4rem;
}

#calendar-input {
  display: none;
}

.game-list {
  display: flex;
  flex-wrap: wrap;
}
</style>

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
  margin-right: 10px;
  font-size: 1.25rem;
  color: #888;
  display: flex;
  align-items: center;
  cursor: pointer;
  gap: 4rem;
}

#calendar-input {
  display: none;
}

.game-list {
  display: flex;
  flex-wrap: wrap;
}
</style>
