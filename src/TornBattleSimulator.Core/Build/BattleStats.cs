﻿using TornBattleSimulator.Core.Extensions;
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
        if (modifier.Type != StatModificationType.Multiplicative)
        {
            throw new InvalidOperationException($"{nameof(Apply)} does not support {modifier.Type} modifiers.");
        }

        Strength = (ulong)(Strength * modifier.GetStrengthModifier());
        Defence = (ulong)(Defence * modifier.GetDefenceModifier());
        Speed = (ulong)(Speed * modifier.GetSpeedModifier());
        Dexterity = (ulong)(Dexterity * modifier.GetDexterityModifier());

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