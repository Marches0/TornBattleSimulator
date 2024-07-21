using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.Extensions;

public static class ModifierExtensions
{
    public static IModifierLifespan CreateLifespan(this IModifier modifier)
    {
        return modifier.Lifespan.LifespanType switch
        {
            ModifierLifespanType.Temporal => new TemporalModifierLifespan(modifier.Lifespan.Duration!.Value),
            ModifierLifespanType.Turns => new TurnModifierLifespan(modifier.Lifespan.TurnCount!.Value),
            ModifierLifespanType.AfterOwnAction => new OwnActionLifespan(),
            ModifierLifespanType.Indefinite => new IndefiniteLifespan(),
            _ => throw new NotImplementedException($"{modifier.Lifespan.LifespanType} not supported.")
        };
    }
}