using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

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
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    /// <inheritdoc/>
    public bool RequiresDamageToApply { get; } = false;

    /// <inheritdoc/>
    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    /// <inheritdoc/>
    public ModifierApplication AppliesAt { get; } = ModifierApplication.FightStart;

    /// <inheritdoc/>
    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    /// <inheritdoc/>
    public ModificationType Type { get; } = ModificationType.Multiplicative;

    /// <inheritdoc/>
    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation)
    {
        return hitLocation.BodyPartStruck == _bodyPart
            ? _damage
            : 1;
    }
}