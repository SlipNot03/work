import { createRouter, createWebHistory } from 'vue-router';

export const pageRoutes = {
  home: '/',
  catalog: '/catalog',
  delivery: '/delivery',
  calculator: '/calculator',
  about: '/',
  contacts: '/',
  where: '/delivery',
};

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('./pages/HomePage.vue'),
      meta: { pageId: 'home' },
    },
    {
      path: '/catalog',
      name: 'catalog',
      component: () => import('./pages/CatalogPage.vue'),
      meta: { pageId: 'catalog' },
    },
    {
      path: '/delivery',
      name: 'delivery',
      component: () => import('./pages/DeliveryPage.vue'),
      meta: { pageId: 'delivery' },
    },
    {
      path: '/calculator',
      name: 'calculator',
      component: () => import('./pages/CalculatorPage.vue'),
      meta: { pageId: 'calculator' },
    },
    {
      path: '/about',
      redirect: '/',
    },
    {
      path: '/contacts',
      redirect: '/',
    },
    {
      path: '/where-to-buy',
      redirect: '/delivery',
    },
    {
      path: '/where',
      redirect: '/delivery',
    },
    {
      path: '/:pathMatch(.*)*',
      redirect: '/',
    },
  ],
  scrollBehavior(to) {
    if (to.hash) {
      return {
        el: to.hash,
        top: 120,
      };
    }

    return { top: 0 };
  },
});
