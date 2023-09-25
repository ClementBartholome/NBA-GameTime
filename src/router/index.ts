import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import StandingsView from '../views/StandingsView.vue'
import StatsView from '../views/StatsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
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
    }
  ]
})

export default router
