using TornBattleSimulator.Battle.Thunderdome.Action;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class CompositeStrategy : IStrategy
{
    private readonly List<IStrategy> _inner;

    public CompositeStrategy(List<IStrategy> inner)
    {
        _inner = inner;
    }

    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        try
        {
            return _inner
                .Select(s => s.GetMove(context, self, other))
                .First(m => m != null);
        }
        catch (Exception ex)
        {

        }

        return null;
    }
}