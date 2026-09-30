<script setup>
defineProps({
  eyebrow: {
    type: String,
    default: '',
  },
  title: {
    type: String,
    required: true,
  },
  text: {
    type: String,
    default: '',
  },
  centered: {
    type: Boolean,
    default: false,
  },
  level: {
    type: Number,
    default: 1,
  },
});
</script>

<template>
  <div class="section-head" :class="{ center: centered }">
    <p v-if="eyebrow" class="eyebrow">{{ eyebrow }}</p>
    <component :is="`h${level}`">{{ title }}</component>
    <p v-if="text">{{ text }}</p>
  </div>
</template>
