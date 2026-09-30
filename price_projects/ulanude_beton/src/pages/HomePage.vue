<script setup>
import ContactForm from '../components/forms/ContactForm.vue';
import QuoteWizard from '../components/forms/QuoteWizard.vue';
import ProductScrollStory from '../components/home/ProductScrollStory.vue';
import CtaPanel from '../components/ui/CtaPanel.vue';
import SectionHeader from '../components/ui/SectionHeader.vue';
import {
  company,
  contactMethods,
  heroFeatures,
  requisites,
} from '../data/siteData';

const emit = defineEmits(['navigate']);

function scrollToContacts() {
  document.getElementById('contacts')?.scrollIntoView({ behavior: 'smooth' });
}
</script>

<template>
  <ProductScrollStory
    :features="heroFeatures"
    @navigate="emit('navigate', $event)"
    @contact="scrollToContacts"
  />

  <CtaPanel
    eyebrow="Расчет материалов"
    title="Рассчитайте газобетон до оформления заказа"
    text="Введите размеры наружных стен и перегородок, чтобы получить ориентировочный объем блоков, количество паллет и расход кладочного клея."
    button-text="Открыть калькулятор"
    @action="emit('navigate', 'calculator')"
  />

  <section id="contacts" class="section contact-page">
    <SectionHeader
      eyebrow="Контакты"
      title="Уточните наличие и стоимость партии"
      text="Сообщите нужный размер блока и объем. Проверим остаток, рассчитаем паллеты, доставку или подготовим заказ к самовывозу."
      :level="2"
    />

    <div class="contact-grid contact-grid--compact">
      <div class="contact-card">
        <h2>Заказ газобетона</h2>
        <div class="contact-methods">
          <div v-for="method in contactMethods" :key="method.title">
            <span>{{ method.title }}</span>
            <a v-if="method.href" :href="method.href">{{ method.value }}</a>
            <strong v-else>{{ method.value }}</strong>
          </div>
        </div>
        <p>{{ company.address }}</p>
        <ContactForm />
      </div>

      <div class="contact-card contact-card--quiet">
        <h2>Оплата и отгрузка</h2>
        <div class="requisites requisites--stacked">
          <article v-for="item in requisites" :key="item">
            {{ item }}
          </article>
        </div>
      </div>
    </div>

    <QuoteWizard />
  </section>

</template>
