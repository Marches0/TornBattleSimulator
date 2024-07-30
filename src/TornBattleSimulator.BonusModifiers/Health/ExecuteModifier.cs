using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.BonusModifiers.Health;

public class ExecuteModifier : IHealthModifier, IConditionalModifier
{
    private readonly double _value;

    public ExecuteModifier(double value)
    {
        _value = value;
    }

    /// <inheritdoc/>
    public bool AppliesOnActivation { get; } = true;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = true;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Execute;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => -target.Health.CurrentHealth;

    /// <inheritdoc/>
    public bool CanActivate(PlayerContext active, PlayerContext other) => other.Health.MaxHealth * _value >= other.Health.CurrentHealth;
}