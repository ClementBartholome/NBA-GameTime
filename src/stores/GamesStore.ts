// stores/GamesStore.ts
import { defineStore } from 'pinia'

export const useGamesStore = defineStore('games', {
  state: () => ({
    gamesBoxScores: [] as Object[]
  }),
  actions: {
    setGamesBoxScores(boxScores: Object[]) {
      this.gamesBoxScores = boxScores
    }
  }
})
