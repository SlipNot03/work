<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { articles } from '../data/articles'
import { tags } from '../data/tags'
import NewsCard from '../components/NewsCard.vue'

const route = useRoute()

const tag = computed(() => {
  return tags.find((item) => item.slug === route.params.slug)
})

const tagArticles = computed(() => {
  return articles.filter((article) => article.tags.includes(route.params.slug))
})
</script>

<template>
  <main>
    <section>
      <h1>#{{ tag?.title || route.params.slug }}</h1>
      <p>Все материалы по выбранной теме.</p>
    </section>

    <section>
      <div v-if="tagArticles.length" class="fresh-feed">
        <NewsCard
          v-for="article in tagArticles"
          :key="article.id"
          :article="article"
        />
      </div>

      <p v-else>Материалы по этому тегу пока не опубликованы.</p>
    </section>
  </main>
</template>
