using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Extensions;

public static class BattleStatsExtensions
{
    public static BattleStats WithModifiers(this BattleStats stats, List<IStatsModifier> modifiers)
    {
        return modifiers
            .Where(m => m.Type == StatModificationType.Multiplicative)
            .Aggregate(ApplyAdditive(stats.Copy(), modifiers), (stats, modifier) => stats.Apply(modifier));
    }

    private static BattleStats ApplyAdditive(BattleStats stats, List<IStatsModifier> modifiers)
    {
        double additiveStrength = 1;
        double additiveDefence = 1;
        double additiveSpeed = 1;
        double additiveDexterity = 1;

        foreach (IStatsModifier additive in modifiers.Where(m => m.Type == StatModificationType.Additive))
        {
            additiveStrength += additive.GetStrengthModifier() - 1;
            additiveDefence += additive.GetDefenceModifier() - 1;
            additiveSpeed += additive.GetSpeedModifier() - 1;
            additiveDexterity += additive.GetDexterityModifier() - 1;
        }

        stats.Strength = (ulong) Math.Max(stats.Strength * additiveStrength, 0);
        stats.Defence = (ulong) Math.Max(stats.Defence * additiveDefence, 0);
        stats.Speed = (ulong) Math.Max(stats.Speed * additiveSpeed, 0);
        stats.Dexterity = (ulong)Math.Max(stats.Dexterity * additiveDexterity, 0);

        return stats;
    }
}