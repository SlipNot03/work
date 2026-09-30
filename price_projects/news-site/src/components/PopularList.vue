<script setup>
import NewsCard from './NewsCard.vue'

defineProps({
  articles: {
    type: Array,
    default: () => [],
  },
})
</script>

<template>
  <section class="reveal">
    <h2>Популярное</h2>

    <div class="popular-list">
      <NewsCard
        v-for="article in articles"
        :key="article.id"
        :article="article"
      />
    </div>
  </section>
</template>