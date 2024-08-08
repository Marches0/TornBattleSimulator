using TornBattleSimulator.BonusModifiers.Target;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Target;

public class TargetSelector
{
    public (PlayerContext target, IModifierContext modifierTarget) GetModifierTarget(
        IModifier modifier,
        AttackContext attack)
    {
        PlayerContext target = modifier.Target == ModifierTarget.Self || modifier.Target == ModifierTarget.SelfWeapon
                ? attack.Active
                : attack.Other;

        if (IsDeflected(modifier, attack, target))
        {
            target = attack.Active;
        }

        IModifierContext contextTarget;
        if (modifier.Target == ModifierTarget.OtherWeapon)
        {
            // target is Other here, so take their weapon.
            contextTarget = target.ActiveWeapon!.Modifiers;
        }
        else
        {
            contextTarget = modifier.Target == ModifierTarget.SelfWeapon
                ? attack.Weapon.Modifiers
                : target.Modifiers;
        }
        
        return (target, contextTarget);
    }

    private bool IsDeflected(
        IModifier modifier,
        AttackContext attack,
        PlayerContext target)
    {
        if (attack.Weapon == null)
        {
            // some hack for so armour application can use this
            return false;
        }

        // Temp weapons are deflected if the target has HomeRun
        return attack.Weapon.Type == WeaponType.Temporary
            && modifier.Target == ModifierTarget.Other // Only ones that effect the other player - can't deflect needles
            && target.Modifiers.Active.OfType<HomeRunModifier>().Any();
    }
}