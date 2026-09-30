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
    required: true,
  },
  buttonText: {
    type: String,
    required: true,
  },
});

const emit = defineEmits(['action']);
</script>

<template>
  <section class="cta-panel">
    <div>
      <p v-if="eyebrow" class="eyebrow">{{ eyebrow }}</p>
      <h2>{{ title }}</h2>
      <p>{{ text }}</p>
    </div>
    <button type="button" @click="emit('action')">{{ buttonText }}</button>
  </section>
</template>
