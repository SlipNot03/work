<script setup>
defineProps({
  items: {
    type: Array,
    default: () => [],
  },
})
</script>

<template>
  <nav class="breadcrumbs reveal" aria-label="Хлебные крошки">
    <span>
      <RouterLink to="/">Главная</RouterLink>
    </span>
    <span
      v-for="item in items"
      :key="item.to || item.label"
    >
      <RouterLink v-if="item.to" :to="item.to">{{ item.label }}</RouterLink>
      <span v-else>{{ item.label }}</span>
    </span>
  </nav>
</template>