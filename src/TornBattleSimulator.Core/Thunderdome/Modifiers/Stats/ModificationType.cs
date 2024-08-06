namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

/// <summary>
///  How a modifier is applied to its target.
/// </summary>
public enum ModificationType
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