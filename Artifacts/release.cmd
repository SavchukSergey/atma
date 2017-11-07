@echo off

rd release /S /Q
md release

cd release
md atma

cd ../../Atmega.Asm
dotnet publish -c Release -r win10-x64 -o ../Artifacts/release/atma
cd ../Artifcats