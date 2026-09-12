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

![Source crop showing the curved vectorium marks](vectorium-breve-notation.png)

This is a visual historical approximation and should be retained consistently
unless a better documented original notation is identified.

## Normal-reciprocal notation (p. 305 onward)

The p. 305 normal-reciprocal relations introduce a second, distinct mark. Use
`\breve{\alpha}` for a vectorium and `\hat{\alpha}` for its normal reciprocal;
for example, $V_0\breve{\alpha}\hat{\alpha}=1$. Do not collapse this paired
notation into a single accent.

![Source crop showing the paired breve and hat marks](normal-reciprocal-notation.png)

## Grouped equations: braces and punctuation

When the source groups alternative equation lines with a large right brace,
preserve the brace and its terminal punctuation before the equation number. In
LaTeX, use `\left.\begin{aligned} ... \end{aligned}\right\}` followed by the
source comma or period, then `\tag{...}`. Do not omit that punctuation. This
was audited and applied to the grouped displays on pp. 297--312; for example,
on p. 309 equation (4) ends with a comma and equation (5) with a period.

![Source crop showing grouped braces and punctuation](grouped-equations-braces-punctuation.png)

## Current handoff status (2026-09-12)

Part I has been transcribed and source-checked through its final journal page,
p. 324. The transcription ends at the article rule; the following Newman
article is deliberately excluded.

## Part II handoff status (2026-09-12)

`McAulay--Multenions and Differential Invariants-II-working-copy.tex` is the
active, source-checked transcription of Part II. It covers journal p. 210;
resume at p. 216, checking every word, symbol, display, brace, equation number,
and page break against the source PDF.
