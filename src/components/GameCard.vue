<template>
  <div class="game-card" v-if="game">
    <div class="home-team" :style="{ backgroundColor: homeTeamColor }">
      <img :src="homeTeamLogo" alt="Logo de l'équipe domicile" />
      <h2>{{ game.home_team.full_name }}</h2>
    </div>
    <div class="away-team" :style="{ backgroundColor: visitorTeamColor }">
      <img :src="visitorTeamLogo" alt="Logo de l'équipe visiteur" />
      <h2>{{ game.visitor_team.full_name }}</h2>
    </div>
  </div>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue'
import { getTeamInfo } from '../util/NbaApi'

export default {
  props: {
    game: Object
  },
  setup(props) {
    const homeTeamLogo = ref('')
    const visitorTeamLogo = ref('')
    const homeTeamColor = ref('')
    const visitorTeamColor = ref('')

    onMounted(async () => {
      if (props.game) {
        const homeTeamInfo = await getTeamInfo(props.game.home_team.name)
        console.log(homeTeamInfo)
        const visitorTeamInfo = await getTeamInfo(props.game.visitor_team.name)

        homeTeamLogo.value = homeTeamInfo.logo
        visitorTeamLogo.value = visitorTeamInfo.logo
        homeTeamColor.value = '#' + homeTeamInfo.primaryColor
        visitorTeamColor.value = '#' + visitorTeamInfo.primaryColor
      }
    })

    return {
      homeTeamLogo,
      visitorTeamLogo,
      homeTeamColor,
      visitorTeamColor
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
  color: #ffffff;
  transition: transform 0.2s ease-in-out;
}

.game-card img {
  background-color: white;
  padding: 0.5rem;
  height: 100px;
  width: 150px;
  border-radius: 20px;
}

.home-team,
.away-team {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 1rem;
  border-radius: 5px;
  width: 300px;
}

.game-card:hover {
  transform: scale(1.02);
}
</style>
