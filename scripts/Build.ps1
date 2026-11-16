#requires -Version 7.0
[CmdletBinding()]
param(
    [string]$DalamudPath = $env:DALAMUD_HOME,
    [string]$OutputDirectory = 'artifacts',
    [string]$DotnetCommand = 'dotnet'
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
if (!$DalamudPath) { throw 'Provide -DalamudPath pointing to a Chinese Dalamud API 15 installation.' }
$frameworkRoot = (Resolve-Path -LiteralPath $DalamudPath).Path
foreach ($file in @('Dalamud.dll', 'FFXIVClientStructs.dll', 'Lumina.dll', 'Lumina.Excel.dll')) {
    if (!(Test-Path -LiteralPath (Join-Path $frameworkRoot $file))) { throw "Missing framework assembly: $file" }
}
if (![System.IO.Path]::IsPathRooted($OutputDirectory)) { $OutputDirectory = Join-Path $repositoryRoot $OutputDirectory }
$outputRoot = [System.IO.Path]::GetFullPath($OutputDirectory)
if ($outputRoot -eq $repositoryRoot) { throw 'OutputDirectory must be separate from the source root.' }

Push-Location $repositoryRoot
try {
    $project = Join-Path $repositoryRoot 'InventoryTools/InventoryTools.csproj'
    & $DotnetCommand build $project --configuration Release -m:1 -nr:false '-clp:ErrorsOnly;Summary' '-p:EnableWindowsTargeting=true' "-p:DalamudLibPath=$frameworkRoot/" "-p:AllaganDotnetCommand=$DotnetCommand"
    if ($LASTEXITCODE -ne 0) { throw "Build failed with exit code $LASTEXITCODE" }
    $builtPackage = Join-Path $repositoryRoot 'InventoryTools/bin/Release/InventoryTools/latest.zip'
    if (!(Test-Path -LiteralPath $builtPackage)) { throw 'The build did not produce a Dalamud package.' }
    [System.IO.Directory]::CreateDirectory($outputRoot) | Out-Null
    $package = Join-Path $outputRoot 'latest.zip'
    Copy-Item -LiteralPath $builtPackage -Destination $package -Force

    $archive = [System.IO.Compression.ZipFile]::Open($package, [System.IO.Compression.ZipArchiveMode]::Update)
    try {
        $names = @($archive.Entries.FullName)
        foreach ($required in @('InventoryTools.dll', 'InventoryTools.json', 'CriticalCommonLib.dll', 'AllaganLib.GameSheets.dll', 'AllaganLib.Interface.dll')) {
            if ($required -cnotin $names) { throw "The package is missing $required" }
        }
        $forbidden = @('Dalamud.dll', 'FFXIVClientStructs.dll', 'Lumina.dll', 'Lumina.Excel.dll', 'InteropGenerator.Runtime.dll')
        foreach ($frameworkAssembly in $forbidden) {
            if ($frameworkAssembly -cin $names) { throw "Framework assembly must not be shipped: $frameworkAssembly" }
        }
        if (!$archive.GetEntry('LICENSE')) {
            [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($archive, (Join-Path $repositoryRoot 'LICENSE'), 'LICENSE') | Out-Null
        }
    } finally { $archive.Dispose() }

    $pluginDirectory = Join-Path $outputRoot 'InventoryTools'
    [System.IO.Compression.ZipFile]::ExtractToDirectory($package, $pluginDirectory, $true)
    Write-Host "Package ready: $package"
    Write-Host "Plugin files: $pluginDirectory"
} finally { Pop-Location }
