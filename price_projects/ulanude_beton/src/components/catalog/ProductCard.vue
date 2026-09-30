<script setup>
defineProps({
  product: {
    type: Object,
    required: true,
  },
});

const emit = defineEmits(['calculate']);
</script>

<template>
  <article v-card-tilt class="product-card card-hover-motion">
    <img :src="product.image" :alt="product.title" />
    <div>
      <div class="product-card__meta">
        <span>{{ product.category }}</span>
        <span>{{ product.inStock }}</span>
      </div>
      <h2>{{ product.title }}</h2>
      <p>{{ product.purpose }}</p>
      <dl>
        <div>
          <dt>Размер</dt>
          <dd>{{ product.size }}</dd>
        </div>
        <div>
          <dt>Плотность</dt>
          <dd>{{ product.density }}</dd>
        </div>
        <div>
          <dt>Прочность</dt>
          <dd>{{ product.strength }}</dd>
        </div>
        <div>
          <dt>Морозостойкость</dt>
          <dd>{{ product.frost }}</dd>
        </div>
      </dl>
      <footer>
        <strong>{{ product.price }}</strong>
        <button type="button" @click="emit('calculate')">Рассчитать объем</button>
      </footer>
    </div>
  </article>
</template>
