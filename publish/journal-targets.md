# Journal Targets

This note records the current recommendation for where to submit the Volatco
quaternion paper once the empirical results are ready.

## Constraints

- no publication fee if possible
- professional journal, not a workshop or magazine
- strong citation profile
- scope compatible with embedded systems, computational methods, or both

## Recommended shortlist

### 1. Journal of Systems Architecture

Primary recommendation.

Why:

- strong fit for embedded systems and software architecture
- current journal page shows `CiteScore 10.5` and `Impact Factor 4.1`
- hybrid model, so standard non-open-access submission avoids an APC
- the current paper direction fits its embedded software and measured systems
  emphasis well

Best version of the paper for this venue:

- emphasize Volatco constraints
- emphasize measured quaternion versus matrix tradeoffs
- emphasize implementation and architecture consequences

Source:

- https://www.sciencedirect.com/journal/journal-of-systems-architecture

### 2. IEEE Transactions on Computers

Prestige-first recommendation.

Why:

- IEEE describes it as the Computer Society flagship journal
- scope includes architecture, software-hardware interaction, and embedded
  systems
- IEEE Computer Society journals use a hybrid model, so a traditional
  submission can avoid open-access fees

Risk:

- significantly tougher bar
- the paper will need a stronger systems contribution and cleaner empirical
  story than it currently has

Best version of the paper for this venue:

- sharpen novelty beyond “quaternion implementation exists”
- make the contribution clearly about architecture/runtime tradeoffs
- provide stronger measurement methodology and results

Sources:

- https://www.computer.org/digital-library/journals/tc/call-for-papers-general-submissions
- https://www.computer.org/publications/author-resources/authors

### 3. Mathematics and Computers in Simulation

Math-and-computation recommendation.

Why:

- strong fit if the paper leans more toward computational representation and
  algorithmic comparison
- current journal page shows `CiteScore 9.5` and `Impact Factor 4.4`
- hybrid model, so standard non-open-access submission avoids an APC

Best version of the paper for this venue:

- emphasize fixed-point quaternion representation
- emphasize quaternion versus matrix computation
- treat Volatco as a constrained computational case study

Sources:

- https://www.sciencedirect.com/journal/mathematics-and-computers-in-simulation

## Journals to avoid under the current no-fee constraint

### ACM Transactions on Embedded Computing Systems

Good scope fit, but ACM’s 2026 publishing model means authors generally need
either:

- an APC payment, or
- a corresponding author at an ACM Open institution

Source:

- https://authors.acm.org/open-access

### ACM Transactions on Mathematical Software

Also a good scope fit in principle, but the same ACM cost constraint applies.

Sources:

- https://authors.acm.org/open-access
- https://www.acm.org/publications/about-publications

## Current recommendation

If the paper remains close to its present trajectory, submit first to:

1. `Journal of Systems Architecture`
2. `IEEE Transactions on Computers`
3. `Mathematics and Computers in Simulation`

## What must be true before submission

- real Volatco benchmark results must exist
- the numeric representation must be fixed and documented
- the claim must be rewritten around measured evidence
- the paper must cite the quaternion literature already collected in this repo
