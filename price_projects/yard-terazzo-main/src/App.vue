<script setup lang="ts">
import { computed, onMounted, onBeforeUnmount, ref } from "vue";
import {
  ArrowUpRight,
  CheckCircle,
  ChevronRight,
  Clock,
  Compass,
  Info,
  Mail,
  MapPin,
  Menu,
  Phone,
  Send,
  Sparkles,
  X,
  Zap,
} from "@lucide/vue";
import maxIcon from "./icons/hbomax.svg";
import telegramIcon from "./icons/telegram.svg";
import whatsappIcon from "./icons/whatsapp.svg";
import bespokeDrawingImage from "./assets/images/bespoke-drawing.jpg";
import catalogConcreteImage from "./assets/images/catalog-concrete.jpg";
import catalogFacadeImage from "./assets/images/catalog-facade.jpg";
import catalogFurnitureImage from "./assets/images/catalog-furniture.jpg";
import catalogKitchenImage from "./assets/images/catalog-kitchen.jpg";
import catalogStepsImage from "./assets/images/catalog-steps.jpg";
import catalogSurfaceImage from "./assets/images/catalog-surface.jpg";
import heroResidenceImage from "./assets/images/hero-residence.jpg";
import textureVerdeImage from "./assets/images/texture-verde.jpg";

type InquiryType = "general" | "palette" | "bespoke" | "product";
type Messenger = "max" | "telegram" | "whatsapp";

interface Product {
  id: string;
  title: string;
  category: string;
  description: string;
  applications: string[];
  image: string;
}

interface Texture {
  id: string;
  name: string;
  type: string;
  description: string;
  image: string;
}

interface Inquiry {
  id: string;
  date: string;
  name: string;
  contact: string;
  messenger: Messenger;
  city: string;
  type: string;
  comment: string;
  objectType: string;
  area: string;
  timeframe: string;
  productTitle?: string;
}

const navLinks = [
  { path: "/catalog", name: "Каталог" },
  { path: "/bespoke", name: "Индивидуальные проекты" },
  { path: "/palette", name: "Палитра" },
  { path: "/applications", name: "Применение" },
  { path: "/philosophy", name: "Философия" },
  { path: "/process", name: "Процесс работы" },
  { path: "/contacts", name: "Контакты" },
];

const mobileLinks = [
  { path: "/catalog", name: "Каталог изделий" },
  { path: "/bespoke", name: "Индивидуальные изделия" },
  { path: "/palette", name: "Палитра фактур" },
  { path: "/applications", name: "Применение в интерьере" },
  { path: "/philosophy", name: "Философия бренда" },
  { path: "/process", name: "Схема производства" },
  { path: "/contacts", name: "Контакты и заявка" },
];

const products: Product[] = [
  {
    id: "surface",
    title: "Плиты и Крупноформатные Поверхности",
    category: "Terrazzo & Stone",
    description:
      "Бесшовные монолитные полы, широкоформатная облицовочная плитка и стеновые слэбы из терраццо с индивидуальной схемой раскладки камня.",
    applications: ["Полы холлов резиденций", "Стеновые панели гостиных", "Зоны ресепшн", "Ванные комнаты"],
    image: catalogSurfaceImage,
  },
  {
    id: "concrete",
    title: "Скульптурный Архитектурный Бетон",
    category: "Concrete Craft",
    description:
      "Интегрированные раковины, массивные консоли под раковины, архитектурные порталы каминов и тонкостенные стеновые панели.",
    applications: ["Премиальные санузлы", "Облицовка каминов", "Зоны акцентов", "Архитектурный свет"],
    image: catalogConcreteImage,
  },
  {
    id: "kitchen",
    title: "Летние Модульные Кухни с Грилем",
    category: "Landscape Elements",
    description:
      "Всепогодные кухни из монолитного архитектурного бетона и плотных магнезиальных плит терраццо. Скрытые коммуникации, вырезы под гриль и варочные панели.",
    applications: ["Уличные патио", "Террасы загородных вилл", "Эксплуатируемые крыши", "Летние веранды"],
    image: catalogKitchenImage,
  },
  {
    id: "furniture",
    title: "Массив уличной мебели и костровищ",
    category: "Bespoke Objects",
    description:
      "Лаконичные уличные лавочки, радиусные диванные группы из камня и бетона, костровые чаши, устойчивые к экстремальным температурным циклам.",
    applications: ["Благоустройство ландшафтов", "Зоны отдыха резиденций", "Общественные парковые зоны", "Террасы ресторанов"],
    image: catalogFurnitureImage,
  },
  {
    id: "steps",
    title: "Своды, Ступени и Входные Группы",
    category: "Basalt & Concrete",
    description:
      "Монолитные прямые и винтовые ступени, подпорные стены заданной геометрии, массивные цоколи и элементы ландшафтных переходов.",
    applications: ["Парадные лестницы", "Уличные террасированные спуски", "Цоколи премиальных фасадов"],
    image: catalogStepsImage,
  },
  {
    id: "facade",
    title: "Премиальные Облицовочные Фасады",
    category: "Architectural Stone",
    description:
      "Долговечные фасадные элементы и навесные плиты с текстурированным финишем, обеспечивающим строгость и монументальный ритм фасада.",
    applications: ["Навесные вентилируемые фасады", "Пилоны и карнизы", "Ограждения элитных усадеб"],
    image: catalogFacadeImage,
  },
];

const textures: Texture[] = [
  {
    id: "verde-sagrato",
    name: "Verde Sagrato",
    type: "Classic Terrazzo",
    description:
      "Крошка благородного мрамора Verde Alpi в дымчатой серо-зеленой связующей матрице. Глубокое ручное лощение.",
    image: textureVerdeImage,
  },
  {
    id: "basalto-crema",
    name: "Basalto Crema",
    type: "Textured Terrazzo",
    description:
      "Базальтовый щебень и вкрапления обожженного песчаника в кремовой, плотной известняковой матрице.",
    image: catalogSurfaceImage,
  },
  {
    id: "alabastro-cotto",
    name: "Alabastro Cotto",
    type: "Earthy Clay Stone",
    description:
      "Калиброванный отсев терракотовой керамики и доломита в теплом песчаном матричном наполнении.",
    image: catalogStepsImage,
  },
  {
    id: "basalto-grigio",
    name: "Basalto Grigio",
    type: "Architectural Concrete",
    description:
      "Черный минеральный заполнитель разной фракции в ультравысокопрочном сером бетоне с матовой шелковистой фактурой.",
    image: catalogConcreteImage,
  },
];

const faqData = [
  {
    q: "Можно ли заказать изделия по моим собственным чертежам?",
    a: "Да, это наш основной режим работы. Мы сотрудничаем с архитекторами и дизайнерами, принимаем чертежи в DWG/PDF и адаптируем их под технологии формования бетона и заливки терраццо.",
  },
  {
    q: "Какие сроки изготовления индивидуального изделия?",
    a: "В среднем изготовление занимает от 20 до 45 календарных дней в зависимости от сложности форм, геометрии и выбранного наполнителя.",
  },
  {
    q: "Работаете ли вы с поставщиками импортных камней?",
    a: "Основной партнер по поставке премиальных блоков натурального камня и крошки — Venezia Stone. Мы закупаем сертифицированные заполнители для прочности и сияния текстур.",
  },
  {
    q: "Какая гарантия предоставляется на готовые архитектурные изделия?",
    a: "Мы предоставляем гарантию от 5 лет на структурную целостность бетонных изделий и устойчивость терраццо.",
  },
];

const processSteps = [
  {
    num: "01",
    title: "Оставление Заявки",
    desc: "Вы связываетесь с нами. Конструктор-технолог перезванивает вам в течение 15 минут для подробного опроса.",
  },
  {
    num: "02",
    title: "Проектирование и Смета",
    desc: "Разрабатываем 3D-чертеж, согласуем детали раскладки крошки терраццо, формируем фиксированную спецификацию.",
  },
  {
    num: "03",
    title: "Формование и Литье",
    desc: "Производим матрицу, армируем фиброволокном, осуществляем заливку бетона и сушку.",
  },
  {
    num: "04",
    title: "Шлифовка и Лощение",
    desc: "Ручная шлифовка до открытия камня в 4 этапа. Полировка, влагозащита и отгрузка.",
  },
];

