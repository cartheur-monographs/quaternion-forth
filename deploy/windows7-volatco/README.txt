Volatco Windows 7 USB Deployment
================================

This folder is meant to be copied to a USB stick and then to the Windows 7
machine.

What is in here
---------------

- VOLATCO.src
  Prebuilt Volatco project image for this repo.
- INSTALL-STEPS.txt
  Short no-agent Windows 7 procedure.
- BLOCK-MAP.txt
  The intended block numbering for this project image.
- check-volatco.txt
  The exact words to type on Volatco after 10 LOAD.

What to do on the Windows 7 machine
-----------------------------------

Read INSTALL-STEPS.txt and follow it exactly.

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
- Fallback serial uploader tools remain in the repo under tools/ if needed, but
  they are intentionally not part of this primary deployment folder.
