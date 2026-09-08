# McAulay's Multenions and Finite-State Automata

Alex McAulay's **multenions** are a historical extension of quaternion
algebra.  A multenion is built from scalar coefficients and anticommuting
generators.  Products of generators provide components of different degrees:
scalars, vectors, bivectors, and higher-grade terms.  McAulay's central idea
is to use one associative, noncommutative product, with operations analogous
to Grassmann's products recovered as selected parts of that product.

The formulation is close in modern spirit to a Clifford or geometric algebra.
It is not, however, a finite-state automaton (FSA): with real scalar
coefficients it contains infinitely many possible elements, whereas an FSA
has a finite, discrete state set.

## The bridge

The connection is a representation of automaton transitions as algebraic
actions:

```text
input word -> product of symbol actions -> resulting state/action
```

Assign each input symbol an algebra element, a McAulay *linity* (linear
transformation), or a matrix representation.  Reading a word composes those
actions by multiplication.  An FSA results when only finitely many reachable,
distinguishable state representatives are retained and an acceptance test is
specified.

For example, let the symbol `a` act by multiplication by a generator `i`
such that `i^2 = 1`:

```text
1 --a--> i --a--> 1 --a--> i ...
```

Taking `1` as the accepting state implements the two-state DFA that accepts
exactly the strings containing an even number of `a` symbols.  The finite
automaton is the two-element reachable orbit `{1, i}`, not the entire
multenion algebra.

For general DFAs, transition matrices are usually the most transparent form:

1. Represent each DFA state by a basis vector.
2. Represent each input symbol by its transition matrix.
3. Multiply matrices in input order.
4. Test whether the resulting basis vector is accepting.

McAulay's algebra can serve as a host language for the same composition of
actions, particularly through his theory of linities.  The modern algebraic
automata viewpoint expresses the same point by saying that a DFA generates a
finite monoid of word-actions.  The relevant finite object is therefore a
finite quotient or orbit of the action system, rather than the full,
continuous multenion algebra.

## Repository sources

- `quaternions/mcaulay/1908-XXXIV--Algebra after Hamilton or Multenions.pdf`
  — McAulay's algebra-focused account.
- `quaternions/mcaulay/McAulay--Multenions and Differential Invariants-I.pdf`
  — develops multenions, linities, covariance, and differential invariants.
- `quaternions/mcaulay/McAulay--Multenions and differential invariants-II.pdf`
  and `quaternions/mcaulay/McAulay--Multenions and differential invariants-III.pdf`
  — geometric, relativity, and Maxwell/action applications.
