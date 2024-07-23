using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Strategy;

public interface IStrategy
{
    BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other);
}