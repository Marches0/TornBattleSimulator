using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Stats;

public class InvulnerableModifier : IModifier, IStatsModifierModifier
{
    public InvulnerableModifier(double value)
    {
        StatsModifierModifier = 1 - value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Invulnerable;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public double StatsModifierModifier { get; }
}