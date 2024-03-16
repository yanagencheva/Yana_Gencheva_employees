# rename-dbcontext-file.ps1
[string]$dbContextFileName = "{dbContextName}.cs"
[string]$dbContextProjectPath = ".\src\EmployeesAPI.Persistence"

$partialdbContextFileName = "DbContext"
$dbContextFiles = Get-ChildItem -Path $dbContextProjectPath -Filter "*$partialdbContextFileName*" -File

if ($dbContextFiles.Count -gt 0) {
    $fileToRename = $dbContextFiles[0]
    $newFilePath = Join-Path $dbContextProjectPath $dbContextFileName
    Move-Item $fileToRename.FullName $newFilePath
} else {
    Write-Output "No db context files found with partial name: $partialdbContextFileName"
}

[string]$clientFileName = "WebApiClient.cs"
[string]$clientProjectPath = ".\src\EmployeesAPI.Communication\Client"

$partialClientFileName = "ApiClient"
$clientFiles = Get-ChildItem -Path $clientProjectPath -Filter "*$partialClientFileName*" -File

if ($clientFiles.Count -gt 0) {
    foreach ($clientFile in $clientFiles) {
        $fileContent = Get-Content $clientFile.FullName -Raw
        $regex = [regex]'\b(?:public\s+)?(?:interface|class)\s+(\w+)'
        $match = $regex.Match($fileContent)

        if ($match.Success) {
            $className = $match.Groups[1].Value
            $newClientFileName = "$className.cs"
            $newClientFilePath = Join-Path $clientProjectPath $newClientFileName
            Move-Item $clientFile.FullName $newClientFilePath
        } else {
            Write-Output "No class or interface name found in file: $($clientFile.Name)"
        }
    }
} else {
    Write-Output "No client files found with partial name: $partialClientFileName"
}
