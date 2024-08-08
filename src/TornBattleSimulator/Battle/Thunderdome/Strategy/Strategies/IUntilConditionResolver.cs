using TornBattleSimulator.Core.Thunderdome.Strategy;
using TornBattleSimulator.Core.Thunderdome;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public interface IUntilConditionResolver
{
    bool Fulfilled(AttackContext attack, StrategyDescription strategy);
}