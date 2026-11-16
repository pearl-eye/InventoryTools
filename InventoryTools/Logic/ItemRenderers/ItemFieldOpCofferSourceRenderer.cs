using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Interface.Textures;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;
using LuminaSupplemental.Excel.Model;

namespace InventoryTools.Logic.ItemRenderers;

public abstract class ItemFieldOpCofferSourceRenderer<T> : ItemInfoRenderer<T> where T : ItemFieldOpCofferSource
{
    private readonly ItemInfoType _itemInfoType;

    public ItemFieldOpCofferSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface, ItemInfoType itemInfoType) : base(textureProvider, pluginInterface, itemSheet, mapSheet)
    {
        _itemInfoType = itemInfoType;
    }

    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => _itemInfoType;
    public override bool ShouldGroup => true;
    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        ImGui.Text("掉落自" + GetCofferTypeName(asSource.CofferType) + "宝箱");
        if (asSource.Min != null && asSource.Max != null)
        {
            ImGui.SameLine();
            if (asSource.Min == asSource.Max)
            {
                ImGui.Text("（掉落1个）");
            }
            else
            {
                ImGui.Text("（掉落" + asSource.Min.Value + " - " + asSource.Max.Value + "）");
            }
        }

        if (asSource.Probability != null)
        {
            ImGui.SameLine();
            ImGui.TextUnformatted($"{asSource.Probability.Value}%");
        }
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.GoldChest2;

    public override Func<ItemSource, string> GetName => _ => "";

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        return GetCofferTypeName(asSource.CofferType) + "宝箱";
    };

    private static string GetCofferTypeName(FieldOpCofferType type) => type switch
    {
        FieldOpCofferType.Bronze => "铜",
        FieldOpCofferType.Silver => "银",
        FieldOpCofferType.Gold => "金",
        _ => type.ToString(),
    };
}

public class ItemPagosTreasureSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemPagosTreasureCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pagos];

    public ItemPagosTreasureSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.PagosTreasure)
    {
    }

    public override string SingularName => "优雷卡涌冰之地（宝箱）";
    public override string HelpText => "此物品是否由涌冰之地的宝箱掉落？";
}

public class ItemPyrosTreasureSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemPyrosTreasureCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pyros];

    public ItemPyrosTreasureSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.PyrosTreasure)
    {
    }

    public override string SingularName => "优雷卡火焰之地（宝箱）";
    public override string HelpText => "此物品是否由火焰之地的宝箱掉落？";
}

public class ItemHydatosTreasureSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemHydatosTreasureCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Hydatos];

    public ItemHydatosTreasureSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.HydatosTreasure)
    {
    }

    public override string SingularName => "优雷卡丰水之地（宝箱）";
    public override string HelpText => "此物品是否由丰水之地的宝箱掉落？";
}

public class ItemOccultTreasureSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultTreasureCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultTreasureSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultTreasure)
    {
    }

    public override string SingularName => "新月岛南部（宝箱）";
    public override string HelpText => "此物品是否由新月岛南部的宝箱掉落？";
}

public class ItemOccultPotSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultPotSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultPotSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultPot)
    {
    }

    public override string SingularName => "新月岛南部（魔壶）";
    public override string HelpText => "此物品是否由新月岛南部的魔壶掉落？";
}

public class ItemOccultGoldenCofferSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultGoldenCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultGoldenCofferSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultGoldenCoffer)
    {
    }

    public override string SingularName => "新月岛南部（金宝箱）";
    public override string HelpText => "此物品是否由新月岛南部的金宝箱掉落？";
}

public class ItemOccultNorthHornTreasureSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultNorthHornTreasureCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultNorthHornTreasureSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultNorthHornTreasure)
    {
    }

    public override string SingularName => "新月岛北部（宝箱）";
    public override string HelpText => "此物品是否由新月岛北部的宝箱掉落？";
}

public class ItemOccultNorthHornPotSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultNorthHornPotSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultNorthHornPotSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultNorthHornPot)
    {
    }

    public override string SingularName => "新月岛北部（魔壶）";
    public override string HelpText => "此物品是否由新月岛北部的魔壶掉落？";
}

public class ItemOccultNorthHornGoldenCofferSourceRenderer : ItemFieldOpCofferSourceRenderer<ItemOccultNorthHornGoldenCofferSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.OccultCrescent];

    public ItemOccultNorthHornGoldenCofferSourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider, IDalamudPluginInterface pluginInterface) : base(itemSheet, mapSheet, textureProvider, pluginInterface, ItemInfoType.OccultNorthHornGoldenCoffer)
    {
    }

    public override string SingularName => "新月岛北部（金宝箱）";
    public override string HelpText => "此物品是否由新月岛北部的金宝箱掉落？";
}
