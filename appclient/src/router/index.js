import { createWebHistory, createRouter } from "vue-router";

import Home from '@/components/Home.vue'
import Account from '@/components/Account.vue'

const routes = [
  {
    path: '/',
    name: "home",
    component: Home,
  },
  {
    path: '/account',
    name: 'account',
    component: Account,
    props: true
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;