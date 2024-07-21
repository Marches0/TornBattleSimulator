using TornBattleSimulator.Battle.Thunderdome.Action;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Actions;

namespace TornBattleSimulator.Battle.Thunderdome.Strategy.Strategies;

public class StunStrategy : IStrategy
{
    public BattleAction? GetMove(ThunderdomeContext context, PlayerContext self, PlayerContext other)
    {
        return self.Modifiers.Active.Any(m => m is ShockModifier)
            ? BattleAction.Stunned
            : null;
    }
}