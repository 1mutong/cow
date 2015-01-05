@echo off
%windir%\Microsoft.NET\Framework\v4.0.30319\installutil.exe %~dp0\DairyService.exe
net start DairyReportaService
pause
echo on