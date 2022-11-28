import { createRouter, createWebHistory } from 'vue-router'
import ThingsPage from '../components/ThingsPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'things',
      component: ThingsPage
    },
  ]
})

export default router
