# replace-yaml-template-values.ps1
param (
    [string]$replacementValue = ""
)

if ($replacementValue -eq "") {
    $replacementValue = Read-Host "Please enter the azure library value to be used in YAML files: "
    $replacementValue = $replacementValue.ToLower()
}
$replacementValueUpper = $replacementValue.Substring(0, 1).ToUpper() + $replacementValue.Substring(1)

# Change the following variables as needed
$folderPath = ".\"
$files = Get-ChildItem -Path $folderPath -Filter *.yaml -Recurse -File

# Print the count of files on the console
Write-Host "Number of files found: " $files.Count
Write-Host "Replacement value: " $replacementValue
Write-Host "Upper replacement value: " $replacementValueUpper

foreach ($file in $files) {
    # Read the file content
    $content = Get-Content $file.FullName

    # Initialize an empty array to store the updated lines
    $updatedContent = @()

    foreach ($line in $content) {

        if ($line -notmatch "- template: ") {
            if ($line -match "SvcName") {
                $replacement = $replacementValueUpper
            } else {
                $replacement = $replacementValue
            }
            $updatedLine = $line -replace 'template', $replacement
        } else {
            $updatedLine = $line
        }
        $updatedContent += $updatedLine
    }

    # Write the updated content back to the file
    Set-Content -Path $file.FullName -Value $updatedContent
}

# Rename files containing "template" in their names
foreach ($file in $files) {
    $fileName = $file.Name
    if ($fileName -match 'template') {
        $newFileName = $fileName -replace 'template', $replacementValue
        Rename-Item -Path $file.FullName -NewName $newFileName
    }
}

# Rename "template" folder in \deploy\manifests
Rename-Item -Path ".\deploy\manifests\template" -NewName $replacementValue