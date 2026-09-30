<script setup>
defineProps({
  groups: {
    type: Array,
    required: true,
  },
});

const emit = defineEmits(['order']);
</script>

<template>
  <div class="order-table">
    <section v-for="group in groups" :key="group.group">
      <h3>{{ group.group }}</h3>
      <div class="data-table">
        <table>
          <thead>
            <tr>
              <th>Размер</th>
              <th>Шт. на поддоне</th>
              <th>м3 на поддоне</th>
              <th>Плотность</th>
              <th>Цена</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="row in group.rows" :key="`${group.group}-${row.size}-${row.density}`">
              <td>{{ row.size }}</td>
              <td>{{ row.palletPieces }}</td>
              <td>{{ row.palletVolume }}</td>
              <td>{{ row.density }}</td>
              <td>{{ row.price }}</td>
              <td>
                <button type="button" @click="emit('order')">Оставить заявку</button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </section>
  </div>
</template>
