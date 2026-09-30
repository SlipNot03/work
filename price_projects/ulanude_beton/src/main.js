import { createApp } from 'vue';
import App from './App.vue';
import './styles.css';
import { cardTilt } from './directives/cardTilt';
import { router } from './router';

createApp(App).use(router).directive('card-tilt', cardTilt).mount('#app');
