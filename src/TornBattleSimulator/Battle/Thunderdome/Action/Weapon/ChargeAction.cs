using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Extensions;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ChargeAction : IAction
{
    public List<ThunderdomeEvent> PerformAction(AttackContext attack)
    {
        foreach (ChargedModifierContainer charge in attack.Weapon.Modifiers.ChargeModifiers)
        {
            charge.Charged = true;
        }

        return [attack.Context.CreateEvent(attack.Active, ThunderdomeEventType.ChargeWeapon, new WeaponChargeData(attack.Weapon.Type))];
    }
}