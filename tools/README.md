# Tools

## `pf_block_pack.py`

Project-image generator for the reproducible Volatco path.

It packs `polyforth/volatco-blocks.pf` into a `4800`-block `VOLATCO.src` image
using:

- blocks `0` through `9` reserved
- block `10` as the application load block
- blocks `11` through `30` as source

### Example

```bash
python3 tools/pf_block_pack.py \
  polyforth/volatco-blocks.pf \
  deploy/windows7-volatco/VOLATCO.src
```

### Notes

- The packer enforces a `16 x 64` classical Forth block shape.
- The generated load block expects you to type `10 LOAD` after `AFORTH`.
- This is the preferred paper-grade duplication path because it matches the
  saneForth block-image model better than line-by-line serial entry.

## `volatco_upload.py`

Dependency-light serial uploader for sending `polyForth` source to Volatco.

It uses Python's POSIX serial primitives directly, so it does not require
`pyserial`.

### Example

```bash
python3 tools/volatco_upload.py polyforth/volatco-blocks.pf --port /dev/ttyUSB0
```

### Safer first run

Start conservatively until the exact Volatco terminal behavior is confirmed:

```bash
python3 tools/volatco_upload.py \
  polyforth/volatco-blocks.pf \
  --port /dev/ttyUSB0 \
  --line-delay 0.10 \
  --read-timeout 0.30
```

### Dry run

```bash
python3 tools/volatco_upload.py polyforth/volatco-blocks.pf --dry-run
```

### Notes

- The serial uploader is now a fallback path, not the main reproducibility
  path.
- The script sends carriage return at end of each line.
- `--wait-for-prompt` is available if the target emits a stable prompt.
- `--strip-comments` can be used if the target or link is sensitive to comment
  traffic, but leave comments enabled initially unless you know they are a
  problem.

## `volatco_upload.tcl`

Tcl version of the uploader intended for older Windows systems, including
Windows 7, where `tclsh` is easier to install or already available.

### Example on Windows

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf
```

### Safer first run

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --line-delay 0.10 --read-timeout 0.30
```

### Notes

- Default port is `COM3`.
- The Tcl uploader is a fallback path when you want direct terminal entry
  instead of replacing a `VOLATCO.src` image in the saneForth project.
- For `COM10` and above, the script normalizes the port to the Windows form
  required by Tcl.
- The script sends carriage return at end of each line.
- `--wait-for-prompt` is available if the target emits a stable prompt.
- `--dry-run` prints the outgoing source without opening the serial port.
