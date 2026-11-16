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
    $baseline = Get-Content (Join-Path $repositoryRoot 'localization/upstream.json') -Raw | ConvertFrom-Json
    [xml]$projectXml = Get-Content -LiteralPath $project -Raw
    $projectVersion = $projectXml.SelectSingleNode('/Project/PropertyGroup/Version').InnerText
    if ($projectVersion -ne $baseline.distributionVersion) {
        throw "The CN version must equal the original distribution version $($baseline.distributionVersion); found $projectVersion."
    }
    & $DotnetCommand run --project (Join-Path $repositoryRoot 'tests/DependencyLocalizationRegression/DependencyLocalizationRegression.csproj') --configuration Release
    if ($LASTEXITCODE -ne 0) { throw 'Dependency display localization checks failed; refusing to package an unsafe UI patch.' }
    & $DotnetCommand run --project (Join-Path $repositoryRoot 'tests/LocalizationRegression/LocalizationRegression.csproj') --configuration Release
    if ($LASTEXITCODE -ne 0) { throw 'Localization regression checks failed; refusing to package untranslated list captions.' }
    & $DotnetCommand run --project (Join-Path $repositoryRoot 'tests/ChangelogRegression/ChangelogRegression.csproj') --configuration Release
    if ($LASTEXITCODE -ne 0) { throw 'Release-note regression checks failed; refusing to package a broken UI dependency.' }
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
