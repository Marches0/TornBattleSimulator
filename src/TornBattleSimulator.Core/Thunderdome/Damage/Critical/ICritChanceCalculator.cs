using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Damage.Critical;

public interface ICritChanceCalculator
{
    double GetCritChance(PlayerContext active, PlayerContext other, WeaponContext weapon);
}