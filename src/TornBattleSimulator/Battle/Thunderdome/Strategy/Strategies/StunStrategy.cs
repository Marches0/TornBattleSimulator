using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Actions;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Strategy;

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