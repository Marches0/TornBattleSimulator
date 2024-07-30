using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Extensions;

public static class ModifierExtensions
{
    public static IModifierLifespan CreateLifespan(this IModifier modifier)
    {
        return modifier.Lifespan.LifespanType switch
        {
            ModifierLifespanType.Temporal => new TemporalModifierLifespan(modifier.Lifespan.Duration!.Value),
            ModifierLifespanType.Turns => new TurnModifierLifespan(modifier.Lifespan.TurnCount!.Value),
            ModifierLifespanType.AfterOwnAction => new OwnActionLifespan(),
            ModifierLifespanType.AfterNextEnemyAction => new NextEnemyActionLifespan(),
            ModifierLifespanType.Indefinite => new IndefiniteLifespan(),
            _ => throw new NotImplementedException($"{modifier.Lifespan.LifespanType} not supported.")
        };
    }
}