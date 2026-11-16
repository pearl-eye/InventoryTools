# Dependency UI localization

The plugin and CriticalCommonLib keep their Chinese UI text in source. Two pinned
NuGet dependencies still contain English UI strings. This tool localizes their
copies in the plugin build output before packaging:

```sh
dotnet run --project tools/LocalizeDependencies -- InventoryTools/bin/Release
```

Pass the actual output directory containing `AllaganLib.GameSheets.dll` and
`AllaganLib.Interface.dll`. An optional second argument selects a different
catalog; the default is `localization/dependencies.json` in this repository.

Rules identify an exact full method signature, original text, Chinese text, and
expected occurrence count. No metadata token, IL offset, installed-game path, or
runtime game connection is needed. All rules are validated before files are
written, and generated assemblies are reopened and verified before replacement.
Repeated execution accepts already translated strings and makes no further edits.

When updating an AllaganLib package, review its UI changes and update the catalog
alongside the package version. A missing method or unexpected string count fails
the build instead of applying a partial match. Keep configuration keys, shared
data tags, resource names, and hidden ImGui identifiers unchanged.

The catalog records package versions for maintenance. The NuGet versions remain
pinned in the plugin projects; the tool additionally validates assembly names,
complete method signatures, and exact string counts.
