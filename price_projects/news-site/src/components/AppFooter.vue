<template>
  <footer class="app-footer">
    <div class="container">
      <section>
        <h4>NewsCity</h4>
        <p>Новости о работе, вакансиях, карьере и профессиональных навыках.</p>
      </section>

      <section>
        <h4>Редакция</h4>
        <ul>
          <li><RouterLink to="/about">О компании</RouterLink></li>
          <li><RouterLink to="/contacts">Контакты</RouterLink></li>
          <li><RouterLink to="/ads">Реклама</RouterLink></li>
        </ul>
      </section>

      <section>
        <h4>Документы</h4>
        <ul>
          <li><RouterLink to="/terms">Пользовательское соглашение</RouterLink></li>
          <li><RouterLink to="/privacy">Политика конфиденциальности</RouterLink></li>
        </ul>
      </section>

      <div class="footer-bottom">
        <span>© 2026 NewsCity</span>
        <span>Редакция о работе и рынке труда</span>
      </div>
    </div>
  </footer>
</template>
