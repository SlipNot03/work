<script setup>
import { onBeforeUnmount, onMounted, ref } from 'vue';
import * as THREE from 'three';
import {
  advantages,
  blockBenefits,
  company,
  metrics,
  products,
  workflowSteps,
} from '../../data/siteData';

defineProps({
  features: {
    type: Array,
    default: () => [],
  },
});

const emit = defineEmits(['navigate', 'contact']);
const canvasRef = ref(null);

const materialCards = [
  {
    number: '01',
    title: 'Стеновые блоки',
    text: 'Блоки D400 и D500 толщиной 200, 300 и 400 мм для наружных и внутренних стен.',
    image: '/assets/generated/aac-wall.jpg',
  },
  {
    number: '02',
    title: 'Перегородки',
    text: 'Блоки D500 толщиной 100 и 150 мм для межкомнатных и технических перегородок.',
    image: '/assets/generated/aac-partitions.jpg',
  },
  {
    number: '03',
    title: 'Кладочный клей',
    text: 'Летняя и зимняя смесь для тонкошовной кладки газобетонных блоков.',
    image: '/assets/generated/aac-adhesive.jpg',
  },
];

const cardImages = [
  '/assets/generated/aac-wall.jpg',
  '/assets/generated/aac-pallets.jpg',
  '/assets/generated/aac-partitions.jpg',
  '/assets/generated/aac-adhesive.jpg',
];

const heroStats = [
  ['Склад', 'Забайкальская, 19'],
  ['Ассортимент', `${products.length} позиций`],
  ['Формат', 'Целые паллеты'],
  ['Подбор', 'D400 и D500'],
];

let renderer;
let scene;
let camera;
let animationId;
let cleanupScene;

function createBlock(width, height, depth, color, position) {
  const geometry = new THREE.BoxGeometry(width, height, depth);
  const material = new THREE.MeshStandardMaterial({
    color,
    roughness: 0.92,
    metalness: 0,
  });
  const mesh = new THREE.Mesh(geometry, material);
  mesh.position.set(...position);
  mesh.castShadow = true;
  mesh.receiveShadow = true;

  const edges = new THREE.LineSegments(
    new THREE.EdgesGeometry(geometry),
    new THREE.LineBasicMaterial({ color: 0x242421, transparent: true, opacity: 0.3 }),
  );
  mesh.add(edges);

  return mesh;
}

function initScene() {
  const canvas = canvasRef.value;
  if (!canvas) return;

  scene = new THREE.Scene();
  camera = new THREE.PerspectiveCamera(34, 1, 0.1, 100);
  camera.position.set(5.8, 3.7, 7.4);
  camera.lookAt(0.58, 0.28, 0);

  renderer = new THREE.WebGLRenderer({
    alpha: true,
    antialias: true,
    canvas,
  });
  renderer.setPixelRatio(Math.min(window.devicePixelRatio, 1.8));
  renderer.toneMapping = THREE.ACESFilmicToneMapping;
  renderer.toneMappingExposure = 0.72;
  renderer.shadowMap.enabled = true;
  renderer.shadowMap.type = THREE.PCFSoftShadowMap;

  const group = new THREE.Group();
  scene.add(group);

  const concrete = 0xb8b8b0;
  const accent = 0xb8bd26;

  const rows = [
    [-2.7, -0.66, 0],
    [-0.9, -0.66, 0],
    [0.9, -0.66, 0],
    [2.7, -0.66, 0],
    [-1.8, 0.05, 0],
    [0, 0.05, 0],
    [1.8, 0.05, 0],
    [3.6, 0.05, 0],
    [-2.7, 0.76, 0],
    [-0.9, 0.76, 0],
    [0.9, 0.76, 0],
    [2.7, 0.76, 0],
    [-1.8, 1.47, 0],
    [0, 1.47, 0],
    [1.8, 1.47, 0],
    [3.6, 1.47, 0],
  ];

  rows.forEach((position, index) => {
    const color = index === 5 ? accent : concrete;
    const block = createBlock(1.72, 0.62, 0.92, color, position);
    group.add(block);
  });

  group.rotation.x = -0.16;
  group.rotation.y = -0.32;
  group.rotation.z = 0.03;
  group.position.set(0.12, -0.2, 0);
  group.scale.setScalar(1);

  const floor = new THREE.Mesh(
    new THREE.PlaneGeometry(10, 8),
    new THREE.ShadowMaterial({ color: 0x000000, opacity: 0.18 }),
  );
  floor.rotation.x = -Math.PI / 2;
  floor.position.y = -1.06;
  floor.receiveShadow = true;
  scene.add(floor);

  scene.add(new THREE.AmbientLight(0xffffff, 0.38));

  const keyLight = new THREE.DirectionalLight(0xffffff, 1.05);
  keyLight.position.set(3.4, 5.2, 4.2);
  keyLight.castShadow = true;
  keyLight.shadow.mapSize.set(1024, 1024);
  scene.add(keyLight);

  const accentLight = new THREE.PointLight(0xe7e5d5, 0.72, 11);
  accentLight.position.set(2.6, 1.9, 2.2);
  scene.add(accentLight);

  const pointer = { x: 0, y: 0 };
  const onPointerMove = (event) => {
    pointer.x = (event.clientX / window.innerWidth - 0.5) * 0.08;
    pointer.y = (event.clientY / window.innerHeight - 0.5) * 0.05;
  };

  const resize = () => {
    const rect = canvas.parentElement.getBoundingClientRect();
    renderer.setSize(rect.width, rect.height, false);
    camera.aspect = rect.width / Math.max(rect.height, 1);
    camera.updateProjectionMatrix();
  };

  const animate = () => {
    animationId = requestAnimationFrame(animate);
    group.rotation.y += (-(0.32 + pointer.x) - group.rotation.y) * 0.035;
    group.rotation.x += (-(0.16 + pointer.y) - group.rotation.x) * 0.035;
    group.position.y = -0.23 + Math.sin(Date.now() * 0.0008) * 0.018;
    renderer.render(scene, camera);
  };

  window.addEventListener('resize', resize);
  window.addEventListener('pointermove', onPointerMove);
  resize();
  animate();

  cleanupScene = () => {
    cancelAnimationFrame(animationId);
    window.removeEventListener('resize', resize);
    window.removeEventListener('pointermove', onPointerMove);
    scene.traverse((object) => {
      object.geometry?.dispose?.();
      if (Array.isArray(object.material)) {
        object.material.forEach((material) => material.dispose?.());
      } else {
        object.material?.dispose?.();
      }
    });
    renderer.dispose();
  };
}

