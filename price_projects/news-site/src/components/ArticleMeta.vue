<script setup>
import { computed } from 'vue'
import { authors } from '../data/authors'
import { categories } from '../data/categories'

const props = defineProps({
  article: {
    type: Object,
    required: true,
  },
})

const author = computed(() => {
  return authors.find((item) => item.slug === props.article.author)
})

const category = computed(() => {
  return categories.find((item) => item.slug === props.article.category)
})

const publishedDate = computed(() => {
  return new Intl.DateTimeFormat('ru-RU', {
    day: 'numeric',
    month: 'long',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  }).format(new Date(props.article.publishedAt))
})
</script>

<template>
  <div class="article-meta reveal">
    <span class="category">{{ category?.title || article.category }}</span>

    <RouterLink
      v-if="author"
      :to="`/author/${author.slug}`"
    >
      {{ author.name }}
    </RouterLink>
    <span v-else>{{ article.author }}</span>

    <time :datetime="article.publishedAt">{{ publishedDate }}</time>
  </div>
</template>