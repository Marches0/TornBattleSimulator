using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackMeleeAction : AttackWeaponAction, IAction
{
    public AttackMeleeAction(
        IDamageCalculator damageCalculator) : base(damageCalculator)
    {

    }

    public void PerformAction(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        PerformAction(context, active, other, active.Melee!);
    }
}