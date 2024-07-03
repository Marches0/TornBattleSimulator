using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class StaticDamageCalculator : IDamageCalculator
{
    private readonly int _damage;

    public StaticDamageCalculator(int damage)
    {
        _damage = damage;
    }

    public int CalculateDamage(ThunderdomeContext context, PlayerContext active, PlayerContext other)
    {
        return _damage;
    }
}