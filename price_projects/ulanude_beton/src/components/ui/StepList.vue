<script setup>
defineProps({
  steps: {
    type: Array,
    required: true,
  },
});
</script>

<template>
  <div class="steps">
    <article v-for="step in steps" :key="step.number">
      <span>{{ step.number }}</span>
      <h2>{{ step.title }}</h2>
      <p>{{ step.text }}</p>
    </article>
  </div>
</template>
