namespace TornBattleSimulator.Core.Thunderdome.Modifiers.DamageOverTime;

/// <summary>
///  A modifier which applies damage over time.
/// </summary>
public interface IDamageOverTimeModifier : IModifier
{
    /// <summary>
    ///  The rate at which the damage applied decays.
    /// </summary>
    double Decay { get; }
}