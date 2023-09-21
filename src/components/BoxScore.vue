<template>
  <div>
    <ul>
      <li v-for="(player, index) in playerStats" :key="index">
        <div>
          <h3>{{ player.Name }}</h3>
          <p>Points: {{ player.Points }}</p>
          <p>Rebounds: {{ player.Rebounds }}</p>
          <p>Assists: {{ player.Assists }}</p>
          <p>Minutes : {{ player.Minutes }}</p>
        </div>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
export default {
  props: {
    playerStats: Array
  }
}
</script>
