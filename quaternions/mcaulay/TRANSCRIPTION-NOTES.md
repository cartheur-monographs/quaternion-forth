# McAulay transcription notes

`McAulay--Multenions and Differential Invariants-I-working-copy.tex` is the
active, progressively typeset transcription of Part I. It currently covers
journal pages 293--296 (PDF pages 2--5).

## Clean rendering workflow

- Work from the page image, not the OCR text alone. OCR is useful for prose but
  loses equation structure, accents, and the placement of equation numbers.
- Retain the journal-page sequence: use one LaTeX page per source journal page,
  reproduce the running header, and begin continuation text where it appears in
  the scan.
- Reflow prose as ordinary paragraphs. Put standalone formulae in display-math
  environments, using `\tag{...}` when the original has an equation number.
- Verify symbols and tables at high-resolution against the source PDF before
  committing them to LaTeX.
- Compile with `pdflatex` and inspect the rendered PDF against the source page.

## Vectorium mark

The mark on vectorium variables is a small curved, inverted accent; it is not a
conventional hat. The working copy represents it with LaTeX `\breve{...}`. For
example, the source notation is rendered as
`\breve{\alpha}`, `\breve{\epsilon}`, `\breve{\eta}`,
`x\breve{\upsilon}`, and `\breve{\omega}`.

This is a visual historical approximation and should be retained consistently
unless a better documented original notation is identified.
