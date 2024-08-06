using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Build;

public class BattleStats
{
    public ulong Strength { get; set; }

    public ulong Defence { get; set; }

    public ulong Speed { get; set; }

    public ulong Dexterity { get; set; }

    public BattleStats Apply(IStatsModifier modifier)
    {
        if (modifier.Type != ModificationType.Multiplicative)
        {
            throw new InvalidOperationException($"{nameof(Apply)} does not support {modifier.Type} modifiers.");
        }

        Strength = (ulong) Math.Max(Strength * modifier.GetStrengthModifier(), 0);
        Defence = (ulong) Math.Max(Defence * modifier.GetDefenceModifier(), 0);
        Speed = (ulong) Math.Max(Speed * modifier.GetSpeedModifier(), 0);
        Dexterity = (ulong)Math.Max(Dexterity * modifier.GetDexterityModifier(), 0);

        return this;
    }

    public BattleStats Copy()
    {
        return new BattleStats()
        {
            Strength = Strength,
            Defence = Defence,
            Speed = Speed,
            Dexterity = Dexterity,
        };
    }

    public override string ToString()
    {
        return $"{Strength.ToSimpleString()} / {Defence.ToSimpleString()} / {Speed.ToSimpleString()} / {Dexterity.ToSimpleString()}";
    }
}