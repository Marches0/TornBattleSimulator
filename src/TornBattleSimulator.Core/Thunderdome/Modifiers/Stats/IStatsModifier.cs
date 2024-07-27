namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

/// <summary>
///  A modifier that changes a target's stats.
/// </summary>
public interface IStatsModifier : IModifier
{
    // put these into their own thing?

    /// <summary>
    ///  The Strength modifier.
    /// </summary>
    float GetStrengthModifier();

    /// <summary>
    ///  The Defence modifier.
    /// </summary>
    float GetDefenceModifier();

    /// <summary>
    ///  The Speed modifier.
    /// </summary>
    float GetSpeedModifier();

    /// <summary>
    ///  The Dexterity modifier.
    /// </summary>
    float GetDexterityModifier();

    /// <summary>
    /// How this modifier is applied to the target's stats.
    /// </summary>
    StatModificationType Type { get; }
}