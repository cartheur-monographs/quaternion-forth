# Results Log

This file is the place for hardware measurements once Volatco runs are
available.

## Template

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

## First hardware campaign checklist

Run and record at least these words:

- `bench-qrotate`
- `bench-mrotate`
- `bench-q*`
- `bench-mmultiply`

Correctness checks before timing:

- `bench-qrotate-report` should print `10 -20 -30`
- `bench-mrotate-report` should print `10 -20 -30`
- `bench-qrotate-full` should yield `0 10 -20 -30`
