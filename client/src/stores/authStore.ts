import { defineStore } from 'pinia'
import type { UserIdentityModel } from '@/types/auth.ts'
import tokenService from '@/services/tokenService.ts'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: null as string | null,
    user: null as UserIdentityModel | null
  }),

  getters: {
    isLoggedIn: (state) => !!state.token
  },

  actions: {
    authenticate(identity: UserIdentityModel) {
      this.user = identity
      const token = identity.token || tokenService.getToken()
      this.setToken(token || '')
    },

    setToken(token: string) {
      this.token = token
      tokenService.setToken(token)
    },

    logout() {
      this.token = null
      this.user = null
      tokenService.removeToken()
    }
  }
})
