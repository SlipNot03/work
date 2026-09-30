<script setup>
import { computed, ref } from 'vue'
import { articles } from '../data/articles'
import NewsCard from '../components/NewsCard.vue'

const query = ref('')

const results = computed(() => {
  const value = query.value.toLowerCase().trim()

  if (!value) {
    return articles.slice(0, 6)
  }

  return articles.filter((article) => {
    return (
      article.title.toLowerCase().includes(value) ||
      article.lead.toLowerCase().includes(value) ||
      article.tags.some((tag) => tag.toLowerCase().includes(value)) ||
      article.author.toLowerCase().includes(value) ||
      article.category.toLowerCase().includes(value)
    )
  })
})
</script>

<template>
  <main>
    <section>
      <h1>Поиск</h1>

      <input
        v-model="query"
        type="search"
        placeholder="Введите тему, автора или рубрику"
      >
    </section>

    <section>
      <div v-if="results.length" class="fresh-feed">
        <NewsCard
          v-for="article in results"
          :key="article.id"
          :article="article"
        />
      </div>

      <p v-else>По вашему запросу ничего не найдено.</p>
    </section>
  </main>
</template>
