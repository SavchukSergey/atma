@echo off
set atmasourcepath=../Atmega.Asm/Bin/Debug
set atmatargetpath=release/atma

rd release /S /Q
md release

cd release
md atma
cd ..

xcopy /y "%atmasourcepath%\atma.exe" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atma.exe.config" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atmega.dll" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\inc" "%atmatargetpath%\inc\*"

pause