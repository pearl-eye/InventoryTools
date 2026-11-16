# Allagan Tools CN

Allagan Tools 的国服汉化分支。原插件由 [Critical_Impact](https://github.com/Critical-Impact/InventoryTools) 开发；本分支由 [pearl-eye](https://github.com/pearl-eye) 维护中文界面与国服适配。

用于同步上游的 fork 位于 [pearl-eye/InventoryTools](https://github.com/pearl-eye/InventoryTools)，本仓库保存汉化源码和可安装版本。

插件可以记录角色和雇员等位置的物品、搜索和整理背包、规划制作清单，并查询物品来源、用途及相关游戏资料。本分支保留插件内部名称 `InventoryTools`，现有设置可继续使用。

## 安装

需要支持 Dalamud API 15 的国服卫月。在卫月设置的自定义插件仓库中添加：

```text
https://github.com/pearl-eye/Allagan-Tools-CN/releases/latest/download/repo.json
```

保存后，在插件安装器中搜索 **Allagan Tools**。安装包和更新索引可在 [Releases](https://github.com/pearl-eye/Allagan-Tools-CN/releases) 下载。

如果已经安装其他来源的 Allagan Tools，请先禁用旧实例，再切换安装来源；不要同时加载两个 `InventoryTools` 实例。更换版本前可备份卫月的 `pluginConfigs/InventoryTools` 目录及 `InventoryTools.json` 配置。

## 本地构建

需要 .NET SDK 10.0.100 或兼容的 10.0 补丁版本、PowerShell 7，以及国服 Dalamud API 15 的开发依赖。

```powershell
git clone --branch zh-CN --recurse-submodules https://github.com/pearl-eye/Allagan-Tools-CN.git
Set-Location Allagan-Tools-CN

./scripts/Build.ps1 -DalamudPath "$env:APPDATA\XIVLauncherCN\addon\Hooks\dev" -OutputDirectory artifacts
./scripts/Generate-Repo.ps1 -ManifestPath artifacts/InventoryTools/InventoryTools.json -PackagePath artifacts/latest.zip -OutputPath artifacts/repo.json
```

`DalamudPath` 应指向包含 `Dalamud.dll` 的国服卫月目录。构建结果在 `artifacts/InventoryTools`，安装包为 `artifacts/latest.zip`，插件仓库索引为 `artifacts/repo.json`。本地调试可将构建后的 `InventoryTools.dll` 加入卫月开发插件列表。

生成索引时会核对安装包和构建清单的版本；下载地址由 `AssemblyVersion` 推导。例如 `15.0.13.1` 对应发布标签 `cn-v15.0.13.1`。索引只收录插件元数据，不包含本机安装状态或玩家配置。

## 自动构建与发布

自动构建是可选功能，配置保存在 [build-release.yml 模板](.github/optional-workflows/build-release.yml)，当前没有启用。需要启用时，将它放入 `.github/workflows/build-release.yml`；通过 GitHub CLI 提交工作流文件需要登录凭据包含 `workflow` 权限。

启用后，流程会在 `zh-CN` 分支更新或收到 Pull Request 时构建，并提供安装包、仓库索引和 SHA-256 校验文件。

手动运行时，默认仅构建。勾选 `publish` 会在构建成功后创建 GitHub Release；也可以推送与程序集版本一致的 `cn-v*` 标签触发发布。手动发布须选择 `zh-CN` 分支或版本标签。已存在的 Release 不会被覆盖，后续更新应提高版本号。

未启用自动构建时，可在本地完成上面的构建及索引生成，再使用 GitHub 网页或 CLI 将 `artifacts/latest.zip` 和 `artifacts/repo.json` 上传到对应 `cn-v*` 标签的 Release。

CI 默认使用固定的国服 Dalamud `26-11-13-01` 依赖包：

```text
https://dalamud-dis.atmoomen.top/26-11-13-01/latest.7z
```

地址格式来自 [国服 Dalamud 的分发工作流](https://github.com/Dalamud-DailyRoutines/Dalamud/blob/master/.github/workflows/r2-upload.yml)，该工作流将 `{tag}/latest.7z` 上传至上述分发服务。需要升级依赖时，可设置仓库 Actions 变量 `DALAMUD_DISTRIB_URL`；手动运行时的 `dalamud_url` 输入优先级更高。更新依赖后应重新核对 API 兼容性。

## 反馈与贡献

中文显示、翻译和本分支的国服兼容问题，请提交到 [本仓库 Issues](https://github.com/pearl-eye/Allagan-Tools-CN/issues)。报告时附上插件版本、问题所在窗口和复现步骤。

修改界面文字时，请保留文本指令、配置字段、序列化名称、IPC 标识和 ImGui 的隐藏 ID。原始上游项目和资料见 [Critical-Impact/InventoryTools](https://github.com/Critical-Impact/InventoryTools) 与 [上游 Wiki](https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview)。

## 许可证与归属

本项目沿用上游的 [GNU General Public License v3](LICENSE)。原作者及各贡献者的版权和声明保持不变。中文界面与本分支维护由 pearl-eye 提供，不代表上游官方发布。

`CriticalCommonLib`、`OtterGui` 及 NuGet 依赖仍遵循各自的许可证和声明。
