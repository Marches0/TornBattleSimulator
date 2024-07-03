using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponContext
{
    public WeaponContext(
        Weapon weapon,
        WeaponType weaponType)
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
    }

    public Weapon Description { get; }
    public WeaponType Type { get; }
    public CurrentAmmo? Ammo { get; }
    public bool CanReload => Ammo?.MagazinesRemaining > 0;
    public bool RequiresReload => Ammo?.MagazineAmmoRemaining == 0;
}