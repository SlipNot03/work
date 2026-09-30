const state = {
  conversationId: null,
  busy: false,
  documentCount: 0,
};

const els = {
  form: document.querySelector("#chat-form"),
  input: document.querySelector("#message-input"),
  messages: document.querySelector("#messages"),
  send: document.querySelector("#send"),
  mode: document.querySelector("#mode"),
  title: document.querySelector("#conversation-title"),
  newChat: document.querySelector("#new-chat"),
  conversationList: document.querySelector("#conversation-list"),
  useDocuments: document.querySelector("#use-documents"),
  documentStatus: document.querySelector("#document-status"),
  searchForm: document.querySelector("#search-form"),
  searchInput: document.querySelector("#search-input"),
  searchResults: document.querySelector("#search-results"),
  reindex: document.querySelector("#reindex"),
};

async function api(path, options = {}) {
  const response = await fetch(path, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });

  if (!response.ok) {
    const error = await response.json().catch(() => ({}));
    throw new Error(error.detail || `Request failed: ${response.status}`);
  }

  if (response.status === 204) {
    return null;
  }

  return response.json();
}

function setBusy(value) {
  state.busy = value;
  els.send.disabled = value;
  els.input.disabled = value;
}

function renderEmpty() {
  els.messages.innerHTML =
    '<div class="empty">Начните новый диалог с SON Assistant.</div>';
}

function renderMessage(message) {
  const div = document.createElement("div");
  div.className = `message ${message.role}`;
  div.textContent = message.content;
  els.messages.appendChild(div);
  els.messages.scrollTop = els.messages.scrollHeight;
}

function renderSources(sources) {
  if (!sources || sources.length === 0) {
    return;
  }

  const wrapper = document.createElement("div");
  wrapper.className = "sources";
  const title = document.createElement("div");
  title.className = "sources-title";
  title.textContent = "Источники из заметок";
  wrapper.appendChild(title);

  for (const source of sources) {
    const item = document.createElement("div");
    item.className = "source";
    const heading = document.createElement("strong");
    heading.textContent = `${source.title} · ${source.path}`;
    const snippet = document.createElement("p");
    snippet.textContent = source.snippet;
    item.append(heading, snippet);
    wrapper.appendChild(item);
  }

  els.messages.appendChild(wrapper);
  els.messages.scrollTop = els.messages.scrollHeight;
}

function renderError(message) {
  const div = document.createElement("div");
  div.className = "message error";
  div.textContent = message;
  els.messages.appendChild(div);
  els.messages.scrollTop = els.messages.scrollHeight;
}

async function loadHealth() {
  const health = await api("/api/health");
  els.mode.textContent = health.mode === "live" ? "live LLM" : "mock mode";
  state.documentCount = health.documents.document_count;
  els.documentStatus.textContent = `${health.documents.document_count} файлов · ${health.documents.chunk_count} фрагментов`;
}

async function loadConversations() {
  const conversations = await api("/api/conversations");
  els.conversationList.innerHTML = "";

  for (const item of conversations) {
    const button = document.createElement("button");
    button.type = "button";
    button.className =
      item.id === state.conversationId
        ? "conversation-item active"
        : "conversation-item";
    button.innerHTML = `<strong></strong><span></span>`;
    button.querySelector("strong").textContent = item.title;
    button.querySelector("span").textContent = `${item.message_count} сообщений`;
    button.addEventListener("click", () => openConversation(item.id));
    els.conversationList.appendChild(button);
  }
}

async function openConversation(id) {
  const conversation = await api(`/api/conversations/${id}`);
  state.conversationId = conversation.id;
  els.title.textContent = conversation.title;
  els.messages.innerHTML = "";

  if (conversation.messages.length === 0) {
    renderEmpty();
  } else {
    for (const message of conversation.messages) {
      renderMessage(message);
    }
  }

  await loadConversations();
}

function newConversation() {
  state.conversationId = null;
  els.title.textContent = "Текстовый ассистент";
  renderEmpty();
  loadConversations().catch((error) => renderError(error.message));
  els.input.focus();
}

async function sendMessage(message) {
  if (!message.trim() || state.busy) {
    return;
  }

  const isNewConversation = !state.conversationId;
  if (isNewConversation) {
    els.messages.innerHTML = "";
  }

  renderMessage({
    role: "user",
    content: message,
    created_at: new Date().toISOString(),
  });

  setBusy(true);
  try {
    const result = await api("/api/chat", {
      method: "POST",
      body: JSON.stringify({
        conversation_id: state.conversationId,
        message,
        use_documents: els.useDocuments.checked,
      }),
    });
    state.conversationId = result.conversation_id;
    if (isNewConversation) {
      els.title.textContent = message.slice(0, 48);
    }
    renderMessage(result.message);
    renderSources(result.sources);
    await loadConversations();
  } catch (error) {
    renderError(error.message);
  } finally {
    setBusy(false);
    els.input.focus();
  }
}

function renderSearchResults(results) {
  els.searchResults.innerHTML = "";
  if (results.length === 0) {
    els.searchResults.innerHTML =
      '<div class="document-status">Ничего не найдено</div>';
    return;
  }

  for (const result of results) {
    const item = document.createElement("div");
    item.className = "search-result";
    const heading = document.createElement("strong");
    heading.textContent = `${result.title} · ${result.path}`;
    const snippet = document.createElement("p");
    snippet.textContent = result.snippet;
    item.append(heading, snippet);
    els.searchResults.appendChild(item);
  }
}

async function searchDocuments(query) {
  if (!query.trim()) {
    els.searchResults.innerHTML = "";
    return;
  }
  const data = await api(`/api/documents/search?q=${encodeURIComponent(query)}`);
  renderSearchResults(data.results);
}

async function reindexDocuments() {
  const stats = await api("/api/documents/reindex", { method: "POST" });
  els.documentStatus.textContent = `${stats.document_count} файлов · ${stats.chunk_count} фрагментов`;
  if (els.searchInput.value.trim()) {
    await searchDocuments(els.searchInput.value);
  }
}

els.form.addEventListener("submit", (event) => {
  event.preventDefault();
  const message = els.input.value.trim();
  els.input.value = "";
  sendMessage(message);
});

els.input.addEventListener("keydown", (event) => {
  if (event.key === "Enter" && !event.shiftKey) {
    event.preventDefault();
    els.form.requestSubmit();
  }
});

els.input.addEventListener("input", () => {
  els.input.style.height = "auto";
  els.input.style.height = `${els.input.scrollHeight}px`;
});

els.newChat.addEventListener("click", newConversation);

els.searchForm.addEventListener("submit", (event) => {
  event.preventDefault();
  searchDocuments(els.searchInput.value).catch((error) =>
    renderError(error.message),
  );
});

els.reindex.addEventListener("click", () => {
  reindexDocuments().catch((error) => renderError(error.message));
});

renderEmpty();
loadHealth().catch((error) => renderError(error.message));
loadConversations().catch((error) => renderError(error.message));
