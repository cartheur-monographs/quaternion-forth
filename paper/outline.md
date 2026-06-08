# Outline: Quaternions in polyForth on Volatco

## Working title

Reasonable Quaternion Arithmetic for `polyForth` on Volatco:
Representation, Implementation, and Performance Evidence

## Abstract

State the engineering problem, the representation choice, the Volatco
constraints, and the measured outcome. Keep the claim narrow and empirical.

## 1. Introduction

- Why quaternion arithmetic is relevant to low-latency control and rotation.
- Why Volatco and `polyForth` are an unusual but relevant platform.
- Why the paper emphasizes reproducibility instead of abstract elegance alone.

## 2. Background

- Historical quaternion references in `quaternions/`.
- Existing alternatives: vector algebra, matrices, paired complex numbers.
- Prior art on quaternion computation and embedded arithmetic.

## 3. Target platform

- Volatco architecture summary.
- `polyForth` execution model.
- Numeric and storage constraints of `18-bit` F18A nodes.
- Why asynchronous execution matters for measurement design.

## 4. Representation choice

- Tuple layout `(a b c d)`.
- Scalar-first stack convention.
- Fixed-point scaling strategy.
- Tradeoff against matrix representation.

## 5. Implementation

- Core quaternion words: `q+`, `q-`, `q*`, `qconj`, `qnorm2`, `qrotate`.
- Comparison matrix words: `m3*`, `m3*v`.
- Stack effects and memory discipline.
- Portability assumptions and target-specific adaptations.

## 6. Benchmark protocol

- Timing method.
- Power measurement method.
- Code-size measurement method.
- Input workloads and iteration counts.
- Pass/fail criteria for reproducibility.

## 7. Results

- Runtime table.
- Energy or current-draw table.
- Code footprint table.
- Quaternion versus matrix comparison table.
- Notes on determinism and watchdog behavior.

## 8. Discussion

- Where quaternions are superior.
- Where they are not.
- Whether any observed gain survives the cost of normalization and setup.
- Limits of the measurement.
- Future optimization work across multiple nodes.

## 9. Conclusion

- Restrained statement of what the evidence supports.

## Appendices

- Full source listings.
- Exact hardware setup.
- Serial/programming workflow.
