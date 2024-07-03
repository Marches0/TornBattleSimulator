using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackSecondaryAction : AttackWeaponAction, IAction
{
    public AttackSecondaryAction(
        IDamageCalculator damageCalculator) : base(damageCalculator)
    { }

    public void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        PerformAction(context, active, other, active.Secondary!);
    }
}