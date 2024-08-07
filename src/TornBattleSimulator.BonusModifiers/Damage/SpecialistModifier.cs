using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class SpecialistModifier : IModifier, IDamageModifier
{
    private readonly double _value;

    public SpecialistModifier(double value)
    {
        _value = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Specialist;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Additive;

    /// <inheritdoc/>
    public double GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext)
    {
        // Could do a custom behaviour interface for this,
        // but just do it here for ease of use.
        if (weapon.Ammo != null)
        {
            weapon.Ammo.MagazinesRemaining = 0;
        }
        
        return _value;
    }
}