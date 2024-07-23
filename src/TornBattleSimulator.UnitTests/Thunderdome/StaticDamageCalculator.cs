using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Damage;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class StaticDamageCalculator : IDamageCalculator
{
    private readonly int _damage;

    public StaticDamageCalculator(int damage)
    {
        _damage = damage;
    }

    public DamageResult CalculateDamage(ThunderdomeContext context, PlayerContext active, PlayerContext other, WeaponContext weapon)
    {
        return new DamageResult(_damage, 0, 0);
    }
}