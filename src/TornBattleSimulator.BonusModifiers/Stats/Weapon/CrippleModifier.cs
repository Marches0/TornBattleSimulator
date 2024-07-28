﻿using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class CrippleModifier : IStackableStatModifier
{
    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Temporal(180);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Cripple;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;

    /// <inheritdoc/>
    public int MaxStacks => 3;

    /// <inheritdoc/>
    public float GetDefenceModifier() => 1;

    /// <inheritdoc/>
    public float GetDexterityModifier() => 0.75f;

    /// <inheritdoc/>
    public float GetSpeedModifier() => 1;

    /// <inheritdoc/>
    public float GetStrengthModifier() => 1;
}