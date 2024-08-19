using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class StaticDamageCalculator : IDamageCalculator
{
    private readonly int _damage;

    public StaticDamageCalculator(int damage)
    {
        _damage = damage;
    }

    public DamageResult CalculateDamage(AttackContext attack)
    {
        return new DamageResult(_damage, 0, 0);
    }
}