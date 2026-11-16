#requires -Version 7.0
[CmdletBinding()]
param(
    [string]$ManifestPath = 'artifacts/InventoryTools/InventoryTools.json',
    [string]$PackagePath = 'artifacts/latest.zip',
    [string]$OutputPath = 'artifacts/repo.json',
    [string]$Repository = 'pearl-eye/InventoryTools',
    [string]$ReleaseTag
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

if ($Repository -notmatch '^[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$') {
    throw 'Repository must use the owner/repository format.'
}
foreach ($path in @($ManifestPath, $PackagePath)) {
    if (!(Test-Path -LiteralPath $path -PathType Leaf)) {
        throw "Required build output was not found: $path"
    }
}

$manifest = Get-Content -LiteralPath $ManifestPath -Raw -Encoding utf8 | ConvertFrom-Json -AsHashtable
if ($manifest.InternalName -ne 'InventoryTools') {
    throw 'The built manifest must have InternalName=InventoryTools.'
}
if (!$manifest.AssemblyVersion -or $manifest.AssemblyVersion -notmatch '^\d+\.\d+\.\d+(\.\d+)?$') {
    throw 'Use the generated build manifest, which must contain a numeric AssemblyVersion.'
}
if ($manifest.DalamudApiLevel -ne 15) {
    throw 'This release workflow currently supports Dalamud API 15 only.'
}

$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$baseline = Get-Content -LiteralPath (Join-Path $repositoryRoot 'localization/upstream.json') -Raw | ConvertFrom-Json
if ($manifest.AssemblyVersion -cne $baseline.distributionVersion) {
    throw "The CN version must equal the original distribution version $($baseline.distributionVersion); found $($manifest.AssemblyVersion)."
}

$expectedTag = "cn-v$($manifest.AssemblyVersion)"
if (!$ReleaseTag) { $ReleaseTag = $expectedTag }
if ($ReleaseTag -cnotin @($expectedTag, "$expectedTag-localization")) {
    throw "ReleaseTag must use the unchanged assembly version: $expectedTag or $expectedTag-localization"
}

# A Dalamud package needs the main assembly and manifest at the archive root.
$archive = [System.IO.Compression.ZipFile]::OpenRead((Resolve-Path -LiteralPath $PackagePath).Path)
try {
    $entries = @($archive.Entries.FullName)
    foreach ($required in @('InventoryTools.dll', 'InventoryTools.json')) {
        if ($required -cnotin $entries) { throw "The package is missing its root entry: $required" }
    }
    $reader = [System.IO.StreamReader]::new($archive.GetEntry('InventoryTools.json').Open())
    try { $packagedManifest = $reader.ReadToEnd() | ConvertFrom-Json -AsHashtable }
    finally { $reader.Dispose() }
    if ($packagedManifest.InternalName -ne $manifest.InternalName -or
        $packagedManifest.AssemblyVersion -ne $manifest.AssemblyVersion -or
        $packagedManifest.DalamudApiLevel -ne $manifest.DalamudApiLevel) {
        throw 'The package and build manifest describe different plugin versions.'
    }
}
finally { $archive.Dispose() }

# Copy only repository metadata; never publish local installed-plugin state.
$entry = [ordered]@{}
$fields = @(
    'Author', 'Name', 'Punchline', 'Description', 'Changelog', 'Tags', 'CategoryTags',
    'InternalName', 'AssemblyVersion', 'ApplicableVersion', 'MinimumDalamudVersion',
    'DalamudApiLevel', 'LoadRequiredState', 'LoadSync', 'LoadPriority', 'CanUnloadAsync',
    'SupportsProfiles', 'ImageUrls', 'IconUrl', 'AcceptsFeedback', 'FeedbackMessage'
)
foreach ($field in $fields) {
    if ($manifest.ContainsKey($field)) { $entry[$field] = $manifest[$field] }
}
if (!$entry.Contains('ApplicableVersion')) { $entry.ApplicableVersion = 'any' }
$downloadUrl = "https://github.com/$Repository/releases/download/$ReleaseTag/latest.zip"
$entry.RepoUrl = "https://github.com/$Repository"
$entry.DownloadLinkInstall = $downloadUrl
$entry.DownloadLinkUpdate = $downloadUrl
$entry.DownloadLinkTesting = $downloadUrl
$entry.LastUpdate = [DateTimeOffset]::UtcNow.ToUnixTimeSeconds()
$entry.DownloadCount = 0
$entry.IsHide = $false
$entry.IsTestingExclusive = $false

$destination = [System.IO.Path]::GetFullPath($OutputPath)
[System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($destination)) | Out-Null
$json = ConvertTo-Json -InputObject @($entry) -Depth 30
[System.IO.File]::WriteAllText($destination, $json + [Environment]::NewLine, [System.Text.UTF8Encoding]::new($false))
Write-Host "Generated $destination for $ReleaseTag"
