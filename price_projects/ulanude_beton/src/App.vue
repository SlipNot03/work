<script setup>
import { computed } from 'vue';
import { RouterView, useRoute, useRouter } from 'vue-router';
import AppFooter from './components/layout/AppFooter.vue';
import AppHeader from './components/layout/AppHeader.vue';
import { navigationPages } from './data/siteData';
import { pageRoutes } from './router';

const route = useRoute();
const router = useRouter();
const currentPage = computed(() => route.meta.pageId ?? 'home');

function navigateTo(pageId) {
  router.push(pageRoutes[pageId] ?? pageRoutes.home).then(() => {
    if (pageId === 'contacts') {
      document.getElementById('contacts')?.scrollIntoView({ behavior: 'smooth' });
    }
  });
}
</script>

<template>
  <AppHeader
    :pages="navigationPages"
    :current-page="currentPage"
    @navigate="navigateTo"
  />

  <main>
    <RouterView v-slot="{ Component }">
      <component :is="Component" @navigate="navigateTo" />
    </RouterView>
  </main>

  <AppFooter />
</template>
