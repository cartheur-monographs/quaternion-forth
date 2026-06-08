Volatco Windows 7 USB Deployment
================================

This folder is meant to be copied to a USB stick and then to the Windows 7
machine.

What is in here
---------------

- upload-volatco-safe.bat
  Conservative uploader for first use.
- upload-volatco.bat
  Faster uploader after the safe path works.
- check-volatco.txt
  The exact words to type on Volatco after upload.
- polyforth\volatco-blocks.pf
  The target polyForth block source.
- tools\volatco_upload.tcl
  Tcl serial uploader. Default port is COM3.

What to do on the Windows 7 machine
-----------------------------------

1. Copy this whole folder from the USB stick onto the Windows machine.
2. Open cmd.exe in this folder.
3. Run:

   upload-volatco-safe.bat

4. Watch for obvious serial errors.
5. Then type the words shown in check-volatco.txt on the Volatco terminal.

Expected first check
--------------------

Both of these should print:

    10 -20 -30

    qreport
    mreport

If that works, you can use:

    upload-volatco.bat

Notes
-----

- The Tcl uploader defaults to COM3.
- If the IDE serial port is not COM3, edit the .bat files or run the Tcl
  script manually with --port COMx.
- This package has not been tested against a real Volatco session yet, so the
  most likely tuning knobs are line delay and prompt behavior.
