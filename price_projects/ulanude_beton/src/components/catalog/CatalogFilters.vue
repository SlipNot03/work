<script setup>
defineProps({
  categories: {
    type: Array,
    required: true,
  },
  densities: {
    type: Array,
    required: true,
  },
  selectedCategory: {
    type: String,
    required: true,
  },
  selectedDensity: {
    type: String,
    required: true,
  },
});

const emit = defineEmits(['update:selectedCategory', 'update:selectedDensity', 'reset']);
</script>

<template>
  <div class="catalog-filters">
    <label>
      Категория
      <select
        :value="selectedCategory"
        @change="emit('update:selectedCategory', $event.target.value)"
      >
        <option v-for="category in categories" :key="category" :value="category">
          {{ category }}
        </option>
      </select>
    </label>

    <label>
      Плотность
      <select
        :value="selectedDensity"
        @change="emit('update:selectedDensity', $event.target.value)"
      >
        <option v-for="density in densities" :key="density" :value="density">
          {{ density }}
        </option>
      </select>
    </label>

    <button type="button" @click="emit('reset')">Сбросить</button>
  </div>
</template>
