using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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