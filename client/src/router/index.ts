import { createRouter, createWebHistory } from 'vue-router'
import MainPreLoginView from '@/views/MainPreLoginView.vue'
import LoginView from '@/views/LoginView.vue'
import HomeView from '@/views/HomeView.vue'
import NotificationView from '@/views/NotificationView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/home',
      name: 'Home',
      component: HomeView
    },
    {
      path: '/',
      name: 'MainPreLogin',
      component: MainPreLoginView,
      meta: { requiresGuest: true }
    },
    {
      path: '/login',
      name: 'Login',
      component: LoginView,
      meta: { requiresGuest: true }
    },
    {
      path: '/notifications',
      name: 'Notifications',
      component: NotificationView,
    },
  ]
})

export default router
