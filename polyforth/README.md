# polyForth Source

This directory holds the Volatco-facing source in a block-oriented style
inspired by the presentation style used in Leo Brodie's *Starting Forth*.

The reference implementation remains in `forth/`, but the file here is intended
to be the better starting point for `polyForth` block-image generation and
manual block transcription on Volatco.

## Files

- `volatco-blocks.pf`
  The Volatco-facing block file. This is the source used to generate the target
  project image and, if necessary, for manual block entry.
- `dev-template-blocks.pf`
  Reusable starting template for new block-style `polyForth` development. This
  is not target code.
- `BLOCK-LAYOUT.md`
  The intended block-number map for the Volatco project image.

## Notes

- The source is organized as numbered blocks in comments.
- The intended project-image layout uses block `10` as the load block and
  blocks `11` through `30` as source.
- The semantics follow the working reference code in `forth/`.
- If a specific `polyForth` image requires naming or formatting changes, make
  them here first rather than in the reference files.
- If you want to know what to type on Volatco first, use
  [../test/volatco-test-runbook.md](/home/cartheur/ame/aiventure/aiventure-github/cartheur-monographs/quaternion-forth/test/volatco-test-runbook.md:1),
  not the development template.
- `.pf` files are associated with the Forth language in VS Code through
  [`.vscode/settings.json`](/home/cartheur/ame/aiventure/aiventure-github/cartheur-monographs/quaternion-forth/.vscode/settings.json:1),
  so they should highlight like `.fs` files inside this workspace.
- The preferred deployment artifact is a generated `VOLATCO.src` project image,
  not a line-by-line terminal paste.
