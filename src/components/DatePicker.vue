<template>
  <!-- DatePicker -->
  <div class="date-picker">
    <button @click="$emit('prevWeek')">&lt;</button>
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

<script lang="ts">
export default {
  props: {
    selectedWeek: Array as () => string[],
    formattedDate: String
  },
  methods: {
    isDateSelected(day: string) {
      return day === this.formattedDate
    },
    selectDay(day: string) {
      console.log(day)
      this.$emit('selectDay', day)
    }
  }
}
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
</style>
