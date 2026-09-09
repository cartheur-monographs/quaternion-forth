# 74LS Switch-and-LED Circuit Suggestions

These breadboard or wire-wrap experiments use 74LS logic, manually selected
inputs, a clock, and LEDs to make digital-state behavior observable.  They
are intended as companion projects to the multenion finite-state automaton,
not claims about the full real-coefficient multenion algebra.

## Common bench conventions

- Use a regulated 5 V supply; place a 100 nF bypass capacitor at every IC.
- Use normally-open momentary switches for clock and reset.  Use latching
  SPST ON-OFF switches for persistent binary choices such as data inputs.
- A Mountain Switch 10TC405 is a suitable latching SPST input switch.  It has
  short solderable terminals: solder one end of a 28 AWG wire-wrap wire to a
  terminal and wrap its other end on a suitable square wire-wrap post.
- Use the `PS1024` Circuit A, `OFF-(ON)`, for a normally-open clock or reset
  pushbutton.
- Keep red for `+5 V`, black for ground, green for clock/control, yellow for
  manually selected inputs, blue for normal logic/state nets, and white for
  active-low nets.

## Suggested projects

| Circuit | Switch role | LED observation | Main idea |
| --- | --- | --- | --- |
| Multenion input-word tester | Latching `i` / `j` selection and a clock button | One of eight signed-state LEDs | Right multiplication acts as a finite-state transition. |
| Noncommutativity demonstrator | Enter `i`, then `j`; reset and reverse the order | `k` versus `-k` | The input order changes the final state. |
| Parity automaton | One symbol input and clock | Even and odd state LEDs | A two-state finite automaton. |
| Binary counter | Reset and clock | Each state bit | Binary state encoding and synchronous counting. |
| Shift register | Serial-data switch and clock | Moving bit pattern | Clocked storage and serial transfer. |
| Sequence recognizer | One switch per input symbol and clock | Match/accept LED | Recognition of words such as `ij` or `ji`. |
| Logic-gate laboratory | Two or more latching inputs | AND, OR, XOR, NAND outputs | Direct Boolean-logic exploration. |
| Ring or Johnson counter | Reset and clock | Cyclic LED pattern | Repeating state orbits. |
| Small cellular automaton | Seed/rule switches and step clock | Cell-state row | Local rules producing successive visible patterns. |
| Simple control-unit model | Opcode/control switches and clock | State and flag LEDs | A small finite-state machine resembling CPU control. |

## Useful input-mode selector

An SPDT ON-OFF-ON switch can select three safe clock modes:

```text
AUTO  -> oscillator clock source
OFF   -> clock disconnected
MANUAL -> debounced pushbutton clock source
```

Feed the selected clock through an appropriate buffer or gate before the
74LS74 clock inputs.  The center position avoids having the automatic source
and manual source drive one clock net together.
