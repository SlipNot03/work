<script setup>
import { computed, reactive } from 'vue';
import { quoteQuestions } from '../../data/siteData';

const answers = reactive(
  quoteQuestions.reduce((result, question) => {
    result[question.id] = question.options[0];
    return result;
  }, {})
);

const summary = computed(() =>
  quoteQuestions
    .map((question) => `${question.label}: ${answers[question.id]}`)
    .join(' / ')
);
</script>

<template>
  <div class="quote-wizard">
    <div class="quote-wizard__head">
      <p class="eyebrow">Подбор партии</p>
      <h2>Уточните задачу для расчета заказа</h2>
      <p>
        По ответам менеджер поймет назначение блоков, вариант получения
        и подготовит список уточняющих вопросов.
      </p>
    </div>

    <div class="quote-wizard__questions">
      <fieldset v-for="question in quoteQuestions" :key="question.id">
        <legend>{{ question.label }}</legend>
        <label v-for="option in question.options" :key="option">
          <input
            v-model="answers[question.id]"
            type="radio"
            :name="question.id"
            :value="option"
          />
          <span>{{ option }}</span>
        </label>
      </fieldset>
    </div>

    <div class="quote-wizard__summary">
      <strong>Параметры обращения</strong>
      <p>{{ summary }}</p>
    </div>
  </div>
</template>
