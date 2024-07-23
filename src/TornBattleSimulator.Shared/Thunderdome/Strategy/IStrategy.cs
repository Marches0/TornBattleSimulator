using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public interface IStrategy
{
    BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other);
}