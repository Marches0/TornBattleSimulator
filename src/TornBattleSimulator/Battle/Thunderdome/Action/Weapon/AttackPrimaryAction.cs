using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackPrimaryAction : AttackWeaponAction, IAction
{
    public AttackPrimaryAction(
        IDamageCalculator damageCalculator) : base(damageCalculator)
    {
    }

    public ThunderdomeEvent PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        return PerformAction(context, active, other, active.Weapons.Primary!);
    }
}