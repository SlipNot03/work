<?php
/**
 * Template Name: Home Page
 */

get_header();
?>

<main class="site-main">
	<section class="hero-section">
		<div class="hero-section__container">
			<p class="section-label">Digital studio</p>
			<h1 class="hero-section__title">Создаём современные сайты для роста бизнеса</h1>
			<p class="hero-section__text">Проектируем, запускаем и поддерживаем быстрые WordPress-решения с чистой структурой и сильной визуальной подачей.</p>
			<a class="button button--primary" href="#request-form">Оставить заявку</a>
		</div>
	</section>

	<section class="advantages-section" id="advantages">
		<div class="section-container">
			<div class="section-heading">
				<p class="section-label">Преимущества</p>
				<h2 class="section-title">Работаем понятно, быстро и без лишнего шума</h2>
			</div>

			<div class="advantages-grid">
				<article class="advantage-card">
					<h3 class="advantage-card__title">Чёткая структура</h3>
					<p class="advantage-card__text">Продумываем путь пользователя и собираем страницы так, чтобы важное было видно сразу.</p>
				</article>

				<article class="advantage-card">
					<h3 class="advantage-card__title">Адаптивный дизайн</h3>
					<p class="advantage-card__text">Интерфейс аккуратно выглядит на телефонах, планшетах и широких экранах.</p>
				</article>

				<article class="advantage-card">
					<h3 class="advantage-card__title">Готовность к развитию</h3>
					<p class="advantage-card__text">Оставляем чистую базу, которую удобно расширять новыми блоками и функциями.</p>
				</article>
			</div>
		</div>
	</section>

	<section class="services-section" id="services">
		<div class="section-container">
			<div class="section-heading">
				<p class="section-label">Услуги</p>
				<h2 class="section-title">Закрываем ключевые задачи сайта</h2>
			</div>

			<div class="services-list">
				<article class="service-item">
					<span class="service-item__number">01</span>
					<h3 class="service-item__title">Лендинги</h3>
					<p class="service-item__text">Страницы для заявок, презентаций услуг и запуска рекламных кампаний.</p>
				</article>

				<article class="service-item">
					<span class="service-item__number">02</span>
					<h3 class="service-item__title">Корпоративные сайты</h3>
					<p class="service-item__text">Сайты с разделами, услугами, портфолио, новостями и удобной админкой.</p>
				</article>

				<article class="service-item">
					<span class="service-item__number">03</span>
					<h3 class="service-item__title">Поддержка WordPress</h3>
					<p class="service-item__text">Обновления, доработки, оптимизация и развитие существующих проектов.</p>
				</article>
			</div>
		</div>
	</section>

	<section class="request-section" id="request-form">
		<div class="request-section__container">
			<div class="request-section__content">
				<p class="section-label">Заявка</p>
				<h2 class="section-title">Расскажите о проекте</h2>
				<p class="request-section__text">Оставьте контакты, и мы обсудим задачу, сроки и подходящий формат работы.</p>
			</div>

			<form class="request-form" action="#" method="post">
				<label class="request-form__field">
					<span class="request-form__label">Имя</span>
					<input class="request-form__input" type="text" name="client_name" placeholder="Ваше имя" required>
				</label>

				<label class="request-form__field">
					<span class="request-form__label">Телефон</span>
					<input class="request-form__input" type="tel" name="client_phone" placeholder="+7 999 000-00-00" required>
				</label>

				<label class="request-form__field">
					<span class="request-form__label">Сообщение</span>
					<textarea class="request-form__textarea" name="client_message" rows="5" placeholder="Коротко опишите задачу"></textarea>
				</label>

				<button class="button button--primary request-form__button" type="submit">Отправить</button>
			</form>
		</div>
	</section>
</main>

<?php
get_footer();
