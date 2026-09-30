<script setup>
import ConcreteCalculator from '../components/calculator/ConcreteCalculator.vue';
import SectionHeader from '../components/ui/SectionHeader.vue';

const emit = defineEmits(['navigate']);
</script>

<template>
  <section class="section calc-section calc-page">
    <SectionHeader
      eyebrow="Калькулятор"
      title="Предварительный расчет газобетона для стен"
      text="Укажите габариты здания, проемы и толщину кладки. Получите ориентировочный объем, число паллет и расход клея перед обращением к менеджеру."
    />

    <ConcreteCalculator @request-calculation="emit('navigate', 'contacts')" />
  </section>
</template>
