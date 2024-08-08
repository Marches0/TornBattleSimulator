using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class CompositeStrategy : IStrategy
{
    public List<IStrategy> Inner { get; }

    public CompositeStrategy(List<IStrategy> inner)
    {
        Inner = inner;
    }

    public TurnAction GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return Inner
            .Select(s => s.GetMove(context, self, other))
            .First(m => m != null)!;
    }
}