using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public class DamageCalculator : IDamageCalculator
{
    // https://www.torn.com/forums.php#/p=threads&f=61&t=16180034&b=0&a=0
    private readonly List<IDamageModifier> _damageModifiers;

    public DamageCalculator(
        IEnumerable<IDamageModifier> damageModifiers)
    {
        _damageModifiers = damageModifiers.ToList();
    }

    public DamageResult CalculateDamage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other)
    {
        var damage = _damageModifiers
            .Aggregate(new { Damage = 1d, BodyPart = (BodyPart)0 },
                (total, modifier) =>
                {
                    DamageModifierResult result = modifier.GetDamageModifier(active, other);
                    return new { Damage = total.Damage * result.Multiplier, BodyPart = total.BodyPart | result.BodyPart };
                });

        return new DamageResult((int)Math.Round(damage.Damage), damage.BodyPart);
    }
}