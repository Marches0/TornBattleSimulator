using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary;

public class SmokedModifier : IStatsModifier
{
    /// <inheritdoc/>
    public double GetDefenceModifier() => 1;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1 / 3d;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 1;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Smoked;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}