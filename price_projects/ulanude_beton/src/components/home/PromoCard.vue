<script setup>
defineProps({
  promo: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(['request']);
</script>

<template>
  <article v-card-tilt class="promo-card card-hover-motion">
    <img :src="promo.image" :alt="promo.title" />
    <div>
      <div>
        <h3>{{ promo.title }}</h3>
        <p>{{ promo.text }}</p>
      </div>
      <button type="button" @click="emit('request')">Рассчитать заказ</button>
    </div>
  </article>
</template>
