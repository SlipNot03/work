<script setup>
import { ref } from 'vue';

defineProps({
  items: {
    type: Array,
    required: true,
  },
});

const openedQuestion = ref('');

function toggleQuestion(question) {
  openedQuestion.value = openedQuestion.value === question ? '' : question;
}
</script>

<template>
  <div class="faq-list">
    <article
      v-for="item in items"
      :key="item.question"
      class="faq-item"
      :class="{ open: openedQuestion === item.question }"
    >
      <button
        type="button"
        :aria-expanded="openedQuestion === item.question"
        @click="toggleQuestion(item.question)"
      >
        <span>{{ item.question }}</span>
      </button>
      <div class="faq-item__answer">
        <p>{{ item.answer }}</p>
      </div>
    </article>
  </div>
</template>
