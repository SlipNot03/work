from typing import Literal

from pydantic import BaseModel, Field


Role = Literal["system", "user", "assistant"]


class Message(BaseModel):
    role: Role
    content: str
    created_at: str


class ChatRequest(BaseModel):
    conversation_id: str | None = None
    message: str = Field(min_length=1)
    use_documents: bool = True


class Source(BaseModel):
    path: str
    title: str
    chunk_id: int
    score: int
    snippet: str


class ChatResponse(BaseModel):
    conversation_id: str
    mode: Literal["live", "mock"]
    message: Message
    sources: list[Source] = []


class ConversationSummary(BaseModel):
    id: str
    title: str
    created_at: str
    updated_at: str
    message_count: int


class ConversationDetail(BaseModel):
    id: str
    title: str
    created_at: str
    updated_at: str
    messages: list[Message]


class HealthResponse(BaseModel):
    app: str
    status: Literal["ok"]
    mode: Literal["live", "mock"]
    documents: dict[str, int | str]


class DocumentSummary(BaseModel):
    path: str
    title: str
    size_bytes: int
    modified_at: float


class DocumentSearchResponse(BaseModel):
    query: str
    results: list[Source]


class DocumentIndexResponse(BaseModel):
    root_dir: str
    document_count: int
    chunk_count: int
    supported_extensions: list[str]
