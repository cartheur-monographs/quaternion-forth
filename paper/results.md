# Results Log

This file is the place for hardware measurements once Volatco runs are
available.

## Experiment 0: Deployment and correctness validation

This experiment exists to show that the paper's deployment path is real and
repeatable. It is a methodology checkpoint, not a substitute for the later
performance comparison.

### Deployment validation run

- Date:
- Board revision:
- Runtime image:
- Source revision:
- Serial path:
- Boot mode:
- Operator command:
  - `AFORTH`
  - `10 LOAD`
- `qreport` output:
- `mreport` output:
- Pass/fail:
- Notes:

Pass only if both report words print:

- `10 -20 -30`

If this experiment fails, do not record any benchmark timings as paper
evidence.

## Experiment 1: Performance comparison

Use the benchmark words only after Experiment 0 passes.

Run and record at least these words:

- `bqr`
- `bmr`
- `bq*`
- `bmm`

Optional additional words:

- `bq+`
- `bqn`

## Experiment 2: Optional reference-platform calibration

Use this section only after the Volatco results exist.

Suggested platform:

- Raspberry Pi `arm64`

Fallback platform:

- `amd64` Linux machine

Use the `gforth` reference implementation and record the same relative
quaternion-versus-matrix comparison. Treat the result as secondary or appendix
material.

Preferred command:

```bash
python3 tools/reference_bench.py --iterations 100000 --repeats 5
```

### Reference platform template

- Date:
- Machine:
- Architecture:
- OS:
- `gforth` version:
- Source revision:
- Iterations:
- `bench-qrotate` elapsed time:
- `bench-mrotate` elapsed time:
- `bench-q*` elapsed time:
- `bench-mmultiply` elapsed time:
- Notes:

### First reference-platform run

- Date: 2026-06-08
- Machine: Raspberry Pi 5 Model B Rev 1.0
- Architecture: `aarch64`
- OS: Linux `6.12.87+rpt-rpi-v8`
- `gforth` version: `gforth 0.7.3`
- Source revision: `f1f63d1`
- Iterations: `100000`
- `bench-qrotate` elapsed time: median `0.155188 s`
- `bench-mrotate` elapsed time: median `0.047465 s`
- `bench-q*` elapsed time: median `0.068970 s`
- `bench-mmultiply` elapsed time: median `0.126295 s`
- Notes:
  - Correctness checks passed:
    - `bench-qrotate-report` -> `10 -20 -30`
    - `bench-mrotate-report` -> `10 -20 -30`
  - This is appendix-grade calibration data only.
  - The matrix rotation path is materially faster than quaternion rotation on
    this Pi run, while quaternion multiplication is materially faster than
    matrix-matrix multiplication.

## Benchmark template

### Benchmark name

- Date:
- Board revision:
- Runtime image:
- Source revision:
- Iterations:
- Inputs:
- Timing method:
- Measurement point:
- Elapsed time:
- Current / power:
- Code size:
- Pass/fail:
- Notes:
