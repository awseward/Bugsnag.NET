@echo off

set paket_args=%*

IF NOT EXIST ".paket\paket.exe" (
".\.paket\paket.bootstrapper.exe"
)

".paket\paket.exe" %paket_args%
