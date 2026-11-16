using System;
using System.Collections.Generic;
using System.Linq;
using AllaganLib.GameSheets.Extensions;
using AllaganLib.GameSheets.Model;
using AllaganLib.GameSheets.Sheets;
using AllaganLib.GameSheets.Sheets.Rows;
using AllaganLib.Shared.Extensions;
using CriticalCommonLib.Models;
using DalaMock.Host.Mediator;
using Dalamud.Interface.Colors;
using Dalamud.Plugin.Services;
using InventoryTools.Compendium.Interfaces;
using InventoryTools.Compendium.Models;
using InventoryTools.Compendium.Sections.Options;
using InventoryTools.Compendium.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Compendium.Types;

public record BNpcZoneInfo(
    IReadOnlyList<TerritoryTypeRow> Zones,
    IReadOnlyList<string> ZoneNames,
    IReadOnlyList<string> ZoneTypes,
    IReadOnlyList<string> Expansions,
    bool IsInOpenWorld);

public class BNpcCompendiumType : CompendiumType<BNpcNameRow>
{
    private const string OpenWorldZoneType = "野外区域";
    private const string OtherZoneType = "其它";
    private const string NotAHuntMark = "非狩猎怪物";

    private static readonly EquipSlot[] SharedModelSlots =
    [
        EquipSlot.MainHand,
        EquipSlot.OffHand,
        EquipSlot.Head,
        EquipSlot.Body,
        EquipSlot.Gloves,
        EquipSlot.Legs,
        EquipSlot.Feet,
        EquipSlot.FingerL,
        EquipSlot.FingerR,
    ];

    private readonly BNpcNameSheet _bNpcNameSheet;
    private readonly TerritoryTypeSheet _territoryTypeSheet;
    private readonly ItemSheet _itemSheet;
    private readonly List<MobSpawnPosition> _mobSpawnPositions;
    private readonly IClientState _clientState;
    private readonly Lazy<List<BNpcNameRow>> _rows;
    private readonly Lazy<Dictionary<uint, BNpcZoneInfo>> _zoneInfo;
    private readonly Lazy<Dictionary<uint, List<uint>>> _mobsByTerritory;

    public BNpcCompendiumType(
        BNpcNameSheet bNpcNameSheet,
        TerritoryTypeSheet territoryTypeSheet,
        ItemSheet itemSheet,
        List<MobSpawnPosition> mobSpawnPositions,
        IClientState clientState,
        CompendiumTable<BNpcNameRow>.Factory tableFactory,
        CompendiumColumnBuilder<BNpcNameRow>.Factory columnBuilder,
        CompendiumViewBuilder.Factory viewBuilderFactory) : base(tableFactory, columnBuilder, viewBuilderFactory)
    {
        _bNpcNameSheet = bNpcNameSheet;
        _territoryTypeSheet = territoryTypeSheet;
        _itemSheet = itemSheet;
        _mobSpawnPositions = mobSpawnPositions;
        _clientState = clientState;
        _rows = new Lazy<List<BNpcNameRow>>(BuildRows);
        _zoneInfo = new Lazy<Dictionary<uint, BNpcZoneInfo>>(BuildZoneInfo);
        _mobsByTerritory = new Lazy<Dictionary<uint, List<uint>>>(BuildMobsByTerritory);
    }

    public override string Singular => "怪物";
    public override string Plural => "怪物";
    public override string Description => "怪物及其出现地点和掉落物品。";
    public override string Key => "mobs";
    public override (string?, uint?) Icon => (null, Icons.MobIcon);

    public override List<Type>? RelatedTypes => [typeof(BNpcName)];

    public override ICompendiumTable<WindowState, MessageBase> BuildTable()
    {
        return Factory.Invoke(new CompendiumTableOptions<BNpcNameRow>()
        {
            Key = Key,
            Name = Plural,
            Columns = BuiltColumns,
            CompendiumType = this,
        });
    }

    public override string? GetName(BNpcNameRow row)
    {
        return AllaganLib.Shared.Extensions.StringExtensions.ToTitleCase(row.Base.Singular.ToImGuiString());
    }

    public override string? GetSubtitle(BNpcNameRow row)
    {
        return GetMobTypes(row);
    }

    public override (string?, uint?) GetIcon(BNpcNameRow row)
    {
        return (null, Icons.MobIcon);
    }

    public override uint GetRowId(BNpcNameRow row)
    {
        return row.RowId;
    }

    public override BNpcNameRow? GetRow(uint row)
    {
        return HasRow(row) ? _bNpcNameSheet.GetRowOrDefault(row) : null;
    }

    public override bool HasRow(uint rowId)
    {
        return _zoneInfo.Value.ContainsKey(rowId);
    }

