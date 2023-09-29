<template>
  <div class="date-picker">
    <button @click="$emit('prevWeek')">&lt;</button>
    <!-- Render the days of the selected week -->
    <div
      class="day-button"
      v-for="day in selectedWeek"
      :key="day"
      @click="selectDay(day)"
      :class="{ selected: isDateSelected(day) }"
    >
      {{ day }}
    </div>
    <button @click="$emit('nextWeek')">&gt;</button>
  </div>
</template>

<script setup lang="ts">
// Define an emit function to emit custom events to the parent component
const emit = defineEmits(['selectDay', 'prevWeek', 'nextWeek'])

const props = defineProps({
  formattedDate: String,
  selectedWeek: Array as () => string[]
})

// Check if the day is selected. If it is, add the 'selected' class
const isDateSelected = (day: string) => day === props.formattedDate

// Select a specific day and emit the 'selectDay' event to the parent (NbaCalendar.vue)
const selectDay = (day: string) => emit('selectDay', day)
</script>

<style scoped>
.date-picker {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 20px;
}
.date-picker button {
  background-color: transparent;
  border: none;
  cursor: pointer;
  color: white;
  font-size: 2rem;
}
.day-button {
  cursor: pointer;
  padding: 5px;
}
.day-button:hover {
  opacity: 0.7;
}
.selected {
  background-color: #007bff;
  color: #fff;
}
.active {
  background-color: #f0f0f0;
  color: #000;
}

@media (max-width: 500px) {
  .date-picker {
    gap: 0;
    font-size: 14px;
  }
}
</style>
