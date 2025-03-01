cd /d %~dp0

@echo [prepare somethings]
del MicaSetup.exe
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -property installationPath`) do set "path=%path%;%%i\MSBuild\Current\Bin;%%i\Common7\IDE"

@echo [prepare version]
cd /d .\MicaSetup
set "script=Get-Content 'Program.cs' ^| Select-String -Pattern 'DisplayVersion.*\"(.*)\"' ^| ForEach-Object { $_.Matches.Groups[1].Value }"

for /f "usebackq delims=" %%i in (`powershell -NoLogo -NoProfile -Command ^"%script%^"`) do set version=%%i

echo currnet version is %version%
cd /d %~dp0

echo [build app using vs2022]
cd ..\src\
dotnet restore
dotnet publish -c Release -p:PublishProfile=FolderProfile
cd /d %~dp0

echo [pack app using 7z]
del ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\*.pdb
mkdir ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins\
copy ..\src\Plugins\Fischless.Plugin.DisplayDefault\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Fischless.Plugin.DisplayDefault.dll ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.LaunchHyperion\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Fischless.Plugin.LaunchHyperion.dll ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.RepairRegedit\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Fischless.Plugin.RepairRegedit.dll ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.Borderless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Fischless.Plugin.Borderless.dll ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.LaunchBetterGI\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Fischless.Plugin.LaunchBetterGI.dll ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\Plugins
MicaSetup.Tools\7-Zip\7z a publish.7z ..\src\Desktop\Fischless\bin\x64\Release\net9.0-windows10.0.22621.0\publish\win-x64\* -t7z -mx=5 -mf=BCJ2 -r -y
copy /y publish.7z .\MicaSetup\Resources\Setups\publish.7z
move publish.7z Fischless_v%version%.7z

@echo [build uninst using vs2022]
msbuild MicaSetup\MicaSetup.Uninst.csproj /t:Rebuild /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /restore

@echo [build setup using vs2022]
copy /y .\MicaSetup\bin\Release\MicaSetup.exe .\MicaSetup\Resources\Setups\Uninst.exe
msbuild MicaSetup\MicaSetup.csproj /t:Build /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /restore

@echo [finish]
copy /y .\MicaSetup\bin\Release\MicaSetup.exe .\
del FischlessSetup.exe
move MicaSetup.exe FischlessSetup_v%version%.exe

@pause
