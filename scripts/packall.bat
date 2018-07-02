@cd /d %~dp0..
@echo ------------------------------------------------
@echo   Moving existing packages to archive folder:
@echo.
@md pkg 2>nul
@cd pkg
@md archive 2>nul
@move *.nupkg archive 2>nul
@cd ..\src
@del *.nupkg /s >nul
@cd..
@echo ------------------------------------------------
@echo   Cleaning ^& building ^& packing all projects...
@echo.
@msbuild /v:m /t:clean^;build^;pack | find "nupkg"
@echo.
@echo ------------------------------------------------
@echo   Copying packages to pkg folder...
@for /r %%b in (debug\*.nupkg) do @copy %%b %~dp0..\pkg\ >nul
@echo   OK!
@echo ------------------------------------------------
@echo   Cleaning all projects, again...
@msbuild /v:m /t:clean 1> nul
@echo   OK!
@echo ------------------------------------------------
@echo   Listing packages in pkg folder:
@echo.
@dir %~dp0..\pkg
@cd /d %~dp0
@echo.
@echo   All done!