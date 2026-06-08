# Volatco Test Runbook

This runbook is the shortest path from the current repo state to useful
hardware results on Volatco.

## Goal

Confirm that the quaternion and matrix benchmark fixtures produce the same
rotation, then record timing, power, and code-size data for the benchmark
words.

## Preferred deployment

Use a generated Volatco project image, not a terminal paste, for the
reproducible path described by the paper.

Generate the image locally:

```bash
python3 tools/pf_block_pack.py \
  polyforth/volatco-blocks.pf \
  deploy/windows7-volatco/VOLATCO.src
```

Then place `deploy/windows7-volatco/VOLATCO.src` into the saneForth Volatco
project directory on the Windows machine so it becomes the active project image.

## Files to load

- `deploy/windows7-volatco/VOLATCO.src`

The `polyforth/` source remains the human-readable Volatco-facing source used
to generate the image. The `forth/` directory remains the reference
implementation used for local checking under `gforth`. Do not use
`polyforth/dev-template-blocks.pf` for hardware testing; it is only for future
development.

## Fallback automated loading option

If you are experimenting and do not want to replace the project image, you can
still try the host-side uploader:

```bash
python3 tools/volatco_upload.py polyforth/volatco-blocks.pf --port /dev/ttyUSB0
```

Start with conservative pacing until the exact terminal behavior is known:

```bash
python3 tools/volatco_upload.py \
  polyforth/volatco-blocks.pf \
  --port /dev/ttyUSB0 \
  --line-delay 0.10 \
  --read-timeout 0.30
```

If the target emits a stable prompt, you can later experiment with:

```bash
python3 tools/volatco_upload.py \
  polyforth/volatco-blocks.pf \
  --port /dev/ttyUSB0 \
  --wait-for-prompt
```

For an old Windows 7 machine with Tcl installed, use:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf
```

## Terminal sequence

This is the minimum sequence to type or run on Volatco:

1. Enter `AFORTH`.
2. Type `10 LOAD`.
3. Type `qreport`
4. Type `mreport`
5. If both print `10 -20 -30`, type `1000 biter!`
6. Then measure:
   `bqr`
   `bmr`
   `bq*`
   `bmm`

If `qreport` and `mreport` do not both print `10 -20 -30`, stop and fix the
loading, block mapping, or dialect issue before measuring anything.

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

## Block map used by this repo

- blocks `0` through `9` are treated as reserved
- block `10` is the quaternion project load block
- blocks `11` through `30` contain the source generated from
  `polyforth/volatco-blocks.pf`
