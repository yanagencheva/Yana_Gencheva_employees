# cleanup.ps1
[string]$configFolderPath = ".\.scripts"
[string]$setupFile = ".\setup.cmd"
[string]$cleanupFile = ".\cleanup.cmd"

Remove-Item -Force $setupFile
Write-Output "The setup.cmd file has been deleted."
Remove-Item -Recurse -Force $cleanupFile
Write-Output "The cleanup.cmd file has been deleted."
Remove-Item -Recurse -Force $configFolderPath
Write-Output "The .config folder has been deleted."