import { defineStore } from 'pinia'

export const useTeamStore = defineStore('team', {
  state: () => ({
    homeTeamInfo: null,
    visitorTeamInfo: null
  }),
  actions: {
    setHomeTeamInfo(info: any) {
      this.homeTeamInfo = info
    },
    setVisitorTeamInfo(info: any) {
      this.visitorTeamInfo = info
    }
  }
})
