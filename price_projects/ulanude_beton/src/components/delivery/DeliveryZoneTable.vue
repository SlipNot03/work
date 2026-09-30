<script setup>
defineProps({
  zones: {
    type: Array,
    required: true,
  },
});
</script>

<template>
  <div class="data-table">
    <table>
      <thead>
        <tr>
          <th>Зона</th>
          <th>Расстояние</th>
          <th>Транспорт</th>
          <th>Условия</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="zone in zones" :key="zone.zone">
          <td>{{ zone.zone }}</td>
          <td>{{ zone.distance }}</td>
          <td>{{ zone.vehicle }}</td>
          <td>{{ zone.terms }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
