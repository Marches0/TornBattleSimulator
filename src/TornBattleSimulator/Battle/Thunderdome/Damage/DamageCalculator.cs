using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public class DamageCalculator
{
    // https://www.torn.com/forums.php#/p=threads&f=61&t=16180034&b=0&a=0
    private readonly List<IDamageModifier> _damageModifiers;

    public DamageCalculator(
        IEnumerable<IDamageModifier> damageModifiers)
    {
        _damageModifiers = damageModifiers.ToList();
    }

    public double CalculateDamage(
        ThunderdomeContext context)
    {
        return _damageModifiers.Aggregate(1d, (damage, modifier) => damage *= modifier.GetDamageModifier(context.Attacker, context.Defender));
    }
}