using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponsFactory
{
    private static readonly List<IModifier> NoModifiers = new List<IModifier>(0);

    public WeaponsFactory()
    {

    }

    public EquippedWeapons Create(BattleBuild build)
    {
        return new EquippedWeapons(
             build.Primary != null ? new WeaponContext(build.Primary, WeaponType.Primary, NoModifiers) : null,
             build.Secondary != null ? new WeaponContext(build.Secondary, WeaponType.Secondary, NoModifiers) : null,
             build.Melee != null ? new WeaponContext(build.Melee, WeaponType.Melee, NoModifiers) : null,
             build.Temporary != null ? new WeaponContext(GetTemporaryWeapon(build.Temporary.Value), WeaponType.Temporary, NoModifiers) : null
        );
    }

    private Weapon GetTemporaryWeapon(TemporaryWeaponType type)
    {
        return new Weapon()
        {
            Accuracy = 200,
            Damage = 0,
            Ammo = new Ammo()
            {
                Magazines = 0,
                MagazineSize = 1
            },
            RateOfFire = new RateOfFire()
            {
                Min = 1,
                Max = 1
            }
        };
    }
}