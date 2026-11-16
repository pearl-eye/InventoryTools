using AllaganLib.Shared.Extensions;
using Lumina.Excel.Sheets;

namespace InventoryTools.Localizers;

public class ContentTypeLocalizer : ILocalizer<ContentType>
{
    public string Format(ContentType instance)
    {
        return instance.RowId switch
        {
            20 => "新人学堂",
            22 => "季节活动",
            23 => "云冠群岛",
            39 => "The Final Verse",
            _ => instance.Name.ToImGuiString()
        };
    }
}
