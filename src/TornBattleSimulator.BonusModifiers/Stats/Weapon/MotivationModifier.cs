using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stackable;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Weapon;

public class MotivationModifier : IModifier, IStatsModifier, IStackableStatModifier
{
    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(180);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = true;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Motivation;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Additive;

    /// <inheritdoc/>
    public int MaxStacks { get; } = 5;

    /// <inheritdoc/>
    public double GetDefenceModifier() => 1.1;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1.1;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1.1;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 1.1;
}