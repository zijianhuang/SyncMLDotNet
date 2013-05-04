:: Need parameter like 1.0.0.1
@Echo off
if (%1)==() (
  echo Need parameter like 1.0.0.1
  goto END
)

set ClickOnceRevision=%1
set DeploymentPath=PathForPublishingMyProgram
set DeploymentManifest=%DeploymentPath%MyProgram.application
mage -Update %DeploymentManifest%  -Processor msil -Install true -Publisher "My Company" -name "My Program" -Version %ClickOnceRevision% -ProviderUrl "%DeploymentManifest%" -AppManifest %DeploymentPath%MyProgram_%ClickOnceRevision%\MyProgram.exe.manifest -ToFile %DeploymentManifest%

::Modify the manifest
XsltRun.exe %DeploymentManifest% ChangeDeploymentManifest.xsl %DeploymentManifest%


mage -Sign %DeploymentManifest% -CertFile MyCert.pfx -password MyPasswordOfPfx
echo It is %DeploymentManifest%
:END

