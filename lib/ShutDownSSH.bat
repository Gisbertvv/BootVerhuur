@echo off
:start
cls

:B
SET MyProcess=ssh.exe
ECHO "%MyProcess%"
TASKLIST | FINDSTR /I "%MyProcess%"
IF ERRORLEVEL 0 (GOTO :StartScripts)
GOTO :B 

:StartScripts 
TASKKILL /F /IM "%MyProcess%"
TASKKILL /F /IM cmd.exe
PAUSE