const messengerOptions: { id: Messenger; label: string; icon: string }[] = [
  { id: "whatsapp", label: "WhatsApp", icon: whatsappIcon },
  { id: "telegram", label: "Telegram", icon: telegramIcon },
  { id: "max", label: "Max", icon: maxIcon },
];

const currentPath = ref("/");
const mobileMenuOpen = ref(false);
const inquiryModalOpen = ref(false);
const modalType = ref<InquiryType>("general");
const selectedProduct = ref<Product | null>(null);
const activeFaq = ref<number | null>(null);
const isSubmitted = ref(false);
const localInquiries = ref<Inquiry[]>([]);

const formName = ref("");
const formContact = ref("");
const formMessenger = ref<Messenger>("whatsapp");
const formCity = ref("");
const formObjectType = ref("Частная резиденция");
const formArea = ref("");
const formTimeframe = ref("В течение 3 месяцев");
const formComment = ref("");
const formConsent = ref(true);

const modalLabel = computed(() => {
  if (modalType.value === "palette") return "Request Palette";
  if (modalType.value === "bespoke") return "Bespoke Blueprint";
  if (selectedProduct.value) return "Product Order Specs";
  return "Studio Consultation";
});

const modalTitle = computed(() => {
  if (modalType.value === "palette") return "Запросить расчет & образцы палитры фактур";
  if (modalType.value === "bespoke") return "Обсудить индивидуальный проект чертежа";
  if (selectedProduct.value) return `Смета для: ${selectedProduct.value.title}`;
  return "Записаться на экспресс-консультацию";
});

const inquiryType = computed(() => {
  if (modalType.value === "palette") return "Запрос палитры";
  if (modalType.value === "bespoke") return "Индивидуальное изделие";
  if (selectedProduct.value) return `Запрос: ${selectedProduct.value.title}`;
  return "Консультация по проекту";
});

const commentPlaceholder = computed(() =>
  modalType.value === "palette"
    ? "Укажите какие оттенки Вас привлекают (темные, светлые терраццо, серый архитектурный бетон)."
    : "Напишите примерные размеры и пожелания.",
);

const normalizedPath = computed(() => {
  const cleanPath = currentPath.value.replace(/\/+$/, "");
  return cleanPath || "/";
});

const knownPaths = ["/", ...navLinks.map((link) => link.path)];
const isKnownRoute = computed(() => knownPaths.includes(normalizedPath.value));

function isPage(...paths: string[]) {
  return paths.includes(normalizedPath.value);
}

function setPathFromLocation() {
  currentPath.value = window.location.pathname || "/";
}

function navigate(path: string) {
  if (window.location.pathname !== path) {
    window.history.pushState({}, "", path);
  }
  currentPath.value = path;
  mobileMenuOpen.value = false;
  window.scrollTo({ top: 0, behavior: "smooth" });
}

function openInquiryModal(type: InquiryType, product?: Product) {
  modalType.value = type;
  selectedProduct.value = product ?? null;
  isSubmitted.value = false;
  inquiryModalOpen.value = true;
  mobileMenuOpen.value = false;
}

function getWhatsAppLink(directMessage?: string) {
  const defaultText = `Здравствуйте! Меня зовут ${formName.value || "Клиент"}. Интересует индивидуальный архитектурный проект (${modalType.value === "palette" ? "запрос палитры фактур" : selectedProduct.value ? selectedProduct.value.title : "терраццо / бетон"}). Напишите мне, пожалуйста.`;
  return `https://wa.me/79991234567?text=${encodeURIComponent(directMessage || defaultText)}`;
}

function submitInquiry() {
  if (!formName.value || !formContact.value || !formConsent.value) return;

  const newInquiry: Inquiry = {
    id: `INQ-${Math.floor(Math.random() * 900000 + 100000)}`,
    date: new Date().toLocaleDateString("ru-RU", {
      hour: "2-digit",
      minute: "2-digit",
      day: "2-digit",
      month: "short",
      year: "numeric",
    }),
    name: formName.value,
    contact: formContact.value,
    messenger: formMessenger.value,
    city: formCity.value,
    type: inquiryType.value,
    comment: formComment.value,
    objectType: formObjectType.value,
    area: formArea.value,
    timeframe: formTimeframe.value,
    productTitle: selectedProduct.value?.title,
  };

  localInquiries.value = [newInquiry, ...localInquiries.value];
  localStorage.setItem("luxury_studio_inquiries", JSON.stringify(localInquiries.value));
  isSubmitted.value = true;
}

onMounted(() => {
  setPathFromLocation();
  try {
    const saved = localStorage.getItem("luxury_studio_inquiries");
    if (saved) localInquiries.value = JSON.parse(saved);
  } catch {
    localInquiries.value = [];
  }
  window.addEventListener("popstate", setPathFromLocation);
});

onBeforeUnmount(() => {
  window.removeEventListener("popstate", setPathFromLocation);
});
</script>

