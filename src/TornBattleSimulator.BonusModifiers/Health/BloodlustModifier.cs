using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.BonusModifiers.Health;

public class BloodlustModifier : IHealthModifier
{
    private readonly double _value;

    public BloodlustModifier(double value)
    {
        _value = value;
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
    public ModifierType Effect { get; } = ModifierType.Bloodlust;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public bool AppliesOnActivation { get; } = false;

    /// <inheritdoc/>
    public int GetHealthModifier(PlayerContext target, DamageResult? damage) => (int)(damage!.Damage * _value);
}