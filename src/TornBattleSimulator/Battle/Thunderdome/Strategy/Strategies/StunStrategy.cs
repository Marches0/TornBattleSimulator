using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Actions;
using TornBattleSimulator.Shared.Thunderdome.Player;
using TornBattleSimulator.Shared.Thunderdome.Strategy;

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