"%ConEmuBaseDir%\ConEmuC.exe" /c "%ProgramFiles(x86)%\Git\bin\sh.exe" --login -i  -new_console:d:%cd%\..\MethodFitness_MEAN2

"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:d:c:\databases\eventstore EventStore.ClusterNode.exe
"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:d:c:\databases\mongo mongod.exe --dbpath ..\mongoData
"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:d:c:\databases\mongo mongo

"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:d:%cd%\src\Projects\MF.Core.Projections.Console\bin\Debug MF.Core.Projections.Console.exe 
cd src\Projects\Domain\MF.Core.Domain.Console\bin\Debug
MF.Core.Domain.Console.exe