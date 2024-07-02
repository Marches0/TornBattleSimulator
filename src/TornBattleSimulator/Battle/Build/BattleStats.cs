using TornBattleSimulator.Battle.Thunderdome.Stats.Modifiers;

namespace TornBattleSimulator.Battle.Build;

public class BattleStats
{
    public ulong Strength { get; set; }

    public ulong Defence { get; set; }

    public ulong Speed { get; set; }

    public ulong Dexterity { get; set; }

    public BattleStats Apply(IStatsModifier modifier)
    {
        Strength *= (ulong)modifier.GetStrengthModifier();
        Defence *= (ulong)modifier.GetDefenceModifier();
        Speed *= (ulong)modifier.GetSpeedModifier();
        Dexterity *= (ulong)modifier.GetDexterityModifier();

        return this;
    }
}