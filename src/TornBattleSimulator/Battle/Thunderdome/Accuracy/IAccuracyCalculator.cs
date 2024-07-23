using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public interface IAccuracyCalculator
{
    double GetAccuracy(PlayerContext active, PlayerContext other, WeaponContext weapon);
}