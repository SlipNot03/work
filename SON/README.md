# SON Assistant v0.2

Минимальный персональный ассистент: FastAPI backend, web UI, история диалогов, OpenAI-compatible LLM endpoint и read-only поиск по заметкам.

## Что входит в v0.2

- Web-чат для общения с ассистентом.
- REST API для отправки сообщений.
- Локальная история диалогов в `data/conversations`.
- Read-only индекс заметок из `notes/`.
- Поиск по `.md`, `.txt`, `.rst`.
- Опциональное добавление найденных фрагментов в prompt как контекст.
- Безопасная граница: ассистент не редактирует файлы, не выполняет команды и не имеет инструментов действий.
- Mock mode, если API ключ не задан.

## Быстрый старт

WSL/Linux:

```bash
python3 -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
cp .env.example .env
uvicorn backend.app.main:app --reload --host 127.0.0.1 --port 8000
```

Windows PowerShell:

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
pip install -r requirements.txt
Copy-Item .env.example .env
uvicorn backend.app.main:app --reload --host 127.0.0.1 --port 8000
```

Откройте:

```text
http://127.0.0.1:8000
```

## Настройка LLM

Скопируйте `.env.example` в `.env` и заполните:

```text
OPENAI_API_KEY=your_key_here
OPENAI_MODEL=gpt-4.1-mini
OPENAI_BASE_URL=https://api.openai.com/v1
```

Если `OPENAI_API_KEY` пустой, приложение работает в mock mode и возвращает тестовые ответы без внешнего API.

Для локального тестирования через Ollama:

```text
OPENAI_API_KEY=ollama
OPENAI_BASE_URL=http://127.0.0.1:11434/v1
OPENAI_MODEL=llama3.2
```

## Заметки и файлы

По умолчанию ассистент индексирует только каталог:

```text
notes/
```

Поддерживаемые расширения:

```text
.md,.txt,.rst
```

Добавьте файл в `notes/`, затем нажмите кнопку обновления индекса в UI или вызовите API:

```http
POST /api/documents/reindex
```

В v0.2 это не полноценный semantic RAG, а простой lexical search по чанкам. Embeddings и vector DB логично добавить в v0.3 вместе с долговременной памятью.

## API

### Health

```http
GET /api/health
```

### Отправить сообщение

```http
POST /api/chat
Content-Type: application/json

{
  "conversation_id": null,
  "message": "Привет. Кто ты?",
  "use_documents": true
}
```

### Список заметок

```http
GET /api/documents
```

### Поиск по заметкам

```http
GET /api/documents/search?q=safety
```

### Переиндексация заметок

```http
POST /api/documents/reindex
```

### Список диалогов

```http
GET /api/conversations
```

### История диалога

```http
GET /api/conversations/{conversation_id}
```

### Удалить диалог

```http
DELETE /api/conversations/{conversation_id}
```

## Следующий этап

v0.3: долговременная память: профиль пользователя, факты, предпочтения, правила запоминания и удаления.

## Частые ошибки

### 429 Too Many Requests

OpenAI принял запрос, но отклонил его из-за лимитов. Обычно причины такие:

- На аккаунте нет активных API credits или billing не настроен.
- Превышен rate limit или usage limit.
- Слишком много запросов подряд.

Что сделать:

1. Проверьте billing и credits в OpenAI Platform.
2. Проверьте лимиты проекта/API key.
3. Подождите несколько минут и повторите запрос.
4. Для разработки можно временно очистить `OPENAI_API_KEY` в `.env`, чтобы вернуться в mock mode.
