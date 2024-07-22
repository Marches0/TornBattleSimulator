using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class ChargeSecondaryAction : ChargeWeaponAction, IAction
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
            active.Weapons.Secondary!
        );
    }
}