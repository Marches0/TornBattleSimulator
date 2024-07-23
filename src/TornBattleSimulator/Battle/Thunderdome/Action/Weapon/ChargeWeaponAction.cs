using TornBattleSimulator.Shared.Extensions;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Events;
using TornBattleSimulator.Shared.Thunderdome.Events.Data;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

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