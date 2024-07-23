using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public interface IAccuracyCalculator
{
    double GetAccuracy(PlayerContext active, PlayerContext other, WeaponContext weapon);
}