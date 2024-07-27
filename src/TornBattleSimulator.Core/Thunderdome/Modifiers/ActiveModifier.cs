using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  A modifier which is currently in effect.
/// </summary>
public class ActiveModifier
{
    public ActiveModifier(IModifierLifespan currentLifespan, IModifier modifier)
    {
        CurrentLifespan = currentLifespan;
        Modifier = modifier;
    }

    /// <summary>
    ///  How much longer <see cref="Modifier"/> will be in effect for.
    /// </summary>
    public IModifierLifespan CurrentLifespan { get; }

    /// <summary>
    ///  The modifier in effect.
    /// </summary>
    public IModifier Modifier { get; }
}