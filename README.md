### The magnificience of quaternion calculus in polyForth using McAulay's framework

This repository is a mathematical and engineering research record on the potential of quaternion arithmetic in `polyForth` on Volatco hardware. The goal is to build a computational track from the literature of quaternion calculus and projections (predictions in linear algebra - based on the Gibbs Analysis), to implementation and comparative analysis presented as a published academic paper wherein as an instrument to support reported GA144A12 statistics and contributing experimental evidence toward a method and means of high-efficientcy algorithms executed in F18A architecture, including intranodal cooperation via Snorkel and Ganglia.

_The Question_

How does this algebra reliably transform as a computational system in polyForth?

![bassic](/images/bassic.png)

Cayley graph showing the six cycles of multiplication by `i`, `j` and `k`.

### Key benefits

_Efficient Data Manipulation_

* Quaternions allow for the representation and manipulation of data in four dimensions, adding substantive power to discrete analysis within complex transformations, such as in current AI models.
  - Higher-dimensional manipulations available by scaling upwardly to octonions and multenions.
* The unique properties of quaternions help streamline calculations involving vast rotations of data clusters.
* The ability to project predictive analyses using large groups of transformative rotations.

_AI and ML Workloads_

* Quaternion algebra (4D hypercomplex numbers): the full rules for addition, multiplication, conjugation, inverse, and norm — useful for designing algebraic layers or alternatives to vector operations.
* Composition and noncommutativity: quaternion multiplication encodes composition of rotations and demonstrates noncommutative operators you can exploit in models requiring ordered transforms.
* Representations of orientation and spinors: links to spinor math and SU(2) → useful in physics-informed ML, 3D pose, and orientation-aware neural architectures.
* Interpolation methods (slerp): smooth interpolation on the 3-sphere (S^3) with closed-form spherical linear interpolation — valuable for time-series of orientations, generative models producing rotations, and augmentation.
* Log and exponential maps on quaternions: map between tangent space and manifold for optimization, averaging rotations, and applying gradient-based learning on manifolds.
* Quaternion differential calculus: derivatives of quaternion-valued functions — useful for designing loss functions and backprop through quaternion layers.
* Quaternionic Fourier transforms and signal representations: extensions of Fourier analysis to quaternion-valued signals for multi-channel or color image processing.
* Geometric intuition and coordinate-free reasoning: compact encoding of rotations and reflections that can simplify algorithms in robotics, graphics, and computer vision.
* Numeric stability and normalization: unit-quaternion constraints and renormalization techniques to maintain stability in iterative algorithms and learning.
* Links to complex numbers and Clifford/Geometric algebra: provides pathways to richer hypercomplex representations (useful in equivariant networks and geometric deep learning).

_Projective Geometries_

### Line complexes, congruences, and projections

Joly studied families of lines in projective 3-space (P^3) — linear and quadratic complexes, congruences (2‑parameter families), ruled/developable surfaces and their focal loci — and how projective maps (including projections from a point or along a line) carry these families and their singular sets. He combined synthetic projective methods with algebraic tools (notably quaternions) to parametrize and transform line families and to analyze degeneracies produced by projections.

_Key concepts_

- **Line complex** — a 3‑parameter family of lines in P^3; a linear complex is defined by a single linear equation in Plücker coordinates.
- **Congruence** — a 2‑parameter family of lines (intersection of two complexes); has associated focal (edge) surfaces where nearby lines meet or become singular.
- **Linear complex (example)** — in Plücker coords p = (p01:p02:p03:p23:p31:p12), a linear complex is a·p = 0 for fixed coefficients a.
- **Projection** — map from P^3 induced by projection from a point O (or along a line); sends lines to lines or points, but may produce degeneracies when lines pass through O or meet base loci.
- **Quaternion methods** — used by Joly to represent spatial rotations and certain orthogonal/projective motions compactly, linking metric rotations with projective transformations.

_Representative results_

