<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { articles } from '../data/articles'
import { categories } from '../data/categories'
import NewsCard from '../components/NewsCard.vue'

const route = useRoute()

const category = computed(() => {
  return categories.find((item) => item.slug === route.params.slug)
})

const categoryArticles = computed(() => {
  return articles.filter((article) => article.category === route.params.slug)
})
</script>

<template>
  <main>
    <section>
      <h1>{{ category?.title || 'Рубрика' }}</h1>
      <p>{{ category?.description || 'Материалы выбранной рубрики.' }}</p>
    </section>

    <section>
      <div v-if="categoryArticles.length" class="fresh-feed">
        <NewsCard
          v-for="article in categoryArticles"
          :key="article.id"
          :article="article"
        />
      </div>

      <p v-else>В этой рубрике пока нет материалов.</p>
    </section>
  </main>
</template>
