REM Update PATH for Visual Studio
set PATH=%PATH%;%VS80COMNTOOLS%..\IDE\

REM Build all solutions

FOR /F %%D IN ('DIR /B /S *.sln') DO (

	devenv /rebuild "Release|Win32" "%%D"

)
