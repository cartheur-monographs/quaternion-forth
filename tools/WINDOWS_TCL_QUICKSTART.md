# Windows Tcl Quick Start

Use these commands on the Windows 7 machine only if you are taking the fallback
serial path. The preferred path is to copy `VOLATCO.src` into the saneForth
Volatco project directory, enter `AFORTH`, and type `10 LOAD`.

## 1. Dry run

This just prints the source that would be sent:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --dry-run
```

## 2. First real upload

Start with conservative pacing:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --line-delay 0.10 --read-timeout 0.30
```

Default port is `COM3`. Only add `--port ...` if Windows shows a different COM
port.

## 3. Faster upload

After the first upload works:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf
```

## 4. Prompt-aware upload

If Volatco prints a stable `ok` prompt:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --wait-for-prompt
```

## 5. COM10 and above

Use the normal name:

```bat
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --port COM10
```

The script converts it internally to the Windows form Tcl needs.

## 6. After upload

On Volatco, check:

```text
qreport
mreport
```

Both should print:

```text
10 -20 -30
```

## 7. What to do, step by step

1. Open `cmd.exe`.
2. Change into the repo directory.
3. Run the conservative upload command from step 2.
4. Watch for obvious serial errors in the console.
5. On the Volatco terminal, type:

```text
qreport
mreport
```

6. If both print `10 -20 -30`, type:

```text
1000 biter!
bqr
bmr
bq*
bmm
```

7. Copy the measured results back into `paper/results.md` later.
