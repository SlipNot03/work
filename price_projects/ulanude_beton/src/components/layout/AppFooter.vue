<script setup>
import { company } from '../../data/siteData';
</script>

<template>
  <footer class="site-footer">
    <div>
      <strong>{{ company.name }}</strong>
      <span>Стеновые и перегородочные газоблоки D400/D500, клей, доставка и самовывоз</span>
    </div>
    <address>
      <a v-if="company.phoneHref" :href="company.phoneHref">{{ company.phone }}</a>
      <a v-if="company.emailHref" :href="company.emailHref">{{ company.email }}</a>
      <span>{{ company.address }}</span>
    </address>
  </footer>
</template>
