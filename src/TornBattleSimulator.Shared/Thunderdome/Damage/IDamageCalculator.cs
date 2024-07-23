using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Core.Thunderdome.Damage;

public interface IDamageCalculator
{
    DamageResult CalculateDamage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon);
}