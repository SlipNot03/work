<script setup>
const comments = [
  {
    author: 'Анна',
    text: 'После материала про резюме читатели попросили короткий чек-лист для первого отклика.',
    meta: '12 минут назад',
  },
  {
    author: 'Марк',
    text: 'В статье про HR-tech стоит добавить абзац о том, почему компании долго согласуют офферы.',
    meta: '38 минут назад',
  },
  {
    author: 'Читатель',
    text: 'Хочется отдельный разбор: что отвечать, если на интервью спрашивают про зарплатные ожидания.',
    meta: '1 час назад',
  },
  {
    author: 'Редакция',
    text: 'Для подборки вакансий проверяем, есть ли наставник, прозрачная оплата и понятные задачи.',
    meta: 'сегодня',
  },
]
</script>

<template>
  <aside class="comments-panel" aria-label="Комментарии редакции">
    <div class="comments-panel__sticky">
      <p class="panel-eyebrow">Обсуждают сейчас</p>

      <article
        v-for="comment in comments"
        :key="comment.text"
        class="comment-card reveal"
      >
        <strong>{{ comment.author }}</strong>
        <p>{{ comment.text }}</p>
        <span>{{ comment.meta }}</span>
      </article>
    </div>
  </aside>
</template>