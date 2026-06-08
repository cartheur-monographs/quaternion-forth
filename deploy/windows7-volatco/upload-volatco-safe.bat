@echo off
setlocal
tclsh tools\volatco_upload.tcl polyforth\volatco-blocks.pf --line-delay 0.10 --read-timeout 0.30
endlocal
