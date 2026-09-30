from __future__ import annotations

import httpx

from backend.app.config import Settings
from backend.app.schemas import Message


class LLMError(RuntimeError):
    pass


class LLMAuthError(LLMError):
    pass


class LLMRateLimitError(LLMError):
    pass


class LLMClient:
    def __init__(self, settings: Settings) -> None:
        self.settings = settings

    async def complete(self, messages: list[dict[str, str]]) -> str:
        if not self.settings.openai_api_key:
            return self._mock_reply(messages)

        url = f"{self.settings.openai_base_url.rstrip('/')}/chat/completions"
        payload = {
            "model": self.settings.openai_model,
            "messages": messages,
            "temperature": 0.4,
        }
        headers = {
            "Authorization": f"Bearer {self.settings.openai_api_key}",
            "Content-Type": "application/json",
        }

        try:
            async with httpx.AsyncClient(timeout=60) as client:
                response = await client.post(url, json=payload, headers=headers)
                response.raise_for_status()
        except httpx.HTTPStatusError as exc:
            if exc.response.status_code == 401:
                raise LLMAuthError(
                    "OpenAI rejected the API key. Check OPENAI_API_KEY in .env."
                ) from exc
            if exc.response.status_code == 429:
                raise LLMRateLimitError(
                    "OpenAI returned 429 Too Many Requests. Check API credits, "
                    "billing status, usage limits, or wait and retry later."
                ) from exc
            raise LLMError(
                f"LLM request failed with HTTP {exc.response.status_code}."
            ) from exc
        except httpx.HTTPError as exc:
            raise LLMError(f"LLM request failed: {exc}") from exc

        data = response.json()
        try:
            content = data["choices"][0]["message"]["content"]
        except (KeyError, IndexError, TypeError) as exc:
            raise LLMError("LLM response has an unexpected format") from exc

        return str(content).strip()

    def _mock_reply(self, messages: list[dict[str, str]]) -> str:
        user_messages = [item["content"] for item in messages if item["role"] == "user"]
        latest = user_messages[-1] if user_messages else ""
        has_document_context = any(
            item["role"] == "system" and "READ-ONLY DOCUMENT CONTEXT" in item["content"]
            for item in messages
        )
        context_note = (
            "\n\nRead-only document context was attached to this request."
            if has_document_context
            else ""
        )
        return (
            "Mock mode active: API key is not configured yet.\n\n"
            "I received your message and the v0.2 chat pipeline is working. "
            f"Latest user message: {latest}{context_note}"
        )


def to_llm_messages(
    system_prompt: str,
    history: list[Message],
    document_context: str = "",
) -> list[dict[str, str]]:
    messages = [{"role": "system", "content": system_prompt}]
    if document_context:
        messages.append(
            {
                "role": "system",
                "content": (
                    "READ-ONLY DOCUMENT CONTEXT\n"
                    "Use this context only if it is relevant. Do not claim access to "
                    "files beyond the excerpts shown here.\n\n"
                    f"{document_context}"
                ),
            }
        )
    return messages + [
        {"role": message.role, "content": message.content}
        for message in history
        if message.role in {"user", "assistant"}
    ]
