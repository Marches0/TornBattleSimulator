using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

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