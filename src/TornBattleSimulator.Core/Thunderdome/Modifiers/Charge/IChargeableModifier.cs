namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Charge;

/// <summary>
///  A modifier which requires charging to be used.
/// </summary>
public interface IChargeableModifier : IModifier
{
    /// <summary>
    ///  Whether the modifier starts charged or discharged.
    /// </summary>
    bool StartsCharged { get; }
}