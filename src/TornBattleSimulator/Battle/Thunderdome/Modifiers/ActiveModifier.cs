using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ActiveModifier
{
    public ActiveModifier(IModifierLifespan currentLifespan, IModifier modifier)
    {
        CurrentLifespan = currentLifespan;
        Modifier = modifier;
    }

    public IModifierLifespan CurrentLifespan { get; }

    public IModifier Modifier { get; }
}