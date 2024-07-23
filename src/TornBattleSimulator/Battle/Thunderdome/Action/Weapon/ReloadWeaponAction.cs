using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

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