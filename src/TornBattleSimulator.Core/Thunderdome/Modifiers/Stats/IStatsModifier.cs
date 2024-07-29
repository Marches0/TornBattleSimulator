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
    double GetStrengthModifier();

    /// <summary>
    ///  The Defence modifier.
    /// </summary>
    double GetDefenceModifier();

    /// <summary>
    ///  The Speed modifier.
    /// </summary>
    double GetSpeedModifier();

    /// <summary>
    ///  The Dexterity modifier.
    /// </summary>
    double GetDexterityModifier();

    /// <summary>
    /// How this modifier is applied to the target's stats.
    /// </summary>
    StatModificationType Type { get; }
}