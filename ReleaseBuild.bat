@echo Build SyncMLDotNet in default directories
msbuild SyncML.sln /target:rebuild /p:configuration=release /p:Platform="Any CPU" /p:WarningLevel=3