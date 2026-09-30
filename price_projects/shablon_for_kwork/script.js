const CRM_WEBHOOK_URL = "";
const DEFAULT_PHONE = "+7 3952 99-99-99";
const TRACKING_PHONES = {
  yandex_search: "+7 3952 88-10-01",
  yandex_rsy: "+7 3952 88-10-02",
  direct: "+7 3952 88-10-03",
};

const form = document.querySelector("#leadForm");
const steps = [...document.querySelectorAll(".quiz__step")];
const currentStepEl = document.querySelector("#currentStep");
const progressBar = document.querySelector("#progressBar");
const estimateText = document.querySelector("#estimateText");
const prevButton = document.querySelector("#prevStep");
const nextButton = document.querySelector("#nextStep");
const submitButton = document.querySelector("#submitLead");
const message = document.querySelector("#formMessage");

let activeStep = 0;

function formatRub(value) {
  return new Intl.NumberFormat("ru-RU", {
    style: "currency",
    currency: "RUB",
    maximumFractionDigits: 0,
  }).format(value);
}

function getSelectedOption(select) {
  return select.options[select.selectedIndex];
}

function calculateEstimate() {
  const data = new FormData(form);
  const area = Number(data.get("area")) || 0;
  const repairSelect = form.elements.repairType;
  const price = Number(getSelectedOption(repairSelect).dataset.price || 0);
  const objectFactor = Number(form.querySelector("[name='objectType']:checked")?.dataset.factor || 1);
  const urgencyFactor = Number(form.querySelector("[name='startDate']:checked")?.dataset.urgency || 1);
  const base = area * price * objectFactor * urgencyFactor;
  const min = Math.max(0, Math.round(base * 0.92));
  const max = Math.max(0, Math.round(base * 1.18));

  estimateText.textContent = `Предварительный расчет: ${formatRub(min)} - ${formatRub(max)}`;
}

function showStep(index) {
  activeStep = Math.min(Math.max(index, 0), steps.length - 1);
  steps.forEach((step, stepIndex) => {
    step.classList.toggle("is-active", stepIndex === activeStep);
  });

  currentStepEl.textContent = String(activeStep + 1);
  progressBar.style.width = `${((activeStep + 1) / steps.length) * 100}%`;
  prevButton.disabled = activeStep === 0;
  nextButton.classList.toggle("is-hidden", activeStep === steps.length - 1);
  submitButton.classList.toggle("is-hidden", activeStep !== steps.length - 1);
  message.textContent = "";
  calculateEstimate();
}

function validateCurrentStep() {
  const inputs = [...steps[activeStep].querySelectorAll("input, select")];
  const invalid = inputs.find((input) => !input.checkValidity());

  if (invalid) {
    invalid.reportValidity();
    return false;
  }

  return true;
}

function getUtmData() {
  const params = new URLSearchParams(window.location.search);
  return {
    utm_source: params.get("utm_source") || "",
    utm_medium: params.get("utm_medium") || "",
    utm_campaign: params.get("utm_campaign") || "",
    utm_content: params.get("utm_content") || "",
    utm_term: params.get("utm_term") || "",
  };
}

function buildPayload() {
  const data = Object.fromEntries(new FormData(form).entries());
  return {
    ...data,
    estimate: estimateText.textContent.replace("Предварительный расчет: ", ""),
    page: window.location.href,
    calltracking_phone: document.querySelector(".js-calltracking-phone")?.textContent || DEFAULT_PHONE,
    created_at: new Date().toISOString(),
    ...getUtmData(),
  };
}

async function sendLead(payload) {
  // Укажите webhook CRM, например Bitrix24 или amoCRM. Без URL заявка имитируется в консоли.
  if (!CRM_WEBHOOK_URL) {
    console.info("Lead payload:", payload);
    return { mocked: true };
  }

  const response = await fetch(CRM_WEBHOOK_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    throw new Error("CRM request failed");
  }

  return response.json();
}

function applyCalltracking() {
  const params = new URLSearchParams(window.location.search);
  const source = params.get("utm_source");
  const medium = params.get("utm_medium");
  const key = source === "yandex" && medium === "cpc" ? "yandex_search" : source || "direct";
  const phone = TRACKING_PHONES[key] || DEFAULT_PHONE;
  const phoneHref = `tel:${phone.replace(/[^\d+]/g, "")}`;

  document.querySelectorAll(".js-calltracking-phone").forEach((node) => {
    node.textContent = phone;
    node.setAttribute("href", phoneHref);
  });
}

prevButton.addEventListener("click", () => showStep(activeStep - 1));

nextButton.addEventListener("click", () => {
  if (validateCurrentStep()) {
    showStep(activeStep + 1);
  }
});

form.addEventListener("input", calculateEstimate);
form.addEventListener("change", calculateEstimate);

form.addEventListener("submit", async (event) => {
  event.preventDefault();

  if (!validateCurrentStep()) {
    return;
  }

  submitButton.disabled = true;
  submitButton.textContent = "Отправляем...";
  message.textContent = "";

  try {
    await sendLead(buildPayload());
    message.textContent = "Заявка отправлена. Специалист свяжется с вами в ближайшее время.";
    form.reset();
    showStep(0);
  } catch (error) {
    message.textContent = "Не удалось отправить заявку. Проверьте CRM webhook или попробуйте позже.";
    console.error(error);
  } finally {
    submitButton.disabled = false;
    submitButton.textContent = "Отправить заявку";
  }
});

applyCalltracking();
showStep(0);
