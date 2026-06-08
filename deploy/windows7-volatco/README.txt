Volatco Windows 7 USB Deployment
================================

This folder is meant to be copied to a USB stick and then to the Windows 7
machine.

What is in here
---------------

- VOLATCO.src
  Prebuilt Volatco project image for this repo.
- upload-volatco-safe.bat
  Conservative fallback uploader for first use.
- upload-volatco.bat
  Faster fallback uploader after the safe path works.
- check-volatco.txt
  The exact words to type on Volatco after 10 LOAD.
- polyforth\volatco-blocks.pf
  The human-readable target polyForth block source.
- tools\volatco_upload.tcl
  Tcl serial uploader. Default port is COM3.

What to do on the Windows 7 machine
-----------------------------------

1. Copy this whole folder from the USB stick onto the Windows machine.
2. Copy VOLATCO.src into the saneForth Volatco project directory so it replaces
   the active project image.
3. Start the normal saneForth / Volatco path until you reach AFORTH.
4. On the Volatco terminal, type:

      10 LOAD

5. Then type the words shown in check-volatco.txt on the Volatco terminal.

Fallback path
-------------

If you do not want to replace VOLATCO.src yet, you can still use:

   upload-volatco-safe.bat

That path is less faithful to the project-image workflow used by saneForth.

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

- The preferred duplication path is project-image based: copy VOLATCO.src, then
  enter AFORTH and type 10 LOAD.
- The Tcl uploader defaults to COM3.
- If the IDE serial port is not COM3, edit the .bat files or run the Tcl
  script manually with --port COMx.
- This package has not been tested against a real Volatco session yet, so the
  most likely tuning knobs are line delay and prompt behavior.
