using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Extensions;

public static class BattleStatsExtensions
{
    public static BattleStats WithModifiers(this BattleStats stats, List<IStatsModifier> modifiers)
    {
        return modifiers
            .Where(m => m.Type == StatModificationType.Multiplicative)
            .Aggregate(ApplyAdditive(stats.Copy(), modifiers), (stats, modifier) => stats.Apply(modifier)); ;
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

        stats.Strength = (ulong)(stats.Strength * additiveStrength);
        stats.Defence = (ulong)(stats.Defence * additiveDefence);
        stats.Speed = (ulong)(stats.Speed * additiveSpeed);
        stats.Dexterity = (ulong)(stats.Dexterity * additiveDexterity);

        return stats;
    }
}