<template>
  <div class="relative min-h-screen font-sans antialiased text-[#1A1D1C]">
    <div class="absolute inset-x-0 top-0 h-full pointer-events-none opacity-[0.03] flex justify-between px-6 md:px-24">
      <div class="w-[1px] h-full bg-[#1A1D1C] hidden sm:block"></div>
      <div class="w-[1px] h-full bg-[#1A1D1C] hidden md:block"></div>
      <div class="w-[1px] h-full bg-[#1A1D1C] hidden lg:block"></div>
      <div class="w-[1px] h-full bg-[#1A1D1C]"></div>
    </div>

    <header class="fixed top-0 left-0 right-0 z-40 bg-[#FAF8F5]/90 backdrop-blur-md border-b border-[#1A1D1C]/5 transition-all duration-300">
      <div class="max-w-[1480px] mx-auto px-4 sm:px-5 md:px-8 xl:px-10 h-20 md:h-24 flex items-center gap-3 sm:gap-5 xl:gap-6">
        <a href="/" @click.prevent="navigate('/')" class="group flex min-w-0 flex-col justify-start shrink-0 sm:min-w-[190px]">
          <span class="font-serif text-lg sm:text-2xl tracking-[0.14em] sm:tracking-[0.18em] font-medium text-[#1A1D1C] uppercase whitespace-nowrap">
            YARD <span class="text-xs tracking-normal font-sans font-light text-[#8A9A86] ml-1">STONE</span>
          </span>
          <span class="hidden min-[380px]:block text-[8px] sm:text-[9px] tracking-[0.18em] sm:tracking-[0.3em] font-mono text-[#1A1D1C]/50 uppercase mt-0.5 max-w-[190px] truncate">
            TERRAZZO & ARCHITECTURAL CONCRETE
          </span>
        </a>

        <nav class="hidden xl:flex flex-1 items-stretch justify-center gap-5 2xl:gap-7 font-mono text-[11px] uppercase tracking-[0.16em]">
          <a
            v-for="link in navLinks"
            :key="link.path"
            :href="link.path"
            @click.prevent="navigate(link.path)"
            class="inline-flex h-20 md:h-24 items-center border-b-2 px-0.5 leading-none whitespace-nowrap text-[#1A1D1C]/70 hover:text-[#1A1D1C] transition-colors"
            :class="normalizedPath === link.path ? 'border-[#2E3A2F] text-[#1A1D1C] font-semibold' : 'border-transparent'"
          >
            {{ link.name }}
          </a>
        </nav>

        <div class="hidden md:flex items-center justify-end shrink-0 ml-auto xl:ml-0">
          <button
            type="button"
            @click="openInquiryModal('general')"
            class="h-12 lg:h-14 px-4 lg:px-6 bg-[#2E3A2F] text-[#FAF8F5] text-[10px] lg:text-[11px] font-mono tracking-[0.14em] lg:tracking-[0.18em] uppercase whitespace-nowrap hover:bg-[#8A9A86] transition-all duration-500 rounded-sm shadow-sm flex items-center gap-2 group"
          >
            <span>Оставить заявку</span>
            <ArrowUpRight class="w-3.5 h-3.5 group-hover:translate-x-0.5 group-hover:-translate-y-0.5 transition-transform" />
          </button>
        </div>

        <button
          type="button"
          @click="mobileMenuOpen = !mobileMenuOpen"
          class="ml-auto xl:hidden p-2 text-[#2E3A2F] hover:bg-[#2E3A2F]/5 rounded-sm transition-colors"
          aria-label="Toggle Menu"
        >
          <Menu class="w-6 h-6" />
        </button>
      </div>
    </header>

    <Transition name="fade">
      <div
        v-if="mobileMenuOpen"
        class="fixed top-20 inset-x-0 z-30 bg-[#FAF8F5] shadow-xl border-b border-[#2E3A2F]/10 flex flex-col p-5 sm:p-6 space-y-3 sm:space-y-4 xl:hidden max-h-[calc(100vh-5rem)] overflow-y-auto"
      >
        <div class="text-[10px] tracking-widest font-mono text-[#1A1D1C]/40 uppercase border-b pb-2">Разделы</div>
        <a
          v-for="link in mobileLinks"
          :key="link.path"
          :href="link.path"
          @click.prevent="navigate(link.path)"
          class="text-xs sm:text-sm font-mono tracking-[0.12em] sm:tracking-wider uppercase py-2.5 text-[#2E3A2F] hover:text-[#8A9A86] transition-colors border-b border-gray-100 flex justify-between items-center gap-3"
        >
          <span>{{ link.name }}</span>
          <ChevronRight class="w-4 h-4 text-[#8A9A86]" />
        </a>
        <div class="pt-4 flex flex-col space-y-3">
          <button
            type="button"
            @click="openInquiryModal('general')"
            class="w-full py-4 bg-[#2E3A2F] text-white font-mono text-xs tracking-widest uppercase text-center rounded-sm hover:bg-[#8A9A86] transition-colors"
          >
            Запросить консультацию
          </button>
        </div>
      </div>
    </Transition>

    <section v-if="isPage('/')" class="relative pt-20 md:pt-24 min-h-screen flex items-center justify-center bg-[#FAF8F5] overflow-hidden">
      <div class="absolute inset-x-0 bottom-0 h-1/2 bg-gradient-to-t from-[#DFE3DD]/30 to-transparent pointer-events-none"></div>
      <div class="absolute top-1/4 -right-24 w-96 h-96 bg-[#8A9A86]/5 rounded-full filter blur-3xl pointer-events-none"></div>

      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 py-12 sm:py-16 md:py-24 grid grid-cols-1 lg:grid-cols-12 gap-10 lg:gap-12 items-center relative z-10 w-full">
        <div class="lg:col-span-7 space-y-6 sm:space-y-8 text-left min-w-0">
          <div class="inline-flex max-w-full items-center gap-2 px-3 py-1 bg-[#2E3A2F]/5 rounded-full border border-[#2E3A2F]/10">
            <Sparkles class="w-3 h-3 text-[#2E3A2F]" />
            <span class="text-[9px] sm:text-[10px] font-mono tracking-[0.14em] sm:tracking-widest uppercase text-[#2E3A2F]/80 truncate">Архитектурная эстетика на века</span>
          </div>

          <h1 class="font-serif text-4xl sm:text-5xl md:text-6xl xl:text-7xl tracking-tight leading-[1.08] text-[#1A1D1C] font-light">
            Чистое ремесло: <br />
            <span class="italic font-normal text-[#2E3A2F]">Terrazzo & <br class="hidden sm:block" />Бетон</span> в вечных формах
          </h1>

          <p class="font-sans text-base md:text-lg text-[#1A1D1C]/75 max-w-xl leading-relaxed">
            Создаем премиальные бесшовные полы, монументальные летние кухни, уличную мебель и индивидуальные скульптурные изделия для премиальных резиденций и знаковых пространств.
          </p>

          <div class="flex flex-col sm:flex-row items-stretch sm:items-center gap-3 sm:gap-4 pt-2 sm:pt-4">
            <button
              type="button"
              @click="openInquiryModal('general')"
              class="px-5 sm:px-8 py-4 sm:py-[18px] bg-[#2E3A2F] text-white font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] transition-all duration-500 rounded-sm shadow-md text-center group flex items-center justify-center gap-2"
            >
              <span>Обсудить концепт проекта</span>
              <ArrowUpRight class="w-4 h-4 group-hover:translate-x-0.5 group-hover:-translate-y-0.5 transition-transform" />
            </button>
            <a
              href="/catalog"
              @click.prevent="navigate('/catalog')"
              class="px-5 sm:px-8 py-4 sm:py-[18px] bg-transparent border border-[#1A1D1C]/20 text-[#1A1D1C] font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#1A1D1C]/5 hover:border-[#1A1D1C] transition-all duration-500 rounded-sm text-center"
            >
              Посмотреть изделия
            </a>
          </div>

          <div class="pt-6 sm:pt-8 border-t border-[#1A1D1C]/10 max-w-xl grid grid-cols-1 min-[430px]:grid-cols-3 gap-4 sm:gap-6 font-mono text-[10px] tracking-wider text-[#1A1D1C]/65">
            <div>
              <span class="block text-lg font-serif font-light text-[#2E3A2F] mb-1">01</span>
              <span>ПОЛНЫЙ ИНДИВИДУАЛЬНЫЙ ПОДХОД</span>
            </div>
            <div>
              <span class="block text-lg font-serif font-light text-[#2E3A2F] mb-1">15 мин</span>
              <span>ОРИЕНТИР СКОРОСТИ ОБРАТНОЙ СВЯЗИ</span>
            </div>
            <div>
              <span class="block text-lg font-serif font-light text-[#2E3A2F] mb-1">5 лет</span>
              <span>ГАРАНТИИ НА ВСЕ СТРУКТУРЫ</span>
            </div>
          </div>
        </div>

        <div class="lg:col-span-5 relative mt-2 sm:mt-6 lg:mt-0">
          <div class="relative aspect-[4/5] sm:aspect-[3/4] w-full rounded-sm overflow-hidden shadow-2xl border border-white/30 group">
            <img
              :src="heroResidenceImage"
              alt="Premium Terrazzo concrete residence interior"
              class="object-cover w-full h-full scale-100 hover:scale-105 duration-1000 transition-all"
              referrerpolicy="no-referrer"
            />
            <div class="absolute inset-0 bg-gradient-to-t from-black/50 via-transparent to-transparent opacity-80 pointer-events-none"></div>
            <div class="absolute bottom-4 sm:bottom-6 left-4 sm:left-6 right-4 sm:right-6 p-3 sm:p-4 bg-[#FAF8F5]/95 backdrop-blur-md rounded border border-white/50 text-[#1A1D1C] flex justify-between items-center gap-3 shadow-lg">
              <div class="space-y-0.5">
                <div class="text-[8px] sm:text-[9px] font-mono tracking-widest text-[#8A9A86] uppercase">Проект резиденции</div>
                <div class="text-xs font-serif italic text-[#1A1D1C] font-semibold">«Мраморная плита Terrazzo Palladiana»</div>
              </div>
              <ArrowUpRight class="w-4 h-4 text-[#2E3A2F]" />
            </div>
          </div>
          <div class="absolute -bottom-8 -left-8 w-32 h-32 bg-[#DFE3DD] rounded-full filter blur-xl opacity-60 -z-10"></div>
        </div>
      </div>
    </section>

    <section v-if="isPage('/')" class="bg-[#2E3A2F] text-white py-12 relative overflow-hidden">
      <div class="absolute right-0 top-0 bottom-0 w-1/3 bg-[#8A9A86]/10 skew-x-12 pointer-events-none"></div>
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 flex flex-col md:flex-row justify-between items-start md:items-center gap-6">
        <div class="space-y-2 max-w-2xl">
          <span class="text-[10px] font-mono tracking-[0.25em] text-[#8A9A86] uppercase block">Материальная синергия и экосистема</span>
          <p class="font-serif text-lg md:text-xl font-light leading-relaxed text-[#FAF8F5]/90">
            «Мы закупаем сертифицированные крупные минеральные фракции и цельные слэбы напрямую у наших партнеров, включая импортные поставки мировых сортов камня от Venezia Stone».
          </p>
        </div>
        <button
          type="button"
          @click="openInquiryModal('general')"
          class="flex w-full sm:w-auto items-center justify-center gap-3 px-5 sm:px-6 py-4 bg-[#FAF8F5] text-[#2E3A2F] font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] hover:text-[#FAF8F5] transition-all duration-500 rounded-sm self-start md:self-auto"
        >
          <span>Запросить партнерство</span>
          <ChevronRight class="w-4 h-4" />
        </button>
      </div>
    </section>

    <section v-if="isPage('/catalog')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#FAF8F5] border-t border-[#1A1D1C]/5">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12">
        <div class="max-w-2xl space-y-4 mb-10 md:mb-16">
          <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Ателье Изделий</span>
          <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight">Благородная витрина решений</h2>
          <p class="font-sans text-sm md:text-base text-[#1A1D1C]/70 leading-relaxed">
            Все наши архитектурные изделия проектируются индивидуально под геометрию и стиль вашего пространства. Мы не занимаемся массовой розничной продажей — каждый предмет отливается вручную.
          </p>
        </div>

        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          <article
            v-for="(product, index) in products"
            :key="product.id"
            class="group relative flex min-w-0 flex-col overflow-hidden rounded-sm border border-[#1A1D1C]/12 bg-[#FAF8F5] p-5 transition-all duration-500 hover:border-[#2E3A2F]/35 hover:bg-white hover:shadow-[0_20px_55px_rgba(26,29,28,0.11)]"
          >
            <div class="absolute left-5 top-5 font-mono text-[10px] text-[#1A1D1C]/30">
              {{ String(index + 1).padStart(2, "0") }}
            </div>

            <div class="mb-5 flex justify-end">
              <span class="border border-[#8A9A86]/30 px-2.5 py-1 font-mono text-[9px] uppercase text-[#2E3A2F]/70">
                {{ product.category }}
              </span>
            </div>

            <div class="relative aspect-[4/3] overflow-hidden rounded-sm border border-[#1A1D1C]/10 bg-[#EEECE6]">
              <img
                :src="product.image"
                :alt="product.title"
                class="h-full w-full object-cover grayscale-[15%] transition-all duration-700 group-hover:scale-[1.04] group-hover:grayscale-0"
                referrerpolicy="no-referrer"
              />
              <div class="absolute inset-3 rounded-[2px] border border-white/45"></div>
              <div class="absolute bottom-3 left-3 rounded-[2px] bg-[#FAF8F5]/92 px-3 py-1.5 font-mono text-[9px] uppercase text-[#2E3A2F]">
                Handcrafted
              </div>
            </div>

            <div class="flex flex-1 flex-col pt-6">
              <h3 class="font-serif text-xl leading-tight text-[#1A1D1C]">
                {{ product.title }}
              </h3>

              <p class="mt-4 min-h-[82px] border-l border-[#8A9A86]/45 pl-4 font-sans text-sm leading-relaxed text-[#1A1D1C]/65">
                {{ product.description }}
              </p>

              <div class="mt-6 grid grid-cols-1 gap-2">
                <div
                  v-for="app in product.applications"
                  :key="app"
                  class="flex items-center gap-2 font-sans text-[11px] text-[#1A1D1C]/62"
                >
                  <span class="h-px w-5 bg-[#8A9A86]/60"></span>
                  <span>{{ app }}</span>
                </div>
              </div>

              <div class="mt-auto flex justify-center border-t border-[#1A1D1C]/10 pt-5">
                <button
                  type="button"
                  @click="openInquiryModal('product', product)"
                  class="inline-flex items-center justify-center gap-3 border border-[#2E3A2F]/20 px-4 py-3 font-mono text-[10px] uppercase text-[#2E3A2F] transition-colors hover:border-[#2E3A2F] hover:bg-[#2E3A2F] hover:text-white"
                >
                  <span>Запросить расчет</span>
                  <ArrowUpRight class="h-3.5 w-3.5" />
                </button>
              </div>
            </div>
          </article>
        </div>

        <div class="mt-12 md:mt-16 p-5 sm:p-8 md:p-12 bg-white border border-[#2E3A2F]/10 rounded-sm flex flex-col md:flex-row justify-between items-stretch md:items-center gap-6 shadow-sm">
          <div class="space-y-2 max-w-xl text-left">
            <span class="inline-block px-2.5 py-0.5 bg-[#8A9A86]/20 text-[#2E3A2F] font-mono text-[9px] tracking-widest uppercase rounded-full">Bespoke Design</span>
            <h3 class="font-serif text-xl sm:text-2xl font-light text-[#1A1D1C]">Не нашли нужного изделия в каталоге?</h3>
            <p class="font-sans text-xs sm:text-sm text-[#1A1D1C]/60 leading-relaxed">
              Мы беремся за любые архитектонические вызовы: от интегрированных раковин сложной геометрии до тонкостенных элементов фасадов.
            </p>
          </div>
          <button
            type="button"
            @click="openInquiryModal('bespoke')"
            class="px-6 py-4 bg-[#2E3A2F] text-white text-xs font-mono tracking-widest uppercase hover:bg-[#8A9A86] transition-colors rounded-sm flex items-center gap-2 shrink-0 w-full sm:w-auto text-center justify-center"
          >
            <span>По индивидуальному ТЗ</span>
            <Compass class="w-4 h-4" />
          </button>
        </div>
      </div>
    </section>

    <section v-if="isPage('/bespoke')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#1A1D1C] text-white overflow-hidden relative">
      <div class="absolute inset-0 bg-[radial-gradient(#2E3A2F_1px,transparent_1px)] [background-size:16px_16px] opacity-25"></div>
      <div class="absolute bottom-0 left-0 right-0 h-48 bg-gradient-to-t from-black to-transparent pointer-events-none"></div>
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 relative z-10 grid grid-cols-1 lg:grid-cols-12 gap-10 lg:gap-12 items-center">
        <div class="lg:col-span-6 relative aspect-video lg:aspect-[4/5] rounded overflow-hidden border border-white/15 shadow-2xl">
          <img
            :src="bespokeDrawingImage"
            alt="Engineering drawing of bespoke architectural stone"
            class="object-cover w-full h-full opacity-80"
            referrerpolicy="no-referrer"
          />
          <div class="absolute inset-0 border-[3px] border-[#8A9A86]/20 m-4 flex flex-col justify-between p-4 pointer-events-none">
            <span class="text-[8px] font-mono text-[#8A9A86] uppercase tracking-widest">ARCHITECTURAL BLUEPRINT 1.0</span>
            <span class="text-[8px] font-mono text-[#8A9A86] uppercase tracking-widest text-right">MADE IN ITALY & RU WORKSHOP</span>
          </div>
        </div>
        <div class="lg:col-span-6 space-y-8 text-left">
          <div class="space-y-3">
            <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Atelier Customization</span>
            <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight leading-tight">Индивидуальное производство под объект</h2>
          </div>
          <p class="font-sans text-sm md:text-base text-white/80 leading-relaxed">
            Мы являемся технологическим ателье. Более 80% наших проектов — это сложные конструкторские элементы, созданные по чертежам дизайнеров.
          </p>
          <div class="space-y-4 font-mono text-xs text-white/90">
            <div class="flex items-start gap-3">
              <div class="w-5 h-5 rounded-full bg-[#8A9A86]/20 flex items-center justify-center text-[#8A9A86] text-[10px] shrink-0 font-bold mt-0.5">✓</div>
              <div>
                <h4 class="font-bold text-white uppercase text-[11px] tracking-wider">Интеграция закладных под сантехнику и свет</h4>
                <p class="text-white/60 font-sans text-xs mt-0.5">Монтируем скрытую проводку, смесители и термостаты непосредственно во внутреннюю структуру бетона.</p>
              </div>
            </div>
            <div class="flex items-start gap-3">
              <div class="w-5 h-5 rounded-full bg-[#8A9A86]/20 flex items-center justify-center text-[#8A9A86] text-[10px] shrink-0 font-bold mt-0.5">✓</div>
              <div>
                <h4 class="font-bold text-white uppercase text-[11px] tracking-wider">Инженерные расчеты жесткости и нагрузок</h4>
                <p class="text-white/60 font-sans text-xs mt-0.5">Рассчитываем параметры для навесных фасадных плит и тяжелых ландшафтных элементов.</p>
              </div>
            </div>
            <div class="flex items-start gap-3">
              <div class="w-5 h-5 rounded-full bg-[#8A9A86]/20 flex items-center justify-center text-[#8A9A86] text-[10px] shrink-0 font-bold mt-0.5">✓</div>
              <div>
                <h4 class="font-bold text-white uppercase text-[11px] tracking-wider">Глубокая гидрофобная обработка</h4>
                <p class="text-white/60 font-sans text-xs mt-0.5">Защищает поверхности от кислот, кофе, вина, солей и жесткого ультрафиолета.</p>
              </div>
            </div>
          </div>
          <button
            type="button"
            @click="openInquiryModal('bespoke')"
            class="w-full sm:w-auto justify-center px-5 sm:px-8 py-4 sm:py-[18px] bg-[#FAF8F5] text-[#1A1D1C] font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] hover:text-white transition-all duration-500 rounded-sm flex items-center gap-2 group shadow-xl"
          >
            <span>Прислать чертеж / Получить консультацию</span>
            <ArrowUpRight class="w-4 h-4 text-[#1A1D1C] group-hover:text-white transition-colors" />
          </button>
        </div>
      </div>
    </section>

    <section v-if="isPage('/palette')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#FAF8F5] border-t border-[#1A1D1C]/5">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12">
        <div class="grid grid-cols-1 lg:grid-cols-12 gap-8 lg:gap-12 items-end mb-10 md:mb-16">
          <div class="lg:col-span-8 space-y-4 text-left">
            <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Материальная палитра</span>
            <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight">Ограниченный тизер благородных фактур</h2>
            <p class="font-sans text-sm md:text-base text-[#1A1D1C]/70 max-w-xl">
              Мы ограничиваем публичный показ всей палитры заполнителей Marble & Terrazzo для сохранения авторской идентичности премиальных объектов.
            </p>
          </div>
          <div class="lg:col-span-4 lg:text-right">
            <button
              type="button"
              @click="openInquiryModal('palette')"
              class="w-full sm:w-auto justify-center px-5 sm:px-6 py-4 bg-[#2E3A2F] text-white font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] transition-colors rounded-sm inline-flex items-center gap-2"
            >
              <span>Запросить полный каталог палитры</span>
              <ChevronRight class="w-4 h-4" />
            </button>
          </div>
        </div>

        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
          <article
            v-for="tex in textures"
            :key="tex.id"
            class="group bg-white rounded-sm overflow-hidden border border-[#1A1D1C]/5 hover:border-[#2E3A2F]/30 p-4 transition-all duration-500 hover:shadow-lg hover:-translate-y-1"
          >
            <div class="aspect-square w-full rounded overflow-hidden relative mb-4">
              <img
                :src="tex.image"
                :alt="tex.name"
                class="object-cover w-full h-full scale-100 hover:scale-110 transition-transform duration-700"
                referrerpolicy="no-referrer"
              />
              <div class="absolute top-3 right-3 w-8 h-8 rounded-full border border-white/20 flex items-center justify-center bg-black/30 backdrop-blur-sm text-[9px] font-mono text-white">
                Sample
              </div>
            </div>
            <div class="space-y-2 text-left">
              <div class="flex flex-col min-[430px]:flex-row min-[430px]:justify-between min-[430px]:items-center gap-2">
                <h3 class="font-serif text-base font-semibold text-[#1A1D1C]">{{ tex.name }}</h3>
                <span class="text-[8px] font-mono uppercase bg-[#2E3A2F]/5 text-[#2E3A2F] px-2 py-0.5 rounded">{{ tex.type }}</span>
              </div>
              <p class="font-sans text-xs text-[#1A1D1C]/70 leading-relaxed">{{ tex.description }}</p>
            </div>
          </article>
        </div>
        <div class="mt-12 text-center">
          <p class="font-mono text-[11px] sm:text-xs text-[#1A1D1C]/50 inline-flex items-start sm:items-center gap-2 text-left sm:text-center">
            <Info class="w-4 h-4 text-[#8A9A86] shrink-0 mt-0.5 sm:mt-0" />
            Все образцы созданы из отборных сортов каррарского мрамора, сланца, базальта и сибирского песчаника.
          </p>
        </div>
      </div>
    </section>

    <section v-if="isPage('/applications')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#FAF8F5] border-t border-[#1A1D1C]/5">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12">
        <div class="text-center max-w-2xl mx-auto space-y-4 mb-10 md:mb-16">
          <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Эстетика пространства</span>
          <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight">Интеграция в контекст премиальной архитектуры</h2>
          <p class="font-sans text-sm text-[#1A1D1C]/65">Натуральный камень и микроцементы терраццо диктуют пространству визуальный монументальный ритм.</p>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-12 gap-6">
          <div class="md:col-span-8 group relative aspect-video md:aspect-[16/9] rounded-sm overflow-hidden shadow-sm hover:shadow-xl transition-all duration-700">
            <img :src="heroResidenceImage" alt="Living room terrazzo" class="object-cover w-full h-full duration-1000 group-hover:scale-105 transition-all" referrerpolicy="no-referrer" />
            <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-80"></div>
            <div class="absolute bottom-6 left-6 text-left text-white space-y-1">
              <span class="font-mono text-[9px] text-[#8A9A86] uppercase tracking-widest">Апартаменты</span>
              <h3 class="font-serif text-lg md:text-xl font-bold italic">«Паркетный» шов бетона и каменные пилоны</h3>
            </div>
          </div>
          <div class="md:col-span-4 group relative aspect-video md:aspect-auto rounded-sm overflow-hidden shadow-sm hover:shadow-xl transition-all duration-700">
            <img :src="catalogConcreteImage" alt="Facade architecture concrete" class="object-cover w-full h-full duration-1000 group-hover:scale-105 transition-all" referrerpolicy="no-referrer" />
            <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-80"></div>
            <div class="absolute bottom-6 left-6 text-left text-white space-y-1">
              <span class="font-mono text-[9px] text-[#8A9A86] uppercase tracking-widest">Фасады</span>
              <h3 class="font-serif text-lg font-bold italic">Монолитные пилоны</h3>
            </div>
          </div>
          <div class="md:col-span-4 group relative aspect-video md:aspect-auto rounded-sm overflow-hidden shadow-sm hover:shadow-xl transition-all duration-700">
            <img :src="catalogKitchenImage" alt="Outdoor concrete zone" class="object-cover w-full h-full duration-1000 group-hover:scale-105 transition-all" referrerpolicy="no-referrer" />
            <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-80"></div>
            <div class="absolute bottom-6 left-6 text-left text-white space-y-1">
              <span class="font-mono text-[9px] text-[#8A9A86] uppercase tracking-widest">Ландшафты</span>
              <h3 class="font-serif text-lg font-bold italic">Уличные кухонные модули с грилем</h3>
            </div>
          </div>
          <div class="md:col-span-8 group relative aspect-video md:aspect-[16/9] rounded-sm overflow-hidden shadow-sm hover:shadow-xl transition-all duration-700">
            <img :src="catalogSurfaceImage" alt="Bespoke luxury kitchen" class="object-cover w-full h-full duration-1000 group-hover:scale-105 transition-all" referrerpolicy="no-referrer" />
            <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-80"></div>
            <div class="absolute bottom-6 left-6 text-left text-white space-y-1">
              <span class="font-mono text-[9px] text-[#8A9A86] uppercase tracking-widest">Интерьеры</span>
              <h3 class="font-serif text-lg md:text-xl font-bold italic">Интегрированные кухонные острова и порталы</h3>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section v-if="isPage('/philosophy')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#2E3A2F] text-[#FAF8F5] relative overflow-hidden">
      <div class="absolute inset-0 pointer-events-none opacity-[0.03]">
        <svg class="w-full h-full" viewBox="0 0 100 100" preserveAspectRatio="none">
          <path d="M0,50 Q25,20 50,50 T100,50" fill="none" stroke="#FAF8F5" stroke-width="2" />
          <path d="M0,60 Q30,10 60,60 T100,30" fill="none" stroke="#FAF8F5" stroke-width="1" />
        </svg>
      </div>
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 relative z-10">
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-10 lg:gap-12 items-center">
          <div class="space-y-6 sm:space-y-8 text-left">
            <div class="space-y-3">
              <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Brand Philosophy</span>
              <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight text-white">Ощущение ремесла и долговечности</h2>
            </div>
            <blockquote class="font-serif text-lg sm:text-xl md:text-2xl font-light italic leading-relaxed text-[#DFE3DD] border-l-2 border-[#8A9A86] pl-6 py-2">
              «Мы пишем вечную повесть течения времени в текстурах, способных пережить поколения».
            </blockquote>
            <p class="font-sans text-sm sm:text-base text-[#FAF8F5]/85 leading-relaxed">
              Смысл нашей работы — уход от массового дешевого наполнения в сторону монументальных, чистых решений, в которых проявляются следы прикосновений рук мастера. Терраццо и высокопрочные бетонные формулы изготавливаются непосредственно в Москве.
            </p>
            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4 sm:gap-6 pt-4 font-mono text-xs">
              <div class="p-4 bg-white/5 rounded border border-white/5">
                <h4 class="font-bold text-[#8A9A86] uppercase tracking-wider mb-1">Сырьевая этика</h4>
                <p class="text-[#FAF8F5]/65 text-[11px] font-sans">Используем портландцемент высших марок и отмытый известняковый наполнитель.</p>
              </div>
              <div class="p-4 bg-white/5 rounded border border-white/5">
                <h4 class="font-bold text-[#8A9A86] uppercase tracking-wider mb-1">Экология круга</h4>
                <p class="text-[#FAF8F5]/65 text-[11px] font-sans">Часть сырья — калиброванные переработанные отсевы каменного пиления.</p>
              </div>
            </div>
          </div>
          <div class="relative flex justify-center">
            <div class="relative aspect-square w-full max-w-[450px] rounded overflow-hidden border border-white/10 shadow-2xl">
              <img :src="catalogFacadeImage" alt="Philosophy of concrete material and texture" class="object-cover w-full h-full filter brightness-90 grayscale-[20%]" referrerpolicy="no-referrer" />
              <div class="absolute bottom-4 sm:bottom-6 left-4 sm:left-6 right-4 sm:right-6 p-4 sm:p-6 bg-[#FAF8F5] text-[#1A1D1C] rounded border border-white/20 text-left shadow-2xl">
                <h4 class="font-serif text-base font-semibold text-[#2E3A2F] italic mb-1">Старший технолог</h4>
                <p class="font-sans text-xs text-[#1A1D1C]/75 leading-relaxed">«Каждое изделие создается из живого материала. Бетон со временем лишь набирает прочность».</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section v-if="isPage('/process')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#FAF8F5] border-t border-[#1A1D1C]/5">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12">
        <div class="max-w-2xl space-y-4 mb-10 md:mb-20 text-left">
          <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Выверенный регламент</span>
          <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight">Процесс создания от концепта до объекта</h2>
          <p class="font-sans text-sm text-[#1A1D1C]/65">Поэтапная схема взаимодействия для реализации объектов любой сложности под строгим контролем.</p>
        </div>
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          <div
            v-for="(step, idx) in processSteps"
            :key="step.num"
            class="relative group bg-white p-5 sm:p-8 rounded-sm border border-[#1A1D1C]/5 shadow-sm text-left hover:border-[#2E3A2F]/20 duration-300 transition-all"
          >
            <div v-if="idx < 3" class="absolute top-1/2 -right-4 w-8 h-[1px] bg-[#1A1D1C]/10 hidden lg:block z-20 pointer-events-none"></div>
            <div class="font-mono text-3xl font-light text-[#8A9A86]/40 mb-6 group-hover:text-[#2E3A2F] duration-300 transition-colors">{{ step.num }}</div>
            <h3 class="font-serif text-lg font-bold text-[#1A1D1C] mb-3">{{ step.title }}</h3>
            <p class="font-sans text-xs sm:text-sm text-[#1A1D1C]/70 leading-relaxed">{{ step.desc }}</p>
          </div>
        </div>
      </div>
    </section>



    <section v-if="isPage('/process')" class="py-16 md:py-24 bg-[#FAF8F5] border-t border-[#1A1D1C]/5">
      <div class="max-w-4xl mx-auto px-5 sm:px-6">
        <div class="text-center space-y-4 mb-10 md:mb-16">
          <span class="text-[10px] font-mono tracking-widest uppercase text-[#8A9A86]">База Знаний</span>
          <h2 class="font-serif text-3xl font-light">Вопросы & Ответы</h2>
        </div>
        <div class="space-y-4">
          <div v-for="(faq, idx) in faqData" :key="faq.q" class="bg-white rounded border border-[#1A1D1C]/5 overflow-hidden transition-all duration-300">
            <button
              type="button"
              @click="activeFaq = activeFaq === idx ? null : idx"
              class="w-full text-left p-5 sm:p-6 font-serif text-sm sm:text-base font-semibold flex justify-between items-center gap-4 hover:bg-gray-50 transition-colors"
            >
              <span>{{ faq.q }}</span>
              <span class="text-[#2E3A2F] transition-transform duration-300 text-xl font-mono shrink-0" :class="activeFaq === idx ? 'rotate-45' : ''">+</span>
            </button>
            <div
              class="grid overflow-hidden transition-[grid-template-rows,opacity] duration-500 ease-out"
              :class="activeFaq === idx ? 'grid-rows-[1fr] opacity-100' : 'grid-rows-[0fr] opacity-0'"
            >
              <div class="min-h-0 overflow-hidden">
                <div
                  class="border-t border-gray-100 bg-[#FAF8F5]/30 p-6 pt-0 text-xs sm:text-sm font-sans text-[#1A1D1C]/75 leading-relaxed transition-transform duration-500 ease-out"
                  :class="activeFaq === idx ? 'translate-y-0' : '-translate-y-3'"
                >
                  {{ faq.a }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <section v-if="isPage('/contacts')" class="pt-28 md:pt-36 pb-16 md:pb-24 bg-[#1A1D1C] text-white border-t border-white/5 relative">
      <div class="absolute top-0 right-1/4 w-72 h-72 bg-[#2E3A2F]/15 rounded-full filter blur-3xl pointer-events-none"></div>
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 grid grid-cols-1 lg:grid-cols-12 gap-10 lg:gap-16 relative z-10">
        <div class="lg:col-span-5 space-y-6 sm:space-y-8 text-left min-w-0">
          <div class="space-y-3">
            <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block">Direct Inquiry</span>
            <h2 class="font-serif text-3xl sm:text-4xl md:text-5xl font-light tracking-tight text-white leading-tight">Обсудим ваш <br />будущий проект</h2>
          </div>
          <p class="font-sans text-sm text-white/70 leading-relaxed max-w-sm">
            Оставьте заявку. Мы связываемся в течение максимально быстрых 15 минут для фиксации технического задания.
          </p>
          <div class="space-y-6 font-mono text-xs text-white/90">
            <div class="flex items-center gap-4">
              <div class="w-10 h-10 rounded-full bg-[#2E3A2F] flex items-center justify-center text-[#8A9A86] shrink-0 border border-white/10"><Phone class="w-4 h-4" /></div>
              <div class="space-y-0.5">
                <div class="text-[9px] text-white/45 uppercase tracking-wider">Телефон по РФ</div>
                <a href="tel:+79991234567" class="text-sm font-bold hover:text-[#8A9A86]">8 (999) 123-45-67</a>
              </div>
            </div>
            <div class="flex items-center gap-4">
              <div class="w-10 h-10 rounded-full bg-[#2E3A2F] flex items-center justify-center text-[#8A9A86] shrink-0 border border-white/10"><Mail class="w-4 h-4" /></div>
              <div class="space-y-0.5">
                <div class="text-[9px] text-white/45 uppercase tracking-wider">Электронная Почта</div>
                <a href="mailto:project@yardstone.ru" class="text-sm font-bold hover:text-[#8A9A86]">project@yardstone.ru</a>
              </div>
            </div>
            <div class="flex items-center gap-4">
              <div class="w-10 h-10 rounded-full bg-[#2E3A2F] flex items-center justify-center text-[#8A9A86] shrink-0 border border-white/10"><Clock class="w-4 h-4" /></div>
              <div class="space-y-0.5">
                <div class="text-[9px] text-white/45 uppercase tracking-wider">График и сроки связи</div>
                <span class="text-sm text-white/80">Работаем ежедневно. Перезвон за 15 минут.</span>
              </div>
            </div>
            <div class="flex items-center gap-4">
              <div class="w-10 h-10 rounded-full bg-[#2E3A2F] flex items-center justify-center text-[#8A9A86] shrink-0 border border-white/10"><MapPin class="w-4 h-4" /></div>
              <div class="space-y-0.5">
                <div class="text-[9px] text-white/45 uppercase tracking-wider">Производство и Офис-ателье</div>
                <span class="text-sm text-white/80">г. Москва</span>
              </div>
            </div>
          </div>
        </div>

        <div class="lg:col-span-7 bg-white/5 rounded border border-white/10 p-5 sm:p-8 text-left min-w-0">
          <h3 class="font-serif text-xl sm:text-2xl font-light mb-6 text-white pb-3 border-b border-white/10">Подать быструю заявку на расчет стоимости</h3>
          <form class="space-y-5" @submit.prevent="submitInquiry">
            <div class="grid grid-cols-1 sm:grid-cols-2 gap-5">
              <div class="space-y-1.5">
                <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Ваше Имя *</label>
                <input v-model="formName" type="text" required placeholder="Александр" class="w-full px-4 py-3 bg-white/5 border border-white/15 focus:border-[#8A9A86] outline-none text-white text-sm rounded transition-all" />
              </div>
              <div class="space-y-1.5">
                <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Телефон / Ник Telegram *</label>
                <input v-model="formContact" type="text" required placeholder="+7 (999) 000-00-00" class="w-full px-4 py-3 bg-white/5 border border-white/15 focus:border-[#8A9A86] outline-none text-white text-sm rounded transition-all" />
              </div>
            </div>
            <div class="grid grid-cols-1 sm:grid-cols-2 gap-5">
              <div class="space-y-1.5">
                <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Город реализации</label>
                <input v-model="formCity" type="text" placeholder="Москва" class="w-full px-4 py-3 bg-white/5 border border-white/15 focus:border-[#8A9A86] outline-none text-white text-sm rounded transition-all" />
              </div>
              <div class="space-y-1.5">
                <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Тип Вашего объекта</label>
                <select v-model="formObjectType" class="form-select-dark">
                  <option>Частная резиденция</option>
                  <option>Квартира / Пентхаус</option>
                  <option>HoReCa / Общественное место</option>
                  <option>Офис / Коммерция</option>
                </select>
              </div>
            </div>
            <div class="space-y-1.5">
              <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Суть Запроса / Описание Изделия</label>
              <textarea v-model="formComment" placeholder="Опишите ваши пожелания к размерам, цвету или типу материала." rows="4" class="w-full px-4 py-3 bg-white/5 border border-white/15 focus:border-[#8A9A86] outline-none text-white text-sm rounded transition-all resize-none"></textarea>
            </div>
            <div class="space-y-1.5">
              <label class="block text-[10px] uppercase font-mono tracking-wider text-white/60">Способы связи</label>
              <div class="flex flex-wrap gap-2">
                <button
                  v-for="type in messengerOptions"
                  :key="type.id"
                  type="button"
                  :aria-label="type.label"
                  :title="type.label"
                  @click="formMessenger = type.id"
                  class="flex h-11 w-14 items-center justify-center rounded border transition-colors"
                  :class="formMessenger === type.id ? 'bg-[#2E3A2F] border-[#8A9A86] text-white' : 'bg-white/5 border-white/15 text-white/60 hover:bg-white/10'"
                >
                  <span class="sr-only">{{ type.label }}</span>
                  <img :src="type.icon" :alt="type.label" class="h-5 w-5 opacity-85 [filter:invert(1)]" />
                </button>
              </div>
            </div>
            <div class="flex items-start gap-2 pt-2">
              <input id="consentForm" v-model="formConsent" type="checkbox" required class="mt-1" />
              <label for="consentForm" class="text-[10px] text-white/50 leading-relaxed font-sans">Даю согласие на обработку персональных данных в рамках первичной обработки обращения.</label>
            </div>
            <div class="pt-4 flex flex-col sm:flex-row items-center gap-4">
              <button type="submit" class="w-full sm:w-auto px-6 sm:px-10 py-4 bg-[#FAF8F5] text-[#1A1D1C] font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] hover:text-white transition-all duration-300 rounded-sm font-bold flex items-center justify-center gap-2 shadow-lg">
                <span>Оставить заявку</span>
                <Send class="w-4 h-4" />
              </button>
            </div>
            <div v-if="isSubmitted" class="p-6 bg-[#2E3A2F]/90 backdrop-blur-md rounded border border-[#8A9A86] space-y-3 relative mt-4 text-[#FAF8F5]">
              <div class="flex items-center gap-3">
                <CheckCircle class="w-5 h-5 text-green-400" />
                <h4 class="font-serif text-lg font-bold">Проект отправлен!</h4>
              </div>
              <p class="font-sans text-xs text-white/90 leading-relaxed">Заявка успешно зарегистрирована в системе. Мы свяжемся с вами в течение 15 минут.</p>
            </div>
          </form>
        </div>
      </div>
    </section>

    <section v-if="!isKnownRoute" class="pt-28 md:pt-36 pb-16 md:pb-24 min-h-[70vh] bg-[#FAF8F5] flex items-center">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 text-left">
        <span class="text-[10px] font-mono tracking-[0.3em] uppercase text-[#8A9A86] block mb-4">Route not found</span>
        <h1 class="font-serif text-4xl sm:text-5xl font-light tracking-tight text-[#1A1D1C] mb-5">Страница не найдена</h1>
        <p class="font-sans text-sm md:text-base text-[#1A1D1C]/70 max-w-xl leading-relaxed mb-8">
          Такой страницы нет в структуре Yard Stone. Вернитесь на главную или откройте каталог изделий.
        </p>
        <div class="flex flex-col sm:flex-row gap-4">
          <a href="/" @click.prevent="navigate('/')" class="px-5 sm:px-8 py-4 sm:py-[18px] bg-[#2E3A2F] text-white font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] transition-all duration-500 rounded-sm text-center">
            На главную
          </a>
          <a href="/catalog" @click.prevent="navigate('/catalog')" class="px-5 sm:px-8 py-4 sm:py-[18px] bg-transparent border border-[#1A1D1C]/20 text-[#1A1D1C] font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#1A1D1C]/5 hover:border-[#1A1D1C] transition-all duration-500 rounded-sm text-center">
            Каталог изделий
          </a>
        </div>
      </div>
    </section>

    <footer class="bg-black text-white/40 py-12 md:py-16 text-xs text-left border-t border-white/5 font-mono">
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-8 md:gap-12 pb-10 md:pb-12 border-b border-white/5">
        <div class="space-y-4">
          <span class="font-serif text-lg text-white font-medium tracking-[0.2em] uppercase">YARD STONE</span>
          <p class="text-[11px] leading-relaxed max-w-xs text-white/55">Премиальные архитектурные поверхности и изделия из терраццо, архитектурного бетона, ремесленное производство для частных вилл.</p>
        </div>
        <div>
          <h4 class="text-white text-xs tracking-wider uppercase mb-4 font-bold border-b border-white/5 pb-2">Разделы</h4>
          <ul class="space-y-2 text-[11px]">
            <li><a href="/catalog" @click.prevent="navigate('/catalog')" class="hover:text-white transition-colors">Каталог изделий</a></li>
            <li><a href="/bespoke" @click.prevent="navigate('/bespoke')" class="hover:text-white transition-colors">Индивидуальные проекты</a></li>
            <li><a href="/palette" @click.prevent="navigate('/palette')" class="hover:text-white transition-colors">Палитра образцов</a></li>
            <li><a href="/applications" @click.prevent="navigate('/applications')" class="hover:text-white transition-colors">Интерьерный контекст</a></li>
          </ul>
        </div>
        <div>
          <h4 class="text-white text-xs tracking-wider uppercase mb-4 font-bold border-b border-white/5 pb-2">Философия</h4>
          <ul class="space-y-2 text-[11px]">
            <li><a href="/philosophy" @click.prevent="navigate('/philosophy')" class="hover:text-white transition-colors">Красота долговечности</a></li>
            <li><a href="/process" @click.prevent="navigate('/process')" class="hover:text-white transition-colors">Схема этапов работы</a></li>
          </ul>
        </div>
        <div>
          <h4 class="text-white text-xs tracking-wider uppercase mb-4 font-bold border-b border-white/5 pb-2">Правовая зона</h4>
          <p class="text-[10px] leading-relaxed">Сайт не является офертой или интернет-магазином. Все цены рассчитываются по индивидуальному техническому заданию технологами.</p>
          <p class="mt-4 text-[9px] text-white/30">© {{ new Date().getFullYear() }} Yard Stone. Crafted locally.</p>
        </div>
      </div>
      <div class="max-w-7xl mx-auto px-5 sm:px-6 md:px-12 pt-8 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 text-[10px]">
        <div>Москва</div>
        <div class="flex gap-4">
          <a href="https://t.me/yardstone_mock" class="hover:text-white transition-colors">Telegram</a>
          <a href="https://instagram.com/yardstone_mock" class="hover:text-white transition-colors">Instagram</a>
          <a href="https://wa.me/79991234567" class="hover:text-white transition-colors">WhatsApp</a>
        </div>
      </div>
    </footer>

    <Transition name="fade">
      <div v-if="inquiryModalOpen" class="fixed inset-0 z-50 flex items-start sm:items-center justify-center p-3 sm:p-4 overflow-y-auto">
        <div class="absolute inset-0 bg-black/75 backdrop-blur-sm" @click="inquiryModalOpen = false"></div>
        <div class="relative my-3 sm:my-0 w-full max-w-2xl bg-[#FAF8F5] text-[#1A1D1C] rounded shadow-2xl overflow-hidden max-h-[calc(100vh-1.5rem)] sm:max-h-[90vh] overflow-y-auto">
          <button type="button" @click="inquiryModalOpen = false" class="absolute top-3 right-3 sm:top-4 sm:right-4 p-2 text-[#2E3A2F] hover:bg-[#2E3A2F]/5 rounded-sm transition-colors z-10" aria-label="Close">
            <X class="w-5 h-5" />
          </button>
          <div class="p-5 sm:p-8">
            <div class="flex items-center gap-2 mb-4">
              <span class="max-w-[calc(100%-2.5rem)] truncate px-2.5 py-0.5 bg-[#2E3A2F]/5 text-[#2E3A2F] rounded-full text-[9px] font-mono tracking-widest uppercase">{{ modalLabel }}</span>
            </div>
            <h3 class="font-serif text-2xl sm:text-3xl font-light text-[#1A1D1C] mb-2 leading-tight pr-8 sm:pr-4">{{ modalTitle }}</h3>
            <p class="font-sans text-xs text-[#1A1D1C]/65 mb-6 leading-relaxed">
              Пожалуйста, заполните форму. Вы свяжетесь с Максом или технологом в пределах 15 минут для проектирования решения. Оплата на сайте не предусмотрена.
            </p>
            <form class="space-y-4" @submit.prevent="submitInquiry">
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div class="space-y-1">
                  <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Ваше Имя *</label>
                  <input v-model="formName" type="text" required placeholder="Алексей" class="w-full px-4 py-3 bg-[#1A1D1C]/5 focus:bg-white border border-[#1A1D1C]/10 focus:border-[#2E3A2F] outline-none text-[#1A1D1C] text-sm rounded transition-all" />
                </div>
                <div class="space-y-1">
                  <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Телефон / Мессенджер *</label>
                  <input v-model="formContact" type="text" required placeholder="+7 (999) 000-00-00" class="w-full px-4 py-3 bg-[#1A1D1C]/5 focus:bg-white border border-[#1A1D1C]/10 focus:border-[#2E3A2F] outline-none text-[#1A1D1C] text-sm rounded transition-all" />
                </div>
              </div>
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div class="space-y-1">
                  <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Город реализации</label>
                  <input v-model="formCity" type="text" placeholder="Москва" class="w-full px-4 py-3 bg-[#1A1D1C]/5 focus:bg-white border border-[#1A1D1C]/10 focus:border-[#2E3A2F] outline-none text-[#1A1D1C] text-sm rounded transition-all" />
                </div>
                <div class="space-y-1">
                  <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Тип объекта</label>
                  <select v-model="formObjectType" class="form-select-light">
                    <option>Частная резиденция</option>
                    <option>HoReCa / Ресторан</option>
                    <option>Пентхаус / Апартаменты</option>
                    <option>Ландшафтная зона</option>
                  </select>
                </div>
              </div>
              <div class="space-y-1">
                <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Комментарии к ТЗ</label>
                <textarea v-model="formComment" :placeholder="commentPlaceholder" rows="3" class="w-full px-4 py-3 bg-[#1A1D1C]/5 focus:bg-white border border-[#1A1D1C]/10 focus:border-[#2E3A2F] outline-none text-[#1A1D1C] text-sm rounded transition-all resize-none"></textarea>
              </div>
              <div class="space-y-1">
                <label class="text-[10px] uppercase font-mono text-[#1A1D1C]/60 tracking-wider">Способы связи</label>
                <div class="flex flex-wrap gap-2">
                  <button
                    v-for="type in messengerOptions"
                    :key="type.id"
                    type="button"
                    :aria-label="type.label"
                    :title="type.label"
                    @click="formMessenger = type.id"
                    class="flex h-11 w-14 items-center justify-center rounded border transition-colors"
                    :class="formMessenger === type.id ? 'bg-[#2E3A2F] border-[#2E3A2F] text-white' : 'bg-[#1A1D1C]/5 border-[#1A1D1C]/10 text-[#1A1D1C]/60 hover:bg-white hover:border-[#2E3A2F]/30'"
                  >
                    <span class="sr-only">{{ type.label }}</span>
                    <img :src="type.icon" :alt="type.label" class="h-5 w-5 opacity-85" :class="formMessenger === type.id ? '[filter:invert(1)]' : ''" />
                  </button>
                </div>
              </div>
              <button type="submit" class="w-full py-4 sm:py-[18px] bg-[#2E3A2F] text-white font-mono text-[11px] sm:text-xs tracking-[0.14em] sm:tracking-widest uppercase hover:bg-[#8A9A86] transition-all duration-300 rounded shadow-md mt-4 flex items-center justify-center gap-2">
                <span>Отправить запрос менеджеру</span>
                <Zap class="w-4 h-4 text-yellow-300" />
              </button>
              <div v-if="isSubmitted" class="p-4 bg-green-50 text-green-800 rounded border border-green-200 mt-4 text-xs font-sans space-y-1">
                <div class="font-bold flex items-center gap-1">
                  <CheckCircle class="w-4 h-4 text-green-600" />
                  <span>Спасибо! Ваша заявка отправлена.</span>
                </div>
                <p class="text-green-700">Мы уже готовим чертежные варианты. Макс свяжется с вами в течение 15 минут.</p>
              </div>
            </form>
          </div>
        </div>
      </div>
    </Transition>
  </div>
</template>
