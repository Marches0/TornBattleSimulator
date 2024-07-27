using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;

public class StrengthenedModifier : IStatsModifier
{
    /// <inheritdoc/>
    public float GetDefenceModifier() => 1;

    /// <inheritdoc/>
    public float GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public float GetSpeedModifier() => 1;

    /// <inheritdoc/>
    public float GetStrengthModifier() => 5;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Strengthened;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}