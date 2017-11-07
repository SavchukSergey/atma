@echo off
set atmasourcepath=../Atmega.Asm/Bin/Debug/netcoreapp2.0
set atmatargetpath=release/atma

rd release /S /Q
md release

cd release
md atma
cd ..

xcopy /y "%atmasourcepath%\atma.dllexe" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atma.dll.config" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\atmega.dll" "%atmatargetpath%\*"
xcopy /y "%atmasourcepath%\inc" "%atmatargetpath%\inc\*"

pause