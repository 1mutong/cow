@echo off
net stop DairyReportaService
%windir%\Microsoft.NET\Framework\v4.0.30319\installutil.exe %~dp0\DairyService.exe /u
pause
echo on