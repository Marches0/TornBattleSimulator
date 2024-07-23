using TornBattleSimulator.Battle.Thunderdome.Events;
using TornBattleSimulator.Battle.Thunderdome.Events.Data;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Extensions;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public abstract class ChargeWeaponAction
{
    public List<ThunderdomeEvent> Charge(ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        foreach (ChargedModifierContainer charge in weapon.ChargedModifiers)
        {
            charge.Charged = true;
        }

        return [ context.CreateEvent(active, ThunderdomeEventType.ChargeWeapon, new WeaponChargeData(weapon.Type)) ];
    }
}