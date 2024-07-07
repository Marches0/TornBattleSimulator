using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Battle.Build;

public class BattleStats
{
    public ulong Strength { get; set; }

    public ulong Defence { get; set; }

    public ulong Speed { get; set; }

    public ulong Dexterity { get; set; }

    public BattleStats Apply(IStatsModifier modifier)
    {
        Strength = (ulong)(Strength * modifier.GetStrengthModifier());
        Defence = (ulong)(Defence * modifier.GetDefenceModifier());
        Speed = (ulong)(Speed * modifier.GetSpeedModifier());
        Dexterity = (ulong)(Dexterity * modifier.GetDexterityModifier());

        return this;
    }
}