const LEAD_EMAIL = "alcocod1@yandex.ru";
const METRIKA_ID = "";

const goalMap = {
  lead_open: "lead_open",
  lead_submit: "lead_submit",
  call_click: "call_click",
  messenger_click: "messenger_click",
};

const menuButton = document.querySelector("[data-menu-toggle]");
const mobileMenu = document.querySelector("[data-mobile-menu]");
const orderInput = document.querySelector("#orderType");
const leadForm = document.querySelector("#leadForm");
const formStatus = document.querySelector("#formStatus");

function reachGoal(goalName, params = {}) {
  const target = goalMap[goalName] || goalName;

  if (window.ym && METRIKA_ID) {
    window.ym(METRIKA_ID, "reachGoal", target, params);
  }

  window.dataLayer = window.dataLayer || [];
  window.dataLayer.push({ event: target, ...params });
  console.info("Goal:", target, params);
}

function setOrderType(value) {
  if (orderInput && value) {
    orderInput.value = value;
  }
}

document.addEventListener("click", (event) => {
  const goalNode = event.target.closest("[data-goal]");
  const orderNode = event.target.closest("[data-order]");

  if (orderNode) {
    setOrderType(orderNode.dataset.order);
  }

  if (goalNode) {
    reachGoal(goalNode.dataset.goal, {
      label: goalNode.textContent.trim(),
      orderType: orderInput?.value || orderNode?.dataset.order || "",
    });
  }
});

menuButton?.addEventListener("click", () => {
  const isOpen = mobileMenu.classList.toggle("is-open");
  document.body.classList.toggle("menu-open", isOpen);
  menuButton.setAttribute("aria-expanded", String(isOpen));
});

mobileMenu?.addEventListener("click", (event) => {
  if (event.target.closest("a")) {
    mobileMenu.classList.remove("is-open");
    document.body.classList.remove("menu-open");
    menuButton?.setAttribute("aria-expanded", "false");
  }
});

leadForm?.addEventListener("submit", (event) => {
  event.preventDefault();

  const payload = Object.fromEntries(new FormData(leadForm).entries());
  payload.targetEmail = LEAD_EMAIL;
  payload.page = window.location.href;
  payload.createdAt = new Date().toISOString();

  reachGoal("lead_submit", {
    orderType: payload.orderType,
  });

  console.info("Lead for email:", LEAD_EMAIL, payload);
  formStatus.textContent = "Спасибо. Заявка подготовлена, специалист свяжется с вами.";
  leadForm.reset();
  setOrderType("Получить консультацию");
});
