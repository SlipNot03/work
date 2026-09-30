<template>
  <header class="app-header">
    <div class="container">
      <RouterLink to="/" class="logo">News<span>City</span></RouterLink>

      <nav aria-label="Основные рубрики">
        <RouterLink to="/category/job-search">Поиск работы</RouterLink>
        <RouterLink to="/category/vacancies">Вакансии</RouterLink>
        <RouterLink to="/category/career">Карьера</RouterLink>
        <RouterLink to="/category/skills">Навыки</RouterLink>
        <RouterLink to="/category/market">Рынок</RouterLink>
      </nav>

      <div class="header-actions">
        <RouterLink to="/search">Поиск</RouterLink>
        <RouterLink to="/about">Sign in</RouterLink>
      </div>
    </div>
  </header>
</template>
