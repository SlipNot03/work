<script setup>
defineProps({
  title: {
    type: String,
    default: 'Материал',
  },
})
</script>

<template>
  <div class="share-links reveal" aria-label="Поделиться материалом">
    <a :href="`https://t.me/share/url?text=${encodeURIComponent(title)}`" target="_blank" rel="noreferrer">Telegram</a>
    <a :href="`https://vk.com/share.php?title=${encodeURIComponent(title)}`" target="_blank" rel="noreferrer">VK</a>
    <a href="mailto:?subject=NewsCity">Email</a>
  </div>
</template>