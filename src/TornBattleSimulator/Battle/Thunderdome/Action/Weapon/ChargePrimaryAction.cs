using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ChargePrimaryAction : ChargeWeaponAction, IAction
{
    public List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        return Charge(
            context,
            active,
            other,
            active.Weapons.Primary!
        );
    }
}