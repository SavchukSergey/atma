@echo off
set atmasourcepath=../Atmega.Asm/Bin/Debug
set atmatargetpath=release/atma
set flashersourcepath=../Atmega.Flasher.Wpf/Bin/Debug
set flashertargetpath=release/flasher

rd release /S /Q
md release

cd release
md atma
md flasher
cd ..

xcopy /y "%atmasourcepath%\atma.exe" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atma.exe.config" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atmega.dll" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\inc" "%atmatargetpath%\inc\*"

xcopy /y "%flashersourcepath%\flasher.exe" "%flashertargetpath%\*"
xcopy /y "%flashersourcepath%\flasher.exe.config" "%flashertargetpath%\*"
xcopy /y "%flashersourcepath%\atmega.dll" "%flashertargetpath%\*"
xcopy /y "%flashersourcepath%\atmega.flasher.dll" "%flashertargetpath%\*"
xcopy /y "%flashersourcepath%\devices.xml" "%flashertargetpath%\*"

pause