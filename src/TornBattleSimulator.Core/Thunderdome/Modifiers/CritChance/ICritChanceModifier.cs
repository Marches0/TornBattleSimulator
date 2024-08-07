namespace TornBattleSimulator.Core.Thunderdome.Modifiers.CritChance;

/// <summary>
///  Modifies the likelihood of scoring a critical hit.
/// </summary>
public interface ICritChanceModifier
{
    /// <summary>
    ///  Gets the crit chance modifier provided by this modifier.
    /// </summary>
    double GetCritChanceModifier();
}