<!doctype html>
<html <?php language_attributes(); ?>>
<head>
	<meta charset="<?php bloginfo( 'charset' ); ?>">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<?php wp_head(); ?>
</head>
<body <?php body_class( 'home-template' ); ?>>
<?php wp_body_open(); ?>

<header class="site-header">
	<div class="site-header__container">
		<a class="site-header__logo" href="<?php echo esc_url( home_url( '/' ) ); ?>" aria-label="<?php esc_attr_e( 'На главную', 'my-theme' ); ?>">
			<?php if ( has_custom_logo() ) : ?>
				<?php the_custom_logo(); ?>
			<?php else : ?>
				<span class="site-header__logo-text"><?php bloginfo( 'name' ); ?></span>
			<?php endif; ?>
		</a>

		<nav class="site-header__nav" aria-label="<?php esc_attr_e( 'Основное меню', 'my-theme' ); ?>">
			<?php if ( has_nav_menu( 'primary' ) ) : ?>
				<?php
				wp_nav_menu(
					array(
						'theme_location' => 'primary',
						'container'      => false,
						'menu_class'     => 'site-menu',
						'fallback_cb'    => false,
					)
				);
				?>
			<?php else : ?>
				<ul class="site-menu">
					<li><a href="#advantages">Преимущества</a></li>
					<li><a href="#services">Услуги</a></li>
					<li><a href="#request-form">Заявка</a></li>
				</ul>
			<?php endif; ?>
		</nav>

		<a class="site-header__phone" href="tel:+79990000000">+7 999 000-00-00</a>
	</div>
</header>
