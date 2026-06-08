# Benchmark Protocol for Volatco Quaternion Experiments

## Goal

Produce a reproducible performance argument for quaternion arithmetic in
`polyForth` on Volatco.

## Comparison

Compare the quaternion kernel against the included alternative:

- `3x3` rotation matrix implementation

Optional secondary comparisons may later include:

- paired complex pipeline
- Euler-angle pipeline

## Metrics

Record the following for each workload:

- board revision
- firmware or boot image revision
- exact `polyForth` source
- iteration count
- elapsed time
- current or power reading
- code size
- pass/fail outcome
- notes on reset or watchdog events

## Workloads

Use at least these workloads:

1. repeated quaternion addition
2. repeated Hamilton product
3. repeated norm-squared
4. repeated vector rotation
5. repeated matrix-vector rotation
6. repeated matrix-matrix multiplication

## Reproducibility rules

Each reported result should include:

- explicit input values
- exact iteration count
- exact jumper and serial setup
- measurement point used for current or power
- statement of whether boot-from-flash or development mode was used

## Pass/fail criteria

A run passes only if:

- it completes without unexpected reset
- the output matches expected values
- the timing method is stated
- the power measurement location is stated

## Reporting format

Store raw notes in `paper/results.md` with one section per benchmark:

- configuration
- source revision
- measured values
- interpretation
- anomalies
