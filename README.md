# Allagan Tools CN

## 简介

Allagan Tools 的国服汉化分支。原插件由 [Critical_Impact](https://github.com/Critical-Impact/InventoryTools) 开发；本分支由 [pearl-eye](https://github.com/pearl-eye) 维护中文界面与国服适配。

插件可以记录角色和雇员等位置的物品、搜索和整理背包、规划制作清单，并查询物品来源、用途及相关游戏资料。本分支保留插件内部名称 `InventoryTools`，现有设置可继续使用。

## 安装方式

需要支持 Dalamud API 15 的国服卫月。在卫月设置的自定义插件仓库中添加：

```text
https://raw.githubusercontent.com/pearl-eye/Dalamud-Plugins-CN/main/repo.json
```

保存后，在插件安装器中搜索 **Allagan Tools CN**。安装包和更新索引可在 [Releases](https://github.com/pearl-eye/Allagan-Tools-CN/releases) 下载。

如果已经安装其他来源的 Allagan Tools，请先禁用旧实例，再切换安装来源；不要同时加载两个 `InventoryTools` 实例。更换版本前可备份卫月的 `pluginConfigs/InventoryTools` 目录及 `InventoryTools.json` 配置。

## 反馈与贡献

中文显示、翻译和本分支的国服兼容问题，请提交到 [本仓库 Issues](https://github.com/pearl-eye/Allagan-Tools-CN/issues)。报告时附上插件版本、问题所在窗口和复现步骤。

修改界面文字时，请保留文本指令、配置字段、序列化名称、IPC 标识和 ImGui 的隐藏 ID。原始上游项目和资料见 [Critical-Impact/InventoryTools](https://github.com/Critical-Impact/InventoryTools) 与 [上游 Wiki](https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview)。

## 许可证与归属

本项目沿用上游的 [GNU General Public License v3](LICENSE)。原作者及各贡献者的版权和声明保持不变。中文界面与本分支维护由 pearl-eye 提供，不代表上游官方发布。

`CriticalCommonLib`、`OtterGui` 及 NuGet 依赖仍遵循各自的许可证和声明。
