using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

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