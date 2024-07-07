using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public abstract class ReloadWeaponAction
{
    protected List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        weapon.Ammo.MagazineAmmoRemaining = weapon.Ammo.MagazineSize;
        --weapon.Ammo.MagazinesRemaining;
        return [ context.CreateEvent(active, ThunderdomeEventType.Reload, new ReloadEvent(weapon.Type)) ];
    }
}