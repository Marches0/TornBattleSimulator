using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;

public class HardenedModifier : IStatsModifier, IHealthModifier
{
    /// <inheritdoc/>
    public double GetDefenceModifier() => 3;

    /// <inheritdoc/>
    public double GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public double GetSpeedModifier() => 1;

    /// <inheritdoc/>
    public double GetStrengthModifier() => 1;

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => (int)(target.Health.MaxHealth * 0.25);

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Hardened;

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    /// <inheritdoc/>
    public bool AppliesOnActivation { get; } = true;
}