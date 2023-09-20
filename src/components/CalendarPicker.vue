<template>
  <div class="calendar-picker">
    <div class="calendar-header">
      <button @click="prevMonth">&lt;</button>
      <span>{{ currentMonth }}</span>
      <button @click="nextMonth">&gt;</button>
    </div>
    <table class="calendar-table">
      <!-- Table headers -->
      <thead>
        <tr>
          <th v-for="day in daysOfWeek" :key="day">{{ day }}</th>
        </tr>
      </thead>
      <!-- Table body with days -->
      <tbody>
        <tr v-for="week in calendar" :key="week[0].toString()">
          <td
            v-for="day in week"
            :key="day.toString()"
            @click="selectDate(day)"
            :class="{ selected: isDateSelected(day) }"
          >
            {{ day.getDate() }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
export default {
  props: {
    currentDate: String // Date format: 'YYYY-MM-DD'
  },
  data() {
    return {
      selectedDate: this.currentDate ? new Date(this.currentDate.replace(/-/g, '/')) : new Date(),
      calendar: [] as Date[][],
      currentMonth: ''
    }
  },
  computed: {
    daysOfWeek() {
      return ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
    }
  },
  watch: {
    currentDate(newDate) {
      // Update selectedDate when currentDate prop changes
      this.selectedDate = new Date(newDate.replace(/-/g, '/'))
      // Recalculate the calendar for the new month
      this.calculateCalendar()
      // Update currentMonth
      this.currentMonth = this.selectedDate.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long'
      })
    }
  },
  mounted() {
    // Calculate the initial calendar
    this.calculateCalendar()
    // Set currentMonth initially
    this.currentMonth = this.selectedDate.toLocaleString('en-US', {
      year: 'numeric',
      month: 'long'
    })
  },
  methods: {
    selectDate(date: Date) {
      console
      this.selectedDate = date
      this.$emit('select', date.toISOString().split('T')[0])
    },
    isDateSelected(date: Date) {
      return date.toISOString().split('T')[0] === this.currentDate
    },
    prevMonth() {
      this.selectedDate.setMonth(this.selectedDate.getMonth() - 1)
      this.calculateCalendar()
      // Update currentMonth
      this.currentMonth = this.selectedDate.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long'
      })
    },
    nextMonth() {
      this.selectedDate.setMonth(this.selectedDate.getMonth() + 1)
      this.calculateCalendar()
      // Update currentMonth
      this.currentMonth = this.selectedDate.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long'
      })
    },
    calculateCalendar() {
      const year = this.selectedDate.getFullYear()
      const month = this.selectedDate.getMonth()
      const lastDay = new Date(year, month + 1, 0)
      const daysInMonth = lastDay.getDate()

      // Ajout de cette ligne pour ajuster au fuseau horaire local
      const firstDay = new Date(Date.UTC(year, month, 1))

      const startOffset = firstDay.getUTCDay() // Utiliser getUTCDay pour le jour de la semaine

      this.calendar = []
      let week: Date[] = []

      for (let day = 1 - startOffset; day <= daysInMonth; day++) {
        // Ajout de cette ligne pour ajuster au fuseau horaire local
        const currentDate = new Date(Date.UTC(year, month, day))
        week.push(currentDate)
        if (week.length === 7) {
          this.calendar.push(week)
          week = []
        }
      }

      if (week.length > 0) {
        this.calendar.push(week)
      }
    }
  }
}
</script>

<style scoped>
.calendar-picker {
  text-align: center;
  position: absolute;
  top: 120px;
  right: 542px;
  border: 1px solid #ccc;
  z-index: 999;
  background-color: #f0f0f0;
  color: black;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
}

.calendar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.calendar-header button {
  border: none;
  font-size: 1.2rem;
}

.calendar-table {
  width: 100%;
  border-collapse: collapse;
}

.calendar-table th,
.calendar-table td {
  padding: 0.5rem;
  border: 1px solid #ccc;
}

.calendar-table td:hover {
  cursor: pointer;
  background-color: #007bff;
  color: #fff;
}

.calendar-table .selected {
  background-color: #007bff;
  color: #fff;
}
</style>
