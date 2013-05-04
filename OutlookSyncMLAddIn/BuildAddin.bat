::Run this after running NewOutlookSyncApplicationManifest.bat in order to obtain the app manfest for the build
@echo Build SyncMLDotNet in default directories

if (%1)==() (
  echo Need parameter like 1.0.0.1
  goto END
)

set ClickOnceRevision=%1
set MyProgramId=OutlookSyncMLAddIn
set PublishPath=D:\Release\OutlookSyncAddIn\
set RevisionPublishPath=%PublishPath%%ClickOnceRevision%\
set BUILDADDIN=true

if not exist %RevisionPublishPath%  mkdir  %RevisionPublishPath% 
::del /Q /S /F %RevisionPublishPath%*.*
msbuild  OutlookSyncMLAddIn.csproj /target:rebuild  /p:ourdir=%RevisionPublishPath% 
xcopy CustomFiles\*.* %RevisionPublishPath% /Y
rem del "OutlookSyncAddIn\publish\Application Files\Fonlow.OutlookSyncAddin.vsto"
