using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Extensions;

public static class BattleStatsExtensions
{
    public static BattleStats WithModifiers(
        this BattleStats stats,
        List<IStatsModifier> modifiers,
        IStatsModifierModifier? statsModifierModifier)
    {
        return modifiers
            .Where(m => m.Type == ModificationType.Multiplicative)
            .Aggregate(ApplyAdditive(stats.Copy(), modifiers, statsModifierModifier), (stats, modifier) => stats.Apply(modifier, statsModifierModifier));
    }

    private static BattleStats ApplyAdditive(BattleStats stats, List<IStatsModifier> modifiers, IStatsModifierModifier? statsModifierModifier)
    {
        double additiveStrength = 1;
        double additiveDefence = 1;
        double additiveSpeed = 1;
        double additiveDexterity = 1;

        foreach (IStatsModifier additive in modifiers.Where(m => m.Type == ModificationType.Additive))
        {
            additiveStrength += GetAdditiveContribution(m => m.GetStrengthModifier(), additive, statsModifierModifier);
            additiveDefence += GetAdditiveContribution(m => m.GetDefenceModifier(), additive, statsModifierModifier);
            additiveSpeed += GetAdditiveContribution(m => m.GetSpeedModifier(), additive, statsModifierModifier);
            additiveDexterity += GetAdditiveContribution(m => m.GetDexterityModifier(), additive, statsModifierModifier);
        }

        stats.Strength = (ulong) Math.Max(stats.Strength * additiveStrength, 0);
        stats.Defence = (ulong) Math.Max(stats.Defence * additiveDefence, 0);
        stats.Speed = (ulong) Math.Max(stats.Speed * additiveSpeed, 0);
        stats.Dexterity = (ulong)Math.Max(stats.Dexterity * additiveDexterity, 0);

        return stats;
    }

    private static double GetAdditiveContribution(
        Func<IStatsModifier, double> modifierGetter,
        IStatsModifier statsModifier,
        IStatsModifierModifier? statsModifierModifier)
    {
        double modifier = modifierGetter(statsModifier) - 1;
        return statsModifierModifier != null && modifier < 0
            ? statsModifierModifier.StatsModifierModifier * modifier
            : modifier;
    }
}