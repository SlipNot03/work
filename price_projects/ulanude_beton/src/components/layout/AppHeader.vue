<script setup>
import { ref, watch } from 'vue';
import { company } from '../../data/siteData';

defineProps({
  pages: {
    type: Array,
    required: true,
  },
  currentPage: {
    type: String,
    required: true,
  },
});

const emit = defineEmits(['navigate']);
const isMenuOpen = ref(false);

watch(isMenuOpen, (isOpen) => {
  document.body.classList.toggle('menu-open', isOpen);
});

function navigate(pageId) {
  isMenuOpen.value = false;
  emit('navigate', pageId);
}
</script>

<template>
  <header class="header-shell">
    <div class="top-line">
      <a v-if="company.emailHref" :href="company.emailHref">Отправить проект: {{ company.email }}</a>
      <span v-else>Отправить проект: {{ company.email }}</span>
      <a v-if="company.phoneHref" :href="company.phoneHref">{{ company.phone }}</a>
      <span v-else>{{ company.phone }}</span>
    </div>

    <div class="site-header">
      <button class="logo" type="button" @click="navigate('home')">
        <span>УУ</span>
        Бетон
      </button>

      <button
        class="menu-toggle"
        type="button"
        :aria-expanded="isMenuOpen"
        aria-label="Открыть меню"
        @click="isMenuOpen = !isMenuOpen"
      >
        <span></span>
        <span></span>
        <span></span>
      </button>

      <nav class="site-nav" :class="{ open: isMenuOpen }">
        <button
          v-for="page in pages"
          :key="page.id"
          type="button"
          :class="{ active: currentPage === page.id }"
          @click="navigate(page.id)"
        >
          {{ page.label }}
        </button>
      </nav>

      <button class="buy-button" type="button" @click="navigate('catalog')">
        Заказать
      </button>
    </div>
  </header>
</template>
