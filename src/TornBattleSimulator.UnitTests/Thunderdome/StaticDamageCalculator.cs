using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class StaticDamageCalculator : IDamageCalculator
{
    private readonly double _damage;

    public StaticDamageCalculator(double damage)
    {
        _damage = damage;
    }

    public double CalculateDamage(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return _damage;
    }
}