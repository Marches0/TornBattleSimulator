﻿using TornBattleSimulator.Shared.Build.Equipment;
using TornBattleSimulator.Shared.Thunderdome.Modifiers;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Shared.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary;

public class BlindedModifier : IStatsModifier
{
    public float GetDefenceModifier() => 1;

    public float GetDexterityModifier() => 1;

    public float GetSpeedModifier() => 1 / 5f;

    public float GetStrengthModifier() => 1;

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(15);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Other;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    public ModifierType Effect => ModifierType.Blinded;

    public StatModificationType Type => StatModificationType.Multiplicative;
}