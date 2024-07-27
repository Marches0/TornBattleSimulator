namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

/// <summary>
///  A modifier which has a random chance of being applied.
/// </summary>
public class PotentialModifier
{
    public PotentialModifier(
        IModifier modifier,
        double chance)
    {
        Modifier = modifier;
        Chance = chance;
    }

    /// <summary>
    ///  The modifier to apply.
    /// </summary>
    public IModifier Modifier { get; }

    /// <summary>
    ///  The likelihood of the modifier being applied.
    /// </summary>
    public double Chance { get; }
}