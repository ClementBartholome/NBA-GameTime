import { defineStore } from 'pinia'

interface TeamInfo {
  gameId: number
  homeTeamInfo: any
  visitorTeamInfo: any
}

export const useTeamStore = defineStore('team', {
  state: () => ({
    teamsInfo: [] as TeamInfo[]
  }),
  actions: {
    // Add team information for a specific game
    addTeamInfo(teamInfo: TeamInfo) {
      this.teamsInfo.push(teamInfo)
    },
    // Retrieve team information for a specific game
    getTeamInfo(gameId: number): TeamInfo | undefined {
      return this.teamsInfo.find((info) => info.gameId === gameId)
    }
  }
})
