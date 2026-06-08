# Volatco Test Runbook

This runbook is the shortest path from the current repo state to useful
hardware results on Volatco.

## Goal

Confirm that the quaternion and matrix benchmark fixtures produce the same
rotation, then record timing, power, and code-size data for the benchmark
words.

## Files to load

- `polyforth/volatco-blocks.pf`

The `polyforth/` source is the Volatco-facing version. The `forth/` directory
remains the reference implementation used for local checking under `gforth`.
Do not use `polyforth/dev-template-blocks.pf` for hardware testing; it is only
for future development.

## Terminal sequence

This is the minimum sequence to type or run on Volatco:

1. Load or enter `polyforth/volatco-blocks.pf`.
2. Type `qreport`
3. Type `mreport`
4. If both print `10 -20 -30`, type `1000 biter!`
5. Then measure:
   `bqr`
   `bmr`
   `bq*`
   `bmm`

If `qreport` and `mreport` do not both print `10 -20 -30`, stop and fix the
loading or dialect issue before measuring anything.

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

- `qreport`
- `mreport`

Expected printed component order in each case:

- `10 -20 -30`

Optional full quaternion inspection:

- there is no separate full-report helper in the `.pf` target file yet; use
  `qreport` for the first correctness check

## Benchmark words to measure

- `bq+`
- `bq*`
- `bqn`
- `bqr`
- `bmr`
- `bmm`

## Iteration control

Default iterations:

- `1000`

To set a new count:

- `5000 biter!`

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

1. `bqr`
2. `bmr`
3. `bq*`
4. `bmm`

That gives the paper its core empirical comparison.

## Where to store results

Enter the measurements into:

- `paper/results.md`

Then revise the LaTeX paper:

- replace TODOs tied to results
- add a real results table
- update the claim to match the measured evidence
