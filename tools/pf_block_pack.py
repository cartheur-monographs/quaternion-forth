#!/usr/bin/env python3
"""Pack Brodie-style polyForth blocks into a Volatco project image."""

from __future__ import annotations

import argparse
from pathlib import Path


BLOCK_BYTES = 1024
LINES_PER_BLOCK = 16
CHARS_PER_LINE = 64
PROJECT_BLOCKS = 4800
DEFAULT_LOAD_BLOCK = 10
DEFAULT_SOURCE_START = 11

DEFAULT_BLOCK0 = [
    "( Volatco Project)   EMPTY AFORTH",
    "( Load quaternion app with 10 LOAD after entering AFORTH )",
    "( Blocks 0-9 reserved. Source lives in 11-30. )",
]


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Build a 4800-block VOLATCO.src image from a .pf block file."
    )
    parser.add_argument("source", help="Input .pf file with '\\ block NN' markers")
    parser.add_argument("output", help="Output .src image path")
    parser.add_argument(
        "--base-image",
        help="Optional existing 4800-block image to overlay instead of creating blank",
    )
    parser.add_argument(
        "--load-block",
        type=int,
        default=DEFAULT_LOAD_BLOCK,
        help="Block number for the application load block",
    )
    parser.add_argument(
        "--source-start",
        type=int,
        default=DEFAULT_SOURCE_START,
        help="First block number used for source from the .pf file",
    )
    parser.add_argument(
        "--project-blocks",
        type=int,
        default=PROJECT_BLOCKS,
        help="Total block count in the project image",
    )
    return parser.parse_args()


def parse_pf_blocks(path: Path) -> list[dict[str, object]]:
    blocks: list[dict[str, object]] = []
    current: dict[str, object] | None = None

    for lineno, raw_line in enumerate(path.read_text().splitlines(), start=1):
        if raw_line.startswith("\\ block "):
            if current is not None:
                blocks.append(current)
            current = {"header": raw_line, "lines": []}
            continue

        if current is None:
            raise SystemExit(f"{path}:{lineno}: content appears before first \\ block header")

        lines = current["lines"]
        assert isinstance(lines, list)
        lines.append(raw_line)

    if current is not None:
        blocks.append(current)

    if not blocks:
        raise SystemExit(f"{path}: no '\\ block' sections found")

    return blocks


def encode_block(lines: list[str], *, label: str) -> bytes:
    if len(lines) > LINES_PER_BLOCK:
        raise SystemExit(f"{label}: has {len(lines)} lines, exceeds {LINES_PER_BLOCK}")

    encoded = bytearray()
    for index in range(LINES_PER_BLOCK):
        line = lines[index] if index < len(lines) else ""
        if len(line) > CHARS_PER_LINE:
            raise SystemExit(
                f"{label}: line {index + 1} has {len(line)} chars, exceeds {CHARS_PER_LINE}"
            )
        encoded.extend(line.ljust(CHARS_PER_LINE).encode("ascii"))

    return bytes(encoded)


def build_load_block(source_start: int, source_count: int) -> list[str]:
    lines = [
        "( Quaternion project load block )",
        "( Type 10 LOAD after AFORTH )",
        "( Source blocks are loaded explicitly for reproducibility. )",
    ]

    line = ""
    for block_no in range(source_start, source_start + source_count):
        token = f"{block_no} LOAD"
        if not line:
            line = token
            continue
        if len(line) + 1 + len(token) <= CHARS_PER_LINE:
            line += " " + token
            continue
        lines.append(line)
        line = token

    if line:
        lines.append(line)

    lines.append("( Then type qreport and mreport manually. )")
    return lines


def ensure_image(path: Path | None, project_blocks: int) -> bytearray:
    if path is None:
        image = bytearray(b" " * (project_blocks * BLOCK_BYTES))
        image[0:BLOCK_BYTES] = encode_block(DEFAULT_BLOCK0, label="default block 0")
        return image

    data = bytearray(path.read_bytes())
    expected = project_blocks * BLOCK_BYTES
    if len(data) != expected:
        raise SystemExit(f"{path}: expected {expected} bytes, found {len(data)}")
    return data


def write_block(image: bytearray, block_no: int, lines: list[str], *, label: str) -> None:
    offset = block_no * BLOCK_BYTES
    image[offset : offset + BLOCK_BYTES] = encode_block(lines, label=label)


def main() -> int:
    args = parse_args()
    source = Path(args.source)
    output = Path(args.output)
    base_image = Path(args.base_image) if args.base_image else None

    blocks = parse_pf_blocks(source)
    image = ensure_image(base_image, args.project_blocks)

    if args.source_start <= args.load_block:
        raise SystemExit("source-start must be greater than load-block")

    last_source_block = args.source_start + len(blocks) - 1
    if last_source_block >= args.project_blocks:
        raise SystemExit("source blocks do not fit in requested project image")

    load_lines = build_load_block(args.source_start, len(blocks))
    write_block(image, args.load_block, load_lines, label=f"load block {args.load_block}")

    for index, block in enumerate(blocks):
        block_no = args.source_start + index
        lines = list(block["lines"])
        write_block(image, block_no, lines, label=f"{source.name} -> block {block_no}")

    output.parent.mkdir(parents=True, exist_ok=True)
    output.write_bytes(image)

    print(f"wrote {output}")
    print(f"load block: {args.load_block}")
    print(f"source blocks: {args.source_start}-{last_source_block}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
