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
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Temporal(300);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Demoralized;

    /// <inheritdoc/>
    public double GetDefenceModifier() => 0.9;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 0.9;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 0.9;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 0.9;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}