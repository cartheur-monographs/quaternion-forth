# Reasonable Quaternion Arithmetic for `polyForth` on Volatco

## Abstract

This paper studies whether quaternion arithmetic is a reasonable computational
choice for `polyForth` on Volatco, an asynchronous F18A-based platform intended
for low-power control, robotics, and experimental machine-intelligence
workloads. The paper adopts a deliberately narrow standard of reasonableness:
the representation must be simple to encode in a stack language, compact enough
to respect the constraints of an `18-bit` node, and measurably competitive with
plausible alternatives on representative rotational workloads. The repository
therefore combines source code, benchmark definitions, and manuscript material
so that the argument can be revised against hardware measurements rather than
asserted from algebra alone.

## 1. Introduction

Quaternion methods are attractive in rotational computation because they offer a
compact composition law and avoid some of the representational overhead of some
matrix-based formulations. Those advantages are familiar in conventional
software environments, but they cannot be assumed on a tiny asynchronous
multicomputer running `polyForth`. On such a platform, the right question is
not whether quaternions are elegant, but whether they are operationally
reasonable.

Volatco is a useful target for this question because it is explicitly Forth
native and designed around deterministic, low-latency execution. Its published
documentation emphasizes reproducible `polyForth` experiments with explicit
pass/fail criteria, power measurement points, and bounded-latency behavior.
That combination makes it possible to write a paper that is both mathematical
and empirical.

## 2. Claim

The working claim of this paper is modest:

> A fixed-point quaternion representation is a reasonable rotational algebra for
> `polyForth` on Volatco when it yields compact code, controlled state, and
> reproducible performance under explicitly measured workloads.

The claim is intentionally narrower than "quaternions are best." It is a claim
about suitability under a specific architecture, runtime style, and evidence
standard.

## 3. Representation

The implementation scaffold in this repository uses two competing
representations. The quaternion kernel stores `(a b c d)`, where `a` is the
scalar part and `b c d` are the vector components. The comparison kernel stores
a `3x3` rotation matrix in row-major order plus a three-component vector. This
side-by-side structure is important: the paper should not ask whether
quaternions are elegant in the abstract, but whether they remain reasonable
once compared to a direct matrix approach under the same tooling and target
constraints.

The quaternion layout is straightforward to reason about in stack comments and
maps directly to Hamilton's multiplication rule. The current draft keeps the
representation abstract enough to allow either integer arithmetic or scaled
fixed-point arithmetic after target measurements determine which is more
practical on Volatco.

The scalar-first ordering is not mathematically necessary. It is adopted here
for engineering clarity, because conjugation, norm-squared, and scalar-vector
decomposition can be written with simple stack effects.

## 4. Implementation strategy

The initial Forth implementation favors transparency over optimization. Core
quaternion words are provided for addition, subtraction, conjugation,
norm-squared, Hamilton product, and vector rotation via
`q * v * conjugate(q)`. A companion matrix kernel performs matrix-vector and
matrix-matrix multiplication in the same address-based style. This is the
correct place to start because a paper cannot defend an optimized kernel unless
it first presents readable reference implementations for both the proposed and
comparison methods.

After reference behavior is established, optimization can proceed in three
directions:

1. reduce stack traffic
2. remove scratch storage where the target system permits
3. distribute work across nodes when the communication cost is justified

## 5. Measurement plan

The benchmark argument in this repository compares quaternion operations against
a direct `3x3` rotation matrix kernel written in the same style. That
comparison is necessary because the matrix method spends more storage but may
win in some workloads by avoiding quaternion conjugation or normalization
overhead. Each benchmark should record:

- iteration count
- elapsed time
- code footprint
- current or power measurement
- watchdog stability during long runs

The results should be treated as evidence about suitability for Volatco, not as
universal claims about all Forth systems.

## 6. Expected contribution

If the measurements support the working claim, the paper contributes a concrete
case study in how a classical algebraic formalism can be adapted to a modern
asynchronous Forth-native platform. If the measurements do not support the
claim, that result is still valuable because it clarifies where quaternion
methods cease to be worth their conceptual appeal under tight embedded
constraints.

The comparison with a matrix kernel is especially important here. A paper that
only demonstrates that quaternion code runs on Volatco would be incomplete. The
interesting result is whether quaternion state, composition, and rotation
support a better tradeoff than a simpler matrix pipeline once timing, code size,
and power are measured on the same board.

## 7. Next revision points

- replace placeholder prose with citations from the local quaternion corpus
- confirm `polyForth` compatibility on Volatco
- choose and document a fixed-point scaling rule
- add measured benchmark results
- interpret the quaternion results against the matrix baseline
- narrow the claim to match the data
