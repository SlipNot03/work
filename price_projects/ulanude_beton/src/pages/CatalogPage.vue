<script setup>
import { computed, ref } from 'vue';
import CatalogFilters from '../components/catalog/CatalogFilters.vue';
import OrderTable from '../components/catalog/OrderTable.vue';
import ProductCard from '../components/catalog/ProductCard.vue';
import CtaPanel from '../components/ui/CtaPanel.vue';
import SectionHeader from '../components/ui/SectionHeader.vue';
import { orderRows, productFilters, products } from '../data/siteData';

const emit = defineEmits(['navigate']);
const selectedCategory = ref('Все');
const selectedDensity = ref('Все');

const filteredProducts = computed(() =>
  products.filter((product) => {
    const categoryMatches =
      selectedCategory.value === 'Все' || product.category === selectedCategory.value;
    const densityMatches =
      selectedDensity.value === 'Все' || product.density === selectedDensity.value;

    return categoryMatches && densityMatches;
  })
);

const previewProduct = computed(() => filteredProducts.value[0] ?? products[0]);

function resetFilters() {
  selectedCategory.value = 'Все';
  selectedDensity.value = 'Все';
}
</script>

<template>
  <section class="section catalog-section">
    <SectionHeader
      title="Наши Товары"
      text="Стеновые блоки D400 и D500, перегородочные блоки толщиной 100 и 150 мм, летний и зимний клей для тонкошовной кладки."
      centered
    />

    <article class="catalog-preview">
      <div>
        <span>{{ previewProduct.category }}</span>
        <strong>{{ previewProduct.title }}</strong>
        <p>{{ previewProduct.purpose }}</p>
        <dl>
          <div>
            <dt>Размер</dt>
            <dd>{{ previewProduct.size }}</dd>
          </div>
          <div>
            <dt>Плотность</dt>
            <dd>{{ previewProduct.density }}</dd>
          </div>
          <div>
            <dt>Цена</dt>
            <dd>{{ previewProduct.price }}</dd>
          </div>
        </dl>
      </div>
      <img :src="previewProduct.image" :alt="previewProduct.title" />
    </article>

    <div class="catalog-command">
      <div>
        <span>01</span>
        <strong>Характеристики блока</strong>
        <p>Сверьте толщину, плотность, класс прочности и морозостойкость перед выбором.</p>
      </div>
      <div>
        <span>02</span>
        <strong>Комплектация паллеты</strong>
        <p>Количество блоков и кубометров на паллете используется для расчета заказа и транспорта.</p>
      </div>
      <div>
        <span>03</span>
        <strong>Итоговая комплектация</strong>
        <p>К выбранным блокам добавим запас, кладочный клей и подходящую доставку.</p>
      </div>
    </div>

    <CatalogFilters
      v-model:selected-category="selectedCategory"
      v-model:selected-density="selectedDensity"
      :categories="productFilters.categories"
      :densities="productFilters.densities"
      @reset="resetFilters"
    />

    <div class="product-grid">
      <ProductCard
        v-for="product in filteredProducts"
        :key="product.title"
        :product="product"
        @calculate="emit('navigate', 'calculator')"
      />
    </div>

    <div v-if="filteredProducts.length === 0" class="empty-state">
      <h2>Блоков с такими параметрами нет в списке</h2>
      <p>Сбросьте фильтры или уточните у менеджера ближайший размер и срок поставки.</p>
    </div>

    <div class="catalog-note">
      <div>
        <h2>Подтвердите цену и остаток перед оплатой</h2>
        <p>
          Стоимость заказа складывается из фактического объема, количества паллет,
          кладочного клея и доставки до объекта.
        </p>
      </div>
      <button type="button" @click="emit('navigate', 'contacts')">
        Уточнить стоимость
      </button>
    </div>
  </section>

  <section class="section section-surface order-section">
    <SectionHeader
      eyebrow="Паллетная отгрузка"
      title="Сколько блоков и кубометров в паллете"
      text="Таблица помогает перевести проектный объем в заводские паллеты. Цены указаны ориентировочно и подтверждаются по текущему остатку."
      :level="2"
    />

    <OrderTable :groups="orderRows" @order="emit('navigate', 'contacts')" />
  </section>

  <CtaPanel
    title="Не уверены в толщине или плотности блока?"
    text="Пришлите план или размеры стен. Подскажем доступные варианты D400 и D500 и подготовим предварительную комплектацию в паллетах."
    button-text="Обсудить заказ"
    @action="emit('navigate', 'contacts')"
  />
</template>
