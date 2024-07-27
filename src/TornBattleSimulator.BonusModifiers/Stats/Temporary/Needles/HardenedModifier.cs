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
    public float GetDefenceModifier() => 3;

    /// <inheritdoc/>
    public float GetDexterityModifier() => 1;

    /// <inheritdoc/>
    public float GetSpeedModifier() => 1;

    /// <inheritdoc/>
    public float GetStrengthModifier() => 1;

    /// <inheritdoc/>
    public int GetHealthMod(PlayerContext target, DamageResult? damage) => (int)(target.Health.MaxHealth * 0.25);

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Temporal(120);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Hardened;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Additive;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Chance;
}