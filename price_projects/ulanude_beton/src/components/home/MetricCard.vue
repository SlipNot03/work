<script setup>
defineProps({
  value: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    required: true,
  },
});
</script>

<template>
  <article>
    <strong>{{ value }}</strong>
    <span>{{ label }}</span>
  </article>
</template>
