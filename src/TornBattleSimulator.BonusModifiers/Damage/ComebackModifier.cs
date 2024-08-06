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

public class ComebackModifier : IModifier, IDamageModifier, IConditionalModifier
{
    private readonly double _value;

    public ComebackModifier(double value)
    {
        _value = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterNextEnemyAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.BeforeAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Comeback;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext) => _value;

    /// <inheritdoc/>
    public bool CanActivate(PlayerContext active, PlayerContext other, AttackResult? attack) => active.Health.CurrentHealth * 4 < active.Health.MaxHealth;
}