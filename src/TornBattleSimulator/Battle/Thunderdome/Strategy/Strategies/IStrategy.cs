using TornBattleSimulator.Battle.Thunderdome.Action;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public interface IStrategy
{
    BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other);
}