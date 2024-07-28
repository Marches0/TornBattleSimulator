using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

/// <summary>
///  A damage modifier which applies a bonus if a particular
///  body part is struck.
/// </summary>
public abstract class BodyPartDamageModifier : IDamageModifier
{
    private readonly BodyPart _bodyPart;
    private readonly double _damage;

    protected BodyPartDamageModifier(
        BodyPart bodyPart,
        double value)
    {
        _bodyPart = bodyPart;
        _damage = 1 + value;
    }

    /// <inheritdoc/>
    public ModifierLifespanDescription Lifespan => ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply => false;

    /// <inheritdoc/>
    public ModifierTarget Target => ModifierTarget.Self;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt => ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour => ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public StatModificationType Type => StatModificationType.Multiplicative;

    /// <inheritdoc/>
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