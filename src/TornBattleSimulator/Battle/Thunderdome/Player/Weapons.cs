using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player;

public class Weapons
{
    public Weapons(BattleBuild build)
    {
        Primary = build.Primary != null ? new WeaponContext(build.Primary, WeaponType.Primary) : null;
        Secondary = build.Secondary != null ? new WeaponContext(build.Secondary, WeaponType.Secondary) : null;
        Melee = build.Melee != null ? new WeaponContext(build.Melee, WeaponType.Melee) : null;
    }

    public WeaponContext? Primary { get; }
    public WeaponContext? Secondary { get; }
    public WeaponContext? Melee { get; }
    //public WeaponContext? Temp
}