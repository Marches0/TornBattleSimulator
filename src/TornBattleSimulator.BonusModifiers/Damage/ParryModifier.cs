using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class ParryModifier : IModifier, IDamageModifier
{
    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = true;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.Other;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.AfterAction;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Parry;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        // Treated as an debuff to the attacker rather than a buff to the defender - seems easier.
        return weapon.Type == WeaponType.Melee
            ? 0
            : 1;
    }
}