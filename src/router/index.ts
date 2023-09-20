import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from '../App.vue'
import HomeView from '../views/HomeView.vue'
import StandingsView from '../views/StandingsView.vue'

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
    }
  ]
})

const app = createApp(App)

app.use(router)

app.mount('#app')

export default router
