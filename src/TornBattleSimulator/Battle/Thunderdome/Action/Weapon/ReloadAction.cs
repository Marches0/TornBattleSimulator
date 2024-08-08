using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReloadAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        attack.Weapon.Ammo.MagazineAmmoRemaining = attack.Weapon.Ammo.MagazineSize;
        --attack.Weapon.Ammo.MagazinesRemaining;
        return [attack.Context.CreateEvent(attack.Active, ThunderdomeEventType.Reload, new ReloadEvent(attack.Weapon.Type))];
    }
}