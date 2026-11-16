using System;
using System.Collections.Generic;
using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using CriticalCommonLib.Models;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Dalamud.Bindings.ImGui;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemBuddySourceRenderer : ItemInfoRenderer<ItemBuddySource>
{
    public ItemBuddySourceRenderer(ItemSheet itemSheet, MapSheet mapSheet, ITextureProvider textureProvider,
        IDalamudPluginInterface dalamudPluginInterface) : base(textureProvider, dalamudPluginInterface, itemSheet, mapSheet)
    {
    }

    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.BuddyItem;
    public override string SingularName => "陆行鸟搭档用品";
    public override bool ShouldGroup => false;
    public override string HelpText => "此物品能否用于陆行鸟搭档？";

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;

        if (usedField)
        {
            ImGui.Text("战斗：增加陆行鸟搭档获得的经验值。");
        }

        if (usedTraining)
        {
            ImGui.Text("鸟房：用于训练寄养在鸟房中的陆行鸟搭档。");
        }

        if (usedDyeing)
        {
            ImGui.Text("染色：用于改变陆行鸟的羽毛颜色。");
        }
    };
    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;
        var name = new List<string>();


        if (usedField)
        {
            name.Add("战斗");
        }

        if (usedTraining)
        {
            name.Add("训练");
        }

        if (usedDyeing)
        {
            name.Add("染色");
        }

        return "陆行鸟" + string.Join(", ", name);
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.ChocoboIcon;

    public override Func<ItemSource, string> GetDescription => source =>
    {
        var asSource = AsSource(source);
        var usedField = asSource.BuddyItem.Value.UseField;
        var usedTraining = asSource.BuddyItem.Value.UseTraining;
        var usedDyeing = asSource.BuddyItem.Value.Unknown0;
        var name = new List<string>();

        if (usedField)
        {
            name.Add("战斗");
        }

        if (usedTraining)
        {
            name.Add("训练");
        }

        if (usedDyeing)
        {
            name.Add("染色");
        }

        return "用于" + string.Join(", ", name);
    };
}