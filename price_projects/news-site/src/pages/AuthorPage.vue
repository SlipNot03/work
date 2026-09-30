<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { articles } from '../data/articles'
import { authors } from '../data/authors'
import AuthorCard from '../components/AuthorCard.vue'
import NewsCard from '../components/NewsCard.vue'

const route = useRoute()

const author = computed(() => {
  return authors.find((item) => item.slug === route.params.slug)
})

const authorArticles = computed(() => {
  return articles.filter((article) => article.author === route.params.slug)
})
</script>

<template>
  <main v-if="author">
    <section>
      <AuthorCard :author="author" heading-tag="h1" show-role />
    </section>

    <section>
      <h2>Публикации автора</h2>

      <div class="fresh-feed">
        <NewsCard
          v-for="article in authorArticles"
          :key="article.id"
          :article="article"
        />
      </div>
    </section>
  </main>

  <main v-else>
    <h1>Автор не найден</h1>
    <RouterLink to="/">На главную</RouterLink>
  </main>
</template>
