using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
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

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Conditional);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    public ModifierType Effect => ModifierType.Comeback;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext) => new(_value);

    public bool IsActive(PlayerContext owner, PlayerContext other) => owner.Health.CurrentHealth * 4 < owner.Health.MaxHealth;
}