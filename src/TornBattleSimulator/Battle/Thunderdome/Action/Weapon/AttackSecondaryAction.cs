using TornBattleSimulator.Battle.Thunderdome.Damage;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon;

public class AttackSecondaryAction : AttackWeaponAction, IAction
{
    public AttackSecondaryAction(
        IDamageCalculator damageCalculator,
        ModifierApplier modifierApplier) : base(damageCalculator, modifierApplier)
    { 
    }

    public List<ThunderdomeEvent> PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        return PerformAction(context, active, other, active.Weapons.Secondary!);
    }
}