    public override List<BNpcNameRow> GetRows()
    {
        return _rows.Value;
    }

    public override string GetDefaultGrouping()
    {
        return "zone_type";
    }

    public override List<ICompendiumGrouping>? GetGroupings()
    {
        return
        [
            new CompendiumGrouping<BNpcNameRow>()
            {
                Key = "zone_type",
                Name = "区域类型",
                GroupsFunc = row => GetZoneInfo(row).ZoneTypes,
                GroupMapping = zoneType => (string)zoneType,
            },
            new CompendiumGrouping<BNpcNameRow>()
            {
                Key = "zone",
                Name = "区域",
                GroupsFunc = row => GetZoneInfo(row).ZoneNames,
                GroupMapping = zoneName => (string)zoneName,
                CurrentGroupFunc = GetCurrentZoneName,
                CurrentGroupLabel = "当前区域",
            },
            new CompendiumGrouping<BNpcNameRow>()
            {
                Key = "expansion",
                Name = "资料片",
                GroupsFunc = row => GetZoneInfo(row).Expansions,
                GroupMapping = expansion => (string)expansion,
            },
            new CompendiumGrouping<BNpcNameRow>()
            {
                Key = "hunt_rank",
                Name = "狩猎等级",
                GroupFunc = GetHuntRank,
                GroupMapping = huntRank => (string)huntRank,
            },
        ];
    }

    public override void BuildColumns(CompendiumColumnBuilder<BNpcNameRow> builder)
    {
        var grouping = builder.CompendiumGrouping?.Key;

        builder.AddCompendiumOpenViewColumn(new()
        {
            Key = "icon",
            Name = "##Icon",
            HelpText = "打开怪物详情",
            Version = "15.0.14",
            ValueSelector = GetIcon,
            CompendiumType = this,
            RowIdSelector = row => row.RowId,
        });
        builder.AddStringColumn(new()
        {
            Key = "name",
            Name = "名称",
            HelpText = "怪物的名称",
            Version = "15.0.14",
            ValueSelector = GetName,
        });
        builder.AddStringColumn(new()
        {
            Key = "type",
            Name = "类型",
            HelpText = "怪物的类型",
            Version = "15.0.14",
            ValueSelector = GetMobTypes,
        });

        if (grouping != "hunt_rank")
        {
            builder.AddStringColumn(new()
            {
                Key = "hunt_rank",
                Name = "狩猎等级",
                HelpText = "怪物的狩猎等级（仅限狩猎怪物）",
                Version = "15.0.14",
                ValueSelector = row => GetHuntRank(row) != NotAHuntMark ? GetHuntRank(row) : null,
            });
        }

        builder.AddBooleanColumn(new()
        {
            Key = "is_hunt_mark",
            Name = "狩猎怪物？",
            HelpText = "此怪物是否为狩猎怪物？",
            Version = "15.1.1",
            ValueSelector = row => GetHuntRank(row) != NotAHuntMark,
        });

        builder.AddStringColumn(new()
        {
            Key = "zones",
            Name = "区域",
            HelpText = "怪物出现的区域",
            Version = "15.0.14",
            ValueSelector = row => string.Join(", ", GetZoneInfo(row).ZoneNames),
        });

        var groupZoneName = grouping == "zone" ? builder.GroupItem as string : null;
        builder.AddMapLinksColumn(new()
        {
            Key = "locations",
            Name = "出现地点",
            HelpText = "怪物的出现地点。点击旗帜可向聊天栏发送地图链接，右键点击可查看更多选项。",
            Version = "15.1.1",
            ValueSelector = row => groupZoneName == null
                ? GetMapLinks(row)
                : GetMapLinks(row).Where(c => c.Name == groupZoneName).ToList(),
        });

        if (grouping != "zone_type")
        {
            builder.AddStringColumn(new()
            {
                Key = "zone_type",
                Name = "区域类型",
                HelpText = "怪物所在区域的类型，例如野外、迷宫挑战或大型任务",
                Version = "15.0.14",
                ValueSelector = row => string.Join(", ", GetZoneInfo(row).ZoneTypes),
            });
        }

        builder.AddBooleanColumn(new()
        {
            Key = "open_world",
            Name = "野外怪物？",
            HelpText = "此怪物是否出现在野外区域？",
            Version = "15.0.14",
            ValueSelector = row => GetZoneInfo(row).IsInOpenWorld,
        });

        if (grouping != "expansion")
        {
            builder.AddStringColumn(new()
            {
                Key = "expansion",
                Name = "资料片",
                HelpText = "怪物出现区域所属的资料片",
                Version = "15.0.14",
                ValueSelector = row => string.Join(", ", GetZoneInfo(row).Expansions),
            });
        }

        builder.AddItemsColumn(new()
        {
            Key = "drops",
            Name = "掉落物品",
            HelpText = "此怪物掉落的物品",
            Version = "15.0.14",
            ValueSelector = GetDrops,
        });
    }

