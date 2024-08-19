using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ReplenishTemporaryAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        if (attack.Active.Weapons.Temporary != null)
        {
            attack.Active.Weapons.Temporary.Ammo!.MagazineAmmoRemaining = 1;
        }

        StorageModifier storage = attack.Weapon.Modifiers.Active.OfType<StorageModifier>().First();
        storage.Consumed = true;

        return [ attack.Context.CreateEvent(attack.Active, ThunderdomeEventType.ReplenishTemporary, new ReplenishedTemporaryData()) ];
    }
}