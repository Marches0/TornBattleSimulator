using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class DemoralizedModifier : IStackableStatModifier
{
    /// <inheritdoc/>
    public int MaxStacks { get; } = 5;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(300);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = true;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Demoralized;

    /// <inheritdoc/>
    public double GetDefenceModifier() => 0.9;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 0.9;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 0.9;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 0.9;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}