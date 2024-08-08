using TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Strategy;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy;

public class StrategyBuilder
{
    private readonly MissTurnStrategy _missTurn;
    private readonly IUntilConditionResolver _untilConditionResolver;

    public StrategyBuilder(
        MissTurnStrategy missTurn,
        IUntilConditionResolver untilConditionResolver)
    {
        _missTurn = missTurn;
        _untilConditionResolver = untilConditionResolver;
    }

    public IStrategy BuildStrategy(BattleBuild build)
    {
        // Turn Miss is automatically the highest priority strategy, so
        // we don't act when it's applied.
        List<IStrategy> strategies = [_missTurn, .. build.Strategy.Select(s => new UseWeaponStrategy(s, _untilConditionResolver))];
        return new CompositeStrategy(strategies);
    }
}