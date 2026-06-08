# Volatco Test Runbook

This runbook is the shortest path from the current repo state to useful
hardware results on Volatco.

## Goal

Confirm that the quaternion and matrix benchmark fixtures produce the same
rotation, then record timing, power, and code-size data for the benchmark
words.

## Files to load

- `forth/quaternion.fs`
- `forth/matrix.fs`
- `forth/benchmarks.fs`

If your `polyForth` image supports relative `include`, loading
`forth/benchmarks.fs` should be sufficient because it includes the other two
files.

## Known aligned rotation fixture

The benchmark uses a concrete rotation that stays integer-only:

- quaternion rotation operand: `(0, 1, 0, 0)`
- input vector: `(10, 20, 30)`
- expected rotated vector: `(10, -20, -30)`

The matrix benchmark uses the equivalent matrix:

```text
[ 1  0  0 ]
[ 0 -1  0 ]
[ 0  0 -1 ]
```

## First correctness checks

Run these words before measuring performance:

- `bench-qrotate-report`
- `bench-mrotate-report`

Expected printed component order in each case:

- `10 -20 -30`

Optional full quaternion inspection:

- `bench-qrotate-full`

If you inspect `bench-qrotate-full` or `bench-qrotate-vector` manually with
repeated `.` operations, remember that plain stack printing shows the top item
first. The `*-report` words are the easiest human check.

## Benchmark words to measure

- `bench-q+`
- `bench-q*`
- `bench-qnorm2`
- `bench-qrotate`
- `bench-mrotate`
- `bench-mmultiply`

## Iteration control

Default iterations:

- `1000`

To set a new count:

- `5000 bench-set-iterations`

Use the same iteration count for quaternion and matrix comparisons.

## What to record

For each benchmark:

- board revision
- runtime image or boot image revision
- whether boot-from-flash or development mode was used
- jumper and serial setup
- iteration count
- timing method
- elapsed time
- current or power measurement point
- measured current or power
- code size if available
- pass/fail outcome
- notes about resets, watchdog behavior, or anomalies

## Minimum useful first pass

If time is limited, measure these first:

1. `bench-qrotate`
2. `bench-mrotate`
3. `bench-q*`
4. `bench-mmultiply`

That gives the paper its core empirical comparison.

## Where to store results

Enter the measurements into:

- `paper/results.md`

Then revise the LaTeX paper:

- replace TODOs tied to results
- add a real results table
- update the claim to match the measured evidence
