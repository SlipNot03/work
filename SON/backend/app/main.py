from pathlib import Path

from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from fastapi.responses import FileResponse
from fastapi.staticfiles import StaticFiles

from backend.app.config import ROOT_DIR, settings
from backend.app.documents import DocumentIndex, SearchHit
from backend.app.llm import (
    LLMAuthError,
    LLMClient,
    LLMError,
    LLMRateLimitError,
    to_llm_messages,
)
from backend.app.schemas import (
    ChatRequest,
    ChatResponse,
    ConversationDetail,
    ConversationSummary,
    DocumentIndexResponse,
    DocumentSearchResponse,
    DocumentSummary,
    HealthResponse,
    Source,
)
from backend.app.storage import ConversationStore


frontend_dir = ROOT_DIR / "frontend"

app = FastAPI(title=settings.app_name)
app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://127.0.0.1:8000", "http://localhost:8000"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

store = ConversationStore(settings.data_dir)
llm = LLMClient(settings)
documents = DocumentIndex(
    root_dir=settings.notes_dir,
    supported_extensions=settings.document_extensions,
    chunk_chars=settings.document_chunk_chars,
)

if frontend_dir.exists():
    app.mount("/assets", StaticFiles(directory=frontend_dir), name="assets")


@app.get("/", include_in_schema=False)
async def index() -> FileResponse:
    return FileResponse(frontend_dir / "index.html")


@app.get("/api/health", response_model=HealthResponse)
async def health() -> HealthResponse:
    stats = documents.stats()
    return HealthResponse(
        app=settings.app_name,
        status="ok",
        mode=settings.llm_mode,
        documents={
            "root_dir": stats.root_dir,
            "document_count": stats.document_count,
            "chunk_count": stats.chunk_count,
        },
    )


@app.get("/api/documents", response_model=list[DocumentSummary])
async def list_documents() -> list[DocumentSummary]:
    return [
        DocumentSummary(
            path=document.path,
            title=document.title,
            size_bytes=document.size_bytes,
            modified_at=document.modified_at,
        )
        for document in documents.list_documents()
    ]


@app.post("/api/documents/reindex", response_model=DocumentIndexResponse)
async def reindex_documents() -> DocumentIndexResponse:
    stats = documents.reindex()
    return DocumentIndexResponse(
        root_dir=stats.root_dir,
        document_count=stats.document_count,
        chunk_count=stats.chunk_count,
        supported_extensions=stats.supported_extensions,
    )


@app.get("/api/documents/search", response_model=DocumentSearchResponse)
async def search_documents(q: str, limit: int = 8) -> DocumentSearchResponse:
    if not q.strip():
        raise HTTPException(status_code=400, detail="Search query is empty")
    safe_limit = min(max(limit, 1), 20)
    return DocumentSearchResponse(
        query=q,
        results=[source_from_hit(hit) for hit in documents.search(q, safe_limit)],
    )


@app.get("/api/conversations", response_model=list[ConversationSummary])
async def list_conversations() -> list[ConversationSummary]:
    return store.list()


@app.get("/api/conversations/{conversation_id}", response_model=ConversationDetail)
async def get_conversation(conversation_id: str) -> ConversationDetail:
    conversation = store.get(conversation_id)
    if conversation is None:
        raise HTTPException(status_code=404, detail="Conversation not found")
    return conversation


@app.delete("/api/conversations/{conversation_id}", status_code=204)
async def delete_conversation(conversation_id: str) -> None:
    deleted = store.delete(conversation_id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Conversation not found")


@app.post("/api/chat", response_model=ChatResponse)
async def chat(request: ChatRequest) -> ChatResponse:
    content = request.message.strip()
    if not content:
        raise HTTPException(status_code=400, detail="Message is empty")
    if len(content) > settings.chat_max_input_chars:
        raise HTTPException(status_code=413, detail="Message is too long")

    conversation = (
        store.get(request.conversation_id)
        if request.conversation_id
        else store.create(content)
    )
    if conversation is None:
        raise HTTPException(status_code=404, detail="Conversation not found")

    store.append_message(conversation.id, "user", content)
    updated = store.get(conversation.id)
    if updated is None:
        raise HTTPException(status_code=500, detail="Conversation storage failed")

    document_context = ""
    source_hits: list[SearchHit] = []
    if request.use_documents:
        document_context, source_hits = documents.context_for(
            query=content,
            limit=settings.document_context_limit,
            max_chars=settings.document_context_max_chars,
        )

    limited_history = updated.messages[-settings.chat_history_limit :]
    llm_messages = to_llm_messages(
        settings.system_prompt,
        limited_history,
        document_context=document_context,
    )

    try:
        reply = await llm.complete(llm_messages)
    except LLMAuthError as exc:
        raise HTTPException(status_code=401, detail=str(exc)) from exc
    except LLMRateLimitError as exc:
        raise HTTPException(status_code=429, detail=str(exc)) from exc
    except LLMError as exc:
        raise HTTPException(status_code=502, detail=str(exc)) from exc

    assistant_message = store.append_message(conversation.id, "assistant", reply)
    return ChatResponse(
        conversation_id=conversation.id,
        mode=settings.llm_mode,
        message=assistant_message,
        sources=[source_from_hit(hit) for hit in source_hits],
    )


def source_from_hit(hit: SearchHit) -> Source:
    return Source(
        path=hit.document_path,
        title=hit.title,
        chunk_id=hit.chunk_id,
        score=hit.score,
        snippet=hit.snippet,
    )
