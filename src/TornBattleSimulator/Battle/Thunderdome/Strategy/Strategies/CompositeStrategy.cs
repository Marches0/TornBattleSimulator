using TornBattleSimulator.Battle.Thunderdome.Action;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class CompositeStrategy : IStrategy
{
    public List<IStrategy> Inner { get; }

    public CompositeStrategy(List<IStrategy> inner)
    {
        Inner = inner;
    }

    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return Inner
            .Select(s => s.GetMove(context, self, other))
            .First(m => m != null);
    }
}