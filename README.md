# quaternion-forth

This repository is a working scaffold for a mathematical and engineering paper
about quaternion arithmetic in `polyForth` on Volatco hardware.

The current goal is not to claim a finished result. The goal is to build a
reproducible path from literature, to implementation, to measurement, so that a
paper can make a defensible performance argument.

_Background_

It's ALL about this....

![bassic](/images/bassic.png)

Cayley graph of the quaternion group Q8 showing the six cycles of multiplication by i, j and k.

## Motivation

On Volatco-class asynchronous embedded systems, the representation of rotational
state is a systems design decision rather than a cosmetic mathematical choice.
Storage, latency, and power are constrained, so this repository asks whether a
fixed-point quaternion representation yields a better tradeoff than a `3x3`
matrix representation for bounded rotational workloads in `polyForth`.

## Repository layout

- `paper/`
  LaTeX manuscript, bibliography, build file, and measurement protocol.
- `polyforth/`
  Block-oriented `polyForth` source and block-layout notes for Volatco.
- `publish/`
  Submission planning notes, including journal targets and fit analysis.
- `test/`
  Hardware runbooks and execution notes for Volatco validation and optional
  reference-platform calibration.
- `tools/`
  Host-side helpers such as the serial uploader for Volatco.
- `forth/`
  Portable Forth source intended as a starting point for `polyForth`.
- `quaternions/`
  Background papers and historical references.

## Research claim

The core claim this repository is structured to test is:

> A fixed-point quaternion representation is a reasonable rotational algebra
> for `polyForth` on Volatco because it keeps state compact, composes cleanly,
> and can be benchmarked against matrix-based alternatives under explicit power
> and latency constraints.

## Current status

This repository now contains:

- a LaTeX paper source with PDF build output
- a first quaternion package in Forth
- a companion `3x3` matrix comparison kernel
- a benchmark scaffold
- a block-image generation path for Volatco project deployment
- a protocol for turning benchmark results into manuscript evidence

It does not yet contain:

- confirmed `polyForth` target-specific compatibility fixes
- hardware measurements from a real Volatco board
- a completed bibliography or final paper text

## Next steps

1. Adapt the Forth words to the exact `polyForth` image available on Volatco.
2. Decide on numeric format: integer, scaled fixed-point, or mixed strategy.
3. Run the benchmark protocol on hardware.
4. Record timing, power, and code-size results in `paper/results.md`.
5. Revise the manuscript claim to match measured evidence.
