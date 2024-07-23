using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Shared.Thunderdome.Modifiers;

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