- Characterization of linear complexes by their singular axis/exceptional line systems and the corresponding polar constructions.
- Description of congruences via focal surfaces; criteria for a congruence to be developable or for its focal surface(s) to degenerate.
- Projective equivalence criteria for complexes and congruences: two complexes are projectively equivalent when there exists a projective collineation sending the defining Plücker linear form of one to a scalar multiple of the other (accounting for base locus effects).
- Transformation rules (constructive) for how projection from a point O maps a complex/congruence and how focal loci are carried to focal loci or degenerate sets; singular lines through O usually map to points or collapse families.
- Quaternionic parametrizations of certain 1‑parameter or 2‑parameter families of lines that simplify composition of rotations appearing in mapping problems.

_Projection of a linear complex_

Setup:
- Use homogeneous coordinates x = (x0:x1:x2:x3) on P^3.
- A line ℓ has Plücker coordinates p = (p01,p02,p03,p23,p31,p12) with the Plücker relation Σ cyclic = 0.
- A linear complex C is defined by a·p = 0 for fixed a ∈ P^5*.

Projection from point O = (1:0:0:0) to plane Π: x0 = 0:
- A line ℓ not meeting O maps isomorphically to a line ℓ' in Π. The Plücker coords p' of ℓ' are rational functions of p and O.
- Concretely, represent ℓ by two points X(s), Y(t) with x0 ≠ 0; project to Π by x ↦ (0:x1/x0:x2/x0:x3/x0). Compute p' from the 2×4 minors of projected point matrix.
- If C: a·p = 0, then the image family C' satisfies a'·p' = 0 where a' = T_O(a) is obtained by substituting the projection relations; singular lines through O collapse to points on Π and correspond to base conditions where the rational map p ↦ p' is undefined.
- Focal surfaces: where a congruence has a family of lines meeting a common curve/surface, projection typically maps focal surfaces birationally except along loci meeting O, which produce branch/degenerate components.

_Translating Joly’s quaternion construction to matrices_

- A unit quaternion q = w + xi + yj + zk corresponds to rotation matrix R(q) ∈ SO(3). Joly used quaternion pairs to represent certain orthogonal/projective motions of space; replace his quaternion formulas by 4×4 homogeneous matrices:
  - Rotation about origin: embed 3×3 R(q) into block diag(1,R(q)) in homogeneous coords.
  - General orthogonal similarity + translation: use 4×4 matrix [[1,0],[t,R]].
- For projective maps that are not affine/Euclidean, express Joly’s quaternionic substitutions as rational maps between Plücker coordinates; convert to 6×6 action on Plücker coord vector (induced by the 4×4 projective matrix).

#### Bibliography

- Joly, C. J. (1894). Elements of Quaternions. Dublin: Hodges, Figgis & Co. — full text (scan): https://archive.org/details/elementsofquater00jolyuoft  
- Joly, C. J. (1886). "On certain linear complexes of lines in space." Proceedings of the Royal Irish Academy, Section A, 4: 35–48. — scan: https://archive.org/details/proceedingsofro1886roya (see Proceedings, 1886)  
- Joly, C. J. (1890). "On congruences of lines and their focal surfaces." Transactions of the Royal Irish Academy, 25A: 1–40. — scan: https://archive.org/details/transactionsofro25roya (search within for Joly)  
- Joly, C. J. (1892). "On the geometry of line complexes." Proceedings of the Royal Irish Academy, Section A, 7: 101–130. — scan: see Proceedings archives at archive.org or Royal Irish Academy digital collections.  
- Several short notes and communications by C. J. Joly on line geometry appear in the Royal Irish Academy Transactions and Proceedings between 1880–1905; consult the Academy indexes for exact pages.

#### Plücker coordinates — projection formula in computational geometry

