using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponContext
{
    public WeaponContext(
        Weapon weapon,
        WeaponType weaponType,
        List<PotentialModifier> modifiers)
    {
        Description = weapon;
        Type = weaponType;
        Ammo = weapon.Ammo != null ? new CurrentAmmo()
        {
            Magazines = weapon.Ammo.Magazines,
            MagazinesRemaining = weapon.Ammo.Magazines,

            MagazineSize = weapon.Ammo.MagazineSize,
            MagazineAmmoRemaining = weapon.Ammo.MagazineSize
        } : null;
        Modifiers = modifiers;
    }

    public Weapon Description { get; }
    public WeaponType Type { get; }
    public CurrentAmmo? Ammo { get; }
    public bool CanReload => Ammo?.MagazinesRemaining > 0;
    public bool RequiresReload => Ammo?.MagazineAmmoRemaining == 0;
    public List<PotentialModifier> Modifiers { get; }
}