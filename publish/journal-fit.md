# Journal Fit Assessment

This note records how well the current paper draft matches the shortlisted
journals, and what must change to make it submission-ready.

## Short answer

The paper does **not** yet match the expectations of any of the shortlisted
journals as a submission-ready article. It currently reads as a research
scaffold or proposal rather than a completed journal paper.

The best fit, once matured, remains:

1. `Journal of Systems Architecture`
2. `IEEE Transactions on Computers`
3. `Mathematics and Computers in Simulation`

## Why the current draft is not ready

The LaTeX draft still contains:

- explicit TODO markers
- a measurement plan instead of a results section
- no fixed numeric model
- no local quaternion literature review
- no real Volatco benchmark data

This means the paper currently explains what it intends to prove, but does not
yet present the evidence needed for journal publication.

## Best current fit: Journal of Systems Architecture

This is the closest fit because the paper is already framed as an embedded
implementation and measured tradeoff study.

Why it fits:

- Volatco is an embedded computing platform
- the paper compares implementation choices under hardware constraints
- the intended contribution is about performance, power, representation, and
  system tradeoffs

Why it still falls short:

- it lacks experimental results
- it lacks a mature methodology section
- it lacks a proper related-work section grounded in the quaternion literature
- it has not yet turned the benchmark scaffold into a defensible empirical
  contribution

## More distant fit: IEEE Transactions on Computers

This venue is plausible only if the paper becomes more clearly a systems or
architecture paper.

To fit better, the paper would need:

- a stronger novelty claim than “we implemented quaternion arithmetic”
- a clearer systems contribution around architecture/runtime tradeoffs
- stronger experimental rigor and broader measurement evidence

In practice, this means the bar is higher than for JSA.

## Alternative fit: Mathematics and Computers in Simulation

This venue becomes attractive if the paper shifts toward computational method
and numerical representation rather than embedded architecture.

To fit better, the paper would need:

- a stronger analysis of the numeric representation
- clearer discussion of precision, overflow, and scaling
- more emphasis on quaternion versus matrix computation as a numerical question
- less emphasis on Volatco as the main story and more emphasis on constrained
  computation as the main story

## Motivation that should drive the paper

The paper needs a sharper motivation than “quaternions are interesting.”

The right motivation is:

> On Volatco-class asynchronous embedded systems, the representation of
> rotational state is a systems design decision because storage, latency, and
> power are constrained. The paper therefore asks whether fixed-point
> quaternions yield a better tradeoff than matrix-based rotation under those
> constraints.

This gives the paper an actual problem to solve:

- not “can we implement quaternions?”
- but “which rotational representation is more reasonable on this hardware, and
  why?”

## Path to match JSA most closely

1. Fix the numeric representation.
2. Confirm the exact `polyForth` compatibility details.
3. Run real Volatco benchmarks for quaternion and matrix kernels.
4. Add results tables for timing, power, and code size.
5. Rewrite the abstract, claim, and discussion around measured evidence.
6. Replace proposal sections with standard journal sections:
   Introduction
   Related work
   Platform and constraints
   Representation and implementation
   Experimental method
   Results
   Discussion
   Conclusion

## What the finished paper should claim

The claim should ultimately be narrow and empirical, for example:

> For bounded rotational workloads on Volatco, a fixed-point quaternion
> representation provides a more favorable tradeoff than a `3x3` matrix
> representation in code size, state compactness, and measured runtime or power,
> subject to the stated numeric limits.

That is a publishable shape. The current draft is not there yet, but the repo
is now organized to get there.