    public override void BuildViewFields(CompendiumViewBuilder viewBuilder, BNpcNameRow row)
    {
        viewBuilder.SetupDefaults(this, row);

        foreach (var mobType in row.MobTypes)
        {
            viewBuilder.AddTag(() => GetMobTypeName(mobType), () => "怪物的类型");
        }

        var huntRank = GetHuntRank(row);
        if (huntRank != NotAHuntMark)
        {
            viewBuilder.AddTag(() => huntRank + "级狩猎怪物", () => "此怪物为狩猎怪物", () => ImGuiColors.DalamudOrange);
        }

        var zoneInfo = GetZoneInfo(row);
        if (zoneInfo.IsInOpenWorld)
        {
            viewBuilder.AddTag(() => OpenWorldZoneType, () => "此怪物出现在野外区域");
        }

        var garlandToolsId = row.GarlandToolsId;
        if (garlandToolsId != null)
        {
            viewBuilder.AddLink($"https://www.garlandtools.org/db/#mob/{garlandToolsId}", "在 Garland Tools 中打开", "garlandtools");
        }

        viewBuilder.AddLink($"https://ffxivteamcraft.com/db/en/mob/{row.RowId}", "在 Teamcraft 中打开", "teamcraft");

        viewBuilder.AddItemListSection(new ItemListSectionOptions()
        {
            SectionKey = "drops",
            SectionName = "掉落物品",
            Items = GetDrops(row).Select(c => new ItemInfo(c)).ToList(),
        });

        viewBuilder.AddMapLinksSectionSection(new MapLinksViewSectionOptions()
        {
            SectionKey = "spawn_locations",
            SectionName = "出现地点",
            MapLinks = GetMapLinks(row),
        });

        viewBuilder.AddCollectionRowRefSection(new CollectionRowRefSectionOptions()
        {
            SectionKey = "zones",
            SectionName = "区域",
            RelatedRefs = zoneInfo.Zones.Select(c => c.Base.AsUntypedRowRef()).ToList(),
        });

        var firstBase = row.RelatedBases.FirstOrDefault();
        if (firstBase != null)
        {
            foreach (var slot in SharedModelSlots)
            {
                viewBuilder.AddItemListSection(new ItemListSectionOptions()
                {
                    SectionKey = "shared_models_" + slot,
                    SectionName = "共用模型 - " + GetEquipSlotName(slot),
                    Items = firstBase.GetRelatedItems(slot).Select(c => new ItemInfo(c)).ToList(),
                });
            }
        }
    }

    public override bool HasLocation => true;

    public override ILocation? GetLocation(BNpcNameRow row)
    {
        return GetMapLinks(row).FirstOrDefault()?.Location;
    }

    public string? GetCurrentZoneName()
    {
        var territoryType = _territoryTypeSheet.GetRowOrDefault(_clientState.TerritoryType);
        if (territoryType == null || territoryType.Base.PlaceName.RowId == 0)
        {
            return null;
        }

        return territoryType.Base.PlaceName.ValueNullable?.Name.ToImGuiString();
    }

    public BNpcZoneInfo GetZoneInfo(BNpcNameRow row)
    {
        return _zoneInfo.Value.TryGetValue(row.RowId, out var zoneInfo) ? zoneInfo : new BNpcZoneInfo([], [], [], [], false);
    }

    public List<uint> GetMobsInTerritory(uint territoryTypeId)
    {
        return _mobsByTerritory.Value.TryGetValue(territoryTypeId, out var mobs) ? mobs : [];
    }

    public string GetHuntRank(BNpcNameRow row)
    {
        return row.NotoriousMonster?.Base.Rank switch
        {
            1 => "B",
            2 => "A",
            3 => "S",
            _ => NotAHuntMark,
        };
    }

    public string GetZoneTypeName(TerritoryTypeRow territoryType)
    {
        var contentFinderCondition = territoryType.Base.ContentFinderCondition;
        if (contentFinderCondition.RowId != 0)
        {
            var contentType = contentFinderCondition.ValueNullable?.ContentType.ValueNullable?.Name.ToImGuiString();
            if (!string.IsNullOrEmpty(contentType))
            {
                return contentType;
            }
        }

        return territoryType.Base.TerritoryIntendedUse.RowId == 1 ? OpenWorldZoneType : OtherZoneType;
    }

