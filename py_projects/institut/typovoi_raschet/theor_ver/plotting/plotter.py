import json
import sys

import matplotlib

matplotlib.use("Agg")
import matplotlib.pyplot as plt
import numpy as np


def save(output):
    plt.tight_layout()
    plt.savefig(output, dpi=160)
    plt.close()


def histogram(payload):
    data = np.array(payload["data"], dtype=float)
    plt.figure(figsize=(10, 5))
    counts, bins, _ = plt.hist(
        data,
        bins=payload.get("bins", 30),
        color="white",
        edgecolor="black",
        alpha=0.85,
    )
    mid = 0.5 * (bins[1:] + bins[:-1])
    plt.plot(mid, counts, color="tab:blue", marker="o", label="Полигон частот")
    plt.title(payload.get("title", "Гистограмма"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "y"))
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def ecdf(payload):
    data = np.sort(np.array(payload["data"], dtype=float))
    y = np.arange(1, len(data) + 1) / len(data)
    plt.figure(figsize=(10, 5))
    plt.step(data, y, where="post", color="tab:green", label="Эмпирическая функция")
    plt.title(payload.get("title", "Эмпирическая функция распределения"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "F(x)"))
    plt.ylim(0, 1.05)
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def scatter_lines(payload):
    plt.figure(figsize=(10, 6))
    plt.scatter(payload["x"], payload["y"], color="tab:blue", label="Данные")
    for line in payload.get("lines", []):
        plt.plot(line["x"], line["y"], label=line.get("label", "Линия"), color=line.get("color"))
    plt.title(payload.get("title", "График"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "y"))
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def line_series(payload):
    plt.figure(figsize=(10, 6))
    scatter = payload.get("scatter")
    if scatter:
        plt.scatter(scatter["x"], scatter["y"], color="black", label=scatter.get("label", "Данные"))
    for line in payload.get("series", []):
        plt.plot(line["x"], line["y"], label=line.get("label", "Линия"), color=line.get("color"))
    plt.title(payload.get("title", "График"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "y"))
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def histogram_polygon(payload):
    data = np.array(payload["data"], dtype=float)
    bins = np.array(payload["bins"], dtype=float)
    plt.figure(figsize=(12, 6))
    counts, edges, _ = plt.hist(data, bins=bins, density=True, color="c", alpha=0.6, label="Гистограмма")
    raw_counts, _ = np.histogram(data, bins=bins)
    relative = raw_counts / len(data)
    mid = edges[:-1] + np.diff(edges) / 2
    plt.plot(mid, relative, marker="o", color="r", label="Полигон относительных частот")
    plt.title(payload.get("title", "Гистограмма и полигон"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "y"))
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def histogram_curve(payload):
    data = np.array(payload["data"], dtype=float)
    bins = np.array(payload["bins"], dtype=float)
    curve = payload["curve"]
    plt.figure(figsize=(12, 6))
    plt.hist(data, bins=bins, density=True, color="c", alpha=0.6, label="Гистограмма")
    plt.plot(curve["x"], curve["y"], color="m", label=curve.get("label", "Кривая"))
    plt.title(payload.get("title", "Гистограмма и кривая"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "y"))
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


def ecdf_curve(payload):
    data = np.sort(np.array(payload["data"], dtype=float))
    y = np.arange(1, len(data) + 1) / len(data)
    curve = payload["curve"]
    plt.figure(figsize=(12, 6))
    plt.step(data, y, where="post", label="Эмпирическая функция распределения")
    plt.plot(curve["x"], curve["y"], color="m", label=curve.get("label", "Кривая"))
    plt.title(payload.get("title", "Функция распределения"))
    plt.xlabel(payload.get("xlabel", "x"))
    plt.ylabel(payload.get("ylabel", "F(x)"))
    plt.ylim(0, 1.05)
    plt.grid(True, alpha=0.25)
    plt.legend()
    save(payload["output"])


HANDLERS = {
    "histogram": histogram,
    "ecdf": ecdf,
    "scatter_lines": scatter_lines,
    "line_series": line_series,
    "histogram_polygon": histogram_polygon,
    "histogram_curve": histogram_curve,
    "ecdf_curve": ecdf_curve,
}


def main():
    with open(sys.argv[1], "r", encoding="utf-8") as file:
        payload = json.load(file)
    HANDLERS[payload["type"]](payload)


if __name__ == "__main__":
    main()
