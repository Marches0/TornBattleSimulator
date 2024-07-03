using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackPrimaryAction : AttackWeaponAction, IAction
{
    public AttackPrimaryAction(
        IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    }

    public void PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        PerformAction(context, active, other, active.Primary!);
    }
}