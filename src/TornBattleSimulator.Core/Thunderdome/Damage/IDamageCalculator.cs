using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Damage;

public interface IDamageCalculator
{
    DamageResult CalculateDamage(AttackContext attack);
}