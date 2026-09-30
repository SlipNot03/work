<script setup>
import { computed } from 'vue'
import { articles } from '../data/articles'
import CategoryBlock from '../components/CategoryBlock.vue'
import NewsCard from '../components/NewsCard.vue'
import PopularList from '../components/PopularList.vue'

const latestArticles = articles.slice(0, 8)

const popularArticles = computed(() => {
  return [...articles].sort((a, b) => b.views - a.views).slice(0, 5)
})

const vacancyArticles = computed(() => {
  return articles.filter((article) => article.category === 'vacancies').slice(0, 3)
})

const careerArticles = computed(() => {
  return articles.filter((article) => article.category === 'career').slice(0, 3)
})
</script>

<template>
  <main class="home-page">
    <section class="page-intro">
      <p class="panel-eyebrow">Работа, вакансии, карьера</p>
      <h1>Все, что помогает найти работу и не потеряться в рынке.</h1>
    </section>

    <section class="news-board">
      <NewsCard
        v-for="article in latestArticles"
        :key="article.id"
        :article="article"
      />
    </section>

    <PopularList :articles="popularArticles" />
    <CategoryBlock title="Вакансии" :articles="vacancyArticles" />
    <CategoryBlock title="Карьера" :articles="careerArticles" />
  </main>
</template>
