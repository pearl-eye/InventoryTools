#requires -Version 7.0
[CmdletBinding()]
param()
$ErrorActionPreference = 'Stop'
$root = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$baseline = Get-Content (Join-Path $root 'localization/upstream.json') -Raw | ConvertFrom-Json
$manifest = Invoke-RestMethod -Uri $baseline.distributionManifest
if ($manifest.InternalName -ne 'InventoryTools' -or $manifest.AssemblyVersion -ne $baseline.distributionVersion) {
    throw "The original plugin has changed. Merge and review the new source before publishing. Expected $($baseline.distributionVersion); original now $($manifest.AssemblyVersion)."
}
if ($manifest.DalamudApiLevel -ne $baseline.dalamudApiLevel) { throw 'The original Dalamud API level has changed.' }
Write-Host "Original release verified: $($manifest.AssemblyVersion), API $($manifest.DalamudApiLevel)"
