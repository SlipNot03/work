from dataclasses import dataclass
import os
from pathlib import Path

from dotenv import load_dotenv


load_dotenv()


ROOT_DIR = Path(__file__).resolve().parents[2]


@dataclass(frozen=True)
class Settings:
    app_name: str = os.getenv("APP_NAME", "SON Assistant")
    app_env: str = os.getenv("APP_ENV", "development")
    data_dir: Path = ROOT_DIR / os.getenv("DATA_DIR", "data")
    notes_dir: Path = ROOT_DIR / os.getenv("NOTES_DIR", "notes")

    openai_api_key: str = os.getenv("OPENAI_API_KEY", "")
    openai_base_url: str = os.getenv("OPENAI_BASE_URL", "https://api.openai.com/v1")
    openai_model: str = os.getenv("OPENAI_MODEL", "gpt-4.1-mini")

    chat_max_input_chars: int = int(os.getenv("CHAT_MAX_INPUT_CHARS", "6000"))
    chat_history_limit: int = int(os.getenv("CHAT_HISTORY_LIMIT", "24"))
    document_chunk_chars: int = int(os.getenv("DOCUMENT_CHUNK_CHARS", "1600"))
    document_context_limit: int = int(os.getenv("DOCUMENT_CONTEXT_LIMIT", "4"))
    document_context_max_chars: int = int(
        os.getenv("DOCUMENT_CONTEXT_MAX_CHARS", "5000")
    )
    document_extensions: set[str] = frozenset(
        extension.strip().lower()
        for extension in os.getenv("DOCUMENT_EXTENSIONS", ".md,.txt,.rst").split(",")
        if extension.strip()
    )

    system_prompt: str = (
        "You are SON, a practical personal AI assistant in the spirit of J.A.R.V.I.S., "
        "but constrained to realistic, safe behavior. In v0.2 you are a text assistant "
        "with optional read-only context from indexed local notes. You cannot edit "
        "files, run commands, browse, control apps, or perform external actions. If "
        "document context is provided, use it as grounded reference material and cite "
        "the relevant file names. Be concise, useful, honest about limitations, and ask "
        "clarifying questions only when needed."
    )

    @property
    def llm_mode(self) -> str:
        return "live" if self.openai_api_key else "mock"


settings = Settings()
