:: Need parameter like 1.0.0.1
@Echo off
if (%1)==() (
  echo Need parameter like 1.0.0.1
  goto END
)

set ClickOnceRevision=%1
set MyProgramId=OutlookSyncMLAddIn
set PublishPath=D:\Release\%MyProgramId%\
set RevisionPublishPath=%PublishPath%%ClickOnceRevision%\
set ApplicationManifest=%RevisionPublishPath%Fonlow.OutlookSyncMLAddIn.dll.manifest

if not exist %RevisionPublishPath%  mkdir  %RevisionPublishPath%
mage.exe -new application -processor msil -tofile %ApplicationManifest% -name "Fonlow.OutlookSyncMLAddIn.dll" -Version %ClickOnceRevision% -fromdirectory %RevisionPublishPath%
pause
mage.exe -sign %ApplicationManifest% -CertFile FonlowSelfSigned.pfx
echo Created %ApplicationManifest%
:END