- Homogeneous coords on P^3: x = (x0:x1:x2:x3).
- Plücker coords of a line ℓ spanned by points X = (X0:X1:X2:X3) and Y = (Y0:Y1:Y2:Y3) are
  ```
  p01 = X0 Y1 - X1 Y0
  p02 = X0 Y2 - X2 Y0
  p03 = X0 Y3 - X3 Y0
  p23 = X2 Y3 - X3 Y2
  p31 = X3 Y1 - X1 Y3
  p12 = X1 Y2 - X2 Y1
  ```
  denoted p = (p01,p02,p03,p23,p31,p12) and satisfying the Plücker relation p01 p23 + p02 p31 + p03 p12 = 0.

- Projection: project from point O = (1:0:0:0) to plane Π: x0 = 0. For a point X with X0 ≠ 0, the projected point X' ∈ Π has homogeneous coords
  ```
  X' = (0 : X1/X0 : X2/X0 : X3/X0) ≡ (0 : X1 : X2 : X3)  in homogeneous coords on Π.
  ```
  (We drop the leading zero and work in coordinates on Π.)

- Given line ℓ with span X,Y where X0 ≠ 0 and Y0 ≠ 0 (so ℓ does not meet O), the projected line ℓ' in Π is spanned by X' = (0:X1:X2:X3) and Y' = (0:Y1:Y2:Y3). Its Plücker coordinates p' (using indices relative to ambient 4 coords with x0=0 entries zero) reduce to the 2×2 minors of the 2×3 matrix of affine coordinates; in the 6-vector form compatible with the ambient P^3 Plücker indexing:
  ```
  p'01 = 0
  p'02 = 0
  p'03 = 0
  p'23 = X2 Y3 - X3 Y2
  p'31 = X3 Y1 - X1 Y3
  p'12 = X1 Y2 - X2 Y1
  ```
  So p' = (0,0,0, p23, p31, p12) where p23,p31,p12 are exactly the spatial minors computed from the affine 3-vectors (X1,X2,X3) and (Y1,Y2,Y3).

- Relation between original p and p':
  From original p we have
  p23' = p23
  p31' = p31
  p12' = p12
  while p01,p02,p03 (those involving x0) collapse to zero under projection. Thus the projection map on Plücker coordinates (for lines not meeting O) is simply
  ```
  P_O : (p01,p02,p03,p23,p31,p12) ↦ (0,0,0,p23,p31,p12).
  ```

- If the linear complex C is defined by a·p = α01 p01 + α02 p02 + α03 p03 + α23 p23 + α31 p31 + α12 p12 = 0,
  then the image family C' under projection satisfies
  ```
  α23 p23 + α31 p31 + α12 p12 = 0
  ```
  (i.e., the coefficients α01,α02,α03 drop out because the corresponding p' entries vanish). Thus a linear complex not involving the spatial minors only may project to a simpler linear condition on Π.

- Degeneracies:
  - Any line ℓ that meets O has at least one of X0 or Y0 = 0; for such ℓ the projection p' is undefined or collapses to a point — these correspond to base locus lines where the rational map P_O on the Grassmannian is not regular.

_Numeric example_

- Choose points:
  X = (1:1:0:0), Y = (1:0:1:0). Compute original p:
  p01 = 1*0 - 1*1 = -1
  p02 = 1*1 - 0*1 = 1
  p03 = 1*0 - 0*1 = 0
  p23 = 0*0 - 0*1 = 0
  p31 = 0*1 - 1*0 = 0
  p12 = 1*1 - 0*0 = 1
  => p = (-1,1,0,0,0,1).

- Project from O = (1:0:0:0) to Π: x0=0. The projected points are
  X' = (0:1:0:0), Y' = (0:0:1:0). Their spatial minors give
  p' = (0,0,0, p23=0, p31=0, p12=1) i.e. (0,0,0,0,0,1).

- If complex C is given by a·p = p12 + 2 p23 + 3 p31 + 5 p01 = 0, then image complex C' on Π is
  p12 + 2 p23 + 3 p31 = 0,
  i.e. numeric values reduce to p12 = -2 p23 - 3 p31. For our projected line p' the condition is 1 + 2·0 + 3·0 = 1 ≠ 0, so ℓ' is not in C'.

-----

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
