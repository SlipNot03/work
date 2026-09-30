from __future__ import annotations

from datetime import datetime, timezone
import json
from pathlib import Path
from uuid import uuid4

from backend.app.schemas import ConversationDetail, ConversationSummary, Message


def utc_now() -> str:
    return datetime.now(timezone.utc).isoformat()


class ConversationStore:
    def __init__(self, data_dir: Path) -> None:
        self.conversations_dir = data_dir / "conversations"
        self.conversations_dir.mkdir(parents=True, exist_ok=True)

    def list(self) -> list[ConversationSummary]:
        summaries: list[ConversationSummary] = []
        for path in self.conversations_dir.glob("*.json"):
            try:
                item = self._read(path)
            except (OSError, json.JSONDecodeError, KeyError):
                continue
            summaries.append(
                ConversationSummary(
                    id=item["id"],
                    title=item["title"],
                    created_at=item["created_at"],
                    updated_at=item["updated_at"],
                    message_count=len(item.get("messages", [])),
                )
            )
        return sorted(summaries, key=lambda item: item.updated_at, reverse=True)

    def get(self, conversation_id: str) -> ConversationDetail | None:
        path = self._path(conversation_id)
        if not path.exists():
            return None
        item = self._read(path)
        return ConversationDetail(
            id=item["id"],
            title=item["title"],
            created_at=item["created_at"],
            updated_at=item["updated_at"],
            messages=[Message(**message) for message in item.get("messages", [])],
        )

    def create(self, first_message: str) -> ConversationDetail:
        now = utc_now()
        conversation_id = str(uuid4())
        item = {
            "id": conversation_id,
            "title": self._title_from_message(first_message),
            "created_at": now,
            "updated_at": now,
            "messages": [],
        }
        self._write(self._path(conversation_id), item)
        return self.get(conversation_id)  # type: ignore[return-value]

    def append_message(self, conversation_id: str, role: str, content: str) -> Message:
        path = self._path(conversation_id)
        item = self._read(path)
        now = utc_now()
        message = {"role": role, "content": content, "created_at": now}
        item["messages"].append(message)
        item["updated_at"] = now
        self._write(path, item)
        return Message(**message)

    def delete(self, conversation_id: str) -> bool:
        path = self._path(conversation_id)
        if not path.exists():
            return False
        path.unlink()
        return True

    def _path(self, conversation_id: str) -> Path:
        safe_id = conversation_id.replace("/", "").replace("\\", "")
        return self.conversations_dir / f"{safe_id}.json"

    def _read(self, path: Path) -> dict:
        return json.loads(path.read_text(encoding="utf-8"))

    def _write(self, path: Path, item: dict) -> None:
        path.write_text(
            json.dumps(item, ensure_ascii=False, indent=2),
            encoding="utf-8",
        )

    def _title_from_message(self, message: str) -> str:
        title = " ".join(message.strip().split())
        if not title:
            return "New conversation"
        return title[:48]