onMounted(initScene);
onBeforeUnmount(() => cleanupScene?.());
</script>

<template>
  <section class="production-hero" aria-label="Главный экран">
    <div class="production-hero__scene" aria-hidden="true">
      <canvas ref="canvasRef"></canvas>
    </div>

    <div class="production-hero__content">
      <p class="eyebrow">Газобетон со склада в Улан-Удэ</p>
      <h1>
        <span>Газобетон</span>
        <span>для стен</span>
        <span>и перегородок</span>
        <span>в Улан-Удэ</span>
      </h1>
      <p>
        Стеновые и перегородочные газоблоки D400 и D500, кладочный клей и
        паллетная отгрузка со склада. Подберем толщину, рассчитаем объем,
        количество паллет и доставку до строительного объекта.
      </p>
      <div class="production-hero__actions">
        <button type="button" @click="emit('navigate', 'catalog')">Посмотреть размеры и цены</button>
        <button type="button" @click="emit('navigate', 'calculator')">Рассчитать объем</button>
        <button type="button" @click="emit('contact')">Контакты</button>
      </div>
    </div>

    <aside class="production-hero__stats" aria-label="Краткая сводка">
      <div v-for="[label, value] in heroStats" :key="label">
        <span>{{ label }}</span>
        <strong>{{ value }}</strong>
      </div>
    </aside>
  </section>

  <div class="feature-rail" aria-label="Условия поставки">
    <span v-for="feature in features" :key="feature">{{ feature }}</span>
  </div>

  <section class="home-section home-section--materials section" aria-label="Материалы">
    <p class="eyebrow">Ассортимент</p>
    <h2>Газобетон для наружных стен и перегородок</h2>
    <p>
      В каталоге собраны ходовые размеры блоков D400 и D500 для основных стен,
      внутренних перегородок и кладочный клей для тонкого шва.
    </p>
    <div class="home-material-grid">
      <article v-for="card in materialCards" :key="card.number">
        <img :src="card.image" :alt="card.title" />
        <span>{{ card.number }}</span>
        <h3>{{ card.title }}</h3>
        <p>{{ card.text }}</p>
      </article>
    </div>
  </section>

  <section class="home-section section" aria-label="Показатели">
    <p class="eyebrow">Формат поставки</p>
    <h2>Отгружаем газобетон заводскими паллетами</h2>
    <div class="home-metrics">
      <article v-for="metric in metrics" :key="metric.value">
        <strong>{{ metric.value }}</strong>
        <span>{{ metric.label }}</span>
      </article>
    </div>
  </section>

  <section class="home-section section" aria-label="Преимущества">
    <p class="eyebrow">Комплектация заказа</p>
    <h2>Подбор блоков, расчет паллет и доставка</h2>
    <p>
      Проверяем размеры стен, подбираем доступную позицию по плотности и толщине,
      затем считаем блоки, клей и подходящий транспорт.
    </p>
    <div class="home-card-grid">
      <article v-for="(item, index) in advantages" :key="item.title">
        <img class="home-card-grid__image" :src="cardImages[index % cardImages.length]" :alt="item.title" />
        <h3>{{ item.title }}</h3>
        <p>{{ item.text }}</p>
      </article>
    </div>
  </section>

  <section class="home-section section" aria-label="Как работаем">
    <p class="eyebrow">Как работаем</p>
    <h2>Как формируется заказ на газобетон</h2>
    <div class="home-steps">
      <article v-for="step in workflowSteps" :key="step.number">
        <span>{{ step.number }}</span>
        <h3>{{ step.title }}</h3>
        <p>{{ step.text }}</p>
      </article>
    </div>
  </section>

  <section class="home-section home-section--benefits section" aria-label="Свойства газобетона">
    <p class="eyebrow">Почему газобетон</p>
    <h2>Что важно знать о газобетонных блоках</h2>
    <div class="home-card-grid">
      <article v-for="(item, index) in blockBenefits.slice(0, 4)" :key="item.number">
        <img class="home-card-grid__image" :src="cardImages[index % cardImages.length]" :alt="item.title" />
        <span>{{ item.number }}</span>
        <h3>{{ item.title }}</h3>
        <p>{{ item.text }}</p>
      </article>
    </div>
  </section>
</template>
