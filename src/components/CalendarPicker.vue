<template>
  <div class="calendar-container">
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
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'

const emit = defineEmits(['inFocus', 'submit', 'select'])

const props = defineProps({
  currentDate: String
})

const selectedDate = ref(
  props.currentDate ? new Date(props.currentDate.replace(/-/g, '/')) : new Date()
)
const calendar = ref([] as Date[][])
const currentMonth = ref('')

const daysOfWeek = computed(() => ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'])

watch(
  () => props.currentDate,
  (newDate) => {
    if (newDate !== undefined) {
      selectedDate.value = new Date(newDate.replace(/-/g, '/'))
      createCalendar()
      currentMonth.value = selectedDate.value.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long'
      })
    }
  }
)

onMounted(() => {
  createCalendar()
  currentMonth.value = selectedDate.value.toLocaleString('en-US', {
    year: 'numeric',
    month: 'long'
  })
})

const selectDate = (date: Date) => {
  selectedDate.value = date
  emit('select', date.toISOString().split('T')[0])
}

const isDateSelected = (date: Date) => {
  return date.toISOString().split('T')[0] === props.currentDate
}

const prevMonth = () => {
  selectedDate.value.setMonth(selectedDate.value.getMonth() - 1)
  createCalendar()
  currentMonth.value = selectedDate.value.toLocaleString('en-US', {
    year: 'numeric',
    month: 'long'
  })
}

const nextMonth = () => {
  selectedDate.value.setMonth(selectedDate.value.getMonth() + 1)
  createCalendar()
  currentMonth.value = selectedDate.value.toLocaleString('en-US', {
    year: 'numeric',
    month: 'long'
  })
}

const createCalendar = () => {
  const year = selectedDate.value.getFullYear()
  const month = selectedDate.value.getMonth()
  const lastDay = new Date(year, month + 1, 0)
  const daysInMonth = lastDay.getDate()
  const firstDay = new Date(year, month, 1)
  const startOffset = firstDay.getDay()

  calendar.value = []
  let week: Date[] = []

  for (let day = 1 - startOffset; day <= daysInMonth; day++) {
    const currentDate = new Date(Date.UTC(year, month, day))
    week.push(currentDate)
    if (week.length === 7) {
      calendar.value.push(week)
      week = []
    }
  }

  if (week.length > 0) {
    calendar.value.push(week)
  }
}
</script>

<style scoped>
.calendar-container {
  display: flex;
  justify-content: center;
  position: absolute;
  top: 150px;
  left: 0;
  right: 0;
  bottom: 0;
}

.calendar-picker {
  text-align: center;
  border: 1px solid #ccc;
  z-index: 999;
  background-color: #f0f0f0;
  color: black;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  width: 320px;
  height: fit-content;
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
