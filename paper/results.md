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
