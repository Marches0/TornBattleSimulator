using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class AssassinateModifier : IModifier, IDamageModifier
{
    private readonly double _value;

    /// <inheritdoc/>
    public AssassinateModifier(double value)
    {
        _value = value + 1;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Turns(1);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => false;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Assassinate;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        return new DamageModifierResult(_value);
    }
}