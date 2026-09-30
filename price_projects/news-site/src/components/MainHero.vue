<script setup>
defineProps({
  article: {
    type: Object,
    required: true,
  },
})
</script>

<template>
  <section class="main-hero reveal">
    <RouterLink :to="`/news/${article.slug}`" class="hero-image">
      <img :src="article.image" :alt="article.title">
    </RouterLink>

    <div class="hero-content">
      <span class="article-kicker">Главное</span>
      <h1>{{ article.title }}</h1>
      <p>{{ article.lead }}</p>
      <RouterLink :to="`/news/${article.slug}`">Читать материал</RouterLink>
    </div>
  </section>
</template>