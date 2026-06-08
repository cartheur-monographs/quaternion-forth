# Final Analysis

## Current position

The project has moved from a speculative scaffold to a defensible experimental
package.

What now exists:

- a formal LaTeX paper with a reproducible deployment story
- a Volatco `polyForth` block-image path based on `VOLATCO.src`
- an explicit Volatco experiment structure:
  - Experiment 0: deployment and correctness validation
  - Experiment 1: same-platform performance comparison
- a secondary reference-platform calibration on Raspberry Pi `arm64`
- a paper/results structure that can absorb real Volatco data cleanly

## What is now established

The paper no longer depends on an informal "type this into the terminal"
workflow. It now describes a block-resident deployment method that matches the
saneForth / Volatco project model.

The reference-platform experiment also already provides a useful caution:

- matrix-vector rotation was faster than quaternion rotation on Raspberry Pi
- quaternion multiplication was faster than matrix-matrix multiplication

That mixed result is valuable because it prevents the paper from making a
universal or ideological claim. It supports a narrower and more credible claim:
quaternion reasonableness is workload-dependent and platform-dependent.

## Main remaining gaps

The remaining unresolved work is now concentrated in a small number of areas:

1. real Volatco measurements
2. exact Volatco `polyForth` dialect confirmation
3. numeric-model choice and justification
4. stronger quaternion literature citations

## Recommended next move

The next serious step is not more repo scaffolding. It is to execute
Experiment 0 and Experiment 1 on Volatco and feed those results back into:

- `paper/results.md`
- the claim section
- the discussion section
- the final results tables

## Publication posture

At this stage the repo is credible as a research package and internal draft,
but not yet as a submission-ready paper.

The difference between those two states is now mostly evidence, not structure.
