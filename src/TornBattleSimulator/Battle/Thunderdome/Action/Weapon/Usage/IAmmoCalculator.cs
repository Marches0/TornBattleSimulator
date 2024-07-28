using TornBattleSimulator.Core.Thunderdome.Player.Weapons;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public interface IAmmoCalculator
{
    int GetAmmoRemaining(PlayerContext active, WeaponContext weapon);
}