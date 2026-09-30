from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
import re


WORD_PATTERN = re.compile(r"[\wа-яА-ЯёЁ]+", re.UNICODE)


@dataclass(frozen=True)
class Document:
    path: str
    title: str
    size_bytes: int
    modified_at: float


@dataclass(frozen=True)
class DocumentChunk:
    document_path: str
    title: str
    chunk_id: int
    content: str


@dataclass(frozen=True)
class SearchHit:
    document_path: str
    title: str
    chunk_id: int
    score: int
    snippet: str


@dataclass(frozen=True)
class IndexStats:
    root_dir: str
    document_count: int
    chunk_count: int
    supported_extensions: list[str]


class DocumentIndex:
    def __init__(
        self,
        root_dir: Path,
        supported_extensions: set[str],
        chunk_chars: int,
    ) -> None:
        self.root_dir = root_dir.resolve()
        self.supported_extensions = supported_extensions
        self.chunk_chars = chunk_chars
        self.documents: list[Document] = []
        self.chunks: list[DocumentChunk] = []
        self.root_dir.mkdir(parents=True, exist_ok=True)
        self.reindex()

    def reindex(self) -> IndexStats:
        documents: list[Document] = []
        chunks: list[DocumentChunk] = []

        for path in self._iter_files():
            try:
                text = path.read_text(encoding="utf-8")
            except UnicodeDecodeError:
                text = path.read_text(encoding="utf-8", errors="ignore")
            except OSError:
                continue

            stat = path.stat()
            relative_path = path.relative_to(self.root_dir).as_posix()
            title = self._title_for(path, text)
            documents.append(
                Document(
                    path=relative_path,
                    title=title,
                    size_bytes=stat.st_size,
                    modified_at=stat.st_mtime,
                )
            )

            for chunk_id, chunk in enumerate(self._chunk_text(text)):
                chunks.append(
                    DocumentChunk(
                        document_path=relative_path,
                        title=title,
                        chunk_id=chunk_id,
                        content=chunk,
                    )
                )

        self.documents = sorted(documents, key=lambda item: item.path)
        self.chunks = chunks
        return self.stats()

    def stats(self) -> IndexStats:
        return IndexStats(
            root_dir=str(self.root_dir),
            document_count=len(self.documents),
            chunk_count=len(self.chunks),
            supported_extensions=sorted(self.supported_extensions),
        )

    def list_documents(self) -> list[Document]:
        return self.documents

    def search(self, query: str, limit: int) -> list[SearchHit]:
        terms = self._tokenize(query)
        if not terms:
            return []

        hits: list[SearchHit] = []
        for chunk in self.chunks:
            chunk_terms = self._tokenize(chunk.content)
            score = sum(chunk_terms.count(term) for term in terms)
            if score == 0:
                continue
            hits.append(
                SearchHit(
                    document_path=chunk.document_path,
                    title=chunk.title,
                    chunk_id=chunk.chunk_id,
                    score=score,
                    snippet=self._snippet(chunk.content, terms),
                )
            )

        return sorted(
            hits,
            key=lambda item: (item.score, item.title),
            reverse=True,
        )[:limit]

    def context_for(self, query: str, limit: int, max_chars: int) -> tuple[str, list[SearchHit]]:
        hits = self.search(query, limit)
        parts: list[str] = []
        remaining = max_chars

        for hit in hits:
            chunk = self._find_chunk(hit.document_path, hit.chunk_id)
            if chunk is None:
                continue
            header = f"[{hit.title} | {hit.document_path}#chunk-{hit.chunk_id}]"
            body = chunk.content.strip()
            text = f"{header}\n{body}"
            if len(text) > remaining:
                text = text[:remaining].rstrip()
            if text:
                parts.append(text)
                remaining -= len(text)
            if remaining <= 0:
                break

        return "\n\n---\n\n".join(parts), hits

    def _iter_files(self) -> list[Path]:
        files: list[Path] = []
        for path in self.root_dir.rglob("*"):
            if not path.is_file():
                continue
            if path.suffix.lower() not in self.supported_extensions:
                continue
            resolved = path.resolve()
            if self.root_dir not in resolved.parents and resolved != self.root_dir:
                continue
            files.append(resolved)
        return sorted(files)

    def _chunk_text(self, text: str) -> list[str]:
        normalized = "\n".join(line.rstrip() for line in text.splitlines()).strip()
        if not normalized:
            return []

        chunks: list[str] = []
        paragraphs = re.split(r"\n\s*\n", normalized)
        current = ""
        for paragraph in paragraphs:
            paragraph = paragraph.strip()
            if not paragraph:
                continue
            if len(current) + len(paragraph) + 2 <= self.chunk_chars:
                current = f"{current}\n\n{paragraph}".strip()
                continue
            if current:
                chunks.append(current)
            current = paragraph
            while len(current) > self.chunk_chars:
                chunks.append(current[: self.chunk_chars].strip())
                current = current[self.chunk_chars :].strip()
        if current:
            chunks.append(current)
        return chunks

    def _title_for(self, path: Path, text: str) -> str:
        for line in text.splitlines():
            stripped = line.strip()
            if stripped.startswith("#"):
                return stripped.lstrip("#").strip() or path.stem
            if stripped:
                return stripped[:80]
        return path.stem

    def _find_chunk(self, document_path: str, chunk_id: int) -> DocumentChunk | None:
        for chunk in self.chunks:
            if chunk.document_path == document_path and chunk.chunk_id == chunk_id:
                return chunk
        return None

    def _snippet(self, text: str, terms: list[str]) -> str:
        lowered = text.lower()
        positions = [lowered.find(term.lower()) for term in terms]
        positions = [position for position in positions if position >= 0]
        start = max(min(positions) - 120, 0) if positions else 0
        end = min(start + 360, len(text))
        snippet = " ".join(text[start:end].split())
        if start > 0:
            snippet = f"...{snippet}"
        if end < len(text):
            snippet = f"{snippet}..."
        return snippet

    def _tokenize(self, text: str) -> list[str]:
        return [match.group(0).lower() for match in WORD_PATTERN.finditer(text)]
