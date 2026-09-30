<script setup>
  import { ref } from 'vue'
  const userName = ref('Artem')
  const userAge = ref(22)
  const myLink = ref('https://vuejs.org')
</script>

<template>
  <h1>Hi, {{ userName }}</h1>
  <p>You is cool {{ userAge }} years,next more</p>

  <p>After 5 years your will {{ userAge + 5 }} year</p>

  <p>{{ userAge >= 25 ? 'cool' : 'cool to' }}.</p>
</template>