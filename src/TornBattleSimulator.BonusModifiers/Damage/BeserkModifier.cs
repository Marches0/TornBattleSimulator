using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class BeserkModifier : IModifier, IDamageModifier, IAccuracyModifier
{
    private readonly double _damageModifier;
    private readonly double _accuracyModifier;

    public BeserkModifier(double value)
    {
        _damageModifier = 1 + value;
        _accuracyModifier = 1 - (value / 2);
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
    public ModifierType Effect { get; } = ModifierType.Beserk;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public double GetAccuracyModifier(PlayerContext active, PlayerContext other, WeaponContext weapon)
    {
        return _accuracyModifier;
    }

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        return new DamageModifierResult(_damageModifier);
    }
}