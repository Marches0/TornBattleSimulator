﻿using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary.Needles;

public class SharpenedModifier : IStatsModifier
{
    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => 1;

    public float GetSpeedModifier() => 1;

    public float GetStrengthModifier() => 6;

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Self;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Sharpened;

    public StatModificationType Type => StatModificationType.Additive;
}