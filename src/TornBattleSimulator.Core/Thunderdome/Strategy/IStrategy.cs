using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Strategy;

public interface IStrategy
{
    TurnAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other);
}