<template>
  <div>
    <h1>Calendrier NBA</h1>
    <div class="calendar-input">
      <label for="calendar-input" class="calendar-icon" @click="toggleCalendar">
        <i class="far fa-calendar"></i>
        <span>{{ currentMonth }}</span>
      </label>
      <input
        type="text"
        id="calendar-input"
        v-model="currentDate"
        @change="fetchGames"
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
      @selectDay="selectDay"
      @prevWeek="prevWeek"
      @nextWeek="nextWeek"
    />

    <ul>
      <li v-for="game in games" :key="game.id">
        {{ game.home_team.full_name }} vs {{ game.visitor_team.full_name }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import axios from 'axios'
import DatePicker from './DatePicker.vue'
import CalendarPicker from './CalendarPicker.vue'

export default {
  components: {
    DatePicker,
    CalendarPicker
  },
  data() {
    return {
      games: [] as any[], // Contains NBA game data, initially an empty array.
      currentDate: '2023-10-25', // Contains the current date in 'YYYY-MM-DD' format.
      selectedWeek: [] as string[], // Contains the selected week's days as strings.
      showCalendar: false // A boolean to control the visibility of the calendar.
    }
  },
  computed: {
    // Format the current date for display
    formattedDate() {
      const dateObj = new Date(this.currentDate)
      const day = dateObj.getDate()
      const month = dateObj.getMonth() + 1
      const year = dateObj.getFullYear()

      return `${day}-${month}-${year}` // Contains the formatted current date.
    },
    currentMonth() {
      const dateObj = new Date(this.currentDate)
      // Utilisez 'fr-FR' pour le français ou 'en-US' pour l'anglais, par exemple
      return dateObj.toLocaleDateString('fr-FR', { year: 'numeric', month: 'long' })
    }
  },
  mounted() {
    // Fetch NBA games and update the selected week
    this.fetchGames()
    this.updateSelectedWeek()
  },
  methods: {
    fetchGames() {
      // Check if game data is in cache
      const cachedData = localStorage.getItem(this.currentDate)
      if (cachedData) {
        this.games = JSON.parse(cachedData)
      } else {
        // If not in cache, fetch data from the NBA API
        axios
          .get('https://www.balldontlie.io/api/v1/games', {
            params: {
              start_date: this.currentDate,
              end_date: this.currentDate
            }
          })
          .then((response) => {
            this.games = response.data.data as any[]

            // Store fetched data in local cache
            localStorage.setItem(this.currentDate, JSON.stringify(this.games))
          })
          .catch((error) => {
            console.error(error)
          })
      }
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
      this.fetchGames()
      this.updateSelectedWeek()
    },
    toggleCalendar() {
      console.log(this.currentDate)
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
      this.fetchGames()
    },
    prevWeek() {
      // Go back one week by subtracting 7 days from currentDate
      const prevDate = new Date(this.currentDate)
      prevDate.setDate(prevDate.getDate() - 7)
      this.currentDate = prevDate.toISOString().split('T')[0]

      // Fetch NBA games for the new week and update selectedWeek
      this.fetchGames()
      this.updateSelectedWeek()
    },
    nextWeek() {
      // Advance one week by adding 7 days to currentDate
      const nextDate = new Date(this.currentDate)
      nextDate.setDate(nextDate.getDate() + 7)
      this.currentDate = nextDate.toISOString().split('T')[0]

      // Fetch NBA games for the new week and update selectedWeek
      this.fetchGames()
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

.calendar-input {
  display: flex;
  align-items: center;
}

.calendar-icon {
  margin-right: 10px; /* Adjust the spacing between the icon and input field as needed */
  font-size: 1.25rem; /* Adjust the icon size as needed */
  color: #888; /* Adjust the icon color as needed */
}

#calendar-input {
  display: none; /* Hide the input field */
}
</style>
