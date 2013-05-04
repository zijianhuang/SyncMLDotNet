@echo Build SyncMLDotNet in default directories
msbuild OcSyncMLSetup\OcSyncMLSetup.vdproj /target:rebuild /p:configuration=release /p:Platform="Any CPU" /p:WarningLevel=3