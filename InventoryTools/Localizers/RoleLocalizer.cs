using AllaganLib.GameSheets.Sheets.Rows;

namespace InventoryTools.Localizers;

public class RoleLocalizer : ILocalizer<RoleType>
{
    public string Format(RoleType instance)
    {
        switch (instance)
        {
            case RoleType.Tank:
                return "防护职业";
            case RoleType.DPSMelee:
                return "近战进攻职业";
            case RoleType.DPSRanged:
                return "远程进攻职业";
            case RoleType.Healer:
                return "治疗职业";
            case RoleType.Crafting:
                return "制作";
            case RoleType.Gathering:
                return "采集";
            case RoleType.Other:
                return "其他";
        }

        return "未知";
    }
}