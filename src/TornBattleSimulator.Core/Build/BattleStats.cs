using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.Core.Build;

public class BattleStats
{
    public ulong Strength { get; set; }

    public ulong Defence { get; set; }

    public ulong Speed { get; set; }

    public ulong Dexterity { get; set; }

    public BattleStats Apply(IStatsModifier modifier, IStatsModifierModifier? statsModifierModifier)
    {
        if (modifier.Type != ModificationType.Multiplicative)
        {
            throw new InvalidOperationException($"{nameof(Apply)} does not support {modifier.Type} modifiers.");
        }

        Strength = (ulong)Math.Max(Strength * GetModifier(m => m.GetStrengthModifier(), modifier, statsModifierModifier), 0);
        Defence = (ulong)Math.Max(Defence * GetModifier(m => m.GetDefenceModifier(), modifier, statsModifierModifier), 0);
        Speed = (ulong)Math.Max(Speed * GetModifier(m => m.GetSpeedModifier(), modifier, statsModifierModifier), 0);
        Dexterity = (ulong)Math.Max(Dexterity * GetModifier(m => m.GetDexterityModifier(), modifier, statsModifierModifier), 0);

        return this;
    }

    private double GetModifier(Func<IStatsModifier, double> modifierGetter, IStatsModifier modifier, IStatsModifierModifier? statsModifierModifier)
    {
        double modifierValue = modifierGetter(modifier);
        
        // Invert to find out the raw amount it decreases it by, apply the modifier modifier, then invert
        // again to get the used multiplier.
        return statsModifierModifier != null && modifierValue < 1
            ? 1 - ((1 - modifierValue) * statsModifierModifier.StatsModifierModifier)
            : modifierValue;
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