    private string GetMobTypes(BNpcNameRow row)
    {
        return string.Join("、", row.MobTypes.Select(GetMobTypeName));
    }

    private static string GetMobTypeName(NpcType type) => type switch
    {
        NpcType.Humanoid => "人形",
        NpcType.Nest => "巢穴",
        NpcType.Misc => "其它",
        NpcType.Monster => "怪物",
        _ => "未知",
    };

    private static string GetEquipSlotName(EquipSlot slot) => slot switch
    {
        EquipSlot.MainHand => "主手",
        EquipSlot.OffHand => "副手",
        EquipSlot.Head => "头部",
        EquipSlot.Body => "身体",
        EquipSlot.Gloves => "手部",
        EquipSlot.Legs => "腿部",
        EquipSlot.Feet => "脚部",
        EquipSlot.FingerL => "左戒指",
        EquipSlot.FingerR => "右戒指",
        _ => slot.ToString(),
    };

    private List<ItemRow> GetDrops(BNpcNameRow row)
    {
        return row.MobDrops
            .Select(c => _itemSheet.GetRowOrDefault(c.ItemId))
            .Where(c => c != null)
            .Select(c => c!)
            .DistinctBy(c => c.RowId)
            .ToList();
    }

    private List<MapLinkEntry> GetMapLinks(BNpcNameRow row)
    {
        var mapLinks = new List<MapLinkEntry>();
        var mobName = GetName(row);
        foreach (var spawn in row.MobSpawnPositions.DistinctBy(c => (c.TerritoryTypeId, MathF.Round(c.Position.X, 1), MathF.Round(c.Position.Y, 1))))
        {
            var territoryType = _territoryTypeSheet.GetRowOrDefault(spawn.TerritoryTypeId);
            if (territoryType == null || territoryType.Base.Map.RowId == 0)
            {
                continue;
            }

            var location = new GenericMapLocation(spawn.Position.X, spawn.Position.Y, territoryType.Base.Map, territoryType.Base.PlaceName, territoryType.RowRef);
            var zoneName = territoryType.Base.PlaceName.ValueNullable?.Name.ToImGuiString() ?? "未知";
            mapLinks.Add(new MapLinkEntry(Icons.FlagIcon, zoneName, $"X: {spawn.Position.X:0.0} Y: {spawn.Position.Y:0.0}", location, mobName));
        }

        return mapLinks;
    }

    private List<BNpcNameRow> BuildRows()
    {
        return _zoneInfo.Value.Keys
            .Select(c => _bNpcNameSheet.GetRowOrDefault(c))
            .Where(c => c != null)
            .Select(c => c!)
            .OrderBy(c => GetName(c))
            .ToList();
    }

    private Dictionary<uint, BNpcZoneInfo> BuildZoneInfo()
    {
        var zoneInfo = new Dictionary<uint, BNpcZoneInfo>();
        foreach (var spawns in _mobSpawnPositions.GroupBy(c => c.BNpcNameId))
        {
            var bNpcName = _bNpcNameSheet.GetRowOrDefault(spawns.Key);
            if (bNpcName == null || bNpcName.Base.Singular.IsEmpty)
            {
                continue;
            }

            var zones = spawns
                .Select(c => c.TerritoryTypeId)
                .Distinct()
                .Select(c => _territoryTypeSheet.GetRowOrDefault(c))
                .Where(c => c != null && c.Base.PlaceName.RowId != 0)
                .Select(c => c!)
                .ToList();
            if (zones.Count == 0)
            {
                continue;
            }

            var zoneTypes = zones.Select(GetZoneTypeName).Distinct().OrderBy(c => c).ToList();
            zoneInfo[spawns.Key] = new BNpcZoneInfo(
                zones,
                zones.Select(c => c.Base.PlaceName.ValueNullable?.Name.ToImGuiString() ?? "未知").Distinct().OrderBy(c => c).ToList(),
                zoneTypes,
                zones.Select(c => c.Base.ExVersion.ValueNullable?.Name.ToImGuiString() ?? "未知").Distinct().ToList(),
                zoneTypes.Contains(OpenWorldZoneType));
        }

        return zoneInfo;
    }

    private Dictionary<uint, List<uint>> BuildMobsByTerritory()
    {
        var mobsByTerritory = new Dictionary<uint, List<uint>>();
        foreach (var (bNpcNameId, zoneInfo) in _zoneInfo.Value)
        {
            foreach (var zone in zoneInfo.Zones)
            {
                if (!mobsByTerritory.TryGetValue(zone.RowId, out var mobs))
                {
                    mobs = [];
                    mobsByTerritory[zone.RowId] = mobs;
                }

                mobs.Add(bNpcNameId);
            }
        }

        return mobsByTerritory;
    }
}
