import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import StandingsView from '../views/StandingsView.vue'
import StatsView from '../views/StatsView.vue'
import BoxScoreView from '../views/BoxScoreView.vue'
import PlayerView from '../views/PlayerView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/standings',
      name: 'standings',
      component: StandingsView
    },
    {
      path: '/stats',
      name: 'stats',
      component: StatsView
    },
    {
      path: '/boxscore/:gameId-:homeTeamName-:visitorTeamName',
      name: 'BoxScoreView',
      component: BoxScoreView
    },
    {
      path: '/player/:playerId',
      name: 'PlayerView',
      component: PlayerView
    }
  ]
})

export default router
