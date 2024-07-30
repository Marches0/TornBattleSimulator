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
    public bool AppliesOnActivation => true;

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => true;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Execute;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => -target.Health.CurrentHealth;

    /// <inheritdoc/>
    public bool CanActivate(PlayerContext active, PlayerContext other) => other.Health.MaxHealth * _value >= other.Health.CurrentHealth;
}