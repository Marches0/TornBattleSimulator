using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary;

public class BlindedModifier : IStatsModifier
{
    /// <inheritdoc/>
    public double GetDefenceModifier() => 1;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1 / 5d;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 1;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(15);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Blinded;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}