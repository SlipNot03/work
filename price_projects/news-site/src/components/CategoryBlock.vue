<script setup>
import NewsCard from './NewsCard.vue'

defineProps({
  title: {
    type: String,
    required: true,
  },
  articles: {
    type: Array,
    default: () => [],
  },
})
</script>

<template>
  <section class="category-block reveal">
    <h2 class="section-title">{{ title }}</h2>

    <div class="category-block-grid">
      <NewsCard
        v-for="article in articles"
        :key="article.id"
        :article="article"
      />
    </div>
  </section>
</template>