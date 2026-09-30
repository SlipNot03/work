<?php
/**
 * Plugin Name: Editor Hover Highlight
 * Description: Adds hover highlight for blocks in the WordPress editor.
 * Version: 1.0
 */

add_action('enqueue_block_editor_assets', function () {
    wp_add_inline_style(
        'wp-edit-blocks',
        '
        .editor-styles-wrapper .block-editor-block-list__block:hover {
            outline: 2px solid #2563eb;
            outline-offset: 4px;
            background-color: rgba(37, 99, 235, 0.06);
        }
        '
    );
});