using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackMeleeAction : AttackWeaponAction, IAction
{
    public AttackMeleeAction(
        IDamageCalculator damageCalculator) : base(damageCalculator)
    {

    }

    public ThunderdomeEvent PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return PerformAction(context, active, other, active.Melee!);
    }
}