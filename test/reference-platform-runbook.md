# Reference Platform Runbook

This runbook defines an optional secondary experiment for a conventional
machine. It is not the paper's main result.

## Purpose

Use this experiment only after the Volatco deployment and same-platform
comparison exist.

The goal is not to show that Volatco is faster than a desktop or a Raspberry
Pi. The goal is to see whether the quaternion-versus-matrix tradeoff looks
similar or different on a mainstream machine.

## Recommended platform

Preferred secondary platform:

- Raspberry Pi `arm64`

Acceptable fallback:

- `amd64` Linux machine

The Raspberry Pi is preferred because it is closer to embedded practice and
less likely to distract the paper toward meaningless desktop-throughput claims.

## Implementation basis

Use the `gforth` reference implementation:

- `forth/quaternion.fs`
- `forth/matrix.fs`
- `forth/benchmarks.fs`

Do not use the `polyForth` block-image path for this experiment.

## First correctness check

Run:

```bash
gforth forth/benchmarks.fs -e 'bench-qrotate-report cr bench-mrotate-report cr bye'
```

Expected output:

```text
10 -20 -30
10 -20 -30
```

## Suggested measurement approach

Preferred first pass:

```bash
python3 tools/reference_bench.py --iterations 100000 --repeats 5
```

This prints a Markdown-ready calibration block that can be copied into
`paper/results.md`.

Fallback manual timing:

```bash
time gforth forth/benchmarks.fs -e '100000 bench-set-iterations bench-qrotate bye'
time gforth forth/benchmarks.fs -e '100000 bench-set-iterations bench-mrotate bye'
time gforth forth/benchmarks.fs -e '100000 bench-set-iterations bench-q* bye'
time gforth forth/benchmarks.fs -e '100000 bench-set-iterations bench-mmultiply bye'
```

If you want a cleaner comparison later, increase repeats or pin CPU governor on
the Raspberry Pi, but do not delay the first pass waiting for perfection.

## What to record

- machine model
- CPU architecture
- OS and kernel
- `gforth` version
- source revision
- iteration count
- elapsed time
- notes about load or thermal throttling

## How to use this in the paper

Treat this as an appendix or secondary table.

Good use:

- compare quaternion-versus-matrix ordering on the reference platform
- note whether the Volatco result is qualitatively similar or different

Bad use:

- claim raw speed superiority of Volatco over a general-purpose processor
- make the desktop or Raspberry Pi result the main evidence
