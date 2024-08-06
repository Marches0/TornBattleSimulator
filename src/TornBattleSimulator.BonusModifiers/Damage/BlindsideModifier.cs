using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class BlindsideModifier : IDamageModifier, IConditionalModifier, IModifier
{
    private readonly double _value;

    public BlindsideModifier(double value)
    {
        _value = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } =   ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterNextEnemyAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.BeforeAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Blindside;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public StatModificationType Type { get; } = StatModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext) => _value;

    /// <inheritdoc/>
    public bool CanActivate(PlayerContext active, PlayerContext other, AttackResult? attack) => other.Health.CurrentHealth == other.Health.MaxHealth;
}