"%ConEmuBaseDir%\ConEmuC.exe" /c "%ProgramFiles(x86)%\Git\bin\sh.exe" --login -i  -new_console:t:"MEAN":d:"%cd%\..\MethodFitness_MEAN2"

"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:t:"EventStore":d:c:\databases\eventstore EventStore.ClusterNode.exe --StatsPeriodSec=600 --run-projections=NONE
"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:t:"Mongo\D":d:c:\databases\mongo mongod.exe --dbpath ..\mongoData
"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:t:"Mongo":d:c:\databases\mongo mongo

PING 127.0.0.1 -n 4 || PING ::1 -n 4

"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:t:"Projections":d:%cd%\src\Projects\MF.Core.Projections.Console\bin\Debug MF.Core.Projections.Console.exe 
"%ConEmuBaseDir%\ConEmuC.exe" /c -new_console:t:"Domain":d:%cd%\src\Projects\Domain\MF.Core.Domain.Console\bin\Debug MF.Core.Domain.Console.exe 
