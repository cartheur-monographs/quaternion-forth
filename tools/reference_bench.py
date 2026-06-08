#!/usr/bin/env python3
"""Run the optional reference-platform quaternion benchmarks under gforth."""

from __future__ import annotations

import argparse
import platform
import statistics
import subprocess
import sys
import time
from pathlib import Path


ROOT = Path(__file__).resolve().parent.parent
BENCH_FILE = ROOT / "forth" / "benchmarks.fs"


def run_cmd(args: list[str]) -> subprocess.CompletedProcess[str]:
    return subprocess.run(args, cwd=ROOT, text=True, capture_output=True, check=True)


def gforth_eval(code: str) -> str:
    proc = run_cmd(["gforth", str(BENCH_FILE), "-e", f"{code} bye"])
    return proc.stdout.strip()


def measure(word: str, iterations: int, repeats: int) -> dict[str, object]:
    samples = []
    for _ in range(repeats):
        start = time.perf_counter()
        run_cmd(
            [
                "gforth",
                str(BENCH_FILE),
                "-e",
                f"{iterations} bench-set-iterations {word} bye",
            ]
        )
        elapsed = time.perf_counter() - start
        samples.append(elapsed)

    return {
        "word": word,
        "samples": samples,
        "median": statistics.median(samples),
        "minimum": min(samples),
        "maximum": max(samples),
    }


def format_seconds(value: float) -> str:
    return f"{value:.6f} s"


def get_git_rev() -> str:
    try:
        return run_cmd(["git", "rev-parse", "--short", "HEAD"]).stdout.strip()
    except Exception:
        return "unknown"


def get_gforth_version() -> str:
    proc = subprocess.run(
        ["gforth", "--version"],
        cwd=ROOT,
        text=True,
        capture_output=True,
        check=True,
    )
    return (proc.stdout or proc.stderr).strip()


def get_uname() -> str:
    return f"{platform.system()} {platform.release()} ({platform.machine()})"


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--iterations", type=int, default=100000)
    parser.add_argument("--repeats", type=int, default=5)
    args = parser.parse_args()

    qreport = gforth_eval("bench-qrotate-report cr bench-mrotate-report cr")
    lines = [line.strip() for line in qreport.splitlines() if line.strip()]
    expected = ["10 -20 -30", "10 -20 -30"]
    if lines != expected:
        print("correctness check failed", file=sys.stderr)
        print(qreport, file=sys.stderr)
        return 1

    results = [
        measure("bench-qrotate", args.iterations, args.repeats),
        measure("bench-mrotate", args.iterations, args.repeats),
        measure("bench-q*", args.iterations, args.repeats),
        measure("bench-mmultiply", args.iterations, args.repeats),
    ]

    print("# Reference Platform Calibration")
    print()
    print(f"- Machine: {platform.node() or 'unknown'}")
    print(f"- Architecture: {platform.machine()}")
    print(f"- OS: {get_uname()}")
    print(f"- `gforth` version: {get_gforth_version()}")
    print(f"- Source revision: {get_git_rev()}")
    print(f"- Iterations: {args.iterations}")
    print(f"- Repeats per benchmark: {args.repeats}")
    print("- Correctness check:")
    print("  - `bench-qrotate-report`: `10 -20 -30`")
    print("  - `bench-mrotate-report`: `10 -20 -30`")
    print()
    print("| Benchmark | Median | Min | Max |")
    print("| --- | ---: | ---: | ---: |")
    for item in results:
        print(
            f"| `{item['word']}` | {format_seconds(item['median'])}"
            f" | {format_seconds(item['minimum'])}"
            f" | {format_seconds(item['maximum'])} |"
        )
    print()
    print("- Notes:")
    print("  - Record Pi model and thermal conditions separately if relevant.")
    print("  - Treat this as appendix data, not the paper's primary evidence.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
