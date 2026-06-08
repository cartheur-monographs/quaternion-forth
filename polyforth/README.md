# polyForth Source

This directory holds the Volatco-facing source in a block-oriented style
inspired by the presentation style used in Leo Brodie's *Starting Forth*.

The reference implementation remains in `forth/`, but the file here is intended
to be the better starting point for `polyForth` loading or manual block
transcription on Volatco.

## Files

- `volatco-blocks.pf`
  The Volatco-facing block file. This is the source to load or type into the
  target system.
- `dev-template-blocks.pf`
  Reusable starting template for new block-style `polyForth` development. This
  is not target code.

## Notes

- The source is organized as numbered blocks in comments.
- The blocks are intended for block-style reading and transcription.
- The semantics follow the working reference code in `forth/`.
- If a specific `polyForth` image requires naming or formatting changes, make
  them here first rather than in the reference files.
- If you want to know what to type on Volatco first, use
  [../test/volatco-test-runbook.md](/home/cartheur/ame/aiventure/aiventure-github/cartheur-monographs/quaternion-forth/test/volatco-test-runbook.md:1),
  not the development template.
- `.pf` files are associated with the Forth language in VS Code through
  [`.vscode/settings.json`](/home/cartheur/ame/aiventure/aiventure-github/cartheur-monographs/quaternion-forth/.vscode/settings.json:1),
  so they should highlight like `.fs` files inside this workspace.
