using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class DemoralizedModifier : IStackableStatModifier
{
    /// <inheritdoc/>
    public int MaxStacks => 5;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Temporal(180); // I think

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Demoralized;

    /// <inheritdoc/>
    public float GetDefenceModifier() => 0.9f;

    /// <inheritdoc/>
    public float GetDexterityModifier() => 0.9f;

    /// <inheritdoc/>
    public float GetSpeedModifier() => 0.9f;

    /// <inheritdoc/>
    public float GetStrengthModifier() => 0.9f;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}