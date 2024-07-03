using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player;
public class WeaponsFactory
{
    public WeaponsFactory()
    {

    }

    public Weapons Create(BattleBuild build)
    {
        return new Weapons(
             build.Primary != null ? new WeaponContext(build.Primary, WeaponType.Primary) : null,
             build.Secondary != null ? new WeaponContext(build.Secondary, WeaponType.Secondary) : null,
             build.Melee != null ? new WeaponContext(build.Melee, WeaponType.Melee) : null,
             null
        );
    }
}