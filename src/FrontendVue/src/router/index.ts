import { createRouter, createWebHistory } from 'vue-router'
import ThingsPage from '../components/ThingsPage.vue'
import ThingsDetailPage from '../components/ThingsDetailPage.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'things',
      component: ThingsPage
    },
    {
      path: "/things/:id",
      name: "things_detail",
      component: ThingsDetailPage,
      props: true,
    },
  ]
})

export default router
