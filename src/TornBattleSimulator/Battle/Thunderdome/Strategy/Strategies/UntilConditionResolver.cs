using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class UntilConditionResolver : IUntilConditionResolver
{
    public bool Fulfilled(AttackContext attack, StrategyDescription strategy)
    {
        // Stop when any condition is fulfilled
        return strategy.Until.Any(u => Fulfilled(attack, u));
    }

    private bool Fulfilled(AttackContext attack, StrategyUntil condition)
    {
        // Fulfilled if the modifier has been applied to the correct target.
        // For self - check on ourself.
        // For other - check on them.
        // Have to retrieve the modifiers first, so we can check against the
        // target on the instance.
        var selfFulfilled = attack.Active.Modifiers.Active.Concat(attack.Weapon.Modifiers.Active)
            .Where(m => m.Effect == condition.Effect 
                     && m.Target is ModifierTarget.Self or ModifierTarget.SelfWeapon);

        var otherFulfilled = attack.Other.Modifiers.Active.Concat(attack.Other.ActiveWeapon?.Modifiers.Active ?? Enumerable.Empty<IModifier>())
            .Where(m => m.Effect == condition.Effect
                     && m.Target is ModifierTarget.Other or ModifierTarget.OtherWeapon);

        return selfFulfilled.Any() || otherFulfilled.Any();
    }
}