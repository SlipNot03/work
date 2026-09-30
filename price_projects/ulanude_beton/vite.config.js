import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  build: {
    rollupOptions: {
      output: {
        manualChunks(id) {
          if (id.includes('node_modules/vue') || id.includes('node_modules/vue-router')) {
            return 'vue-vendor';
          }

          return undefined;
        },
      },
    },
  },
  plugins: [
    {
      name: 'admin-index-route',
      configureServer(server) {
        server.middlewares.use((req, _res, next) => {
          if (req.url === '/admin/' || req.url === '/admin') {
            req.url = '/admin/index.html';
          }
          next();
        });
      },
    },
    vue(),
  ],
});
