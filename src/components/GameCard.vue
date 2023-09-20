<template>
  <div class="game-card" v-if="game">
    <div class="home-team">
      <img :src="homeTeamLogo" alt="Logo de l'équipe domicile" />
      <h2>{{ game.home_team.full_name }}</h2>
    </div>
    <div class="away-team">
      <img :src="visitorTeamLogo" alt="Logo de l'équipe visiteur" />
      <h2>{{ game.visitor_team.full_name }}</h2>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'
import { getTeamLogo } from '../util/NbaApi'

export default {
  props: {
    game: Object
  },
  setup(props) {
    const homeTeamLogo = ref('')
    const visitorTeamLogo = ref('')

    onMounted(async () => {
      if (props.game) {
        homeTeamLogo.value = await getTeamLogo(props.game.home_team.name)
        visitorTeamLogo.value = await getTeamLogo(props.game.visitor_team.name)
      }
    })

    return {
      homeTeamLogo,
      visitorTeamLogo
    }
  }
}
</script>

<style scoped>
.game-card {
  display: flex;
  justify-content: center;
  gap: 1.5rem;
  border: 1px solid #ccc;
  padding: 1rem;
  margin: 1rem;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  background-color: #071a21;
  color: #ffffff;
  transition: transform 0.2s ease-in-out;
}

.home-team,
.away-team {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.game-card img {
  width: 75px;
  height: 75px;
}

.game-card:hover {
  transform: scale(1.02);
}
</style>
