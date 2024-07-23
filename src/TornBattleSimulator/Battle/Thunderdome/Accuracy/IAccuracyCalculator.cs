using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public interface IAccuracyCalculator
{
    double GetAccuracy(PlayerContext active, PlayerContext other, WeaponContext weapon);
}