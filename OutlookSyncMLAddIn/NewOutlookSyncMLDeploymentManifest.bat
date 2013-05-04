:: Need parameter like 1.0.0.1
@Echo off
if (%1)==() (
  echo Need parameter like 1.0.0.1
  goto END
)

set ClickOnceRevision=%1
set MyProgramId=OutlookSyncAddIn
set PublishPath=D:\Release\%MyProgramId%\
set RevisionPublishPath=%PublishPath%%ClickOnceRevision%\
set ApplicationManifest=%RevisionPublishPath%Fonlow.OutlookSyncAddIn.dll.manifest
set DeploymentManifest=%PublishPath%Fonlow.OutlookSyncAddIn.dll.vsto
mage.exe -New Deployment -Processor msil -Install true -Publisher "Fonlow IT" -name "Fonlow.OutlookSyncAddIn.dll"  -AppManifest %ApplicationManifest% -ToFile %DeploymentManifest%


mage -Sign %DeploymentManifest% -CertFile FonlowSelfSigned.pfx
echo Created %DeploymentManifest%
:END
