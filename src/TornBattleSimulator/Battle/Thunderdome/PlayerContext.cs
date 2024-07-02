using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Thunderdome.Stats.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome;

/// <summary>
///  A build taking part in a battle.
/// </summary>
public class PlayerContext
{
    public PlayerContext(BattleBuild build)
    {
        Build = build;
    }

    /// <summary>
    ///  The build taking part.
    /// </summary>
    public BattleBuild Build { get; }

    /// <summary>
    ///  Stat modifiers currently applied to the build.
    /// </summary>
    public List<IStatsModifier> StatModifiers { get; set; } = new List<IStatsModifier>();

    /// <summary>
    ///  The build's current stats.
    /// </summary>
    /// <returns></returns>
    public BattleStats GetStats()
    {
        var baseStats = new BattleStats()
        {
            Strength = Build.BattleStats.Strength,
            Defence = Build.BattleStats.Defence,
            Speed = Build.BattleStats.Speed,
            Dexterity = Build.BattleStats.Dexterity,
        };

        return StatModifiers
            .Aggregate(baseStats, (stats, modifier) => stats.Apply(modifier));
    }
}