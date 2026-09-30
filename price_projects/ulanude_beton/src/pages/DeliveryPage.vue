<script setup>
import DeliveryZoneTable from '../components/delivery/DeliveryZoneTable.vue';
import InfoCard from '../components/ui/InfoCard.vue';
import SectionHeader from '../components/ui/SectionHeader.vue';
import StepList from '../components/ui/StepList.vue';
import { deliverySteps, deliveryTerms, deliveryZones } from '../data/siteData';
</script>

<template>
  <section class="section delivery">
    <SectionHeader
      eyebrow="Доставка"
      title="Доставка Газобетона"
    />

    <div class="delivery-layout">
      <img
        class="delivery-photo"
        src="/assets/generated/aac-delivery.jpg"
        alt="Доставка газобетона на объект"
      />
      <StepList :steps="deliverySteps" />
    </div>

    <div class="delivery-terms">
      <InfoCard
        v-for="term in deliveryTerms"
        :key="term.title"
        :title="term.title"
        :text="term.text"
      />
    </div>

    <SectionHeader
      eyebrow="Зоны доставки"
      title="Что нужно учесть перед доставкой"
      text="Для расчета сообщите адрес, количество паллет и условия подъезда. Отдельно согласуем грузоподъемность машины и способ разгрузки."
      :level="2"
    />

    <DeliveryZoneTable :zones="deliveryZones" />
  </section>
</template>
