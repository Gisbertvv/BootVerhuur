@echo off
:start
cls

IF EXIST "%0\..\OpenSSHPrivateKey" (
ssh -L 1433:localhost:1433 student@145.44.233.236 -i %0\..\OpenSSHPrivateKey
) ELSE (
echo Private Key does not exist
)