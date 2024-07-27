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

    public AssassinateModifier(double value)
    {
        _value = value + 1;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Turns(1);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    public ModifierType Effect => ModifierType.Assassinate;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext)
    {
        return new DamageModifierResult(_value);
    }
}