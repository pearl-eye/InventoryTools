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
            // ContentType 39 has an empty name; use the shared title of duties 60051/60052.
            39 => "卓异的悲寂",
            _ => instance.Name.ToImGuiString()
        };
    }
}
