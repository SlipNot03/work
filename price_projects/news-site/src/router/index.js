import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('../pages/HomePage.vue'),
    meta: {
      title: 'Главная',
      description: 'Новости о работе, вакансиях, карьере и рынке труда.',
    },
  },
  {
    path: '/category/:slug',
    alias: '/rubrika/:slug',
    name: 'category',
    component: () => import('../pages/CategoryPage.vue'),
    meta: {
      title: 'Рубрика',
      description: 'Материалы выбранной рубрики.',
    },
  },
  {
    path: '/news/:slug',
    name: 'article',
    component: () => import('../pages/ArticlePage.vue'),
    meta: {
      title: 'Материал',
      description: 'Статья о работе, вакансиях или карьере.',
    },
  },
  {
    path: '/search',
    name: 'search',
    component: () => import('../pages/SearchPage.vue'),
    meta: {
      title: 'Поиск',
      description: 'Поиск по статьям, вакансиям, тегам и авторам.',
    },
  },
  {
    path: '/tag/:slug',
    name: 'tag',
    component: () => import('../pages/TagPage.vue'),
    meta: {
      title: 'Тег',
      description: 'Материалы по выбранной теме.',
    },
  },
  {
    path: '/author/:slug',
    name: 'author',
    component: () => import('../pages/AuthorPage.vue'),
    meta: {
      title: 'Автор',
      description: 'Профиль автора и список публикаций.',
    },
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('../pages/StaticPage.vue'),
    meta: {
      title: 'О компании',
      description: 'Информация о NewsCity.',
    },
  },
  {
    path: '/ads',
    name: 'ads',
    component: () => import('../pages/StaticPage.vue'),
    meta: {
      title: 'Реклама',
      description: 'Рекламные возможности NewsCity.',
    },
  },
  {
    path: '/contacts',
    name: 'contacts',
    component: () => import('../pages/StaticPage.vue'),
    meta: {
      title: 'Контакты',
      description: 'Контакты редакции NewsCity.',
    },
  },
  {
    path: '/terms',
    name: 'terms',
    component: () => import('../pages/StaticPage.vue'),
    meta: {
      title: 'Пользовательское соглашение',
      description: 'Правила использования сайта.',
    },
  },
  {
    path: '/privacy',
    name: 'privacy',
    component: () => import('../pages/StaticPage.vue'),
    meta: {
      title: 'Политика конфиденциальности',
      description: 'Правила обработки персональных данных.',
    },
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'not-found',
    component: () => import('../pages/NotFoundPage.vue'),
    meta: {
      title: 'Страница не найдена',
      description: 'Страница не найдена.',
    },
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { top: 0 }
  },
})

router.afterEach((to) => {
  document.title = to.meta.title ? `${to.meta.title} | NewsCity` : 'NewsCity'
})

export default router
