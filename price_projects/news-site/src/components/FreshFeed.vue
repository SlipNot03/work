<script setup>
import NewsCard from './NewsCard.vue'

defineProps({
  articles: {
    type: Array,
    required: true,
  },
})
</script>

<template>
  <section class="reveal">
    <h2>Свежее</h2>

    <div class="fresh-feed">
      <NewsCard
        v-for="article in articles"
        :key="article.id"
        :article="article"
      />
    </div>
  </section>
</template>