cd /d %~dp0

@echo [prepare somethings]
del MicaSetup.exe
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -property installationPath`) do set "path=%path%;%%i\MSBuild\Current\Bin;%%i\Common7\IDE"

echo [build app using vs2022]
cd ..\src\
@REM dotnet publish .\Desktop\Fischless\Fischless.csproj -c Release -p:PublishProfile=FolderProfile
dotnet restore
msbuild .\Desktop\Fischless\Fischless.csproj /t:Publish /p:Configuration=Release /p:PublishProfile=FolderProfile
msbuild .\Plugins\Fischless.Plugin.DisplayDefault\Fischless.Plugin.DisplayDefault.csproj /t:Publish /p:Configuration=Release
msbuild .\Plugins\Fischless.Plugin.LaunchHyperion\Fischless.Plugin.LaunchHyperion.csproj /t:Publish /p:Configuration=Release
msbuild .\Plugins\Fischless.Plugin.RepairRegedit\Fischless.Plugin.RepairRegedit.csproj /t:Publish /p:Configuration=Release
cd /d %~dp0

echo [pack app using 7z]
del ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\*.pdb
mkdir ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\Plugins\
copy ..\src\Plugins\Fischless.Plugin.DisplayDefault\bin\Release\net7.0-windows10.0.22621.0\publish\Fischless.Plugin.DisplayDefault.dll ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.LaunchHyperion\bin\Release\net7.0-windows10.0.22621.0\publish\Fischless.Plugin.LaunchHyperion.dll ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\Plugins
copy ..\src\Plugins\Fischless.Plugin.RepairRegedit\bin\Release\net7.0-windows10.0.22621.0\publish\Fischless.Plugin.RepairRegedit.dll ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\Plugins
MicaSetup.Tools\7-Zip\7z a publish.7z ..\src\Desktop\Fischless\bin\x64\Release\net7.0-windows10.0.22621.0\publish\win-x64\* -t7z -mx=5 -mf=BCJ2 -r -y
copy /y publish.7z .\MicaSetup\Resources\Setups\publish.7z

@echo [build uninst using vs2022]
msbuild MicaSetup\MicaSetup.Uninst.csproj /t:Rebuild /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /restore

@echo [build setup using vs2022]
copy /y .\MicaSetup\bin\Release\net472\MicaSetup.exe .\MicaSetup\Resources\Setups\Uninst.exe
msbuild MicaSetup\MicaSetup.csproj /t:Build /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /restore

@echo [finish]
copy /y .\MicaSetup\bin\Release\net472\MicaSetup.exe .\
del FischlessSetup.exe
move MicaSetup.exe FischlessSetup.exe

@pause
