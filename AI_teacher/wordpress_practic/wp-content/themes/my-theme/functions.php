<?php

add_action(
	'after_setup_theme',
	function () {
		add_theme_support( 'custom-logo' );

		register_nav_menus(
			array(
				'primary' => __( 'Primary Menu', 'my-theme' ),
			)
		);
	}
);

add_action(
	'wp_enqueue_scripts',
	function () {
		wp_enqueue_style(
			'my-theme-style',
			get_stylesheet_uri(),
			array(),
			'1.0.0'
		);
	}
);
