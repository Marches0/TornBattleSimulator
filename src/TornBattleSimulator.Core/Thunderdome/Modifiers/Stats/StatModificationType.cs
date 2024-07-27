namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

/// <summary>
///  How a stat modifier is applied to the target's stats.
/// </summary>
public enum StatModificationType
{
    /// <summary>
    ///  Applies an additive bonus.
    /// </summary>
    Additive = 1,

    /// <summary>
    ///  Applies a multiplicative bonus.
    /// </summary>
    Multiplicative
}