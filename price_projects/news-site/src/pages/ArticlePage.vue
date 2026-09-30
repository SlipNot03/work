<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { articles } from '../data/articles'
import { authors } from '../data/authors'
import ArticleMeta from '../components/ArticleMeta.vue'
import AuthorCard from '../components/AuthorCard.vue'
import NewsCard from '../components/NewsCard.vue'

const route = useRoute()

const article = computed(() => {
  return articles.find((item) => item.slug === route.params.slug)
})

const author = computed(() => {
  if (!article.value) {
    return null
  }

  return authors.find((item) => item.slug === article.value.author)
})

const relatedArticles = computed(() => {
  if (!article.value) {
    return []
  }

  return articles
    .filter((item) => item.id !== article.value.id && item.category === article.value.category)
    .slice(0, 3)
})
</script>

<template>
  <main v-if="article" class="article-page">
    <h1>{{ article.title }}</h1>
    <ArticleMeta :article="article" />
    <p class="lead-text">{{ article.lead }}</p>

    <img :src="article.image" :alt="article.title">

    <div class="content">
      <p
        v-for="paragraph in article.content"
        :key="paragraph"
      >
        {{ paragraph }}
      </p>
    </div>

    <section v-if="article.tags.length">
      <h2>Темы</h2>
      <div class="share-links">
        <RouterLink
          v-for="tag in article.tags"
          :key="tag"
          :to="`/tag/${tag}`"
        >
          #{{ tag }}
        </RouterLink>
      </div>
    </section>

    <section v-if="author">
      <h2>Автор</h2>
      <AuthorCard :author="author" />
    </section>

    <section v-if="relatedArticles.length">
      <h2>Еще по теме</h2>
      <div class="fresh-feed">
        <NewsCard
          v-for="item in relatedArticles"
          :key="item.id"
          :article="item"
        />
      </div>
    </section>
  </main>

  <main v-else>
    <h1>Материал не найден</h1>
    <p>Возможно, ссылка устарела или статья была снята с публикации.</p>
    <RouterLink to="/">На главную</RouterLink>
  </main>
</template>
