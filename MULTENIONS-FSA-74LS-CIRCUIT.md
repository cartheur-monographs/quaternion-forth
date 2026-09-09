# 74LS Circuit for a Multenion Finite-State Automaton

This circuit implements a finite orbit of McAulay-style multenion products
with ordinary 5 V 74LS TTL logic.  It is a deterministic finite-state
automaton: the three stored bits encode one of eight algebraic states, and
each clocked input applies a selected multiplication.

## State encoding

Represent the current state by

```text
(-1)^S i^A j^B
```

where `S`, `A`, and `B` are one-bit state registers.  The encoded states are:

| `S A B` | State |
| --- | --- |
| `000` | `1` |
| `010` | `i` |
| `001` | `j` |
| `011` | `k = ij` |
| `100` | `-1` |
| `110` | `-i` |
| `101` | `-j` |
| `111` | `-k` |

The multiplication rules are:

```text
i^2 = 1
j^2 = 1
i j = -j i
```

## Inputs and next-state logic

Use input bit `X` to request right multiplication by `i`, and input bit `Y`
to request right multiplication by `j`.  With the inputs stable before the
clock edge, the next-state equations are:

```text
A(next) = A XOR X
B(next) = B XOR Y
S(next) = S XOR (X AND B)
```

When `X = Y = 0`, the state holds.  For ordinary single-symbol operation use
only `10` (`i`) and `01` (`j`) as active inputs.  The unused `11` input can
be forbidden, or treated as applying `i` followed by `j` during one clock.

```text
X ───────┬──────────────> XOR ─────────> D input of A flip-flop
         │
B ───────┼──────> AND ───> XOR with S ─> D input of S flip-flop
         │
Y ───────────────────────> XOR ─────────> D input of B flip-flop

                    shared, debounced clock ─> all flip-flops
```

The circuit's noncommutative test is:

```text
reset:     000 = 1
i, then j: 011 = k
j, then i: 111 = -k
```

The two word orders therefore reach different finite states.

## 74LS parts list

The purchase-ready bill of materials is available as
[`MULTENIONS-FSA-74LS-BOM.csv`](MULTENIONS-FSA-74LS-BOM.csv).  It includes
the supporting LEDs, switches, reset/debounce components, supply, and
per-IC bypass capacitors, in addition to the logic ICs below.

| Function | Suggested part | Notes |
| --- | --- | --- |
| Three D state registers | 2 × 74LS74 | One dual flip-flop package supplies `S` and `A`; use one half of the second for `B`. |
| XOR logic | 1 × 74LS86 | Three XOR gates implement the three next-state expressions. |
| AND logic | 1 × 74LS08 | One AND gate generates `X AND B`. |
| State display / accepting-state decode | 1 × 74LS138 | Its active-low outputs select one of eight state LEDs. |
| Pushbutton conditioning | 1 × 74LS14 plus RC components | A Schmitt trigger debounces the manual clock; a 74LS123 may be added for a one-shot clock pulse. |

Connect the active-low clear inputs of all used 74LS74 flip-flops to a reset
switch so that reset makes `S = A = B = 0`, the identity state `1`.  Hold
the active-low preset inputs high.  Tie every unused TTL input to a defined
logic level; do not leave inputs floating.

Connect `S`, `A`, and `B` to the 74LS138 address inputs.  Label its decoded
outputs as `1`, `i`, `j`, `k`, `-1`, `-i`, `-j`, and `-k` according to the
chosen wiring order.  Because the 74LS138 outputs are active low, an LED and
series resistor connected to `+5 V` will illuminate when its corresponding
output is selected.  The `000` decoder output can also serve as an
"accepting" indicator: it means the accumulated product is the identity.

## Practical notes

- Use a regulated 5 V supply and place a 0.1 µF bypass capacitor across the
  supply pins of each IC.
- The sub-miniature toggle terminals are suitable for soldering but are too
  short for a dependable direct wire-wrap.  With 28 AWG wire-wrap wire,
  solder one end to the switch terminal and wrap the other end onto a proper
  square wire-wrap post.  Use a tool and posts specified for 28 AWG wire.
- Keep a consistent wiring convention: red for `+5 V`, black for ground,
  green for clock/control, yellow for manual inputs such as `X` and `Y`, blue
  for ordinary state/logic signals, and white for active-low signals such as
  `/CLR` and the 74LS138 outputs.
- Begin with a clean, slow clock source. Add a debounced pushbutton only once
  the transition logic has been checked.
- Confirm the hold case first, then test `i i = 1`, `j j = 1`, `i j = k`,
  and `j i = -k`.
- This hardware realizes the selected eight-state orbit, not the complete
  real-coefficient multenion algebra.
