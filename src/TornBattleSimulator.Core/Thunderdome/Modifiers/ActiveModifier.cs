using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

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