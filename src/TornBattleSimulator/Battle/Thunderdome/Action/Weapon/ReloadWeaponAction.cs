using TornBattleSimulator.Battle.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public abstract class ReloadWeaponAction
{
    protected void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        weapon.Ammo.MagazineAmmoRemaining = weapon.Ammo.MagazineSize;
        --weapon.Ammo.MagazinesRemaining;
    }
}