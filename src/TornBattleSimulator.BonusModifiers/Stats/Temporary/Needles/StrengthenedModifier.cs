using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;

public class StrengthenedModifier : IStatsModifier
{
    /// <inheritdoc/>
    public double GetDefenceModifier() => 1;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 5;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Strengthened;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;
}