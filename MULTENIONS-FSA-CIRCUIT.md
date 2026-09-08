# A Circuit Test for the Multenion–Automaton Bridge

This note proposes a small synchronous digital circuit to test the idea that
multenion multiplication can act as finite-state transition composition.  It
does not claim that the full, real-coefficient multenion algebra is finite;
the finite-state system is a deliberately selected finite orbit of algebraic
elements.

## Eight-state noncommutative test

Use the finite signed set

```text
{ 1, -1, i, -i, j, -j, k, -k },  where k = i j
```

with the rules

```text
i^2 = 1
j^2 = 1
i j = -j i
```

The circuit stores one of the eight elements in a three-bit state register.
An input symbol selects `hold`, `i`, or `j`; a combinational lookup table
(LUT) calculates the next state by right multiplication.

```text
                     input: hold / i / j
                               |
                               v
state register  ------------> transition LUT ------------> D inputs
      ^                                                       |
      +------------------ clocked three-bit register --------+
```

Reset the state register to `1`.  The central part of the LUT is:

| Current state | Input `i` | Input `j` |
| --- | --- | --- |
| `1` | `i` | `j` |
| `i` | `1` | `k` |
| `j` | `-k` | `1` |
| `k` | `-j` | `i` |
| `-1` | `-i` | `-j` |
| `-i` | `-1` | `-k` |
| `-j` | `k` | `-1` |
| `-k` | `j` | `-i` |

The test that matters is order sensitivity:

```text
reset -> i -> j     produces k
reset -> j -> i     produces -k
```

Thus two words made from the same symbols produce distinct finite states.
The circuit realizes a deterministic finite automaton whose transitions are
the selected multenion multiplications.  It also visibly tests the role of
noncommutativity: exchanging the two input symbols changes the result.

An FPGA is the most direct implementation: use three flip-flops for the
state, two bits for the input encoding, and a small ROM or synthesized
case-statement for the LUT.  LEDs can display the current three-bit code;
an additional LED can indicate a chosen accepting state, such as `1`.

## Minimal breadboard variant: parity

A two-state version gives a simpler initial test.  Take a generator `i` such
that `i^2 = 1`, represent the states `1` and `i`, and let input `a` mean
multiply by `i`.

```text
1 --a--> i --a--> 1 --a--> i ...
```

With one-hot state bits `Q0` for `1` and `Q1` for `i`, the next-state logic
is a conditional swap:

```text
D0 = (!a AND Q0) OR (a AND Q1)
D1 = (!a AND Q1) OR (a AND Q0)
```

Reset to `Q0 = 1, Q1 = 0`, and define `Q0` as accepting.  The resulting
circuit is the standard two-state DFA accepting precisely the strings with
an even number of `a` symbols.  It confirms the finite-orbit idea, while the
eight-state circuit provides the stronger noncommutative test.

## Interpretation

The multenion algebra is the host for transition composition.  The FSA is
not the whole algebra; it is the finite set of reachable representatives,
together with selected input actions and an acceptance rule.  This distinction
is essential when comparing McAulay's real-coefficient algebra with finite,
discrete automata.
