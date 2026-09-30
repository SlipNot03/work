<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()

const pages = {
  about: {
    title: 'О компании',
    text: 'NewsCity — новостной портал о работе, вакансиях и рынке труда. Редакция следит за изменениями в найме, объясняет карьерные тренды и помогает читателям принимать решения без шума и случайных советов.',
    highlights: [
      'Новости рынка труда, которые помогают понимать контекст, а не просто следить за заголовками.',
      'Разборы о резюме, собеседованиях, зарплате, росте и смене профессии.',
      'Подборки вакансий с акцентом на условия, требования и реальные ожидания работодателей.',
    ],
  },
  ads: {
    title: 'Реклама',
    text: 'NewsCity работает с брендами, образовательными проектами, работодателями и сервисами для профессионального развития. Возможны партнерские материалы, спецпроекты, подборки вакансий и нативные размещения.',
  },
  contacts: {
    title: 'Контакты',
    text: 'Свяжитесь с редакцией, если хотите предложить тему, прислать вакансию, обсудить партнерство или уточнить условия размещения.',
  },
  terms: {
    title: 'Пользовательское соглашение',
    text: 'На этой странице размещаются правила использования материалов, условия работы с сайтом и порядок обращения в редакцию по вопросам публикаций.',
  },
  privacy: {
    title: 'Политика конфиденциальности',
    text: 'NewsCity уважает приватность пользователей и обрабатывает контактные данные только для ответа на обращения, редакционные запросы и деловую переписку.',
  },
}

const page = computed(() => {
  return pages[route.name] || {
    title: 'Страница',
    text: 'Материалы раздела проходят редакционную подготовку.',
  }
})

const isContactsPage = computed(() => route.name === 'contacts')
</script>

<template>
  <main>
    <section>
      <div v-if="route.name === 'about'" class="about-hero">
        <div class="about-copy">
          <p class="panel-eyebrow">NewsCity</p>
          <h1>{{ page.title }}</h1>
          <p>{{ page.text }}</p>
        </div>
      </div>

      <template v-else>
        <h1>{{ page.title }}</h1>
        <p>{{ page.text }}</p>
      </template>
    </section>

    <section v-if="route.name === 'about'" class="about-grid">
      <article
        v-for="(item, index) in page.highlights"
        :key="item"
      >
        <span>0{{ index + 1 }}</span>
        <p>{{ item }}</p>
      </article>
    </section>

    <section v-if="isContactsPage" class="contacts-layout">
      <div class="contacts-info">
        <h2>Связаться напрямую</h2>

        <dl>
          <div>
            <dt>Телефон</dt>
            <dd><a href="tel:+73952000000">+7 3952 00-00-00</a></dd>
          </div>

          <div>
            <dt>Почта</dt>
            <dd><a href="mailto:hello@newscity.media">hello@newscity.media</a></dd>
          </div>

          <div>
            <dt>Мессенджеры</dt>
            <dd>
              <a href="https://t.me/newscity_media" target="_blank" rel="noreferrer">Telegram</a>
              <a href="https://wa.me/73952000000" target="_blank" rel="noreferrer">WhatsApp</a>
            </dd>
          </div>
        </dl>
      </div>

      <form class="contact-form">
        <label>
          Имя
          <input type="text" name="name" placeholder="Как к вам обращаться">
        </label>

        <label>
          Телефон
          <input type="tel" name="phone" placeholder="+7 900 000-00-00">
        </label>

        <label>
          Почта
          <input type="email" name="email" placeholder="name@company.ru">
        </label>

        <label>
          Удобный мессенджер
          <select name="messenger">
            <option>Telegram</option>
            <option>WhatsApp</option>
            <option>Email</option>
          </select>
        </label>

        <label class="contact-form__message">
          Сообщение
          <textarea name="message" rows="5" placeholder="Опишите вопрос или предложение"></textarea>
        </label>

        <button type="button">Отправить</button>
      </form>
    </section>
  </main>
</template>
