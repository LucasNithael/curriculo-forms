import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'cadastrar',
      meta: { requiresAuth: true },
      component: () => import('@/pages/curriculo/cadastrar.vue')
    },
    {
      path: '/curriculos',
      name: 'listar',
      meta: { requiresAuth: true },
      component: () => import('@/pages/curriculo/listar.vue')
    },
  ]
})

export default router