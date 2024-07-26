using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

public abstract class BodyPartDamageModifier : IDamageModifier
{
    private readonly BodyPart _bodyPart;
    private readonly double _damage;

    protected BodyPartDamageModifier(
        BodyPart bodyPart,
        double damage)
    {
        _bodyPart = bodyPart;
        _damage = 1 + damage;
    }

    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply => false;

    public ModifierTarget Target => ModifierTarget.Self;

    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    public DamageModifierResult GetDamageModifier(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        DamageContext damageContext
    )
    {
        double multiplier = damageContext.TargetBodyPart!.Value == _bodyPart
            ? _damage
            : 1;

        return new DamageModifierResult(multiplier);
    }
}